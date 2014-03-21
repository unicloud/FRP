#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/13 20:32:30
// 文件名：FunctionItemBCTests
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/13 20:32:30
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.BaseManagementBC.FunctionItemServices;
using UniCloud.Application.BaseManagementBC.Query.FunctionItemQueries;
using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.BaseManagementBC.Repositories;
using UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.BaseManagementBC.Tests
{
    [TestClass]
    public class FunctionItemBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                         .RegisterType<IQueryableUnitOfWork, BaseManagementBCUnitOfWork>(new WcfPerRequestLifetimeManager())
            #region 相关配置，包括查询，应用服务，仓储注册

.RegisterType<IFunctionItemAppService, FunctionItemAppService>()
                         .RegisterType<IFunctionItemQuery, FunctionItemQuery>()
                         .RegisterType<IFunctionItemRepository, FunctionItemRepository>()
            #endregion

;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGetFunctionItems()
        {
            var work = DefaultContainer.Resolve<BaseManagementBCUnitOfWork>();
            var aaa = work.FunctionItems.Include(p => p.SubFunctionItems).Where(p => p.ParentItemId == null).ToList();
            var service = DefaultContainer.Resolve<IFunctionItemAppService>();
            var result = service.GetFunctionItemsWithHierarchy();
        }
    }
}
