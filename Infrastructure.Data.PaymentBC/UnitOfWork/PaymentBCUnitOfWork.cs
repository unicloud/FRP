#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，13:49
// 方案：FRP
// 项目：Infrastructure.Data.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Data.Entity;
using UniCloud.Domain.PaymentBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.PaymentBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PaymentBC.Aggregates.BankAccountAgg;
using UniCloud.Domain.PaymentBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PaymentBC.Aggregates.ContractEngineAgg;
using UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PaymentBC.Aggregates.GuaranteeAgg;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;
using UniCloud.Domain.PaymentBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainContractAgg;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainCostAgg;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;
using UniCloud.Domain.PaymentBC.Aggregates.PaymentNoticeAgg;
using UniCloud.Domain.PaymentBC.Aggregates.PaymentScheduleAgg;
using UniCloud.Domain.PaymentBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PaymentBC.Aggregates.TradeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork
{
    public class PaymentBCUnitOfWork : UniContext<PaymentBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<ActionCategory> _actionCategories;
        private IDbSet<AircraftType> _aircraftTypes;
        private IDbSet<BankAccount> _bankAccounts;
        private IDbSet<BasePurchaseInvoice> _basePurchaseInvoices;
        private IDbSet<ContractAircraft> _contractAircrafts;
        private IDbSet<ContractEngine> _contractEngines;
        private IDbSet<Currency> _currencies;
        private IDbSet<Guarantee> _guarantees;
        private IDbSet<Invoice> _invoices;
        private IDbSet<Linkman> _linkmen;
        private IDbSet<MaintainContract> _maintainContracts;
        private IDbSet<MaintainCost> _maintainCosts;
        private IDbSet<MaintainInvoice> _maintainInvoices;
        private IDbSet<Order> _orders;
        private IDbSet<PnReg> _pnRegs;
        private IDbSet<PaymentNotice> _paymentNotices;
        private IDbSet<PaymentSchedule> _paymentSchedules;
        private IDbSet<SupplierRole> _supplierRoles;
        private IDbSet<Supplier> _suppliers;
        private IDbSet<Trade> _trades;

        public IDbSet<MaintainCost> MaintainCosts
        {
            get { return _maintainCosts ?? (_maintainCosts = Set<MaintainCost>()); }
        }

        public IDbSet<ActionCategory> ActionCategories
        {
            get { return _actionCategories ?? (_actionCategories = Set<ActionCategory>()); }
        }

        public IDbSet<AircraftType> AircraftTypes
        {
            get { return _aircraftTypes ?? (_aircraftTypes = Set<AircraftType>()); }
        }

        public IDbSet<BankAccount> BankAccounts
        {
            get { return _bankAccounts ?? (_bankAccounts = Set<BankAccount>()); }
        }

        public IDbSet<ContractAircraft> ContractAircrafts
        {
            get { return _contractAircrafts ?? (_contractAircrafts = Set<ContractAircraft>()); }
        }

        public IDbSet<ContractEngine> ContractEngines
        {
            get { return _contractEngines ?? (_contractEngines = Set<ContractEngine>()); }
        }

        public IDbSet<Currency> Currencies
        {
            get { return _currencies ?? (_currencies = Set<Currency>()); }
        }

        public IDbSet<Guarantee> Guarantees
        {
            get { return _guarantees ?? (_guarantees = Set<Guarantee>()); }
        }

        public IDbSet<Invoice> Invoices
        {
            get { return _invoices ?? (_invoices = Set<Invoice>()); }
        }

        public IDbSet<BasePurchaseInvoice> BasePurchaseInvoices
        {
            get { return _basePurchaseInvoices ?? (_basePurchaseInvoices = Set<BasePurchaseInvoice>()); }
        }

        public IDbSet<Linkman> Linkmen
        {
            get { return _linkmen ?? (_linkmen = Set<Linkman>()); }
        }

        public IDbSet<MaintainContract> MaintainContracts
        {
            get { return _maintainContracts ?? (_maintainContracts = Set<MaintainContract>()); }
        }

        public IDbSet<MaintainInvoice> MaintainInvoices
        {
            get { return _maintainInvoices ?? (_maintainInvoices = Set<MaintainInvoice>()); }
        }

        public IDbSet<Order> Orders
        {
            get { return _orders ?? (_orders = Set<Order>()); }
        }

        public IDbSet<PnReg> PnRegs
        {
            get { return _pnRegs ?? (_pnRegs = Set<PnReg>()); }
        }

        public IDbSet<PaymentNotice> PaymentNotices
        {
            get { return _paymentNotices ?? (_paymentNotices = Set<PaymentNotice>()); }
        }

        public IDbSet<PaymentSchedule> PaymentSchedules
        {
            get { return _paymentSchedules ?? (_paymentSchedules = Set<PaymentSchedule>()); }
        }

        public IDbSet<Supplier> Suppliers
        {
            get { return _suppliers ?? (_suppliers = Set<Supplier>()); }
        }

        public IDbSet<Trade> Trades
        {
            get { return _trades ?? (_trades = Set<Trade>()); }
        }

        public IDbSet<SupplierRole> SupplierRoles
        {
            get { return _supplierRoles ?? (_supplierRoles = Set<SupplierRole>()); }
        }

        #endregion
    }
}