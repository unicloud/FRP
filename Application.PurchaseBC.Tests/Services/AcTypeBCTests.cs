#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，14:11
// 文件名：AcTypeBCTests.cs
// 程序集：UniCloud.Application.PurchaseBC.Tests
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PurchaseBC.AcTypeServices;
using UniCloud.Application.PurchaseBC.Query.AcTypeQueries;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.PurchaseBC.Tests.Services
{
    [TestClass]
    public class AcTypeBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            Configuration.Create()
                .UseAutofac()
                .CreateLog()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())

                #region 机型相关配置，包括查询，应用服务，仓储注册

                .Register<IAcTypeQuery, AcTypeQuery>()
                .Register<IAcTypeAppService, AcTypeAppService>()
                #endregion

                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        /// <summary>
        ///     获取机型信息
        /// </summary>
        [TestMethod]
        public void GetAcTypes()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IAcTypeAppService>();
            // Act
            var result = service.GetAcTypes().ToList();
            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}