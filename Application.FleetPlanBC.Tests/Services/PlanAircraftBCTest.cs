#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/6 19:27:45
// 文件名：PlanAircraftBCTest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.FleetPlanBC.ActionCategoryServices;
using UniCloud.Application.FleetPlanBC.AircraftPlanServices;
using UniCloud.Application.FleetPlanBC.AircraftServices;
using UniCloud.Application.FleetPlanBC.AircraftTypeServices;
using UniCloud.Application.FleetPlanBC.AirlinesServices;
using UniCloud.Application.FleetPlanBC.PlanAircraftServices;
using UniCloud.Application.FleetPlanBC.Query.ActionCategoryQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftPlanQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftTypeQueries;
using UniCloud.Application.FleetPlanBC.Query.AirlinesQueries;
using UniCloud.Application.FleetPlanBC.Query.PlanAircraftQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.FleetPlanBC.Tests.Services
{
    [TestClass]
    public class PlanAircraftBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IPlanAircraftRepository, PlanAircraftRepository>()
                .Register<IPlanAircraftAppService, PlanAircraftAppService>()
                .Register<IPlanAircraftQuery, PlanAircraftQuery>()

                #region 飞机计划相关配置，包括查询，应用服务，仓储注册

                .Register<IPlanQuery, PlanQuery>()
                .Register<IPlanAppService, PlanAppService>()
                .Register<IPlanRepository, PlanRepository>()
                #endregion

                #region 活动类型相关配置，包括查询，应用服务，仓储注册

                .Register<IActionCategoryQuery, ActionCategoryQuery>()
                .Register<IActionCategoryAppService, ActionCategoryAppService>()
                .Register<IActionCategoryRepository, ActionCategoryRepository>()
                #endregion
                #region 机型相关配置，包括查询，应用服务，仓储注册

                .Register<IAircraftTypeQuery, AircraftTypeQuery>()
                .Register<IAircraftTypeAppService, AircraftTypeAppService>()
                .Register<IAircraftTypeRepository, AircraftTypeRepository>()
                #endregion
                #region 航空公司相关配置，包括查询，应用服务，仓储注册

                .Register<IAirlinesQuery, AirlinesQuery>()
                .Register<IAirlinesAppService, AirlinesAppService>()
                .Register<IAirlinesRepository, AirlinesRepository>()
                #endregion

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
        public void TestGetPlanAircrafts()
        {
            // Arrange
            var service = UniContainer.Resolve<IPlanAircraftAppService>();

            // Act
            var result = service.GetPlanAircrafts().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}