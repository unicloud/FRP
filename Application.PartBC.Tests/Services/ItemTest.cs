#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/3 16:45:14
// 文件名：ItemTest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PartBC.AcConfigServices;
using UniCloud.Application.PartBC.ItemServices;
using UniCloud.Application.PartBC.Query.AcConfigQueries;
using UniCloud.Application.PartBC.Query.ItemQueries;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PartBC.Repositories;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.PartBC.Tests.Services
{
    [TestClass]
    public class ItemTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, PartBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")

                #region 附件相关配置，包括查询，应用服务，仓储注册

                .Register<IItemQuery, ItemQuery>()
                .Register<IItemAppService, ItemAppService>()
                .Register<IItemRepository, ItemRepository>()
                .Register<IMaintainCtrlRepository, MaintainCtrlRepository>()
                .Register<IPnRegRepository, PnRegRepository>()
                #endregion

                .Register<IAcConfigQuery, AcConfigQuery>()
                .Register<IAcConfigAppService, AcConfigAppService>()
                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetItems()
        {
            // Arrange
            var service = UniContainer.Resolve<IItemAppService>();

            var aircraftTypeId = Guid.Parse("5C690CB2-2D33-4006-858B-0BE610E9CB47");

            // Act
            var result = service.GetItemsByAircraftType(aircraftTypeId);

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetAcConfigs()
        {
            // Arrange
            var service = UniContainer.Resolve<IAcConfigAppService>();

            var date = new DateTime(2014, 04, 14);
            // Act
            var result = service.QueryAcConfigs(5, date);

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}