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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.FleetPlanBC.AirlinesServices;
using UniCloud.Application.FleetPlanBC.EngineServices;
using UniCloud.Application.FleetPlanBC.EngineTypeServices;
using UniCloud.Application.FleetPlanBC.PlanEngineServices;
using UniCloud.Application.FleetPlanBC.Query.AirlinesQueries;
using UniCloud.Application.FleetPlanBC.Query.EngineQueries;
using UniCloud.Application.FleetPlanBC.Query.EngineTypeQueries;
using UniCloud.Application.FleetPlanBC.Query.PlanEngineQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg;
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
    public class PlanEngineBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IPlanEngineRepository, PlanEngineRepository>()
                .Register<IPlanEngineAppService, PlanEngineAppService>()
                .Register<IPlanEngineQuery, PlanEngineQuery>()

                #region 机型相关配置，包括查询，应用服务，仓储注册

                .Register<IEngineTypeQuery, EngineTypeQuery>()
                .Register<IEngineTypeAppService, EngineTypeAppService>()
                .Register<IEngineTypeRepository, EngineTypeRepository>()
                #endregion
                #region 航空公司相关配置，包括查询，应用服务，仓储注册

                .Register<IAirlinesQuery, AirlinesQuery>()
                .Register<IAirlinesAppService, AirlinesAppService>()
                .Register<IAirlinesRepository, AirlinesRepository>()
                #endregion

                .Register<IEngineRepository, EngineRepository>()
                .Register<IEngineAppService, EngineAppService>()
                .Register<IEngineQuery, EngineQuery>();
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
            var service = UniContainer.Resolve<IPlanEngineAppService>();

            // Act
            var result = service.GetPlanEngines().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void AddPlanEngines()
        {
            var context = UniContainer.Resolve<IPlanEngineRepository>();
            var airlinesContext = UniContainer.Resolve<IAirlinesRepository>();
            var engineTypeContext = UniContainer.Resolve<IEngineTypeRepository>();
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