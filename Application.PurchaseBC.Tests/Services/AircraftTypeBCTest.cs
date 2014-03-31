#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/4 9:46:42
// 文件名：AircraftTypeBCTest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PurchaseBC.AircraftTypeServices;
using UniCloud.Application.PurchaseBC.Query.AircraftTypeQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;
#endregion

namespace UniCloud.Application.PurchaseBC.Tests.Services
{
    [TestClass]
    public class AircraftTypeBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<IAircraftTypeRepository, AircraftTypeRepository>()
                .RegisterType<IAircraftTypeAppService, AircraftTypeAppService>()
                .RegisterType<IAircraftTypeQuery, AircraftTypeQuery>();

        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGetAircraftTypes()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IAircraftTypeAppService>();

            // Act
            var result = service.GetAircraftTypes().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}
