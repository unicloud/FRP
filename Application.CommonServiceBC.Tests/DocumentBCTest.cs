#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.CommonServiceBC.DocumentServices;
using UniCloud.Application.CommonServiceBC.Query.DocumentQueries;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentPathAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.CommonServiceBC.Repositories;
using UniCloud.Infrastructure.Data.CommonServiceBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

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
            Configuration.Create()
                         .UseAutofac()
                         .CreateLog()
                         .Register<IQueryableUnitOfWork, CommonServiceBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                #region 文档相关配置，包括查询，应用服务，仓储注册

                         .Register<IDocumentAppService, DocumentAppService>()
                         .Register<IDocumentQuery, DocumentQuery>()
                         .Register<IDocumentPathRepository, DocumentPathRepository>()
                         .Register<IDocumentRepository, DocumentRepository>()
                #endregion

                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetDocumentPaths()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IDocumentAppService>();

            // Act
            var result = service.GetDocumentPaths().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}