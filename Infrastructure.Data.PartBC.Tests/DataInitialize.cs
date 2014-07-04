#region 版本控制
// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：12:02
// 方案：FRP
// 项目：Infrastructure.Data.PartBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================
#endregion

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.ThrustAgg;
using UniCloud.Infrastructure.Unity;

namespace UniCloud.Infrastructure.Data.PartBC.Tests
{
    [TestClass]
    public class DataInitialize
    {
        [TestMethod]
        public void OilMonitorDataInitialize()
        {
            // Arrange
            var trRep = UniContainer.Resolve<IThrustRepository>();
            var pnRep = UniContainer.Resolve<IPnRegRepository>();

            new List<Thrust>
            {
                ThrustFactory.CreateThrust("24K", "推力24K"),
                ThrustFactory.CreateThrust("25K", "推力25K"),
                ThrustFactory.CreateThrust("27K", "推力27K"),
            }.ForEach(trRep.Add);

            var pn1 = PnRegFactory.CreatePnReg(true, "V2527-A5", string.Empty);
            var pn2 = PnRegFactory.CreatePnReg(true, "Trent772C", string.Empty);
            pnRep.Add(pn1);
            pnRep.Add(pn2);

            // Act
            trRep.UnitOfWork.Commit();

            // Assert
            Assert.IsTrue(true);
        }
    }
}