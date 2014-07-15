#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/24，11:32
// 方案：FRP
// 项目：Infrastructure.Data.PartBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.ThrustAgg;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Tests
{
    [TestClass]
    public class OilMonitorRepositoryTests
    {
        [TestMethod]
        public void OilMonitorDataInitialize()
        {
            // Arrange
            var trRep = UniContainer.Resolve<IThrustRepository>();
            var pnRep = UniContainer.Resolve<IPnRegRepository>();
            var acRep = UniContainer.Resolve<IAircraftRepository>();
            var snRep = UniContainer.Resolve<ISnRegRepository>();

            var ac1 = acRep.GetFiltered(ac => ac.RegNumber == "B-6323").FirstOrDefault();
            var ac2 = acRep.GetFiltered(ac => ac.RegNumber == "B-6517").FirstOrDefault();

            var tr1 = ThrustFactory.CreateThrust("24K", "推力24K");
            var tr2 = ThrustFactory.CreateThrust("25K", "推力25K");
            var tr3 = ThrustFactory.CreateThrust("27K", "推力27K");
            trRep.Add(tr1);
            trRep.Add(tr2);
            trRep.Add(tr3);

            var pn1 = PnRegFactory.CreatePnReg(true, "V2527-A5", string.Empty);
            var pn2 = PnRegFactory.CreatePnReg(true, "Trent772C", string.Empty);
            var pn3 = PnRegFactory.CreatePnReg(true, "APU1", string.Empty);
            pnRep.Add(pn1);
            pnRep.Add(pn2);
            pnRep.Add(pn3);

            var eng1 = SnRegFactory.CreateEngineReg(new DateTime(2010, 1, 1), pn1, tr2, "V15749");
            var eng2 = SnRegFactory.CreateEngineReg(new DateTime(2010, 1, 1), pn1, tr2, "V15089");
            var eng3 = SnRegFactory.CreateEngineReg(new DateTime(2010, 1, 1), pn2, tr2, "41715");
            var eng4 = SnRegFactory.CreateEngineReg(new DateTime(2010, 1, 1), pn2, tr2, "41736");
            eng1.SetAircraft(ac1);
            eng2.SetAircraft(ac1);
            eng3.SetAircraft(ac2);
            eng4.SetAircraft(ac2);
            snRep.Add(eng1);
            snRep.Add(eng2);
            snRep.Add(eng3);
            snRep.Add(eng4);

            var apu1 = SnRegFactory.CreateAPUReg(new DateTime(2010, 1, 1), pn3, "APU123");
            snRep.Add(apu1);

            // Act
            trRep.UnitOfWork.Commit();

            // Assert
            Assert.IsTrue(true);
        }



    }
}