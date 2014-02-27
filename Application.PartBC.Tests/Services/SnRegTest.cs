#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/26 21:43:59
// 文件名：SnRegTest
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
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.SnRegQueries;
using UniCloud.Application.PartBC.SnRegServices;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PartBC.Repositories;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.PartBC.Tests.Services
{
    [TestClass]
    public class SnRegTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PartBCUnitOfWork>(new WcfPerRequestLifetimeManager())



            #region 序号件相关配置，包括查询，应用服务，仓储注册

.RegisterType<ISnRegQuery, SnRegQuery>()
                .RegisterType<ISnRegAppService, SnRegAppService>()
                .RegisterType<ISnRegRepository, SnRegRepository>()
            #endregion

.RegisterType<IAircraftRepository, AircraftRepository>()
                .RegisterType<IPnRegRepository, PnRegRepository>()
                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void GetSnRegs()
        {
            // Arrange
            var service = DefaultContainer.Resolve<SnRegAppService>();

            // Act
            List<SnRegDTO> result = service.GetSnRegs().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void AddSnRegs()
        {
            var context = DefaultContainer.Resolve<ISnRegRepository>();
            var aircraftContext = DefaultContainer.Resolve<IAircraftRepository>();
            var pnRegContext = DefaultContainer.Resolve<IPnRegRepository>();
            var maintainWorkContext = DefaultContainer.Resolve<IMaintainWorkRepository>();
            //获取
            Aircraft aircraft = aircraftContext.GetAll().ToList().First();
            PnReg pnReg = pnRegContext.GetAll().ToList().First();
            MaintainWork maintainWork = maintainWorkContext.GetAll().ToList().First();

            const string sn = "339832";
            SnReg newSnReg = SnRegFactory.CreateSnReg(aircraft, DateTime.Now, false, pnReg, sn);

            var lifeMonitor = newSnReg.AddNewLifeMonitor();
            lifeMonitor.SetLifeTimeLimit("1234");
            lifeMonitor.SetMaintainWork(maintainWork);
            lifeMonitor.SetMointorStart(DateTime.Now);
            lifeMonitor.SetSn(sn);

            var snHistory = newSnReg.AddNewSnHistory();
            snHistory.SetAircraft(aircraft);
            snHistory.SetCSN("1234");
            snHistory.SetCSR("123");
            snHistory.SetFiNumber("sadsag");
            snHistory.SetInstallDate(DateTime.Now);
            snHistory.SetRemoveDate(DateTime.Now);
            snHistory.SetTSN("34234");
            snHistory.SetTSR("23443");

            context.Add(newSnReg);
            context.UnitOfWork.Commit();
        }

        #endregion
    }
}