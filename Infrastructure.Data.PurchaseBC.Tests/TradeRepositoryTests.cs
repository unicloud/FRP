#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/18，20:52
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
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Tests
{
    [TestClass]
    public class TradeRepositoryTests
    {
        [TestMethod]
        public void GetAllTrade()
        {
            // Arrange
            var service = UniContainer.Resolve<ITradeRepository>();

            // Act
            var result = service.GetAll().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void CreateTrade()
        {
            // Arrange
            var supplierRep = UniContainer.Resolve<ISupplierRepository>();
            var tradeRep = UniContainer.Resolve<ITradeRepository>();

            var supplier = supplierRep.GetAll().FirstOrDefault();
            var trade = TradeFactory.CreateTrade("购买飞机", null, DateTime.Now, "购买飞机");
            // 设置交易编号
            var date = DateTime.Now.Date;
            var seq = tradeRep.GetFiltered(t => t.CreateDate > date).Count() + 1;
            trade.SetTradeNumber(seq);
            // 设置供应商
            trade.SetSupplier(supplier);
            // 修改状态
            trade.SetStatus(TradeStatus.进行中);

            // Act
            tradeRep.Add(trade);
            tradeRep.UnitOfWork.Commit();
        }
    }
}