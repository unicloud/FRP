#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/31 16:08:34
// 文件名：AnnualBCTest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.FleetPlanBC.AnnualServices;
using UniCloud.Application.FleetPlanBC.Query.AnnualQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.FleetPlanBC.Tests.Services
{
    [TestClass]
    public class AnnualBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IAnnualRepository, AnnualRepository>()
                .Register<IAnnualAppService, AnnualAppService>()
                .Register<IAnnualQuery, AnnualQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGetAnnuals()
        {
            // Arrange
            var service = UniContainer.Resolve<IAnnualAppService>();

            // Act
            var result = service.GetAnnuals().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void ModefyAnnuals()
        {
            var service = UniContainer.Resolve<IAnnualRepository>();
            var annual = service.GetAll().FirstOrDefault();
            if (annual != null)
            {
                annual.SetIsOpen(true);
            }
            service.UnitOfWork.Commit();
        }

        [TestMethod]
        public void TestGetPlanYears()
        {
            // Arrange
            var service = UniContainer.Resolve<IAnnualAppService>();

            // Act
            var result = service.GetPlanYears().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}