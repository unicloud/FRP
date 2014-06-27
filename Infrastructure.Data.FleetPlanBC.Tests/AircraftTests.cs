#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：17:06
// 方案：FRP
// 项目：Infrastructure.Data.FleetPlanBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.Tests
{
    [TestClass]
    public class AircraftTests
    {
        [TestMethod]
        public void CreatePlanAircraftTest()
        {
            // Arrange
            var alRep = UniContainer.Resolve<IAirlinesRepository>();
            var atRep = UniContainer.Resolve<IAircraftTypeRepository>();
            var paRep = UniContainer.Resolve<IPlanAircraftRepository>();
            var airlines = alRep.GetFiltered(a => a.CnShortName == "川航").FirstOrDefault();
            var aircraftType = atRep.GetFiltered(a => a.Name == "A320").FirstOrDefault();
            var planAircraft = PlanAircraftFactory.CreatePlanAircraft();
            planAircraft.SetAircraftType(aircraftType);
            planAircraft.SetAirlines(airlines);

            // Act
            paRep.Add(planAircraft);
            paRep.UnitOfWork.Commit();

            // Assert
            Assert.IsTrue(true);
        }
    }
}