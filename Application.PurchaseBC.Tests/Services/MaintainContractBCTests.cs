#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/19 14:06:59
// 文件名：MaintainContractBCTests
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.MaintainContractServices;
using UniCloud.Application.PurchaseBC.Query.MaintainContractQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.PurchaseBC.Tests.Services
{
    [TestClass]
    public class MaintainContractBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IMaintainContractRepository, MaintainContractRepository>()
                .Register<IMaintainContractAppService, MaintainContractAppService>()
                .Register<IMaintainContractQuery, MaintainContractQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetApuMaintainContracts()
        {
            // Arrange
            var service = UniContainer.Resolve<IMaintainContractAppService>();

            // Act
            //var result = service.GetApuMaintainContracts().ToList();
            var add = new APUMaintainContractDTO();

            // Assert
            //Assert.IsTrue(result.Any());
        }
    }
}