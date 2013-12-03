#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/18，17:12
// 方案：FRP
// 项目：Infrastructure.Data.UberModel
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Domain.UberModel.Aggregates.CurrencyAgg;
using UniCloud.Domain.UberModel.Aggregates.LinkmanAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierRoleAgg;
using UniCloud.Domain.UberModel.Aggregates.TradeAgg;
using UniCloud.Domain.UberModel.Enums;
using UniCloud.Domain.UberModel.ValueObjects;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class TradeData : InitialDataBase
    {
        public TradeData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        public override void InitialData()
        {
            var supplier = SupplierFactory.CreateSupplier(SupplierType.Foreign, "V0001", "波音", null);
            supplier.GenerateNewIdentity();

            var supplierCompany = SupplierCompanyFactory.CreateSupplieCompany(supplier.Code);
            supplierCompany.GenerateNewIdentity();
            supplier.SetSupplierCompany(supplierCompany);

            Context.Suppliers.Add(supplier);

            var trade = TradeFactory.CreateTrade("购买飞机", null, DateTime.Now);
            trade.GenerateNewIdentity();
            // 设置交易编号
            var date = DateTime.Now.Date;
            var seq = Context.Trades.Count(t => t.CreateDate > date) + 1;
            trade.SetTradeNumber(seq);
            // 设置供应商
            trade.SetSupplier(supplier);
            Context.Trades.Add(trade);

            var acLeaseSupplier = SupplierRoleFactory.CreateAircraftLeaseSupplier(supplierCompany);
            var acPurchaseSupplier = SupplierRoleFactory.CreateAircraftPurchaseSupplier(supplierCompany);
            var bfePurchaseSupplier = SupplierRoleFactory.CreateBFEPurchaseSupplier(supplierCompany);
            var engLeaseSupplier = SupplierRoleFactory.CreateEngineLeaseSupplier(supplierCompany);
            var engPurchaseSupplier = SupplierRoleFactory.CreateEnginePurchaseSupplier(supplierCompany);
            Context.SupplierRoles.Add(acLeaseSupplier);
            Context.SupplierRoles.Add(acPurchaseSupplier);
            Context.SupplierRoles.Add(bfePurchaseSupplier);
            Context.SupplierRoles.Add(engLeaseSupplier);
            Context.SupplierRoles.Add(engPurchaseSupplier);

            var currency = new Currency
            {
                CnName = "美元",
                EnName = "Dollers"
            };
            currency.GenerateNewIdentity();
            Context.Currencies.Add(currency);

            var linkman = LinkmanFactory.CreateLinkman("DDD", "12345", "3333", null, "abc@3g",
                new Address("成都", "361000", null, null), Guid.NewGuid());
            linkman.SetSourceId(supplierCompany.LinkmanId);
            Context.Linkmen.Add(linkman);
        }
    }
}