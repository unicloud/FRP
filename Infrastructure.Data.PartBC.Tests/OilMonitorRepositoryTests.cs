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
using UniCloud.Domain.PartBC.Aggregates.OilUserAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
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
                .RegisterType<IOilUserRepository, OilUserRepository>()
                .RegisterType<IOilMonitorRepository, OilMonitorRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void CreateEngineOilTest()
        {
            // Arrange
            var oilRep = DefaultContainer.Resolve<IOilUserRepository>();
            var snRep = DefaultContainer.Resolve<ISnRegRepository>();
            var snReg = snRep.GetAll().FirstOrDefault();
            var engineOil = OilUserFactory.CreateEngineOil(snReg, 100, 30, 50, 15);

            // Act
            oilRep.Add(engineOil);
            oilRep.UnitOfWork.Commit();
        }

        [TestMethod]
        public void GetAllEngineOils()
        {
            // Arrange
            var oilRep = DefaultContainer.Resolve<IOilMonitorRepository>();

            // Act
            var result = oilRep.GetAll().OfType<EngineOil>().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void CreateOilMonitor()
        {
            // Arrange
            var monitorRep = DefaultContainer.Resolve<IOilMonitorRepository>();
            var userRep = DefaultContainer.Resolve<IOilUserRepository>();
            var oilUser = userRep.GetAll().FirstOrDefault();
            var rTsn = new Random();
            var rTsr = new Random();
            var rOil = new Random();
            var rDelta = new Random();
            for (var i = -90; i < 0; i++)
            {
                var oil = OilMonitorFactory.CreateOilMonitor(oilUser, DateTime.Now.AddDays(i), rTsn.Next(90, 110),
                    rTsr.Next(10, 30), rOil.Next(10, 30), rOil.Next(10, 30), rDelta.Next(-5, 5), rOil.Next(10, 30),
                    rOil.Next(10, 30));
                monitorRep.Add(oil);
            }

            // Act
            monitorRep.UnitOfWork.Commit();
        }
    }
}