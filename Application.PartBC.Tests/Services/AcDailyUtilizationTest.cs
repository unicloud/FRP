#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/26 22:00:24
// 文件名：AcDailyUtilizationTest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PartBC.AcDailyUtilizationServices;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.AcDailyUtilizationQueries;
using UniCloud.Domain.PartBC.Aggregates.AcDailyUtilizationAgg;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PartBC.Repositories;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.PartBC.Tests.Services
{
    [TestClass]
    public class AcDailyUtilizationTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PartBCUnitOfWork>(new WcfPerRequestLifetimeManager())



                #region 附件相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IAcDailyUtilizationQuery, AcDailyUtilizationQuery>()
                .RegisterType<IAcDailyUtilizationAppService, AcDailyUtilizationAppService>()
                .RegisterType<IAcDailyUtilizationRepository, AcDailyUtilizationRepository>()
                #endregion

                .RegisterType<IAircraftRepository, AircraftRepository>()
                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void GetBasicConfigGroups()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IAcDailyUtilizationAppService>();

            // Act
            List<AcDailyUtilizationDTO> result = service.GetAcDailyUtilizations().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void AddAcDailyUtilizations()
        {
            var context = DefaultContainer.Resolve<IAcDailyUtilizationRepository>();
            var aircraftContext = DefaultContainer.Resolve<IAircraftRepository>();
            //获取
            Aircraft aircraft = aircraftContext.GetAll().ToList().First();

            const string pn = "EN33423423";
            AcDailyUtilization newAcDailyUtilization = AcDailyUtilizationFactory.CreateAcDailyUtilization(aircraft,
                (decimal) 9.02, (decimal) 9.3, true, 9, "B-2313", 2013);
            context.Add(newAcDailyUtilization);
            context.UnitOfWork.Commit();
        }

        #endregion
    }
}