﻿#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/4 9:59:44
// 文件名：PlanAircraftBCTest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PurchaseBC.PlanAircraftServices;
using UniCloud.Application.PurchaseBC.Query.PlanAircraftQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.PlanAircraftAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Application.PurchaseBC.Tests.Services
{
    [TestClass]
    public class PlanAircraftBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IPlanAircraftRepository, PlanAircraftRepository>()
                .Register<IPlanAircraftAppService, PlanAircraftAppService>()
                .Register<IPlanAircraftQuery, PlanAircraftQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGetPlanAircrafts()
        {
            // Arrange
            var service = UniContainer.Resolve<IPlanAircraftAppService>();

            // Act
            var result = service.GetPlanAircrafts().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}