#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：15:57
// 方案：FRP
// 项目：Infrastructure.Data.UberModel
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
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql;
using UniCloud.Infrastructure.Security;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork
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

                #region AcDailyUtilizationAgg

                .Add(new AcDailyUtilizationEntityConfiguration())
                #endregion

                #region ActionCategoryAgg

                .Add(new ActionCategoryEntityConfiguration())

                #endregion

                #region AircraftSeriesAgg

                .Add(new AircraftSeriesEntityConfiguration())
                .Add(new AtaEntityConfiguration())
                #endregion

                #region AircraftCategoryAgg

                .Add(new AircraftCategoryEntityConfiguration())

                #endregion

                #region AircraftAgg

                .Add(new AircraftEntityConfiguration())
                .Add(new AircraftBusinessEntityConfiguration())
                .Add(new OperationHistoryEntityConfiguration())
                .Add(new OwnershipHistoryEntityConfiguration())
                .Add(new AcConfigHistoryEntityConfiguration())

                #endregion

                #region AircraftPlanAgg

                .Add(new PlanEntityConfiguration())
                .Add(new PlanHistoryEntityConfiguration())
                .Add(new ChangePlanEntityConfiguration())
                .Add(new OperationPlanEntityConfiguration())

                #endregion

                #region AircraftTypeAgg

                .Add(new AircraftTypeEntityConfiguration())
                .Add(new CAACAircraftTypeEntityConfiguration())
                #endregion

                #region AirlinesAgg

                .Add(new AirlinesEntityConfiguration())

                #endregion

                #region AirProgrammingAgg

                .Add(new AirProgrammingEntityConfiguration())
                .Add(new AirProgrammingLineEntityConfiguration())

                #endregion

                #region AnnualAgg

                .Add(new AnnualEntityConfiguration())
                .Add(new AnnualMaintainPlanEntityConfiguration())
                .Add(new EngineMaintainPlanEntityConfiguration())
                .Add(new EngineMaintainPlanDetailEntityConfiguration())
                .Add(new AircraftMaintainPlanEntityConfiguration())
                .Add(new AircraftMaintainPlanDetailEntityConfiguration())
                #endregion

                #region ApprovalDocAgg

                .Add(new ApprovalDocEntityConfiguration())

                #endregion

                #region BankAccountAgg

                .Add(new BankAccountEntityConfiguration())

                #endregion

                #region BasicConfigGroupAgg

                .Add(new BasicConfigEntityConfiguration())
                .Add(new BasicConfigGroupEntityConfiguration())
                .Add(new BasicConfigHistoryEntityConfiguration())
                #endregion

                #region CaacProgrammingAgg

                .Add(new CaacProgrammingEntityConfiguration())
                .Add(new CaacProgrammingLineEntityConfiguration())

                #endregion

                #region ContentTagAgg

                .Add(new ContentTagEntityConfiguration())

                #endregion

                #region ContractAircraftAgg

                .Add(new ContractAircraftEntityConfiguration())
                .Add(new LeaseContractAircraftEntityConfiguration())
                .Add(new PurchaseContractAircraftEntityConfiguration())

                #endregion

                #region ContractAircraftBFEAgg

                .Add(new ContractAircraftBFEEntityConfiguration())

                #endregion

                #region ContractEngineAgg

                .Add(new ContractEngineEntityConfiguration())
                .Add(new LeaseContractEngineEntityConfiguration())
                .Add(new PurchaseContractEngineEntityConfiguration())

                #endregion

                #region CtrlUnitAgg

                .Add(new CtrlUnitEntityConfiguration())
                #endregion

                #region CurrencyAgg

                .Add(new CurrencyEntityConfiguration())

                #endregion

                #region DocumentAgg

                .Add(new DocumentEntityConfiguration())
                .Add(new OfficialDocumentEntityConfiguration())
                .Add(new StandardDocumentEntityConfiguration())
                .Add(new DocumentTypeEntityConfiguration())
                #endregion

                #region DocumentPathAgg

                .Add(new DocumentPathEntityConfiguration())

                #endregion

                #region EngineAgg

                .Add(new EngineEntityConfiguration())
                .Add(new EngineBusinessHistoryEntityConfiguration())
                .Add(new EngineOwnershipHistoryEntityConfiguration())

                #endregion

                #region EnginePlanAgg

                .Add(new EnginePlanEntityConfiguration())
                .Add(new EnginePlanHistoryEntityConfiguration())

                #endregion

                #region EngineTypeAgg

                .Add(new EngineTypeEntityConfiguration())

                #endregion

                #region FlightLogAgg

                .Add(new FlightLogEntityConfiguration())

                #endregion

                #region ForwarderAgg

                .Add(new ForwarderEntityConfiguration())

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

                #region ItemAgg

                .Add(new ItemEntityConfiguration())
                #endregion

                #region InstallControllerAgg

                .Add(new InstallControllerEntityConfiguration())
                .Add(new DependencyEntityConfiguration())
                #endregion

                #region IssuedUnitAgg

                .Add(new IssuedUnitEntityConfiguration())
                #endregion

                #region LinkmanAgg

                .Add(new LinkmanEntityConfiguration())

                #endregion

                #region MailAddressAgg

                .Add(new MailAddressEntityConfiguration())

                #endregion

                #region MaintainContractAgg

                .Add(new MaintainContractEntityConfiguration())
                .Add(new APUMaintainContractEntityConfiguration())
                .Add(new EngineMaintainContractEntityConfiguration())
                .Add(new UndercartMaintainContractEntityConfiguration())
                .Add(new AirframeMaintainContractEntityConfiguration())
                #endregion

                #region MaintainCtrlAgg

                .Add(new MaintainCtrlEntityConfiguration())
                .Add(new ItemMaintainCtrlEntityConfiguration())
                .Add(new PnMaintainCtrlEntityConfiguration())
                .Add(new SnMaintainCtrlEntityConfiguration())
                #endregion

                #region MaintainInvoiceAgg

                .Add(new MaintainInvoiceEntityConfiguration())
                .Add(new AirframeMaintainInvoiceEntityConfiguration())
                .Add(new APUMaintainInvoiceEntityConfiguration())
                .Add(new EngineMaintainInvoiceEntityConfiguration())
                .Add(new UndercartMaintainInvoiceEntityConfiguration())
                .Add(new MaintainInvoiceLineEntityConfiguration())
                #endregion

                #region MaintainWorkAgg

                .Add(new MaintainWorkEntityConfiguration())
                #endregion

                #region ManagerAgg

                .Add(new ManagerEntityConfiguration())

                #endregion

                #region ManufacturerAgg

                .Add(new ManufacturerEntityConfiguration())

                #endregion

                #region MaterialAgg

                .Add(new MaterialEntityConfiguration())
                .Add(new AircraftMaterialEntityConfiguration())
                .Add(new BFEMaterialEntityConfiguration())
                .Add(new EngineMaterialEntityConfiguration())

                #endregion

                #region ModAgg

                .Add(new ModEntityConfiguration())
                #endregion

                #region OilMonitorAgg

                .Add(new OilMonitorEntityConfiguration())

                #endregion

                #region OrderAgg

                .Add(new OrderEntityConfiguration())
                .Add(new ContractContentEntityConfiguration())
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
                .Add(new MaintainPaymentScheduleEntityConfiguration())
                #endregion

                #region PlanAircraftAgg

                .Add(new PlanAircraftEntityConfiguration())

                #endregion

                #region PlanEngineAgg

                .Add(new PlanEngineEntityConfiguration())

                #endregion

                #region PnRegAgg

                .Add(new PnRegEntityConfiguration())
                #endregion

                #region ProgrammingAgg

                .Add(new ProgrammingEntityConfiguration())

                #endregion

                #region ProgrammingAgg

                .Add(new ProgrammingFileEntityConfiguration())

                #endregion

                #region ReceptionAgg

                .Add(new ReceptionEntityConfiguration())
                .Add(new ReceptionLineEntityConfiguration())
                .Add(new ReceptionScheduleEntityConfiguration())
                .Add(new AircraftLeaseReceptionEntityConfiguration())
                .Add(new AircraftLeaseReceptionLineEntityConfiguration())
                .Add(new AircraftPurchaseReceptionEntityConfiguration())
                .Add(new AircraftPurchaseReceptionLineEntityConfiguration())
                .Add(new EngineLeaseReceptionEntityConfiguration())
                .Add(new EngineLeaseReceptionLineEntityConfiguration())
                .Add(new EnginePurchaseReceptionEntityConfiguration())
                .Add(new EnginePurchaseReceptionLineEntityConfiguration())

                #endregion

                #region RelatedDocAgg

                .Add(new RelatedDocEntityConfiguration())

                #endregion

                #region RequestAgg

                .Add(new RequestEntityConfiguration())
                .Add(new ApprovalHistoryEntityConfiguration())

                #endregion

                #region ScnAgg

                .Add(new ScnEntityConfiguration())
                .Add(new ApplicableAircraftEntityConfiguration())
                .Add(new AirBusScnEntityConfiguration())
                #endregion

                #region SnRegAgg

                .Add(new SnRegEntityConfiguration())
                .Add(new LifeMonitorEntityConfiguration())
                .Add(new EngineRegEntityConfiguration())
                .Add(new APURegEntityConfiguration())

                #endregion

                #region SnHistoryAgg

                .Add(new SnHistoryEntityConfiguration())

                #endregion

                #region SnRemInstRecordAgg

                .Add(new SnRemInstRecordEntityConfiguration())

                #endregion

                #region SpecialConfigAgg

                .Add(new AcConfigEntityConfiguration())
                .Add(new SpecialConfigEntityConfiguration())

                #endregion

                #region SupplierAgg

                .Add(new SupplierEntityConfiguration())

                #endregion

                #region SupplierCompanyAgg

                .Add(new SupplierCompanyEntityConfiguration())

                #endregion

                #region SupplierCompanyMaterialAgg

                .Add(new SupplierCompanyMaterialEntityConfiguration())

                #endregion

                #region SupplierRoleAgg

                .Add(new SupplierRoleEntityConfiguration())
                .Add(new AircraftLeaseSupplierEntityConfiguration())
                .Add(new AircraftPurchaseSupplierEntityConfiguration())
                .Add(new BFEPurchaseSupplierEntityConfiguration())
                .Add(new EngineLeaseSupplierEntityConfiguration())
                .Add(new EnginePurchaseSupplierEntityConfiguration())
                .Add(new MaintainSupplierEntityConfiguration())
                .Add(new OtherSupplierEntityConfiguration())
                #endregion

                #region TradeAgg

                .Add(new TradeEntityConfiguration())

                #endregion

                #region ThresholdAgg

                .Add(new ThresholdEntityConfiguration())

                #endregion

                #region ThrustAgg

                .Add(new ThrustEntityConfiguration())

                #endregion

                #region XmlConfigAgg

                .Add(new XmlConfigEntityConfiguration())

                #endregion

                #region XmlSettingAgg

                .Add(new XmlSettingEntityConfiguration())

                #endregion

                #region ProjectAgg

                .Add(new ProjectEntityConfiguration())
                .Add(new TaskEntityConfiguration())

                #endregion

                #region ProjectTempAgg

                .Add(new ProjectTempEntityConfiguration())
                .Add(new TaskTempEntityConfiguration())

                #endregion

                #region TaskStandardAgg

                .Add(new TaskStandardEntityConfiguration())
                .Add(new TaskCaseEntityConfiguration())

                #endregion

                #region WorkGroupAgg

                .Add(new WorkGroupEntityConfiguration())
                .Add(new MemberEntityConfiguration())

                #endregion

                #region AircraftLicense

                .Add(new AircraftLicenseEntityConfiguration())
                .Add(new LicenseTypeEntityConfiguration())
                #endregion

                #region AirStructureDamageAgg

                .Add(new AirStructureDamageEntityConfiguration())
                #endregion

                #region  AdSbAgg

                .Add(new AdSbEntityConfiguration())
                #endregion

                #region AircraftConfigurationAgg

                .Add(new AircraftConfigurationEntityConfiguration())
                .Add(new AircraftCabinEntityConfiguration())
                .Add(new AircraftCabinTypeEntityConfiguration())
                #endregion

                #region FunctionItemAgg

                .Add(new FunctionItemEntityConfiguration())

                #endregion

                #region OrganizationUserAgg

                .Add(new OrganizationUserEntityConfiguration())

                #endregion

                #region OrganizationRoleAgg

                .Add(new OrganizationRoleEntityConfiguration())

                #endregion

                #region OrganizationAgg

                .Add(new OrganizationEntityConfiguration())

                #endregion

                #region RoleFunctionAgg

                .Add(new RoleFunctionEntityConfiguration())

                #endregion

                #region RoleAgg

                .Add(new RoleEntityConfiguration())

                #endregion

                #region UserRoleAgg

                .Add(new UserRoleEntityConfiguration())

                #endregion

                #region UserAgg

                .Add(new UserEntityConfiguration())

                #endregion

                #region BusinessLicenseAgg

                .Add(new BusinessLicenseEntityConfiguration())
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

                .Add(new AddressConfiguration());
        }

        #endregion
    }
}