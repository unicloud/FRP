#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：18:36
// 方案：FRP
// 项目：Infrastructure.Data.AircraftConfigBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AirlinesAgg;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.Tests
{
    [TestClass]
    public class AircraftRepositoryTests
    {
        [TestMethod]
        public void CreateAircraftTest()
        {
            // Arrange
            var alRep = UniContainer.Resolve<IAirlinesRepository>();
            var atRep = UniContainer.Resolve<IAircraftTypeRepository>();
            var acRep = UniContainer.Resolve<IAircraftRepository>();

            var airlines = alRep.GetFiltered(al => al.CnShortName == "川航").FirstOrDefault();
            if (airlines == null) throw new Exception("航空公司为空。");
            var aircraftType = atRep.GetFiltered(at => at.Name == "A320").FirstOrDefault();
            if (aircraftType == null) throw new Exception("机型为空。");

            new List<Aircraft>
            {
                AircraftFactory.CreateAircraft("B-6323", "6323", airlines.Id, aircraftType.Id,
                    new Guid("8c58622e-01e3-4f61-b34d-619d3fb432af")),
                AircraftFactory.CreateAircraft("B-6517", "6517", airlines.Id, aircraftType.Id,
                    new Guid("8c58622e-01e3-4f61-b34d-619d3fb432af"))
            }.ForEach(acRep.Add);

            // Act
            acRep.UnitOfWork.Commit();

            // Assert
            Assert.IsTrue(true);
        }
    }
}