#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/19 18:11:10
// 文件名：ReceptionBCTests
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PurchaseBC.Query.ReceptionQueries;
using UniCloud.Application.PurchaseBC.ReceptionServices;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.PurchaseBC.Tests.Services
{
    [TestClass]
    public class ReceptionBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            Configuration.Create()
                .UseAutofac()
                .CreateLog()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IReceptionRepository, ReceptionRepository>()
                .Register<IAircraftLeaseReceptionAppService, AircraftLeaseReceptionAppService>()
                .Register<IAircraftPurchaseReceptionAppService, AircraftPurchaseReceptionAppService>()
                .Register<IEngineLeaseReceptionAppService, EngineLeaseReceptionAppService>()
                .Register<IEnginePurchaseReceptionAppService, EnginePurchaseReceptionAppService>()
                .Register<IAircraftLeaseReceptionQuery, AircraftLeaseReceptionQuery>()
                .Register<IAircraftPurchaseReceptionQuery, AircraftPurchaseReceptionQuery>()
                .Register<IEngineLeaseReceptionQuery, EngineLeaseReceptionQuery>()
                .Register<IEnginePurchaseReceptionQuery, EnginePurchaseReceptionQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGetAircraftLeaseReceptions()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IAircraftLeaseReceptionAppService>();

            // Act
            var result = service.GetAircraftLeaseReceptions().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void TestModifyAircraftLeaseReception()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IAircraftLeaseReceptionAppService>();

            // Act
            var result = service.GetAircraftLeaseReceptions().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}