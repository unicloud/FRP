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
            //// Arrange
            //var oilRep = DefaultContainer.Resolve<IOilMonitorRepository>();
            //var snReg = SnRegFactory.CreateSnReg();
            //var engineOil1 = OilMonitorFactory.CreateEngineOil(snReg, new DateTime(2014, 1, 1), 100, 20, 21, 24, 5, 21, 21);
            //var engineOil2 = OilMonitorFactory.CreateEngineOil(snReg, new DateTime(2014, 1, 2), 102, 22, 21, 24, 5, 20, 21);
            //var engineOil3 = OilMonitorFactory.CreateEngineOil(snReg, new DateTime(2014, 1, 3), 103, 23, 20, 23, 5, 20, 20);
            //var engineOil4 = OilMonitorFactory.CreateEngineOil(snReg, new DateTime(2014, 1, 4), 106, 26, 25, 26, 5, 22, 21);
            //var engineOil5 = OilMonitorFactory.CreateEngineOil(snReg, new DateTime(2014, 1, 5), 108, 28, 23, 24, 5, 23, 22);
            //var engineOil6 = OilMonitorFactory.CreateEngineOil(snReg, new DateTime(2014, 1, 6), 109, 29, 21, 22, 5, 22, 23);
            //var engineOil7 = OilMonitorFactory.CreateEngineOil(snReg, new DateTime(2014, 1, 7), 112, 32, 22, 23, 5, 21, 22);

            //// Act
            //oilRep.Add(engineOil1);
            //oilRep.Add(engineOil2);
            //oilRep.Add(engineOil3);
            //oilRep.Add(engineOil4);
            //oilRep.Add(engineOil5);
            //oilRep.Add(engineOil6);
            //oilRep.Add(engineOil7);
            //oilRep.UnitOfWork.Commit();

            //// Assert
            //Assert.IsNotNull(engineOil1);
        }

        [TestMethod]
        public void GetAllEngineOils()
        {
            // Arrange
            var oilRep = DefaultContainer.Resolve<IOilMonitorRepository>();

            // Act
            var result = oilRep.GetAll().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}