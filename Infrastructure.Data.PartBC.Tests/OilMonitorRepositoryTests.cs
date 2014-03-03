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
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PartBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<ISnRegRepository, SnRegRepository>()
                .RegisterType<IPnRegRepository,PnRegRepository>()
                .RegisterType<IThrustRepository,ThrustRepository>()
                .RegisterType<IOilMonitorRepository, OilMonitorRepository>();
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
            var pnRep = DefaultContainer.Resolve<IPnRegRepository>();
            var thrustRep = DefaultContainer.Resolve<IThrustRepository>();
            var snRep = DefaultContainer.Resolve<ISnRegRepository>();
            var pn = pnRep.GetAll().FirstOrDefault();
            var thrust = thrustRep.GetAll().FirstOrDefault();

            // Act
            var engine = SnRegFactory.CreateEngineReg(new DateTime(2014, 1, 1), pn, thrust, "2334", 100, 20, 30, 20);
            snRep.Add(engine);
            snRep.UnitOfWork.Commit();
        }

        [TestMethod]
        public void CreateOilMonitor()
        {
            // Arrange
            var monitorRep = DefaultContainer.Resolve<IOilMonitorRepository>();
            var snRep = DefaultContainer.Resolve<ISnRegRepository>();
            var snReg = snRep.GetAll().OfType<EngineReg>().FirstOrDefault();
            var rTsn = new Random();
            var rTsr = new Random();
            var rOil = new Random();
            var rDelta = new Random();
            for (var i = -90; i < 0; i++)
            {
                var oil = OilMonitorFactory.CreateEngineOil(snReg, DateTime.Now.AddDays(i), rTsn.Next(90, 110),
                    rTsr.Next(10, 30), rOil.Next(10, 30), rOil.Next(10, 30), rDelta.Next(-5, 5), rOil.Next(10, 30),
                    rOil.Next(10, 30));
                monitorRep.Add(oil);
            }

            // Act
            monitorRep.UnitOfWork.Commit();
        }
    }
}