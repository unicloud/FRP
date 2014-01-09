#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2014/01/05，09:01
// 文件名：PlanBCTest.cs
// 程序集：UniCloud.Application.FleetPlanBC.Tests
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.FleetPlanBC.ActionCategoryServices;
using UniCloud.Application.FleetPlanBC.AircraftPlanServices;
using UniCloud.Application.FleetPlanBC.AircraftTypeServices;
using UniCloud.Application.FleetPlanBC.AirlinesServices;
using UniCloud.Application.FleetPlanBC.AnnualServices;
using UniCloud.Application.FleetPlanBC.PlanAircraftServices;
using UniCloud.Application.FleetPlanBC.Query.ActionCategoryQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftPlanQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftTypeQueries;
using UniCloud.Application.FleetPlanBC.Query.AirlinesQueries;
using UniCloud.Application.FleetPlanBC.Query.AnnualQueries;
using UniCloud.Application.FleetPlanBC.Query.PlanAircraftQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.FleetPlanBC.Tests.Services
{
    [TestClass]
    public class PlanBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())
            #region 飞机计划相关配置，包括查询，应用服务，仓储注册

.RegisterType<IPlanQuery, PlanQuery>()
                .RegisterType<IPlanAppService, PlanAppService>()
                .RegisterType<IPlanRepository, PlanRepository>()
            #endregion

            #region 活动类型相关配置，包括查询，应用服务，仓储注册

.RegisterType<IActionCategoryQuery, ActionCategoryQuery>()
                .RegisterType<IActionCategoryAppService, ActionCategoryAppService>()
                .RegisterType<IActionCategoryRepository, ActionCategoryRepository>()
            #endregion
            #region 机型相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAircraftTypeQuery, AircraftTypeQuery>()
                .RegisterType<IAircraftTypeAppService, AircraftTypeAppService>()
                .RegisterType<IAircraftTypeRepository, AircraftTypeRepository>()
            #endregion
            #region 航空公司相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAirlinesQuery, AirlinesQuery>()
                .RegisterType<IAirlinesAppService, AirlinesAppService>()
                .RegisterType<IAirlinesRepository, AirlinesRepository>()
            #endregion
            #region 计划年度相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAnnualQuery, AnnualQuery>()
                .RegisterType<IAnnualAppService, AnnualAppService>()
                .RegisterType<IAnnualRepository, AnnualRepository>()
            #endregion

            #region 计划飞相关配置，包括查询，应用服务，仓储注册

.RegisterType<IPlanAircraftQuery, PlanAircraftQuery>()
                .RegisterType<IPlanAircraftAppService, PlanAircraftAppService>()
                .RegisterType<IPlanAircraftRepository, PlanAircraftRepository>()
            #endregion

;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetPlans()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IPlanAppService>();

            // Act
            var result = service.GetPlans().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void ModefyPlans()
        {
            var context = DefaultContainer.Resolve<IPlanRepository>();
            var plan = context.GetAll().ToList().FirstOrDefault();
            if (plan != null)
            {
                plan.SetTitle("2013年运力规划");
                var ph = plan.PlanHistories.FirstOrDefault();
                if (ph != null) ph.SetSeatingCapacity(231);
            }
            context.UnitOfWork.Commit();
        }
    }
}