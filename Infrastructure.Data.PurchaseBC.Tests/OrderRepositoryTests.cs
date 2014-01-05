﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/20，10:05
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
using UniCloud.Domain.PurchaseBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftBFEAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;
#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Tests
{
    [TestClass]
    public class OrderRepositoryTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<IOrderRepository, OrderRepository>()
                .RegisterType<ICurrencyRepository, CurrencyRepository>()
                .RegisterType<ITradeRepository, TradeRepository>()
                .RegisterType<IContractAircraftRepository, ContractAircraftRepository>()
                .RegisterType<IContractAircraftBFERepository, ContractAircraftBFERepository>()
                .RegisterType<IAircraftTypeRepository, AircraftTypeRepository>()
                .RegisterType<IActionCategoryRepository, ActionCategoryRepository>()
                .RegisterType<IRelatedDocRepository, RelatedDocRepository>()
                .RegisterType<ILinkmanRepository, LinkmanRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetAllOrders()
        {
            // Arrange
            var orderRep = DefaultContainer.Resolve<IOrderRepository>();

            // Act
            var result = orderRep.GetAll().OfType<AircraftPurchaseOrder>().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void CreateOrder()
        {
            // Arrange
            var orderRep = DefaultContainer.Resolve<IOrderRepository>();
            var tradeRep = DefaultContainer.Resolve<ITradeRepository>();
            var currencyRep = DefaultContainer.Resolve<ICurrencyRepository>();
            var linkmanRep = DefaultContainer.Resolve<ILinkmanRepository>();
            var acTypeRep = DefaultContainer.Resolve<IAircraftTypeRepository>();
            var impTypeRep = DefaultContainer.Resolve<IActionCategoryRepository>();

            var trade = tradeRep.GetAll().FirstOrDefault();
            var currency = currencyRep.GetAll().FirstOrDefault();
            var linkman = linkmanRep.GetAll().FirstOrDefault();
            var aircraftType = acTypeRep.GetFiltered(a => a.Name == "B737-800").FirstOrDefault();
            var impType = impTypeRep.GetFiltered(i => i.ActionName == "购买").FirstOrDefault();

            // 1、创建订单
            // 须设置交易、币种
            // 可选设置联系人
            var acPurchaseOrder = OrderFactory.CreateAircraftPurchaseOrder(2, "XXX", DateTime.Now);
            acPurchaseOrder.GenerateNewIdentity();
            acPurchaseOrder.SetTrade(trade);
            acPurchaseOrder.SetLinkman(linkman);
            acPurchaseOrder.SetCurrency(currency);

            // 2、添加订单行
            var acPurchaseOrderLine1 = acPurchaseOrder.AddNewAircraftPurchaseOrderLine(200M, 1, 0, DateTime.Now);
            acPurchaseOrderLine1.SetCost(100M, 50M, 50M);

            // 3、创建与订单对应的合同飞机
            var contractAircraft = ContractAircraftFactory.CreatePurchaseContractAircraft("购机合同", "0001");
            contractAircraft.GenerateNewIdentity();
            contractAircraft.SetAircraftType(aircraftType);
            contractAircraft.SetImportCategory(impType);

            // 4、为订单行设置对应的合同飞机
            acPurchaseOrderLine1.SetContractAircraft(contractAircraft);

            // Act
            orderRep.Add(acPurchaseOrder);
            orderRep.UnitOfWork.Commit();
        }


        [TestMethod]
        public void AddOrderLine()
        {
            // Arrange
            var orderRep = DefaultContainer.Resolve<IOrderRepository>();
            var order = orderRep.GetAll().OfType<AircraftPurchaseOrder>().FirstOrDefault();
            if (order != null)
            {
                order.AddNewAircraftPurchaseOrderLine(200M, 1, 0, DateTime.Now);
            }

            // Act
            orderRep.UnitOfWork.Commit();
        }

        [TestMethod]
        public void RemoveOrder()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IOrderRepository>();
            var order = service.GetAll().OfType<AircraftPurchaseOrder>().FirstOrDefault(o => o.OrderLines.Any());
            if (order != null)
            {
                service.Remove(order);
            }

            // Act
            service.UnitOfWork.Commit();
        }

        [TestMethod]
        public void AddContractAircraftWithBFEOrder()
        {
            // Arrange
            var orderRep = DefaultContainer.Resolve<IOrderRepository>();
            var acTypeRep = DefaultContainer.Resolve<IAircraftTypeRepository>();
            var impRep = DefaultContainer.Resolve<IActionCategoryRepository>();
            var tradeRep = DefaultContainer.Resolve<ITradeRepository>();
            var currencyRep = DefaultContainer.Resolve<ICurrencyRepository>();
            var linkmanRep = DefaultContainer.Resolve<ILinkmanRepository>();

            var trade = tradeRep.GetAll().FirstOrDefault();
            var currency = currencyRep.GetAll().FirstOrDefault();
            var linkman = linkmanRep.GetAll().FirstOrDefault();
            var acType = acTypeRep.GetFiltered(a => a.Name == "B737-800").FirstOrDefault();
            var imp = impRep.GetFiltered(i => i.ActionName == "购买").FirstOrDefault();

            var contractAc = ContractAircraftFactory.CreatePurchaseContractAircraft("购买飞机", "0002");
            contractAc.GenerateNewIdentity();
            contractAc.SetAircraftType(acType);
            contractAc.SetImportCategory(imp);

            var order = OrderFactory.CreateBFEPurchaseOrder(3, "", DateTime.Now);
            order.GenerateNewIdentity();
            order.SetTrade(trade);
            order.SetLinkman(linkman);
            order.SetCurrency(currency);
            order.AddNewContractAircraft(contractAc);

            // Act
            orderRep.Add(order);
            orderRep.UnitOfWork.Commit();
        }

        [TestMethod]
        public void DeleteContractAircraftFromBFEOrder()
        {
            // Arrange
            var orderRep = DefaultContainer.Resolve<IOrderRepository>();
            var caRep = DefaultContainer.Resolve<IContractAircraftRepository>();
            var cabRep = DefaultContainer.Resolve<IContractAircraftBFERepository>();

            var order = orderRep.GetAll().OfType<BFEPurchaseOrder>().FirstOrDefault(o => o.ContractAircraftBfes.Any());
            if (order == null)
            {
                throw new ArgumentException("订单为空！");
            }
            var contractAircraft =
                caRep.GetAll()
                    .SelectMany(c => c.ContractAircraftBfes)
                    .Where(c => c.BFEPurchaseOrderId == order.Id)
                    .Select(c => c.ContractAircraft)
                    .FirstOrDefault();

            var cab =
                cabRep.GetFiltered(c => c.BFEPurchaseOrderId == order.Id && c.ContractAircraftId == contractAircraft.Id)
                    .FirstOrDefault();

            // Act
            cabRep.Remove(cab);
            cabRep.UnitOfWork.Commit();
        }

        [TestMethod]
        public void GetRelatedDocByOrder()
        {
            // Arrange
            var docRep = DefaultContainer.Resolve<IRelatedDocRepository>();
            var orderId = Guid.Parse("066157a2-bf6c-4c7d-86e1-adfd7b725f72");

            // Act
            var result =
                docRep.GetFiltered(d => d.SourceId == orderId).ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}