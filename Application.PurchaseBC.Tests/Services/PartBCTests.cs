﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，14:11
// 文件名：PartBCTests.cs
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
using UniCloud.Application.PurchaseBC.PnRegServices;
using UniCloud.Application.PurchaseBC.Query.PnRegQueries;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Application.PurchaseBC.Tests.Services
{
    [TestClass]
    public class PartBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")

                #region 部件相关配置，包括查询，应用服务，仓储注册

                .Register<IPnRegQuery, PnRegQuery>()
                .Register<IPnRegAppService, PnRegAppService>()
                #endregion

                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        /// <summary>
        ///     获取部件相关信息
        /// </summary>
        [TestMethod]
        public void TestGetParts()
        {
            // Arrange
            var service = UniContainer.Resolve<IPnRegAppService>();
            // Act
            var result = service.GetPnRegs().ToList();
            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}