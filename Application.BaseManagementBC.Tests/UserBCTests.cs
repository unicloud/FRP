#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/19 15:46:48
// 文件名：UserBCTests
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/19 15:46:48
// 修改说明：
// ========================================================================*/
#endregion

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.BaseManagementBC.Query.UserQueries;
using UniCloud.Application.BaseManagementBC.UserServices;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.BaseManagementBC.Repositories;
using UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;

namespace UniCloud.Application.BaseManagementBC.Tests
{
    [TestClass]
    public class UserBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            log4net.Config.XmlConfigurator.Configure();
            DefaultContainer.CreateContainer()
                         .RegisterType<IQueryableUnitOfWork, BaseManagementBCUnitOfWork>(new WcfPerRequestLifetimeManager())
            #region 相关配置，包括查询，应用服务，仓储注册

.RegisterType<IUserAppService, UserAppService>()
                         .RegisterType<IUserQuery, UserQuery>()
                         .RegisterType<IUserRepository, UserRepository>()
            #endregion

;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGetUsers()
        {
            var service = DefaultContainer.Resolve<IUserAppService>();
            var result = service.GetUsers();
        }
    }
}
