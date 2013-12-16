#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，15:11
// 方案：FRP
// 项目：Application.PurchaseBC.Tests
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PurchaseBC.Query.TradeQueries;
using UniCloud.Application.PurchaseBC.TradeServices;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.PurchaseBC.Tests.Services
{
    [TestClass]
    public class TradeBCTests
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
                .Register<ISupplierRepository, SupplierRepository>()
                .Register<IOrderRepository, OrderRepository>()
                .Register<IRelatedDocRepository,RelatedDocRepository>()
                .Register<ITradeAppService, TradeAppService>()
                .Register<ITradeQuery, TradeQuery>()
                .Register<IOrderQuery, OrderQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetTrades()
        {
            // Arrange
            var service = DefaultContainer.Resolve<ITradeAppService>();

            // Act
            var result = service.GetTrades().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetOrders()
        {
            // Arrange
            var service = DefaultContainer.Resolve<ITradeAppService>();

            // Act
            var result = service.GetAircraftPurchaseOrders().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetAircraftPurchaseOrderWithOrderLine()
        {
            // Arrange
            var service = DefaultContainer.Resolve<ITradeAppService>();

            // Act
            var result = service.GetAircraftPurchaseOrders().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetTradWithOrderAndOrderLine()
        {
            // Arrange
            var service = DefaultContainer.Resolve<ITradeAppService>();

            // Act
            var result1 = service.GetTrades().ToList();
            var result2 = service.GetAircraftPurchaseOrders().ToList();

            // Assert
            Assert.IsTrue(result1.Any());
            Assert.IsTrue(result2.Any());
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