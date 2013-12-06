#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/19，13:29
// 方案：FRP
// 项目：Domain.PurchaseBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierRoleAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Domain.PurchaseBC.Enums;

#endregion

namespace UniCloud.Domain.PurchaseBC.Tests
{
    [TestClass]
    public class TradeAggTests
    {
        [TestMethod]
        public void CreateNewTrade()
        {
            // Arrange
            var supplier = SupplierFactory.CreateSupplier(SupplierType.国外, "V0001", "波音", null);

            var trade = TradeFactory.CreateTrade(null, null, DateTime.Now);
            trade.SetTradeNumber(1);

            var supplierCompany = supplier.SupplierCompany;

            var acLeaseSupplier = SupplierRoleFactory.CreateAircraftLeaseSupplier(supplierCompany);
            var acPurchaseSupplier = SupplierRoleFactory.CreateAircraftPurchaseSupplier(supplierCompany);
            var bfePurchaseSupplier = SupplierRoleFactory.CreateBFEPurchaseSupplier(supplierCompany);
            var engLeaseSupplier = SupplierRoleFactory.CreateEngineLeaseSupplier(supplierCompany);
            var engPurchaseSupplier = SupplierRoleFactory.CreateEnginePurchaseSupplier(supplierCompany);

            // Act

            // Assert
            Assert.IsNotNull(trade);
        }
    }
}