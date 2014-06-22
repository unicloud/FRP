#region 版本控制
// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：14:06
// 方案：FRP
// 项目：Application.BaseManagementBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================
#endregion

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.BaseManagementBC.FunctionItemServices;
using UniCloud.Application.BaseManagementBC.Query.FunctionItemQueries;
using UniCloud.Application.BaseManagementBC.Query.UserQueries;
using UniCloud.Application.BaseManagementBC.UserServices;
using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserRoleAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.BaseManagementBC.Repositories;
using UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.Application.BaseManagementBC.Tests
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
                .Register<IQueryableUnitOfWork, BaseManagementBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")

                .Register<IFunctionItemAppService, FunctionItemAppService>()
                .Register<IFunctionItemQuery, FunctionItemQuery>()
                .Register<IFunctionItemRepository, FunctionItemRepository>()
                .Register<IUserAppService, UserAppService>()
                .Register<IUserQuery, UserQuery>()
                .Register<IUserRepository, UserRepository>()
                .Register<IUserRoleRepository,UserRoleRepository>();
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