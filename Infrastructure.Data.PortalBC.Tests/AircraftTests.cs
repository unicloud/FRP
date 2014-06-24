#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:10
// 方案：FRP
// 项目：Infrastructure.Data.PortalBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PortalBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PortalBC.Tests
{
    [TestClass]
    public class AircraftTests
    {
        [TestMethod]
        public void GetAircraftSeriesTest()
        {
            // Arrange
            var asRep = UniContainer.Resolve<IAircraftSeriesRepository>();

            // Act
            var result = asRep.GetAll().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}