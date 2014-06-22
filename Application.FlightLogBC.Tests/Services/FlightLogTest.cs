#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/26 14:57:53
// 文件名：FlightLogTest
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/26 14:57:53
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.FlightLogBC.FlightLogServices;
using UniCloud.Application.FlightLogBC.Query.FlightLogQueries;
using UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FlightLogBC.Repositories;
using UniCloud.Infrastructure.Data.FlightLogBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using FlightLogBCUnitOfWork = UniCloud.Infrastructure.Data.FlightLogBC.UnitOfWork.Mapping.FlightLogBCUnitOfWork;

#endregion

namespace UniCloud.Application.FlightLogBC.Tests.Services
{
    [TestClass]
    public class FlightLogTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, FlightLogBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IFlightLogRepository, FlightLogRepository>()
                .Register<IFlightLogAppService, FlightLogAppService>()
                .Register<IFlightLogQuery, FlightLogQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }


        [TestMethod]
        public void TestGetFlightLogs()
        {
            // Arrange
            var service = UniContainer.Resolve<IFlightLogAppService>();

            // Act
            var result = service.GetFlightLogs().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }


        [TestMethod]
        public void TestQueryAcFlightData()
        {
            // Arrange
            var service = UniContainer.Resolve<IFlightLogAppService>();

            var date = new DateTime(2014, 2, 28);
            const string regNum = "B-2286";
            // Act
            var result = service.QueryAcFlightData(regNum, date).ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        #endregion
    }
}