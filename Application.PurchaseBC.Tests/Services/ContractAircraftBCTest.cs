#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/4 10:03:30
// 文件名：ContractAircraftBCTest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PurchaseBC.ContractAircraftServices;
using UniCloud.Application.PurchaseBC.Query.ContractAircraftQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Application.PurchaseBC.Tests.Services
{
    [TestClass]
    public class ContractAircraftBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IContractAircraftRepository, ContractAircraftRepository>()
                .Register<IContractAircraftAppService, ContractAircraftAppService>()
                .Register<ILeaseContractAircraftAppService, LeaseContractAircraftAppService>()
                .Register<IPurchaseContractAircraftAppService, PurchaseContractAircraftAppService>()
                .Register<IContractAircraftQuery, ContractAircraftQuery>()
                .Register<ILeaseContractAircraftQuery, LeaseContractAircraftQuery>()
                .Register<IPurchaseContractAircraftQuery, PurchaseContractAircraftQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGeContractAircrafts()
        {
            // Arrange
            var service = UniContainer.Resolve<IContractAircraftAppService>();

            // Act
            var result = service.GetContractAircrafts().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void TestGetLeaseContractAircrafts()
        {
            // Arrange
            var service = UniContainer.Resolve<ILeaseContractAircraftAppService>();

            // Act
            var result = service.GetLeaseContractAircrafts().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }


        [TestMethod]
        public void TestGetPurchaseContractAircrafts()
        {
            // Arrange
            var service = UniContainer.Resolve<IPurchaseContractAircraftAppService>();

            // Act
            var result = service.GetPurchaseContractAircrafts().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}