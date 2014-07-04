#region 命名空间

using System;
using System.Configuration;
using System.IO;
using System.Linq;
using OfficeFiles.Reader;
using UniCloud.Application.CommonServiceBC.DocumentServices;
using UniCloud.Infrastructure.Unity;

#endregion

namespace Unicloud.DocumentIndexService
{
    public class DocumentIndex
    {
        private readonly IDocumentAppService _documentAppService;

        public DocumentIndex()
        {
            _documentAppService = UniContainer.Resolve<IDocumentAppService>();
        }
         
        public void UpdateIndex()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var indexUpdateTime = DateTime.MinValue;
            if (!string.IsNullOrEmpty(config.AppSettings.Settings["IndexUpdateTime"].Value))
            {
                indexUpdateTime = DateTime.Parse(config.AppSettings.Settings["IndexUpdateTime"].Value);
            }
            var documents =
                _documentAppService.GetDocumentsWithContent().Where(p => p.UpdateTime >= indexUpdateTime).ToList();
            var documentIndexService = new UniCloud.Application.LuceneSearch.DocumentIndexService();
            documents.ForEach(p =>
            {
                if (p.UpdateTime.CompareTo(indexUpdateTime) > 0)
                {
                    indexUpdateTime = p.UpdateTime;
                }
                if (p.FileStorage != null)
                {
                    var path = AppDomain.CurrentDomain.BaseDirectory + "\\" + p.Name;
                    File.WriteAllBytes(path, p.FileStorage);
                    var content = string.Empty;
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