#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/04，10:11
// 方案：FRP
// 项目：Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UniCloud.Domain.UberModel.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftLicenseAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.AirlinesAgg;
using UniCloud.Domain.UberModel.Aggregates.AirProgrammingAgg;
using UniCloud.Domain.UberModel.Aggregates.AnnualAgg;
using UniCloud.Domain.UberModel.Aggregates.ApprovalDocAgg;
using UniCloud.Domain.UberModel.Aggregates.AtaAgg;
using UniCloud.Domain.UberModel.Aggregates.BankAccountAgg;
using UniCloud.Domain.UberModel.Aggregates.CaacProgrammingAgg;
using UniCloud.Domain.UberModel.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.UberModel.Aggregates.ContractAircraftBFEAgg;
using UniCloud.Domain.UberModel.Aggregates.ContractEngineAgg;
using UniCloud.Domain.UberModel.Aggregates.CurrencyAgg;
using UniCloud.Domain.UberModel.Aggregates.DocumentAgg;
using UniCloud.Domain.UberModel.Aggregates.DocumentPathAgg;
using UniCloud.Domain.UberModel.Aggregates.EngineAgg;
using UniCloud.Domain.UberModel.Aggregates.EnginePlanAgg;
using UniCloud.Domain.UberModel.Aggregates.EngineTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.ForwarderAgg;
using UniCloud.Domain.UberModel.Aggregates.GuaranteeAgg;
using UniCloud.Domain.UberModel.Aggregates.InvoiceAgg;
using UniCloud.Domain.UberModel.Aggregates.LicenseTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.LinkmanAgg;
using UniCloud.Domain.UberModel.Aggregates.MailAddressAgg;
using UniCloud.Domain.UberModel.Aggregates.MaintainContractAgg;
using UniCloud.Domain.UberModel.Aggregates.MaintainInvoiceAgg;
using UniCloud.Domain.UberModel.Aggregates.ManagerAgg;
using UniCloud.Domain.UberModel.Aggregates.ManufacturerAgg;
using UniCloud.Domain.UberModel.Aggregates.MaterialAgg;
using UniCloud.Domain.UberModel.Aggregates.OrderAgg;
using UniCloud.Domain.UberModel.Aggregates.PartAgg;
using UniCloud.Domain.UberModel.Aggregates.PaymentNoticeAgg;
using UniCloud.Domain.UberModel.Aggregates.PaymentScheduleAgg;
using UniCloud.Domain.UberModel.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.UberModel.Aggregates.PlanEngineAgg;
using UniCloud.Domain.UberModel.Aggregates.ProgrammingAgg;
using UniCloud.Domain.UberModel.Aggregates.ProjectAgg;
using UniCloud.Domain.UberModel.Aggregates.ProjectTempAgg;
using UniCloud.Domain.UberModel.Aggregates.ReceptionAgg;
using UniCloud.Domain.UberModel.Aggregates.RelatedDocAgg;
using UniCloud.Domain.UberModel.Aggregates.RequestAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierCompanyMaterialAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierRoleAgg;
using UniCloud.Domain.UberModel.Aggregates.TaskStandardAgg;
using UniCloud.Domain.UberModel.Aggregates.TradeAgg;
using UniCloud.Domain.UberModel.Aggregates.UserAgg;
using UniCloud.Domain.UberModel.Aggregates.WorkGroupAgg;
using UniCloud.Domain.UberModel.Aggregates.XmlConfigAgg;
using UniCloud.Domain.UberModel.Aggregates.XmlSettingAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork
{
    public class UberModelUnitOfWork : BaseContext<UberModelUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<AircraftSeries> _acTypes;
        private IDbSet<ActionCategory> _actionCategories;
        private IDbSet<AirProgramming> _airProgrammings;
        private IDbSet<AircraftCategory> _aircraftCategories;
        private IDbSet<AircraftType> _aircraftTypes;
        private IDbSet<Aircraft> _aircrafts;
        private IDbSet<Airlines> _airlineses;
        private IDbSet<Annual> _annuals;
        private IDbSet<ApprovalDoc> _approvalDocs;
        private IDbSet<BankAccount> _bankAccounts;
        private IDbSet<CaacProgramming> _caacProgrammings;
        private IDbSet<ContractAircraftBFE> _contractAircraftBfes;
        private IDbSet<ContractAircraft> _contractAircrafts;
        private IDbSet<ContractEngine> _contractEngines;
        private IDbSet<Currency> _currencies;
        private IDbSet<DocumentPath> _documentPaths;
        private IDbSet<Document> _documents;
        private IDbSet<EnginePlan> _enginePlans;
        private IDbSet<EngineType> _engineTypes;
        private IDbSet<Engine> _engines;
        private IDbSet<Forwarder> _forwarders;
        private IDbSet<Guarantee> _guarantees;
        private IDbSet<Invoice> _invoices;
        private IDbSet<Linkman> _linkmen;
        private IDbSet<MailAddress> _mailAddresses;
        private IDbSet<MaintainContract> _maintainContracts;
        private IDbSet<MaintainInvoice> _maintainInvoices;
        private IDbSet<Manager> _managers;
        private IDbSet<Manufacturer> _manufacturers;
        private IDbSet<Material> _materials;
        private IDbSet<Order> _orders;
        private IDbSet<Part> _parts;
        private IDbSet<PaymentNotice> _paymentNotices;
        private IDbSet<PaymentSchedule> _paymentSchedules;
        private IDbSet<PlanAircraft> _planAircrafts;
        private IDbSet<PlanEngine> _planEngines;
        private IDbSet<Plan> _plans;
        private IDbSet<Programming> _programmings;
        private IDbSet<ProjectTemp> _projectTemps;
        private IDbSet<Project> _projects;
        private IDbSet<Reception> _receptions;
        private IDbSet<RelatedDoc> _relatedDocs;
        private IDbSet<Request> _requests;
        private IDbSet<SupplierCompany> _supplierCompanies;
        private IDbSet<SupplierCompanyMaterial> _supplierCompanyMaterials;
        private IDbSet<SupplierRole> _supplierRoles;
        private IDbSet<Supplier> _suppliers;
        private IDbSet<TaskStandard> _taskStandards;
        private IDbSet<Trade> _trades;
        private IDbSet<User> _users;
        private IDbSet<WorkGroup> _workGroups;
        private IDbSet<XmlConfig> _xmlConfigs;
        private IDbSet<XmlSetting> _xmlSettings;
        private IDbSet<AircraftLicense> _aircraftLicenses;
        private IDbSet<LicenseType> _licenseTypes;
        private IDbSet<Ata> _atas;


        public IDbSet<AircraftLicense> AircraftLicenses
        {
            get { return _aircraftLicenses ?? (_aircraftLicenses = base.Set<AircraftLicense>()); }
        }

        public IDbSet<LicenseType> LicenseTypes
        {
            get { return _licenseTypes ?? (_licenseTypes = base.Set<LicenseType>()); }
        }

        public IDbSet<Ata> Atas
        {
            get { return _atas ?? (_atas = base.Set<Ata>()); }
        }

        public IDbSet<ActionCategory> ActionCategories
        {
            get { return _actionCategories ?? (_actionCategories = base.Set<ActionCategory>()); }
        }

        public IDbSet<AircraftSeries> AcTypes
        {
            get { return _acTypes ?? (_acTypes = base.Set<AircraftSeries>()); }
        }

        public IDbSet<AircraftCategory> AircraftCategories
        {
            get { return _aircraftCategories ?? (_aircraftCategories = base.Set<AircraftCategory>()); }
        }

        public IDbSet<Aircraft> Aircrafts
        {
            get { return _aircrafts ?? (_aircrafts = base.Set<Aircraft>()); }
        }

        public IDbSet<AircraftType> AircraftTypes
        {
            get { return _aircraftTypes ?? (_aircraftTypes = base.Set<AircraftType>()); }
        }

        public IDbSet<Airlines> Airlineses
        {
            get { return _airlineses ?? (_airlineses = base.Set<Airlines>()); }
        }

        public IDbSet<AirProgramming> AirProgrammings
        {
            get { return _airProgrammings ?? (_airProgrammings = base.Set<AirProgramming>()); }
        }

        public IDbSet<Annual> Annuals
        {
            get { return _annuals ?? (_annuals = base.Set<Annual>()); }
        }

        public IDbSet<ApprovalDoc> ApprovalDocs
        {
            get { return _approvalDocs ?? (_approvalDocs = base.Set<ApprovalDoc>()); }
        }

        public IDbSet<BankAccount> BankAccounts
        {
            get { return _bankAccounts ?? (_bankAccounts = base.Set<BankAccount>()); }
        }

        public IDbSet<CaacProgramming> CaacProgrammings
        {
            get { return _caacProgrammings ?? (_caacProgrammings = base.Set<CaacProgramming>()); }
        }

        public IDbSet<ContractAircraft> ContractAircrafts
        {
            get { return _contractAircrafts ?? (_contractAircrafts = base.Set<ContractAircraft>()); }
        }

        public IDbSet<ContractAircraftBFE> ContractAircraftBfes
        {
            get { return _contractAircraftBfes ?? (_contractAircraftBfes = base.Set<ContractAircraftBFE>()); }
        }

        public IDbSet<ContractEngine> ContractEngines
        {
            get { return _contractEngines ?? (_contractEngines = base.Set<ContractEngine>()); }
        }

        public IDbSet<Currency> Currencies
        {
            get { return _currencies ?? (_currencies = base.Set<Currency>()); }
        }

        public IDbSet<Document> Documents
        {
            get { return _documents ?? (_documents = base.Set<Document>()); }
        }

        public IDbSet<DocumentPath> DocumentPaths
        {
            get { return _documentPaths ?? (_documentPaths = base.Set<DocumentPath>()); }
        }

        public IDbSet<Engine> Engines
        {
            get { return _engines ?? (_engines = base.Set<Engine>()); }
        }

        public IDbSet<EnginePlan> EnginePlans
        {
            get { return _enginePlans ?? (_enginePlans = base.Set<EnginePlan>()); }
        }

        public IDbSet<EngineType> EngineTypes
        {
            get { return _engineTypes ?? (_engineTypes = base.Set<EngineType>()); }
        }

        public IDbSet<Forwarder> Forwarders
        {
            get { return _forwarders ?? (_forwarders = base.Set<Forwarder>()); }
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

        public IDbSet<MailAddress> MailAddresses
        {
            get { return _mailAddresses ?? (_mailAddresses = base.Set<MailAddress>()); }
        }

        public IDbSet<MaintainContract> MaintainContracts
        {
            get { return _maintainContracts ?? (_maintainContracts = base.Set<MaintainContract>()); }
        }

        public IDbSet<MaintainInvoice> MaintainInvoices
        {
            get { return _maintainInvoices ?? (_maintainInvoices = base.Set<MaintainInvoice>()); }
        }

        public IDbSet<Manager> Managers
        {
            get { return _managers ?? (_managers = base.Set<Manager>()); }
        }

        public IDbSet<Manufacturer> Manufacturers
        {
            get { return _manufacturers ?? (_manufacturers = base.Set<Manufacturer>()); }
        }

        public IDbSet<Material> Materials
        {
            get { return _materials ?? (_materials = base.Set<Material>()); }
        }

        public IDbSet<Order> Orders
        {
            get { return _orders ?? (_orders = base.Set<Order>()); }
        }

        public IDbSet<Part> Parts
        {
            get { return _parts ?? (_parts = base.Set<Part>()); }
        }

        public IDbSet<PaymentNotice> PaymentNotices
        {
            get { return _paymentNotices ?? (_paymentNotices = base.Set<PaymentNotice>()); }
        }

        public IDbSet<PaymentSchedule> PaymentSchedules
        {
            get { return _paymentSchedules ?? (_paymentSchedules = base.Set<PaymentSchedule>()); }
        }

        public IDbSet<Plan> Plans
        {
            get { return _plans ?? (_plans = base.Set<Plan>()); }
        }

        public IDbSet<PlanAircraft> PlanAircrafts
        {
            get { return _planAircrafts ?? (_planAircrafts = base.Set<PlanAircraft>()); }
        }

        public IDbSet<PlanEngine> PlanEngines
        {
            get { return _planEngines ?? (_planEngines = base.Set<PlanEngine>()); }
        }

        public IDbSet<Programming> Programmings
        {
            get { return _programmings ?? (_programmings = base.Set<Programming>()); }
        }

        public IDbSet<Reception> Receptions
        {
            get { return _receptions ?? (_receptions = base.Set<Reception>()); }
        }

        public IDbSet<RelatedDoc> RelatedDocs
        {
            get { return _relatedDocs ?? (_relatedDocs = base.Set<RelatedDoc>()); }
        }

        public IDbSet<Request> Requests
        {
            get { return _requests ?? (_requests = base.Set<Request>()); }
        }

        public IDbSet<Supplier> Suppliers
        {
            get { return _suppliers ?? (_suppliers = base.Set<Supplier>()); }
        }

        public IDbSet<SupplierCompany> SupplierCompanies
        {
            get { return _supplierCompanies ?? (_supplierCompanies = base.Set<SupplierCompany>()); }
        }

        public IDbSet<SupplierCompanyMaterial> SupplierCompanyMaterials
        {
            get
            {
                return _supplierCompanyMaterials ?? (_supplierCompanyMaterials = base.Set<SupplierCompanyMaterial>());
            }
        }

        public IDbSet<SupplierRole> SupplierRoles
        {
            get { return _supplierRoles ?? (_supplierRoles = base.Set<SupplierRole>()); }
        }

        public IDbSet<Trade> Trades
        {
            get { return _trades ?? (_trades = base.Set<Trade>()); }
        }

        public IDbSet<XmlConfig> XmlConfigs
        {
            get { return _xmlConfigs ?? (_xmlConfigs = base.Set<XmlConfig>()); }
        }

        public IDbSet<XmlSetting> XmlSettings
        {
            get { return _xmlSettings ?? (_xmlSettings = base.Set<XmlSetting>()); }
        }

        public IDbSet<Project> Projects
        {
            get { return _projects ?? (_projects = base.Set<Project>()); }
        }

        public IDbSet<ProjectTemp> ProjectTemps
        {
            get { return _projectTemps ?? (_projectTemps = base.Set<ProjectTemp>()); }
        }

        public IDbSet<TaskStandard> TaskStandards
        {
            get { return _taskStandards ?? (_taskStandards = base.Set<TaskStandard>()); }
        }

        public IDbSet<User> Users
        {
            get { return _users ?? (_users = base.Set<User>()); }
        }

        public IDbSet<WorkGroup> WorkGroups
        {
            get { return _workGroups ?? (_workGroups = base.Set<WorkGroup>()); }
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

            #region AcTypeAgg

.Add(new AircraftSeriesEntityConfiguration())

            #endregion

            #region AircraftCategoryAgg

.Add(new AircraftCategoryEntityConfiguration())

            #endregion

            #region AircraftAgg

.Add(new AircraftEntityConfiguration())
                .Add(new AircraftBusinessEntityConfiguration())
                .Add(new OperationHistoryEntityConfiguration())
                .Add(new OwnershipHistoryEntityConfiguration())

            #endregion

            #region AircraftPlanAgg

.Add(new PlanEntityConfiguration())
                .Add(new PlanHistoryEntityConfiguration())
                .Add(new ChangePlanEntityConfiguration())
                .Add(new OperationPlanEntityConfiguration())

            #endregion

            #region AircraftTypeAgg

.Add(new AircraftTypeEntityConfiguration())

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

            #endregion

            #region ApprovalDocAgg

.Add(new ApprovalDocEntityConfiguration())

            #endregion

            #region BankAccountAgg

.Add(new BankAccountEntityConfiguration())

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

            #region CurrencyAgg

.Add(new CurrencyEntityConfiguration())

            #endregion

            #region DocumentAgg

.Add(new DocumentEntityConfiguration())
                .Add(new OfficialDocumentEntityConfiguration())
                .Add(new StandardDocumentEntityConfiguration())

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
                .Add(new CreditNoteInvoiceEntityConfiguration())
                .Add(new LeaseInvoiceEntityConfiguration())
                .Add(new PurchaseInvoiceEntityConfiguration())
                .Add(new PrepaymentInvoiceEntityConfiguration())

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

            #endregion

            #region MaintainInvoiceAgg

.Add(new MaintainInvoiceEntityConfiguration())
                .Add(new MaintainInvoiceLineEntityConfiguration())
                .Add(new AirframeMaintainInvoiceEntityConfiguration())
                .Add(new APUMaintainInvoiceEntityConfiguration())
                .Add(new EngineMaintainInvoiceEntityConfiguration())
                .Add(new UndercartMaintainInvoiceEntityConfiguration())

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

            #endregion

            #region PlanAircraftAgg

.Add(new PlanAircraftEntityConfiguration())

            #endregion

            #region PlanEngineAgg

.Add(new PlanEngineEntityConfiguration())

            #endregion

            #region ProgrammingAgg

.Add(new ProgrammingEntityConfiguration())

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

            #endregion

            #region TradeAgg

.Add(new TradeEntityConfiguration())

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

            #region UserAgg

.Add(new UserEntityConfiguration())

            #endregion

            #region WorkGroupAgg

.Add(new WorkGroupEntityConfiguration())
                .Add(new MemberEntityConfiguration())

            #endregion

            #region AircraftLicense
.Add(new AircraftLicenseEntityConfiguration())
                .Add(new LicenseTypeEntityConfiguration())
            #endregion

.Add(new AtaEntityConfiguration())
.Add(new AddressConfiguration());
        }

        #endregion
    }
}