#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：15:23
// 方案：FRP
// 项目：Infrastructure.Data.PartBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Tests
{
    [TestClass]
    public class PnRegRepositoryTests
    {
        [TestMethod]
        public void CreatePnRegTest()
        {
            // Arrange
            var pnRep = UniContainer.Resolve<IPnRegRepository>();
            new List<PnReg>
            {
                PnRegFactory.CreatePnReg(true, "12345", "A"),
                PnRegFactory.CreatePnReg(true, "12346", "B"),
                PnRegFactory.CreatePnReg(true, "12347", "C"),
                PnRegFactory.CreatePnReg(true, "13348", "D"),
                PnRegFactory.CreatePnReg(true, "13349", "E"),
                PnRegFactory.CreatePnReg(true, "13341", "F"),
            }.ForEach(pnRep.Add);

            // Act
            pnRep.UnitOfWork.Commit();

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void CreateMorePnRegTest()
        {
            // Arrange
            var pnRep = UniContainer.Resolve<IPnRegRepository>();
            for (var i = 0; i < 3000; i++)
            {
                var pnReg = PnRegFactory.CreatePnReg(true, i.ToString(CultureInfo.InvariantCulture), "A" + i);
                pnRep.Add(pnReg);
            }

            // Act
            pnRep.UnitOfWork.Commit();

            // Assert
            Assert.IsTrue(true);
        }
    }
}