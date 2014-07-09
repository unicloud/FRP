#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：14:50
// 方案：FRP
// 项目：Infrastructure.Data.AircraftConfigBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AirlinesAgg;
using UniCloud.Infrastructure.Data.AircraftConfigBC.Repositories;
using UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.Tests
{
    [TestClass]
    public class AssemblyTestsInitialize
    {
        /// <summary>
        ///     初始化测试用的Unity容器
        /// </summary>
        /// <param name="context">MS TEST 上下文</param>
        [AssemblyInitialize]
        public static void InitializeContainer(TestContext context)
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, AircraftConfigBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IAirlinesRepository, AirlinesRepository>()
                .Register<IAircraftSeriesRepository, AircraftSeriesRepository>()
                .Register<IAircraftTypeRepository, AircraftTypeRepository>()
                .Register<IAircraftRepository, AircraftRepository>();
        }

        /// <summary>
        ///     清理测试用的Unity容器
        /// </summary>
        [AssemblyCleanup]
        public static void CleanupContainer()
        {
            UniContainer.Cleanup();
        }
    }
}