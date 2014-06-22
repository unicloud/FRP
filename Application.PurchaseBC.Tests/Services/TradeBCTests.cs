﻿#region 版本信息

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
using UniCloud.Application.PurchaseBC.Query.SupplierQueries;
using UniCloud.Application.PurchaseBC.Query.TradeQueries;
using UniCloud.Application.PurchaseBC.SupplierServices;
using UniCloud.Application.PurchaseBC.TradeServices;
using UniCloud.Domain.Events;
using UniCloud.Domain.PurchaseBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractEngineAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Domain.PurchaseBC.Events;
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
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IEventAggregator, EventAggregator>(new WcfPerRequestLifetimeManager())
                .Register<IPurchaseEvent, PurchaseEvent>(new WcfPerRequestLifetimeManager())

                #region 交易相关配置，包括查询，应用服务，仓储注册

                .Register<ITradeQuery, TradeQuery>()
                .Register<IOrderQuery, OrderQuery>()
                .Register<ITradeAppService, TradeAppService>()
                .Register<ITradeRepository, TradeRepository>()
                .Register<IOrderRepository, OrderRepository>()
                .Register<IMaterialRepository, MaterialRepository>()
                .Register<IActionCategoryRepository, ActionCategoryRepository>()
                .Register<IRelatedDocRepository, RelatedDocRepository>()
                .Register<IContractAircraftRepository, ContractAircraftRepository>()
                .Register<IContractEngineRepository, ContractEngineRepository>()

                #endregion

                #region 供应商相关配置，包括查询，应用服务，仓储注册

                .Register<ISupplierQuery, SupplierQuery>()
                .Register<ISupplierAppService, SupplierAppService>()
                .Register<ISupplierCompanyRepository, SupplierCompanyRepository>()
                .Register<ISupplierRepository, SupplierRepository>();

            #endregion
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
            var service = UniContainer.Resolve<ITradeAppService>();

            // Act
            var result = service.GetTrades().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetOrders()
        {
            // Arrange
            var service = UniContainer.Resolve<ITradeAppService>();

            // Act
            var result = service.GetAircraftPurchaseOrders().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetAircraftPurchaseOrderWithOrderLine()
        {
            // Arrange
            var service = UniContainer.Resolve<ITradeAppService>();

            // Act
            var result = service.GetAircraftPurchaseOrders().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetTradWithOrderAndOrderLine()
        {
            // Arrange
            var service = UniContainer.Resolve<ITradeAppService>();

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
            var supplierRep = UniContainer.Resolve<ISupplierRepository>();
            var tradeRep = UniContainer.Resolve<ITradeRepository>();

            var supplier = supplierRep.GetAll().FirstOrDefault();
            var trade1 = TradeFactory.CreateTrade("购买飞机", null, DateTime.Now, "购买飞机");
            trade1.SetTradeNumber(1);
            var trade2 = TradeFactory.CreateTrade("购买飞机", null, DateTime.Now, "购买飞机");
            trade2.SetTradeNumber(2);
            var trade3 = TradeFactory.CreateTrade("购买飞机", null, DateTime.Now, "购买飞机");
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

        [TestMethod]
        public void FulfilOrderEventTest()
        {
            // Arrange
            var service = UniContainer.Resolve<IOrderRepository>();
            UniContainer.Resolve<IPurchaseEvent>();
            var order = service.GetAll().FirstOrDefault();

            // Act
            DomainEvent.Publish<FulfilOrderEvent, Order>(order);
        }
    }
}