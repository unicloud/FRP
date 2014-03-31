#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/22 9:17:34
// 文件名：EnginePlanBCTest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.FleetPlanBC.ActionCategoryServices;
using UniCloud.Application.FleetPlanBC.AirlinesServices;
using UniCloud.Application.FleetPlanBC.AnnualServices;
using UniCloud.Application.FleetPlanBC.EnginePlanServices;
using UniCloud.Application.FleetPlanBC.EngineTypeServices;
using UniCloud.Application.FleetPlanBC.PlanEngineServices;
using UniCloud.Application.FleetPlanBC.Query.ActionCategoryQueries;
using UniCloud.Application.FleetPlanBC.Query.AirlinesQueries;
using UniCloud.Application.FleetPlanBC.Query.AnnualQueries;
using UniCloud.Application.FleetPlanBC.Query.EnginePlanQueries;
using UniCloud.Application.FleetPlanBC.Query.EngineTypeQueries;
using UniCloud.Application.FleetPlanBC.Query.PlanEngineQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanEngineAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.FleetPlanBC.Tests.Services
{
    [TestClass]
    public class EnginePlanBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())
            #region 飞机计划相关配置，包括查询，应用服务，仓储注册

.RegisterType<IEnginePlanQuery, EnginePlanQuery>()
                .RegisterType<IEnginePlanAppService, EnginePlanAppService>()
                .RegisterType<IEnginePlanRepository, EnginePlanRepository>()
            #endregion

            #region 活动类型相关配置，包括查询，应用服务，仓储注册

.RegisterType<IActionCategoryQuery, ActionCategoryQuery>()
                .RegisterType<IActionCategoryAppService, ActionCategoryAppService>()
                .RegisterType<IActionCategoryRepository, ActionCategoryRepository>()
            #endregion
            #region 机型相关配置，包括查询，应用服务，仓储注册

.RegisterType<IEngineTypeQuery, EngineTypeQuery>()
                .RegisterType<IEngineTypeAppService, EngineTypeAppService>()
                .RegisterType<IEngineTypeRepository, EngineTypeRepository>()
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

.RegisterType<IPlanEngineQuery, PlanEngineQuery>()
                .RegisterType<IPlanEngineAppService, PlanEngineAppService>()
                .RegisterType<IPlanEngineRepository, PlanEngineRepository>()
            #endregion

;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetEnginePlans()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IEnginePlanAppService>();

            // Act
            var result = service.GetEnginePlans().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void AddEnginePlans()
        {
            var context = DefaultContainer.Resolve<IEnginePlanRepository>();
            var airlinesContext = DefaultContainer.Resolve<IAirlinesRepository>();
            var annualContext = DefaultContainer.Resolve<IAnnualRepository>();
            var engineTypeContext = DefaultContainer.Resolve<IEngineTypeRepository>();
            var actionCategoryContext = DefaultContainer.Resolve<IActionCategoryRepository>();
            //获取
            var actionCategory = actionCategoryContext.GetAll().ToList().First();
            var engineType = engineTypeContext.GetAll().ToList().First();

            var airlines = airlinesContext.GetAll().ToList().First(p => p.IsCurrent);
            var annual = annualContext.GetAll().ToList().First(p => p.IsOpen);

            var newEnginePlan = EnginePlanFactory.CreateEnginePlan(1);
            if (newEnginePlan != null)
            {
                newEnginePlan.SetAirlines(airlines);
                newEnginePlan.SetAnnual(annual);
                newEnginePlan.SetTitle(null);
                newEnginePlan.SetNote(null);

                // 添加接机行
                var newEnginePlanHistory = newEnginePlan.AddNewEnginePlanHistory();
                newEnginePlanHistory.SetActionCategory(actionCategory);
                newEnginePlanHistory.SetEngineType(engineType);
                newEnginePlanHistory.SetMaxThrust(1123);
                newEnginePlanHistory.SetNote("beizhu");
                newEnginePlanHistory.SetPerformDate(annual, 11);
                newEnginePlanHistory.SetPlanEngine(null);
                newEnginePlanHistory.SetPlanEngine(Guid.Parse("CB9920C3-13F4-C70A-A7B6-08D0E52D20F9"));
            }
            context.Add(newEnginePlan);
            context.UnitOfWork.Commit();
        }
    }
}
