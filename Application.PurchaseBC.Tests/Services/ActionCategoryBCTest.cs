#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/4 10:01:15
// 文件名：ActionCategoryBCTest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PurchaseBC.ActionCategoryServices;
using UniCloud.Application.PurchaseBC.Query.ActionCategoryQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.ActionCategoryAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;
#endregion

namespace UniCloud.Application.PurchaseBC.Tests.Services
{
    [TestClass]
    public class ActionCategoryBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<IActionCategoryRepository, ActionCategoryRepository>()
                .RegisterType<IActionCategoryAppService, ActionCategoryAppService>()
                .RegisterType<IActionCategoryQuery, ActionCategoryQuery>();

        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGetActionCategorys()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IActionCategoryAppService>();

            // Act
            var result = service.GetActionCategories().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}
