#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/21 10:50:58
// 文件名：DocumentSearchBCTests
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/21 10:50:58
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

//using OfficeFiles.Reader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.CommonServiceBC.DocumentServices;
using UniCloud.Application.CommonServiceBC.DocumnetSearch;
using UniCloud.Application.CommonServiceBC.Query.DocumentQueries;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentPathAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentTypeAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.CommonServiceBC.Repositories;
using UniCloud.Infrastructure.Data.CommonServiceBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Application.CommonServiceBC.Tests
{
    [TestClass]
    public class DocumentSearchBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, CommonServiceBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                #region 文档相关配置，包括查询，应用服务，仓储注册

                .Register<IDocumentAppService, DocumentAppService>()
                .Register<IDocumentQuery, DocumentQuery>()
                .Register<IDocumentPathRepository, DocumentPathRepository>()
                .Register<IDocumentRepository, DocumentRepository>()
                .Register<IDocumentTypeRepository, DocumentTypeRepository>()
                .Register<IDocumentSearchAppService, DocumentSearchAppService>()
                #endregion

                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void TestSearch()
        {
            var service = UniContainer.Resolve<IDocumentSearchAppService>();
            var result = service.Search("之", "1");
        }

        [TestMethod]
        public void AddIndex()
        {
            var service = UniContainer.Resolve<IQueryableUnitOfWork>();
            var documents = service.CreateSet<Document>();
            //foreach (var document in documents)
            //{
            //    if (document.FileStorage != null)
            //    {
            //        string path = @"d:\" + document.FileName;
            //        File.WriteAllBytes(path, document.FileStorage);
            //        string content = string.Empty;
            //        if (document.Extension.Contains("docx"))
            //        {
            //            DocxFile file = new DocxFile(path);
            //            content = file.ParagraphText;
            //        }
            //        else if (document.Extension.Contains("pdf"))
            //        {
            //            content = PdfFile.ReadPdfFile(path);
            //        }
            //        var service1 = new DocumentIndexService();

            //        Document document1 = DocumentFactory.CreateStandardDocument(document.Id, document.FileName, document.Extension, document.Abstract, document.Note, document.Uploader,
            //            true, null, content, document.DocumentTypeId);
            //        service1.AddDocumentSearchIndex(document1);
            //    }
            //}
        }

        #endregion
    }
}