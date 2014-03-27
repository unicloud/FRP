#region 命名空间

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PurchaseBC.ContractDocumentServices;
using UniCloud.Application.PurchaseBC.Query.ContractDocumentQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;
#endregion

namespace UniCloud.Application.PurchaseBC.Tests.Services
{
    [TestClass]
    public class ContractDocumentBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                         .RegisterType<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                #region 订单文档相关配置，包括查询，应用服务，仓储注册

                         .RegisterType<IContractDocumentAppService, ContractDocumentAppService>()
                         .RegisterType<IContractDocumentQuery, ContractDocumentQuery>()
                          .RegisterType<IMaintainContractRepository, MaintainContractRepository>()
                #endregion

                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void GetContractDocument()
        {
            try
            {
                // Arrange
                var service = DefaultContainer.Resolve<IContractDocumentAppService>();
                var result = service.GetContractDocuments();
                Assert.IsTrue(result.Any());
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion
    }
}