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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.FleetPlanBC.AnnualServices;
using UniCloud.Application.FleetPlanBC.ApprovalDocServices;
using UniCloud.Application.FleetPlanBC.Query.AnnualQueries;
using UniCloud.Application.FleetPlanBC.Query.ApprovalDocQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.Application.FleetPlanBC.Tests.Services
{
        [TestClass]
    public class ApprovalBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                #region 批文相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IApprovalDocQuery, ApprovalDocQuery>()
                .RegisterType<IApprovalDocAppService, ApprovalDocAppService>()
                .RegisterType<IApprovalDocRepository, ApprovalDocRepository>()
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
            var service = DefaultContainer.Resolve<IApprovalDocAppService>();

            // Act
            var result = service.GetApprovalDocs().ToList();

            // Assert
           Assert.IsTrue(result.Any());
        }
    }
}
