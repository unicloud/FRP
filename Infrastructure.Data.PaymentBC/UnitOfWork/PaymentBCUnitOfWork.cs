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
using System.Data.Entity.ModelConfiguration.Conventions;
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
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;
using UniCloud.Domain.PaymentBC.Aggregates.PaymentNoticeAgg;
using UniCloud.Domain.PaymentBC.Aggregates.PaymentScheduleAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork
{
    public class PaymentBCUnitOfWork : BaseContext<PaymentBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<ActionCategory> _actionCategories;
        private IDbSet<AircraftType> _aircraftTypes;
        private IDbSet<BankAccount> _bankAccounts;
        private IDbSet<ContractAircraft> _contractAircrafts;
        private IDbSet<ContractEngine> _contractEngines;
        private IDbSet<Currency> _currencies;
        private IDbSet<Guarantee> _guarantees;
        private IDbSet<Invoice> _invoices;
        private IDbSet<Linkman> _linkmen;
        private IDbSet<MaintainContract> _maintainContracts;
        private IDbSet<Order> _orders;
        private IDbSet<PaymentNotice> _paymentNotices;
        private IDbSet<PaymentSchedule> _paymentSchedules;
        private IDbSet<Supplier> _suppliers;

        public IDbSet<ActionCategory> ActionCategories
        {
            get { return _actionCategories ?? (_actionCategories = base.Set<ActionCategory>()); }
        }

        public IDbSet<AircraftType> AircraftTypes
        {
            get { return _aircraftTypes ?? (_aircraftTypes = base.Set<AircraftType>()); }
        }

        public IDbSet<BankAccount> BankAccounts
        {
            get { return _bankAccounts ?? (_bankAccounts = base.Set<BankAccount>()); }
        }

        public IDbSet<ContractAircraft> ContractAircrafts
        {
            get { return _contractAircrafts ?? (_contractAircrafts = base.Set<ContractAircraft>()); }
        }

        public IDbSet<ContractEngine> ContractEngines
        {
            get { return _contractEngines ?? (_contractEngines = base.Set<ContractEngine>()); }
        }

        public IDbSet<Currency> Currencies
        {
            get { return _currencies ?? (_currencies = base.Set<Currency>()); }
        }

        public IDbSet<Guarantee> Guarantees
        {
            get { return _guarantees ?? (_guarantees = base.Set<Guarantee>()); }
        }

        public IDbSet<Invoice> Invoices
        {
            get { return _invoices ?? (_invoices = base.Set<Invoice>()); }
        }

        public IDbSet<Linkman> Linkmen
        {
            get { return _linkmen ?? (_linkmen = base.Set<Linkman>()); }
        }

        public IDbSet<MaintainContract> MaintainContracts
        {
            get { return _maintainContracts ?? (_maintainContracts = base.Set<MaintainContract>()); }
        }

        public IDbSet<Order> Orders
        {
            get { return _orders ?? (_orders = base.Set<Order>()); }
        }

        public IDbSet<PaymentNotice> PaymentNotices
        {
            get { return _paymentNotices ?? (_paymentNotices = base.Set<PaymentNotice>()); }
        }

        public IDbSet<PaymentSchedule> PaymentSchedules
        {
            get { return _paymentSchedules ?? (_paymentSchedules = base.Set<PaymentSchedule>()); }
        }

        public IDbSet<Supplier> Suppliers
        {
            get { return _suppliers ?? (_suppliers = base.Set<Supplier>()); }
        }

        #endregion

        #region DbContext 重载

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 移除不需要的公约
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // 添加通过“TypeConfiguration”类的方式建立的配置
            if (DbConfig.DbUniCloud.Contains("Oracle"))
            {
                OracleConfigurations(modelBuilder);
            }
            else if (DbConfig.DbUniCloud.Contains("Sql"))
            {
                SqlConfigurations(modelBuilder);
            }
        }

        /// <summary>
        ///     Oracle数据库
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void OracleConfigurations(DbModelBuilder modelBuilder)
        {
        }

        /// <summary>
        ///     SqlServer数据库
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void SqlConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations

                #region ActionCategoryAgg

                .Add(new ActionCategoryEntityConfiguration())

                #endregion

                #region AircraftTypeAgg

                .Add(new AircraftTypeEntityConfiguration())

                #endregion

                #region BankAccountAgg

                .Add(new BankAccountEntityConfiguration())

                #endregion

                #region ContractAircraftAgg

                .Add(new ContractAircraftEntityConfiguration())
                .Add(new LeaseContractAircraftEntityConfiguration())
                .Add(new PurchaseContractAircraftEntityConfiguration())

                #endregion

                #region ContractEngineAgg

                .Add(new ContractEngineEntityConfiguration())
                .Add(new LeaseContractEngineEntityConfiguration())
                .Add(new PurchaseContractEngineEntityConfiguration())

                #endregion

                #region CurrencyAgg

                .Add(new CurrencyEntityConfiguration())

                #endregion

                #region GuaranteeAgg

                .Add(new GuaranteeEntityConfiguration())
                .Add(new LeaseGuaranteeEntityConfiguration())
                .Add(new MaintainGuaranteeEntityConfiguration())

                #endregion

                #region InvoiceAgg

                .Add(new InvoiceEntityConfiguration())
                .Add(new InvoiceLineEntityConfiguration())
                .Add(new LeaseInvoiceEntityConfiguration())
                .Add(new LeaseInvoiceLineEntityConfiguration())
                .Add(new PurchaseInvoiceEntityConfiguration())
                .Add(new PurchaseInvoiceLineEntityConfiguration())
                .Add(new PrepaymentInvoiceEntityConfiguration())
                .Add(new PrepaymentInvoiceLineEntityConfiguration())

                #endregion

                #region LinkmanAgg

                .Add(new LinkmanEntityConfiguration())

                #endregion

                #region MaintainContractAgg

                .Add(new MaintainContractEntityConfiguration())
                .Add(new APUMaintainContractEntityConfiguration())
                .Add(new EngineMaintainContractEntityConfiguration())
                .Add(new UndercartMaintainContractEntityConfiguration())

                #endregion

                #region OrderAgg

                .Add(new OrderEntityConfiguration())
                .Add(new OrderLineEntityConfiguration())
                .Add(new AircraftLeaseOrderEntityConfiguration())
                .Add(new AircraftLeaseOrderLineEntityConfiguration())
                .Add(new AircraftPurchaseOrderEntityConfiguration())
                .Add(new AircraftPurchaseOrderLineEntityConfiguration())
                .Add(new BFEPurchaseOrderEntityConfiguration())
                .Add(new BFEPurchaseOrderLineEntityConfiguration())
                .Add(new EngineLeaseOrderEntityConfiguration())
                .Add(new EngineLeaseOrderLineEntityConfiguration())
                .Add(new EnginePurchaseOrderEntityConfiguration())
                .Add(new EnginePurchaseOrderLineEntityConfiguration())

                #endregion

                #region PaymentNoticeAgg

                .Add(new PaymentNoticeEntityConfiguration())
                .Add(new PaymentNoticeLineEntityConfiguration())

                #endregion

                #region PaymentScheduleAgg

                .Add(new PaymentScheduleEntityConfiguration())
                .Add(new PaymentScheduleLineEntityConfiguration())
                .Add(new AircraftPaymentScheduleEntityConfiguration())
                .Add(new EnginePaymentScheduleEntityConfiguration())
                .Add(new StandardPaymentScheduleEntityConfiguration())

                #endregion

                .Add(new SupplierEntityConfiguration());
        }

        #endregion
    }
}