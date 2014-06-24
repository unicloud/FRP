#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/17 11:29:57
// 文件名：AircraftBCTests
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/17 11:29:57
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.AircraftConfigBC.AircraftServices;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Application.AircraftConfigBC.Query.AircraftQueries;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.AircraftConfigBC.Repositories;
using UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace Application.AircraftConfigBC.Tests
{
    [TestClass]
    public class AircraftBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, AircraftConfigBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IAircraftQuery, AircraftQuery>()
                .Register<IAircraftAppService, AircraftAppService>()
                .Register<IAircraftRepository, AircraftRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGetAircrafts()
        {
            // Arrange
            var service = UniContainer.Resolve<IAircraftAppService>();

            // Act
            var result = service.GetAircrafts().ToList();
            var first = result.FirstOrDefault();
            var aircraftLicense = new AircraftLicenseDTO();
            aircraftLicense.AircraftLicenseId = 1;
            aircraftLicense.Name = "123";
            aircraftLicense.LicenseTypeId = 1;
            aircraftLicense.IssuedDate = DateTime.Now;
            aircraftLicense.ExpireDate = DateTime.Now;
            aircraftLicense.IssuedUnit = "111";
            aircraftLicense.ValidMonths = 11;
            aircraftLicense.State = 0;
            first.AircraftLicenses.Add(aircraftLicense);

            service.ModifyAircraft(first);
            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}