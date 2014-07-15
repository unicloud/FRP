#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：8:55
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
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRemInstRecordAgg;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Tests
{
    [TestClass]
    public class SnHistoryTests
    {
        [TestMethod]
        public void CreateSnHistoryTest()
        {
            // Arrange
            var srRep = UniContainer.Resolve<ISnRemInstRecordRepository>();
            var shRep = UniContainer.Resolve<ISnHistoryRepository>();
            var acRep = UniContainer.Resolve<IAircraftRepository>();
            var pnRep = UniContainer.Resolve<IPnRegRepository>();
            var snRep = UniContainer.Resolve<ISnRegRepository>();

            var ac1 = acRep.GetFiltered(ac => ac.RegNumber == "B-6323").FirstOrDefault();
            var ac2 = acRep.GetFiltered(ac => ac.RegNumber == "B-6517").FirstOrDefault();

            var pn1 = pnRep.GetFiltered(pn => pn.Pn == "V2527-A5").FirstOrDefault();
            var pn2 = pnRep.GetFiltered(pn => pn.Pn == "Trent772C").FirstOrDefault();
            var pn3 = pnRep.GetFiltered(pn => pn.Pn == "APU1").FirstOrDefault();

            var sn1 = snRep.GetFiltered(sn => sn.Sn == "V15749").FirstOrDefault();
            var sn2 = snRep.GetFiltered(sn => sn.Sn == "V15089").FirstOrDefault();
            var sn3 = snRep.GetFiltered(sn => sn.Sn == "41715").FirstOrDefault();
            var sn4 = snRep.GetFiltered(sn => sn.Sn == "41736").FirstOrDefault();
            var sn5 = snRep.GetFiltered(sn => sn.Sn == "APU123").FirstOrDefault();

            var snRemInst1 = SnRemInstRecordFactory.CreateSnRemInstRecord("0001", new DateTime(2010, 1, 1),
                ActionType.装上, "装机", ac1);
            var snRemInst2 = SnRemInstRecordFactory.CreateSnRemInstRecord("0001", new DateTime(2010, 1, 1),
                ActionType.装上, "装机", ac2);
            srRep.Add(snRemInst1);
            srRep.Add(snRemInst2);

            var sh1 = SnHistoryFactory.CreateSnHistory(sn1, pn1, 0, 0, 0, 0, ActionType.装上, ac1,
                new DateTime(2010, 1, 1), snRemInst1, SnStatus.装机, Position.发动机1);
            var sh2 = SnHistoryFactory.CreateSnHistory(sn2, pn1, 0, 0, 0, 0, ActionType.装上, ac1,
                new DateTime(2010, 1, 1), snRemInst1, SnStatus.装机, Position.发动机2);
            var sh3 = SnHistoryFactory.CreateSnHistory(sn3, pn2, 0, 0, 0, 0, ActionType.装上, ac2,
                new DateTime(2010, 1, 1), snRemInst2, SnStatus.装机, Position.发动机1);
            var sh4 = SnHistoryFactory.CreateSnHistory(sn4, pn2, 0, 0, 0, 0, ActionType.装上, ac2,
                new DateTime(2010, 1, 1), snRemInst2, SnStatus.装机, Position.发动机2);
            shRep.Add(sh1);
            shRep.Add(sh2);
            shRep.Add(sh3);
            shRep.Add(sh4);

            var sh5 = SnHistoryFactory.CreateSnHistory(sn5, pn3, 0, 0, 0, 0, ActionType.装上, ac1,
                new DateTime(2010, 1, 1), snRemInst1, SnStatus.装机, Position.机身);
            shRep.Add(sh5);

            // Act
            shRep.UnitOfWork.Commit();

            // Assert
            Assert.IsTrue(true);
        }
    }
}