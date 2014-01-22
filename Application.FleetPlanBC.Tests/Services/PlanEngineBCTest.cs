#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/22 9:17:52
// 文件名：PlanEngineBCTest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.FleetPlanBC.AirlinesServices;
using UniCloud.Application.FleetPlanBC.PlanEngineServices;
using UniCloud.Application.FleetPlanBC.EngineServices;
using UniCloud.Application.FleetPlanBC.EngineTypeServices;
using UniCloud.Application.FleetPlanBC.Query.AirlinesQueries;
using UniCloud.Application.FleetPlanBC.Query.PlanEngineQueries;
using UniCloud.Application.FleetPlanBC.Query.EngineQueries;
using UniCloud.Application.FleetPlanBC.Query.EngineTypeQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanEngineAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.FleetPlanBC.Tests.Services
{
    [TestClass]
    public class PlanEngineBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<IPlanEngineRepository, PlanEngineRepository>()
                .RegisterType<IPlanEngineAppService, PlanEngineAppService>()
                .RegisterType<IPlanEngineQuery, PlanEngineQuery>()

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

.RegisterType<IEngineRepository, EngineRepository>()
                .RegisterType<IEngineAppService, EngineAppService>()
                .RegisterType<IEngineQuery, EngineQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGetPlanEngines()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IPlanEngineAppService>();

            // Act
            var result = service.GetPlanEngines().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void AddPlanEngines()
        {
            var context = DefaultContainer.Resolve<IPlanEngineRepository>();
            var airlinesContext = DefaultContainer.Resolve<IAirlinesRepository>();
            var engineTypeContext = DefaultContainer.Resolve<IEngineTypeRepository>();
            //获取
            var engineType = engineTypeContext.GetAll().ToList().First();
            var airlines = airlinesContext.GetAll().ToList().First(p => p.IsCurrent);

            var newPlanEngine = PlanEngineFactory.CreatePlanEngine();
            if (newPlanEngine != null)
            {
                newPlanEngine.SetEngineType(engineType);
                newPlanEngine.SetAirlines(airlines);
            }
            context.Add(newPlanEngine);
            context.UnitOfWork.Commit();
        }
    }
}
