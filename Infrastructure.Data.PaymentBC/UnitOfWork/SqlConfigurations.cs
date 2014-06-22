#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：15:27
// 方案：FRP
// 项目：Infrastructure.Data.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork
{
    public class SqlConfigurations : IModelConfiguration
    {
        #region IModelConfiguration 成员

        public string GetDatabaseType()
        {
            return "Sql";
        }

        public DbConnection GetDbConnection()
        {
            var connString = ConfigurationManager.ConnectionStrings["SqlFRP"].ToString();
            var connectionString = Cryptography.GetConnString(connString);
            var dbConnection = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection();
            if (dbConnection != null)
            {
                dbConnection.ConnectionString = connectionString;
            }
            return dbConnection;
        }

        public void AddConfiguration(DbModelBuilder modelBuilder)
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
                .Add(new PurchaseCreditNoteInvoiceEntityConfiguration())
                .Add(new MaintainCreditNoteInvoiceEntityConfiguration())
                .Add(new LeaseInvoiceEntityConfiguration())
                .Add(new PurchaseInvoiceEntityConfiguration())
                .Add(new PurchasePrepaymentInvoiceEntityConfiguration())
                .Add(new MaintainPrepaymentInvoiceEntityConfiguration())
                .Add(new PurchaseInvoiceLineEntityConfiguration())
                .Add(new BasePurchaseInvoiceEntityConfiguration())
                .Add(new SundryInvoiceEntityConfiguration())
                .Add(new SpecialRefitInvoiceEntityConfiguration())
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

                #region MaintainInvoiceAgg

                .Add(new MaintainInvoiceEntityConfiguration())
                .Add(new AirframeMaintainInvoiceEntityConfiguration())
                .Add(new APUMaintainInvoiceEntityConfiguration())
                .Add(new EngineMaintainInvoiceEntityConfiguration())
                .Add(new UndercartMaintainInvoiceEntityConfiguration())
                .Add(new MaintainInvoiceLineEntityConfiguration())
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

                #region PartAgg

                .Add(new PartEntityConfiguration())

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
                .Add(new MaintainPaymentScheduleEntityConfiguration())

                #endregion

                #region TradeAgg

                .Add(new TradeEntityConfiguration())

                #endregion

                #region MaintainCostAgg

                .Add(new MaintainCostEntityConfiguration())
                .Add(new RegularCheckMaintainCostEntityConfiguration())
                .Add(new UndercartMaintainCostEntityConfiguration())
                .Add(new SpecialRefitMaintainCostEntityConfiguration())
                .Add(new NonFhaMaintainCostEntityConfiguration())
                .Add(new ApuMaintainCostEntityConfiguration())
                .Add(new FhaMaintainCostEntityConfiguration())
                #endregion

                #region

                .Add(new SupplierEntityConfiguration())
                .Add(new SupplierRoleEntityConfiguration())
                .Add(new AircraftLeaseSupplierEntityConfiguration())
                .Add(new AircraftPurchaseSupplierEntityConfiguration())
                .Add(new BFEPurchaseSupplierEntityConfiguration())
                .Add(new EngineLeaseSupplierEntityConfiguration())
                .Add(new EnginePurchaseSupplierEntityConfiguration())
                .Add(new MaintainSupplierEntityConfiguration())
                .Add(new OtherSupplierEntityConfiguration())
                .Add(new SupplierCompanyEntityConfiguration())

                #endregion

                ;
        }

        #endregion
    }
}