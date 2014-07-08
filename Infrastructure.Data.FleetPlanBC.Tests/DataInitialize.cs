#region 版本控制
// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：21:21
// 方案：FRP
// 项目：Infrastructure.Data.FleetPlanBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================
#endregion

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Infrastructure.Unity;

namespace UniCloud.Infrastructure.Data.FleetPlanBC.Tests
{
    [TestClass]
    public class DataInitialize
    {
        [TestMethod]
        public void Plan()
        {
            // Arrange
            var alRep = UniContainer.Resolve<IAirlinesRepository>();
            var anRep = UniContainer.Resolve<IAnnualRepository>();
            var plRep = UniContainer.Resolve<IPlanRepository>();
            var airlines = alRep.GetFiltered(a => a.CnShortName == "川航").FirstOrDefault();
            var annual = anRep.GetFiltered(a => a.Year == 2013).FirstOrDefault();
            if(annual==null)throw new Exception("年度不能为空！");
            annual.SetIsOpen(true);
            var plan = PlanFactory.CreatePlan(1);
            plan.SetAirlines(airlines);
            plan.SetAnnual(annual);
            plan.SetTitle("2013年度机队规划");
            plan.SetDocNumber("[2013]007号");

            // Act
            plRep.Add(plan);
            plRep.UnitOfWork.Commit();

            // Assert
            Assert.IsTrue(true);
        }
    }
}