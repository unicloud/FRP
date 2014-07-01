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
using UniCloud.Domain.UberModel.Aggregates.AcDailyUtilizationAgg;
using UniCloud.Domain.UberModel.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.UberModel.Aggregates.AdSbAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftCabinTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftConfigurationAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftLicenseAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.AirlinesAgg;
using UniCloud.Domain.UberModel.Aggregates.AirProgrammingAgg;
using UniCloud.Domain.UberModel.Aggregates.AirStructureDamageAgg;
using UniCloud.Domain.UberModel.Aggregates.AnnualAgg;
using UniCloud.Domain.UberModel.Aggregates.AnnualMaintainPlanAgg;
using UniCloud.Domain.UberModel.Aggregates.ApprovalDocAgg;
using UniCloud.Domain.UberModel.Aggregates.AtaAgg;
using UniCloud.Domain.UberModel.Aggregates.BankAccountAgg;
using UniCloud.Domain.UberModel.Aggregates.BasicConfigAgg;
using UniCloud.Domain.UberModel.Aggregates.BasicConfigGroupAgg;
using UniCloud.Domain.UberModel.Aggregates.BasicConfigHistoryAgg;
using UniCloud.Domain.UberModel.Aggregates.BusinessLicenseAgg;
using UniCloud.Domain.UberModel.Aggregates.CAACAircraftTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.CaacProgrammingAgg;
using UniCloud.Domain.UberModel.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.UberModel.Aggregates.ContractAircraftBFEAgg;
using UniCloud.Domain.UberModel.Aggregates.ContractEngineAgg;
using UniCloud.Domain.UberModel.Aggregates.CtrlUnitAgg;
using UniCloud.Domain.UberModel.Aggregates.CurrencyAgg;
using UniCloud.Domain.UberModel.Aggregates.DocumentAgg;
using UniCloud.Domain.UberModel.Aggregates.DocumentPathAgg;
using UniCloud.Domain.UberModel.Aggregates.DocumentTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.EngineAgg;
using UniCloud.Domain.UberModel.Aggregates.EnginePlanAgg;
using UniCloud.Domain.UberModel.Aggregates.EngineTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.FlightLogAgg;
using UniCloud.Domain.UberModel.Aggregates.ForwarderAgg;
using UniCloud.Domain.UberModel.Aggregates.FunctionItemAgg;
using UniCloud.Domain.UberModel.Aggregates.GuaranteeAgg;
using UniCloud.Domain.UberModel.Aggregates.InstallControllerAgg;
using UniCloud.Domain.UberModel.Aggregates.InvoiceAgg;
using UniCloud.Domain.UberModel.Aggregates.IssuedUnitAgg;
using UniCloud.Domain.UberModel.Aggregates.ItemAgg;
using UniCloud.Domain.UberModel.Aggregates.LicenseTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.LinkmanAgg;
using UniCloud.Domain.UberModel.Aggregates.MailAddressAgg;
using UniCloud.Domain.UberModel.Aggregates.MaintainContractAgg;
using UniCloud.Domain.UberModel.Aggregates.MaintainCostAgg;
using UniCloud.Domain.UberModel.Aggregates.MaintainCtrlAgg;
using UniCloud.Domain.UberModel.Aggregates.MaintainInvoiceAgg;
using UniCloud.Domain.UberModel.Aggregates.MaintainWorkAgg;
using UniCloud.Domain.UberModel.Aggregates.ManagerAgg;
using UniCloud.Domain.UberModel.Aggregates.ManufacturerAgg;
using UniCloud.Domain.UberModel.Aggregates.MaterialAgg;
using UniCloud.Domain.UberModel.Aggregates.ModAgg;
using UniCloud.Domain.UberModel.Aggregates.OilMonitorAgg;
using UniCloud.Domain.UberModel.Aggregates.OrderAgg;
using UniCloud.Domain.UberModel.Aggregates.OrganizationAgg;
using UniCloud.Domain.UberModel.Aggregates.OrganizationRoleAgg;
using UniCloud.Domain.UberModel.Aggregates.OrganizationUserAgg;
using UniCloud.Domain.UberModel.Aggregates.PaymentNoticeAgg;
using UniCloud.Domain.UberModel.Aggregates.PaymentScheduleAgg;
using UniCloud.Domain.UberModel.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.UberModel.Aggregates.PlanEngineAgg;
using UniCloud.Domain.UberModel.Aggregates.PnRegAgg;
using UniCloud.Domain.UberModel.Aggregates.ProgrammingAgg;
using UniCloud.Domain.UberModel.Aggregates.ProgrammingFileAgg;
using UniCloud.Domain.UberModel.Aggregates.ProjectAgg;
using UniCloud.Domain.UberModel.Aggregates.ProjectTempAgg;
using UniCloud.Domain.UberModel.Aggregates.ReceptionAgg;
using UniCloud.Domain.UberModel.Aggregates.RelatedDocAgg;
using UniCloud.Domain.UberModel.Aggregates.RequestAgg;
using UniCloud.Domain.UberModel.Aggregates.RoleAgg;
using UniCloud.Domain.UberModel.Aggregates.RoleFunctionAgg;
using UniCloud.Domain.UberModel.Aggregates.ScnAgg;
using UniCloud.Domain.UberModel.Aggregates.SnHistoryAgg;
using UniCloud.Domain.UberModel.Aggregates.SnRegAgg;
using UniCloud.Domain.UberModel.Aggregates.SnRemInstRecordAgg;
using UniCloud.Domain.UberModel.Aggregates.SpecialConfigAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierCompanyMaterialAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierRoleAgg;
using UniCloud.Domain.UberModel.Aggregates.TaskStandardAgg;
using UniCloud.Domain.UberModel.Aggregates.ThresholdAgg;
using UniCloud.Domain.UberModel.Aggregates.ThrustAgg;
using UniCloud.Domain.UberModel.Aggregates.TradeAgg;
using UniCloud.Domain.UberModel.Aggregates.UserAgg;
using UniCloud.Domain.UberModel.Aggregates.UserRoleAgg;
using UniCloud.Domain.UberModel.Aggregates.WorkGroupAgg;
using UniCloud.Domain.UberModel.Aggregates.XmlConfigAgg;
using UniCloud.Domain.UberModel.Aggregates.XmlSettingAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork
{
    public class UberModelUnitOfWork : UniContext<UberModelUnitOfWork>
    {
        public UberModelUnitOfWork()
        {
            //自动检测功能
            Configuration.AutoDetectChangesEnabled = true;
            //延迟加载功能
            Configuration.LazyLoadingEnabled = true;
        }

        #region IDbSet成员

        private IDbSet<AcDailyUtilization> _acDailyUtilizations;
        private IDbSet<ActionCategory> _actionCategories;
        private IDbSet<AdSb> _adSbs;
        private IDbSet<AirBusScn> _airBusScns;
        private IDbSet<AirProgramming> _airProgrammings;
        private IDbSet<AirStructureDamage> _airStructureDamages;
        private IDbSet<AircraftCabinType> _aircraftCabinTypes;
        private IDbSet<AircraftCabin> _aircraftCabins;
        private IDbSet<AircraftCategory> _aircraftCategories;
        private IDbSet<AircraftConfiguration> _aircraftConfigurations;
        private IDbSet<AircraftLicense> _aircraftLicenses;
        private IDbSet<AircraftMaintainPlan> _aircraftMaintainPlans;
        private IDbSet<AircraftSeries> _aircraftSeries;
        private IDbSet<AircraftType> _aircraftTypes;
        private IDbSet<Aircraft> _aircrafts;
        private IDbSet<Airlines> _airlineses;
        private IDbSet<Annual> _annuals;
        private IDbSet<ApprovalDoc> _approvalDocs;
        private IDbSet<Ata> _atas;
        private IDbSet<BankAccount> _bankAccounts;
        private IDbSet<BasePurchaseInvoice> _basePurchaseInvoices;
        private IDbSet<BasicConfigGroup> _basicConfigGroups;
        private IDbSet<BasicConfigHistory> _basicConfigHistories;
        private IDbSet<BasicConfig> _basicConfigs;
        private IDbSet<BusinessLicense> _businessLicenses;
        private IDbSet<CAACAircraftType> _caacAircraftTypes;
        private IDbSet<CaacProgramming> _caacProgrammings;
        private IDbSet<ContractAircraftBFE> _contractAircraftBfes;
        private IDbSet<ContractAircraft> _contractAircrafts;
        private IDbSet<ContractEngine> _contractEngines;
        private IDbSet<CtrlUnit> _ctrlUnits;
        private IDbSet<Currency> _currencies;
        private IDbSet<DocumentPath> _documentPaths;
        private IDbSet<DocumentType> _documentTypes;
        private IDbSet<Document> _documents;
        private IDbSet<EngineMaintainPlan> _engineMaintainPlans;
        private IDbSet<EnginePlan> _enginePlans;
        private IDbSet<EngineType> _engineTypes;
        private IDbSet<Engine> _engines;
        private IDbSet<FlightLog> _flightLogs;
        private IDbSet<Forwarder> _forwarders;
        private IDbSet<FunctionItem> _functionItems;
        private IDbSet<Guarantee> _guarantees;
        private IDbSet<InstallController> _installControllers;
        private IDbSet<Invoice> _invoices;
        private IDbSet<IssuedUnit> _issuedUnits;
        private IDbSet<Item> _items;
        private IDbSet<LicenseType> _licenseTypes;
        private IDbSet<Linkman> _linkmen;
        private IDbSet<MailAddress> _mailAddresses;
        private IDbSet<MaintainContract> _maintainContracts;
        private IDbSet<MaintainCost> _maintainCosts;
        private IDbSet<MaintainCtrl> _maintainCtrls;
        private IDbSet<MaintainInvoice> _maintainInvoices;
        private IDbSet<MaintainWork> _maintainWorks;
        private IDbSet<Manager> _managers;
        private IDbSet<Manufacturer> _manufacturers;
        private IDbSet<Material> _materials;
        private IDbSet<Mod> _mods;
        private IDbSet<OilMonitor> _oilMonitors;
        private IDbSet<Order> _orders;
        private IDbSet<OrganizationRole> _organizationRoles;
        private IDbSet<OrganizationUser> _organizationUsers;
        private IDbSet<Organization> _organizations;
        private IDbSet<PaymentNotice> _paymentNotices;
        private IDbSet<PaymentSchedule> _paymentSchedules;
        private IDbSet<PlanAircraft> _planAircrafts;
        private IDbSet<PlanEngine> _planEngines;
        private IDbSet<Plan> _plans;
        private IDbSet<PnReg> _pneRegs;
        private IDbSet<ProgrammingFile> _programmingFiles;
        private IDbSet<Programming> _programmings;
        private IDbSet<ProjectTemp> _projectTemps;
        private IDbSet<Project> _projects;
        private IDbSet<Reception> _receptions;
        private IDbSet<RelatedDoc> _relatedDocs;
        private IDbSet<Request> _requests;
        private IDbSet<RoleFunction> _roleFunctions;
        private IDbSet<Role> _roles;
        private IDbSet<Scn> _scns;
        private IDbSet<SnHistory> _snHistories;
        private IDbSet<SnReg> _snRegs;
        private IDbSet<SnRemInstRecord> _snRemInstRecords;
        private IDbSet<SpecialConfig> _specialConfigs;
        private IDbSet<SupplierCompany> _supplierCompanies;
        private IDbSet<SupplierCompanyMaterial> _supplierCompanyMaterials;
        private IDbSet<SupplierRole> _supplierRoles;
        private IDbSet<Supplier> _suppliers;
        private IDbSet<TaskStandard> _taskStandards;
        private IDbSet<Threshold> _thresholds;
        private IDbSet<Thrust> _thrusts;
        private IDbSet<Trade> _trades;
        private IDbSet<UserRole> _userRoles;
        private IDbSet<User> _users;
        private IDbSet<WorkGroup> _workGroups;
        private IDbSet<XmlConfig> _xmlConfigs;
        private IDbSet<XmlSetting> _xmlSettings;


        public IDbSet<MaintainCost> MaintainCosts
        {
            get { return _maintainCosts ?? (_maintainCosts = Set<MaintainCost>()); }
        }

        public IDbSet<AircraftMaintainPlan> AircraftMaintainPlans
        {
            get { return _aircraftMaintainPlans ?? (_aircraftMaintainPlans = Set<AircraftMaintainPlan>()); }
        }

        public IDbSet<EngineMaintainPlan> EngineMaintainPlans
        {
            get { return _engineMaintainPlans ?? (_engineMaintainPlans = Set<EngineMaintainPlan>()); }
        }

        public IDbSet<AcDailyUtilization> AcDailyUtilizations
        {
            get { return _acDailyUtilizations ?? (_acDailyUtilizations = Set<AcDailyUtilization>()); }
        }

        public IDbSet<AircraftLicense> AircraftLicenses
        {
            get { return _aircraftLicenses ?? (_aircraftLicenses = Set<AircraftLicense>()); }
        }

        public IDbSet<LicenseType> LicenseTypes
        {
            get { return _licenseTypes ?? (_licenseTypes = Set<LicenseType>()); }
        }

        public IDbSet<Ata> Atas
        {
            get { return _atas ?? (_atas = Set<Ata>()); }
        }

        public IDbSet<ActionCategory> ActionCategories
        {
            get { return _actionCategories ?? (_actionCategories = Set<ActionCategory>()); }
        }

        public IDbSet<AircraftSeries> AircraftSeries
        {
            get { return _aircraftSeries ?? (_aircraftSeries = Set<AircraftSeries>()); }
        }

        public IDbSet<AircraftCategory> AircraftCategories
        {
            get { return _aircraftCategories ?? (_aircraftCategories = Set<AircraftCategory>()); }
        }

        public IDbSet<Aircraft> Aircrafts
        {
            get { return _aircrafts ?? (_aircrafts = Set<Aircraft>()); }
        }

        public IDbSet<AircraftConfiguration> AircraftConfigurations
        {
            get { return _aircraftConfigurations ?? (_aircraftConfigurations = Set<AircraftConfiguration>()); }
        }

        public IDbSet<AircraftCabinType> AircraftCabinTypes
        {
            get { return _aircraftCabinTypes ?? (_aircraftCabinTypes = Set<AircraftCabinType>()); }
        }

        public IDbSet<AircraftCabin> AircraftCabins
        {
            get { return _aircraftCabins ?? (_aircraftCabins = Set<AircraftCabin>()); }
        }

        public IDbSet<AircraftType> AircraftTypes
        {
            get { return _aircraftTypes ?? (_aircraftTypes = Set<AircraftType>()); }
        }

        public IDbSet<CAACAircraftType> CaacAircraftTypes
        {
            get { return _caacAircraftTypes ?? (_caacAircraftTypes = Set<CAACAircraftType>()); }
        }

        public IDbSet<Airlines> Airlineses
        {
            get { return _airlineses ?? (_airlineses = Set<Airlines>()); }
        }

        public IDbSet<AirProgramming> AirProgrammings
        {
            get { return _airProgrammings ?? (_airProgrammings = Set<AirProgramming>()); }
        }

        public IDbSet<Annual> Annuals
        {
            get { return _annuals ?? (_annuals = Set<Annual>()); }
        }

        public IDbSet<ApprovalDoc> ApprovalDocs
        {
            get { return _approvalDocs ?? (_approvalDocs = Set<ApprovalDoc>()); }
        }

        public IDbSet<AirStructureDamage> AirStructureDamages
        {
            get { return _airStructureDamages ?? (_airStructureDamages = Set<AirStructureDamage>()); }
        }

        public IDbSet<BankAccount> BankAccounts
        {
            get { return _bankAccounts ?? (_bankAccounts = Set<BankAccount>()); }
        }

        public IDbSet<BasicConfig> BasicConfigs
        {
            get { return _basicConfigs ?? (_basicConfigs = Set<BasicConfig>()); }
        }

        public IDbSet<BasicConfigGroup> BasicConfigGroups
        {
            get { return _basicConfigGroups ?? (_basicConfigGroups = Set<BasicConfigGroup>()); }
        }

        public IDbSet<BasicConfigHistory> BasicConfigHistories
        {
            get { return _basicConfigHistories ?? (_basicConfigHistories = Set<BasicConfigHistory>()); }
        }

        public IDbSet<CaacProgramming> CaacProgrammings
        {
            get { return _caacProgrammings ?? (_caacProgrammings = Set<CaacProgramming>()); }
        }

        public IDbSet<ContractAircraft> ContractAircrafts
        {
            get { return _contractAircrafts ?? (_contractAircrafts = Set<ContractAircraft>()); }
        }

        public IDbSet<ContractAircraftBFE> ContractAircraftBfes
        {
            get { return _contractAircraftBfes ?? (_contractAircraftBfes = Set<ContractAircraftBFE>()); }
        }

        public IDbSet<ContractEngine> ContractEngines
        {
            get { return _contractEngines ?? (_contractEngines = Set<ContractEngine>()); }
        }

        public IDbSet<CtrlUnit> CtrlUnits
        {
            get { return _ctrlUnits ?? (_ctrlUnits = Set<CtrlUnit>()); }
        }

        public IDbSet<Currency> Currencies
        {
            get { return _currencies ?? (_currencies = Set<Currency>()); }
        }

        public IDbSet<Document> Documents
        {
            get { return _documents ?? (_documents = Set<Document>()); }
        }

        public IDbSet<DocumentPath> DocumentPaths
        {
            get { return _documentPaths ?? (_documentPaths = Set<DocumentPath>()); }
        }

        public IDbSet<Engine> Engines
        {
            get { return _engines ?? (_engines = Set<Engine>()); }
        }

        public IDbSet<EnginePlan> EnginePlans
        {
            get { return _enginePlans ?? (_enginePlans = Set<EnginePlan>()); }
        }

        public IDbSet<EngineType> EngineTypes
        {
            get { return _engineTypes ?? (_engineTypes = Set<EngineType>()); }
        }

        public IDbSet<FlightLog> FlightLogs
        {
            get { return _flightLogs ?? (_flightLogs = Set<FlightLog>()); }
        }

        public IDbSet<Forwarder> Forwarders
        {
            get { return _forwarders ?? (_forwarders = Set<Forwarder>()); }
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

        public IDbSet<Item> Items
        {
            get { return _items ?? (_items = Set<Item>()); }
        }

        public IDbSet<InstallController> InstallControllers
        {
            get { return _installControllers ?? (_installControllers = Set<InstallController>()); }
        }

        public IDbSet<IssuedUnit> IssuedUnits
        {
            get { return _issuedUnits ?? (_issuedUnits = Set<IssuedUnit>()); }
        }

        public IDbSet<Linkman> Linkmen
        {
            get { return _linkmen ?? (_linkmen = Set<Linkman>()); }
        }

        public IDbSet<MailAddress> MailAddresses
        {
            get { return _mailAddresses ?? (_mailAddresses = Set<MailAddress>()); }
        }

        public IDbSet<MaintainContract> MaintainContracts
        {
            get { return _maintainContracts ?? (_maintainContracts = Set<MaintainContract>()); }
        }

        public IDbSet<MaintainCtrl> MaintainCtrls
        {
            get { return _maintainCtrls ?? (_maintainCtrls = Set<MaintainCtrl>()); }
        }

        public IDbSet<MaintainInvoice> MaintainInvoices
        {
            get { return _maintainInvoices ?? (_maintainInvoices = Set<MaintainInvoice>()); }
        }

        public IDbSet<MaintainWork> MaintainWorks
        {
            get { return _maintainWorks ?? (_maintainWorks = Set<MaintainWork>()); }
        }

        public IDbSet<Manager> Managers
        {
            get { return _managers ?? (_managers = Set<Manager>()); }
        }

        public IDbSet<Manufacturer> Manufacturers
        {
            get { return _manufacturers ?? (_manufacturers = Set<Manufacturer>()); }
        }

        public IDbSet<Material> Materials
        {
            get { return _materials ?? (_materials = Set<Material>()); }
        }

        public IDbSet<Mod> Mods
        {
            get { return _mods ?? (_mods = Set<Mod>()); }
        }

        public IDbSet<OilMonitor> OilMonitors
        {
            get { return _oilMonitors ?? (_oilMonitors = Set<OilMonitor>()); }
        }

        public IDbSet<Order> Orders
        {
            get { return _orders ?? (_orders = Set<Order>()); }
        }

        public IDbSet<PaymentNotice> PaymentNotices
        {
            get { return _paymentNotices ?? (_paymentNotices = Set<PaymentNotice>()); }
        }

        public IDbSet<PaymentSchedule> PaymentSchedules
        {
            get { return _paymentSchedules ?? (_paymentSchedules = Set<PaymentSchedule>()); }
        }

        public IDbSet<Plan> Plans
        {
            get { return _plans ?? (_plans = Set<Plan>()); }
        }

        public IDbSet<PlanAircraft> PlanAircrafts
        {
            get { return _planAircrafts ?? (_planAircrafts = Set<PlanAircraft>()); }
        }

        public IDbSet<PlanEngine> PlanEngines
        {
            get { return _planEngines ?? (_planEngines = Set<PlanEngine>()); }
        }

        public IDbSet<PnReg> PnRegs
        {
            get { return _pneRegs ?? (_pneRegs = Set<PnReg>()); }
        }

        public IDbSet<Programming> Programmings
        {
            get { return _programmings ?? (_programmings = Set<Programming>()); }
        }

        public IDbSet<ProgrammingFile> ProgrammingFiles
        {
            get { return _programmingFiles ?? (_programmingFiles = Set<ProgrammingFile>()); }
        }

        public IDbSet<Reception> Receptions
        {
            get { return _receptions ?? (_receptions = Set<Reception>()); }
        }

        public IDbSet<RelatedDoc> RelatedDocs
        {
            get { return _relatedDocs ?? (_relatedDocs = Set<RelatedDoc>()); }
        }

        public IDbSet<Request> Requests
        {
            get { return _requests ?? (_requests = Set<Request>()); }
        }

        public IDbSet<Scn> Scns
        {
            get { return _scns ?? (_scns = Set<Scn>()); }
        }

        public IDbSet<AirBusScn> AirBusScns
        {
            get { return _airBusScns ?? (_airBusScns = Set<AirBusScn>()); }
        }

        public IDbSet<AdSb> AdSbs
        {
            get { return _adSbs ?? (_adSbs = Set<AdSb>()); }
        }

        public IDbSet<SnReg> SnRegs
        {
            get { return _snRegs ?? (_snRegs = Set<SnReg>()); }
        }

        public IDbSet<SnHistory> SnHistories
        {
            get { return _snHistories ?? (_snHistories = Set<SnHistory>()); }
        }

        public IDbSet<SnRemInstRecord> SnRemInstRecords
        {
            get { return _snRemInstRecords ?? (_snRemInstRecords = Set<SnRemInstRecord>()); }
        }


        public IDbSet<SpecialConfig> SpecialConfigs
        {
            get { return _specialConfigs ?? (_specialConfigs = Set<SpecialConfig>()); }
        }

        public IDbSet<Supplier> Suppliers
        {
            get { return _suppliers ?? (_suppliers = Set<Supplier>()); }
        }

        public IDbSet<SupplierCompany> SupplierCompanies
        {
            get { return _supplierCompanies ?? (_supplierCompanies = Set<SupplierCompany>()); }
        }

        public IDbSet<SupplierCompanyMaterial> SupplierCompanyMaterials
        {
            get { return _supplierCompanyMaterials ?? (_supplierCompanyMaterials = Set<SupplierCompanyMaterial>()); }
        }

        public IDbSet<SupplierRole> SupplierRoles
        {
            get { return _supplierRoles ?? (_supplierRoles = Set<SupplierRole>()); }
        }

        public IDbSet<Trade> Trades
        {
            get { return _trades ?? (_trades = Set<Trade>()); }
        }

        public IDbSet<XmlConfig> XmlConfigs
        {
            get { return _xmlConfigs ?? (_xmlConfigs = Set<XmlConfig>()); }
        }

        public IDbSet<XmlSetting> XmlSettings
        {
            get { return _xmlSettings ?? (_xmlSettings = Set<XmlSetting>()); }
        }

        public IDbSet<Project> Projects
        {
            get { return _projects ?? (_projects = Set<Project>()); }
        }

        public IDbSet<ProjectTemp> ProjectTemps
        {
            get { return _projectTemps ?? (_projectTemps = Set<ProjectTemp>()); }
        }

        public IDbSet<TaskStandard> TaskStandards
        {
            get { return _taskStandards ?? (_taskStandards = Set<TaskStandard>()); }
        }

        public IDbSet<Threshold> Thresholds
        {
            get { return _thresholds ?? (_thresholds = Set<Threshold>()); }
        }

        public IDbSet<Thrust> Thrusts
        {
            get { return _thrusts ?? (_thrusts = Set<Thrust>()); }
        }

        public IDbSet<User> Users
        {
            get { return _users ?? (_users = Set<User>()); }
        }

        public IDbSet<WorkGroup> WorkGroups
        {
            get { return _workGroups ?? (_workGroups = Set<WorkGroup>()); }
        }

        public IDbSet<DocumentType> DocumentTypes
        {
            get { return _documentTypes ?? (_documentTypes = Set<DocumentType>()); }
        }

        public IDbSet<FunctionItem> FunctionItems
        {
            get { return _functionItems ?? (_functionItems = Set<FunctionItem>()); }
        }

        public IDbSet<Organization> Organizations
        {
            get { return _organizations ?? (_organizations = Set<Organization>()); }
        }

        public IDbSet<OrganizationRole> OrganizationRoles
        {
            get { return _organizationRoles ?? (_organizationRoles = Set<OrganizationRole>()); }
        }

        public IDbSet<OrganizationUser> OrganizationUsers
        {
            get { return _organizationUsers ?? (_organizationUsers = Set<OrganizationUser>()); }
        }

        public IDbSet<Role> Roles
        {
            get { return _roles ?? (_roles = Set<Role>()); }
        }

        public IDbSet<RoleFunction> RoleFunctions
        {
            get { return _roleFunctions ?? (_roleFunctions = Set<RoleFunction>()); }
        }

        public IDbSet<UserRole> UserRoles
        {
            get { return _userRoles ?? (_userRoles = Set<UserRole>()); }
        }

        public IDbSet<BusinessLicense> BusinessLicenses
        {
            get { return _businessLicenses ?? (_businessLicenses = Set<BusinessLicense>()); }
        }

        #endregion
    }
}