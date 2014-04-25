﻿#region 版本信息
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PartBC.AcConfigServices;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.ItemServices;
using UniCloud.Application.PartBC.PnRegServices;
using UniCloud.Application.PartBC.Query.AcConfigQueries;
using UniCloud.Application.PartBC.Query.AcDailyUtilizationQueries;
using UniCloud.Application.PartBC.Query.ItemQueries;
using UniCloud.Application.PartBC.Query.PnRegQueries;
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
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PartBCUnitOfWork>(new WcfPerRequestLifetimeManager())

            #region 附件相关配置，包括查询，应用服务，仓储注册

.RegisterType<IItemQuery, ItemQuery>()
                .RegisterType<IItemAppService, ItemAppService>()
                .RegisterType<IItemRepository, ItemRepository>()
                .RegisterType<IMaintainCtrlRepository, MaintainCtrlRepository>()
                .RegisterType<IPnRegRepository, PnRegRepository >()
            #endregion

.RegisterType<IAcConfigQuery, AcConfigQuery>()
                .RegisterType<IAcConfigAppService, AcConfigAppService>()
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
            var service = DefaultContainer.Resolve<IItemAppService>();

            Guid aircraftTypeId = Guid.Parse("5C690CB2-2D33-4006-858B-0BE610E9CB47");

            // Act
            List<ItemDTO> result = service.GetItemsByAircraftType(aircraftTypeId);

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetAcConfigs()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IAcConfigAppService>();
            
            DateTime date = new DateTime(2014, 04, 14);
            // Act
            List<AcConfigDTO> result = service.QueryAcConfigs(5,date);

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}