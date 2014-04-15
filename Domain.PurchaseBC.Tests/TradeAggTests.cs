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
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierRoleAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;

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
            var supplier = SupplierFactory.CreateSupplier();
            SupplierFactory.SetSupplier(supplier, SupplierType.国外, "V0001", "波音", "波音", true, null, supplier.SupplierCompanyId);
            var trade = TradeFactory.CreateTrade(null, null, DateTime.Now, "购买飞机");
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