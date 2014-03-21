#region 命名空间

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.CommonServiceBC.DocumentServices;
using UniCloud.Application.CommonServiceBC.Query.DocumentQueries;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentPathAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentTypeAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.CommonServiceBC.Repositories;
using UniCloud.Infrastructure.Data.CommonServiceBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;
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
            DefaultContainer.CreateContainer()
                         .RegisterType<IQueryableUnitOfWork, CommonServiceBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                #region 文档相关配置，包括查询，应用服务，仓储注册

                         .RegisterType<IDocumentAppService, DocumentAppService>()
                         .RegisterType<IDocumentQuery, DocumentQuery>()
                         .RegisterType<IDocumentPathRepository, DocumentPathRepository>()
                         .RegisterType<IDocumentRepository, DocumentRepository>()
                          .RegisterType<IDocumentTypeRepository, DocumentTypeRepository>()
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
            var service = DefaultContainer.Resolve<IDocumentAppService>();
            var result = service.GetSingleDocument(new Guid("C2830DD6-AF4C-4726-9922-22A6F798BCDE"));
        }
     
    }
}