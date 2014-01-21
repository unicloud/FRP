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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.AircraftConfigBC.AircraftLicenseServices;
using UniCloud.Application.AircraftConfigBC.AircraftServices;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Application.AircraftConfigBC.Query.AircraftLicenseQueries;
using UniCloud.Application.AircraftConfigBC.Query.AircraftQueries;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.LicenseTypeAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.AircraftConfigBC.Repositories;
using UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

namespace Application.AircraftConfigBC.Tests
{
    [TestClass]
    public class AircraftBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, AircraftConfigBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<IAircraftQuery, AircraftQuery>()
                .RegisterType<IAircraftAppService, AircraftAppService>()
                .RegisterType<IAircraftRepository, AircraftRepository>();

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
            var service = DefaultContainer.Resolve<IAircraftAppService>();

            // Act
            var result = service.GetAircrafts().ToList();
            var first = result.FirstOrDefault();
            AircraftLicenseDTO aircraftLicense = new AircraftLicenseDTO();
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
