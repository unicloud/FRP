#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：10:32
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PurchaseBC.Aggregates.ManufacturerAgg;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Tests
{
    [TestClass]
    public class ManufacturerRepositoryTests
    {
        [TestMethod]
        public void CreateEngineManufacturerTest()
        {
            // Arrange
            var mfRep = UniContainer.Resolve<IManufacturerRepository>();
            var manufacturer1 =
                ManufacturerFactory.CreateManufacturer(Guid.Parse("CB646641-3267-40FD-BE0E-793CBA6BE272"), "CFM", 2);
            var manufacturer2 =
                ManufacturerFactory.CreateManufacturer(Guid.Parse("990AD80D-E277-4360-A8B8-35582EA74C44"), "普惠", 2);

            // Act
            mfRep.Add(manufacturer1);
            mfRep.Add(manufacturer2);
            mfRep.UnitOfWork.Commit();

            // Assert
            Assert.IsTrue(true);
        }
    }
}