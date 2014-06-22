#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2014/01/07，21:01
// 文件名：ApprovalBCTest.cs
// 程序集：UniCloud.Application.FleetPlanBC.Tests
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.FleetPlanBC.ApprovalDocServices;
using UniCloud.Application.FleetPlanBC.Query.ApprovalDocQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.FleetPlanBC.Tests.Services
{
    [TestClass]
    public class ApprovalBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                #region 批文相关配置，包括查询，应用服务，仓储注册

                .Register<IApprovalDocQuery, ApprovalDocQuery>()
                .Register<IApprovalDocAppService, ApprovalDocAppService>()
                .Register<IApprovalDocRepository, ApprovalDocRepository>()
                #endregion

                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetApprovalDocs()
        {
            // Arrange
            var service = UniContainer.Resolve<IApprovalDocAppService>();

            // Act
            var result = service.GetApprovalDocs().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}