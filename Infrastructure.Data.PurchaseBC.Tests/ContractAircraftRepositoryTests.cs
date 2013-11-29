#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/22，14:27
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
using UniCloud.Domain.PurchaseBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Tests
{
    [TestClass]
    public class ContractAircraftRepositoryTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            Configuration.Create()
                .UseAutofac()
                .CreateLog()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IAircraftTypeRepository, AircraftTypeRepository>()
                .Register<IActionCategoryRepository, ActionCategoryRepository>()
                .Register<ICurrencyRepository, CurrencyRepository>()
                .Register<ITradeRepository, TradeRepository>()
                .Register<ILinkmanRepository, LinkmanRepository>()
                .Register<IOrderRepository, OrderRepository>()
                .Register<IContractAircraftRepository, ContractAircraftRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void AddBFEOrderWithContractAircraft()
        {
            // Arrange
            var contractAircraftRep = DefaultContainer.Resolve<IContractAircraftRepository>();
            var acTypeRep = DefaultContainer.Resolve<IAircraftTypeRepository>();
            var impRep = DefaultContainer.Resolve<IActionCategoryRepository>();
            var tradeRep = DefaultContainer.Resolve<ITradeRepository>();
            var currencyRep = DefaultContainer.Resolve<ICurrencyRepository>();
            var linkmanRep = DefaultContainer.Resolve<ILinkmanRepository>();

            var trade = tradeRep.GetAll().FirstOrDefault();
            var currency = currencyRep.GetAll().FirstOrDefault();
            var linkman = linkmanRep.GetAll().FirstOrDefault();
            var acType = acTypeRep.GetAll().FirstOrDefault();
            var imp = impRep.GetAll().FirstOrDefault();

            var contractAc = ContractAircraftFactory.CreatePurchaseContractAircraft("购买飞机", "0002");
            contractAc.SetAircraftType(acType);
            contractAc.SetImportCategory(imp);
            var order = OrderFactory.CreateBFEPurchaseOrder(3, 123M, "", DateTime.Now);
            order.SetTrade(trade);
            order.SetLinkman(linkman);
            order.SetCurrency(currency);

            // Act
            contractAc.BFEPurchaseOrders.Add(order);
            contractAircraftRep.Add(contractAc);
            contractAircraftRep.UnitOfWork.Commit();
        }
    }
}