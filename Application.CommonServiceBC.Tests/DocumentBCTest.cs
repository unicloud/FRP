#region 命名空间

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.CommonServiceBC.DocumentServices;
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
    public class DocumentBCTest
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
                #endregion

                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGet()
        {
            var service = UniContainer.Resolve<IDocumentAppService>();
            var result = service.GetSingleDocument(new Guid("C2830DD6-AF4C-4726-9922-22A6F798BCDE"));
        }
    }
}