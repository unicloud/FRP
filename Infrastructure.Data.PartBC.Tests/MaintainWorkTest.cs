﻿#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/6/16 15:14:01
// 文件名：MaintainWorkTest
// 版本：V1.0.0
//
// 修改者：  时间：2014/6/16 15:14:01
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
using UniCloud.Infrastructure.Data.PartBC.Repositories;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Tests
{
    [TestClass]
    public class MaintainWorkTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PartBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<IMaintainWorkRepository, MaintainWorkRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void CreateCtrlUnits()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IMaintainWorkRepository>();

            // Act
            var maintainWorks = new List<MaintainWork>
            {
                MaintainWorkFactory.CreateMaintainWork("大修", "大修（对应到TSO、CSO）"),
                MaintainWorkFactory.CreateMaintainWork("装机", "装机（对应TSI、CSI）"),
                MaintainWorkFactory.CreateMaintainWork("A检", "A检"),
                MaintainWorkFactory.CreateMaintainWork("C检", "C检"),
                MaintainWorkFactory.CreateMaintainWork("D检", "D检（对应到TSD、CSD）"),
                MaintainWorkFactory.CreateMaintainWork("检修或测试", "检修或测试（对应TSR、CSR）"),
            };
            maintainWorks.ForEach(service.Add);
            service.UnitOfWork.Commit();
        }
    }
}