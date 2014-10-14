#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/16 15:49:53
// 文件名：AircraftLicenseBCTests
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/16 15:49:53
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.AircraftConfigBC.AircraftLicenseServices;
using UniCloud.Application.AircraftConfigBC.Query.AircraftLicenseQueries;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftLicenseAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.LicenseTypeAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.AircraftConfigBC.Repositories;
using UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;
namespace Application.AircraftConfigBC.Tests
{
    [TestClass]
   public class AircraftLicenseBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, AircraftConfigBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<IAircraftLicenseQuery, AircraftLicenseQuery>()
                .RegisterType<IAircraftLicenseAppService, AircraftLicenseAppService>()
                .RegisterType<IAircraftLicenseRepository, AircraftLicenseRepository>()
                .RegisterType<ILicenseTypeRepository, LicenseTypeRepository>();

        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGetLicenseTypes()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IAircraftLicenseAppService>();

            // Act
            var result = service.GetLicenseTypes().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}
