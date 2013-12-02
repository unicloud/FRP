﻿#region 版本信息

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
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Tests
{
    [TestClass]
    public class TradeRepositoryTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            Configuration.Create()
                .UseAutofac()
                .CreateLog()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<ITradeRepository, TradeRepository>()
                .Register<ISupplierRepository, SupplierRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetAllTrade()
        {
            // Arrange
            var service = DefaultContainer.Resolve<ITradeRepository>();

            // Act
            var result = service.GetAll().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void CreateTrade()
        {
            // Arrange
            var supplierRep = DefaultContainer.Resolve<ISupplierRepository>();
            var tradeRep = DefaultContainer.Resolve<ITradeRepository>();

            var supplier = supplierRep.GetAll().FirstOrDefault();
            var trade1 = TradeFactory.CreateTrade("购买飞机", null, DateTime.Now);
            trade1.SetTradeNumber(1);
            var trade2 = TradeFactory.CreateTrade("购买飞机", null, DateTime.Now);
            trade2.SetTradeNumber(2);
            var trade3 = TradeFactory.CreateTrade("购买飞机", null, DateTime.Now);
            trade3.SetTradeNumber(3);
            trade1.SetSupplier(supplier);
            trade2.SetSupplier(supplier);
            trade3.SetSupplier(supplier);

            // Act
            tradeRep.Add(trade1);
            tradeRep.Add(trade2);
            tradeRep.Add(trade3);
            tradeRep.UnitOfWork.Commit();
        }
    }
}