#region 命名空间

using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using OfficeFiles.Reader;
using UniCloud.Application.CommonServiceBC.DocumentServices;
using UniCloud.Application.CommonServiceBC.Query.DocumentQueries;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.CommonServiceBC.Repositories;
using UniCloud.Infrastructure.Data.CommonServiceBC.UnitOfWork;

#endregion

namespace Unicloud.DocumentIndexService
{
    public class DocumentIndex
    {
        private readonly DocumentAppService _documentAppService;

        public DocumentIndex()
        {
            IQueryableUnitOfWork unitOfWork = new CommonServiceBCUnitOfWork();
            _documentAppService = new DocumentAppService(new DocumentQuery(unitOfWork), new DocumentRepository(unitOfWork),
                new DocumentTypeRepository(unitOfWork));
        }

        public void UpdateIndex()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            DateTime indexUpdateTime = DateTime.MinValue;
            if (!string.IsNullOrEmpty(config.AppSettings.Settings["IndexUpdateTime"].Value))
            {
                indexUpdateTime = DateTime.Parse(config.AppSettings.Settings["IndexUpdateTime"].Value);
            }
            var documents = _documentAppService.GetDocumentsWithContent().Where(p => p.UpdateTime >= indexUpdateTime).ToList();
            var documentIndexService = new UniCloud.Application.LuceneSearch.DocumentIndexService();
            documents.ForEach(p =>
                              {
                                  if (p.UpdateTime.CompareTo(indexUpdateTime) > 0)
                                  {
                                      indexUpdateTime = p.UpdateTime;
                                  }
                                  if (p.FileStorage != null)
                                  {
                                      string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + p.Name;
                                      File.WriteAllBytes(path, p.FileStorage);
                                      string content = string.Empty;
                                      if (p.Extension.Contains("docx"))
                                      {
                                          var file = new DocxFile(path);
                                          content = file.ParagraphText;
                                      }
                                      else if (p.Extension.Contains("pdf"))
                                      {
                                          content = PdfFile.ReadPdfFile(path);
                                      }
                                      if (p.CreateTime.CompareTo(p.UpdateTime) == 0)
                                      {
                                          documentIndexService.AddDocumentSearchIndex(p.DocumentId, p.DocumentTypeId, p.Name, content);
                                      }
                                      else
                                      {
                                          documentIndexService.UpdateDocumentSearchIndex(p.DocumentId, p.DocumentTypeId, p.Name, content);
                                      }
                                      File.Delete(path);
                                  }
                              });
            config.AppSettings.Settings["IndexUpdateTime"].Value = indexUpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
