#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/24，11:32
// 方案：FRP
// 项目：Infrastructure.Data.PartBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.OilMonitorAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.ThrustAgg;
using UniCloud.Infrastructure.Data.PartBC.Repositories;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Tests
{
    [TestClass]
    public class OilMonitorRepositoryTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, PartBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<ISnRegRepository, SnRegRepository>()
                .Register<IPnRegRepository, PnRegRepository>()
                .Register<IThrustRepository, ThrustRepository>()
                .Register<IOilMonitorRepository, OilMonitorRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void CreateEngineRegTest()
        {
            // Arrange
            var pnRep = UniContainer.Resolve<IPnRegRepository>();
            var thrustRep = UniContainer.Resolve<IThrustRepository>();
            var snRep = UniContainer.Resolve<ISnRegRepository>();
            var pn = pnRep.GetAll().FirstOrDefault();
            var thrust = thrustRep.GetAll().FirstOrDefault();

            // Act
            var engine1 = SnRegFactory.CreateEngineReg(new DateTime(2014, 1, 1), pn, thrust, "2334");
            engine1.SetMonitorStatus(OilMonitorStatus.关注);
            var engine2 = SnRegFactory.CreateEngineReg(new DateTime(2014, 1, 1), pn, thrust, "2335");
            engine2.SetMonitorStatus(OilMonitorStatus.警告);
            var engine3 = SnRegFactory.CreateEngineReg(new DateTime(2014, 1, 1), pn, thrust, "2336");
            engine3.SetMonitorStatus(OilMonitorStatus.正常);
            snRep.Add(engine1);
            snRep.Add(engine2);
            snRep.Add(engine3);
            snRep.UnitOfWork.Commit();
        }

        [TestMethod]
        public void CreateOilMonitor()
        {
            // Arrange
            var monitorRep = UniContainer.Resolve<IOilMonitorRepository>();
            var snRep = UniContainer.Resolve<ISnRegRepository>();
            var rTsn = new Random();
            var rTsr = new Random();
            var rOil = new Random();
            var rDelta = new Random();

            var snReg1 = snRep.GetAll().OfType<EngineReg>().FirstOrDefault(r => r.MonitorStatus == OilMonitorStatus.正常);
            for (var i = -90; i < 0; i++)
            {
                var oil = OilMonitorFactory.CreateEngineOil(snReg1, DateTime.Now.AddDays(i), rTsn.Next(90, 110),
                    rTsr.Next(10, 30), rOil.Next(10, 30), rOil.Next(10, 30), rDelta.Next(-5, 5));
                monitorRep.Add(oil);
            }

            var snReg2 = snRep.GetAll().OfType<EngineReg>().FirstOrDefault(r => r.MonitorStatus == OilMonitorStatus.关注);
            for (var i = -90; i < 0; i++)
            {
                var oil = OilMonitorFactory.CreateEngineOil(snReg2, DateTime.Now.AddDays(i), rTsn.Next(90, 110),
                    rTsr.Next(10, 30), rOil.Next(10, 30), rOil.Next(10, 30), rDelta.Next(-5, 5));
                monitorRep.Add(oil);
            }

            var snReg3 = snRep.GetAll().OfType<EngineReg>().FirstOrDefault(r => r.MonitorStatus == OilMonitorStatus.警告);
            for (var i = -90; i < 0; i++)
            {
                var oil = OilMonitorFactory.CreateEngineOil(snReg3, DateTime.Now.AddDays(i), rTsn.Next(90, 110),
                    rTsr.Next(10, 30), rOil.Next(10, 30), rOil.Next(10, 30), rDelta.Next(-5, 5));
                monitorRep.Add(oil);
            }

            // Act
            monitorRep.UnitOfWork.Commit();
        }
    }
}