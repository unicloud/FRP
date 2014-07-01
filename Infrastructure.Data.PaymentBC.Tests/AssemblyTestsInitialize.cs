#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：22:20
// 方案：FRP
// 项目：Infrastructure.Data.PaymentBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PaymentBC.Aggregates.ContractEngineAgg;
using UniCloud.Infrastructure.Data.PaymentBC.Repositories;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.Tests
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
                .Register<IQueryableUnitOfWork, PaymentBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IContractEngineRepository, ContractEngineRepository>();
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