#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/1 11:54:23
// 文件名：ProgrammingBCTest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.FleetPlanBC.ProgrammingServices;
using UniCloud.Application.FleetPlanBC.Query.ProgrammingQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;
#endregion

namespace UniCloud.Application.FleetPlanBC.Tests.Services
{
    [TestClass]
    public class ProgrammingBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<IProgrammingRepository, ProgrammingRepository>()
                .RegisterType<IProgrammingAppService, ProgrammingAppService>()
                .RegisterType<IProgrammingQuery, ProgrammingQuery>();

        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGetProgrammings()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IProgrammingAppService>();

            // Act
            var result = service.GetProgrammings().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}
