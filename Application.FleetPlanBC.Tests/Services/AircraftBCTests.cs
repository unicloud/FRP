#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/31 14:32:36
// 文件名：AircraftBCTests
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/31 14:32:36
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.FleetPlanBC.AircraftServices;
using UniCloud.Application.FleetPlanBC.Query.AircraftQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.FleetPlanBC.Tests.Services
{
    [TestClass]
    public class AircraftBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            Configuration.Create()
                .UseAutofac()
                .CreateLog()
                .Register<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IAircraftRepository, AircraftRepository>()
                .Register<IAircraftAppService, AircraftAppService>()
                .Register<IAircraftQuery, AircraftQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetAircrafts()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IAircraftAppService>();

            // Act
            var result = service.GetAircrafts().ToList();

            // Assert
            //Assert.IsTrue(result.Any());
        }
    }
}
