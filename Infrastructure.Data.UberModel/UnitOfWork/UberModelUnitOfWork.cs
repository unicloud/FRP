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
using UniCloud.Domain.UberModel.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.BankAccountAgg;
using UniCloud.Domain.UberModel.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.UberModel.Aggregates.ContractAircraftBFEAgg;
using UniCloud.Domain.UberModel.Aggregates.ContractEngineAgg;
using UniCloud.Domain.UberModel.Aggregates.CurrencyAgg;
using UniCloud.Domain.UberModel.Aggregates.DocumentAgg;
using UniCloud.Domain.UberModel.Aggregates.DocumentPathAgg;
using UniCloud.Domain.UberModel.Aggregates.ForwarderAgg;
using UniCloud.Domain.UberModel.Aggregates.LinkmanAgg;
using UniCloud.Domain.UberModel.Aggregates.MaintainContractAgg;
using UniCloud.Domain.UberModel.Aggregates.ManufacturerAgg;
using UniCloud.Domain.UberModel.Aggregates.MaterialAgg;
using UniCloud.Domain.UberModel.Aggregates.OrderAgg;
using UniCloud.Domain.UberModel.Aggregates.PartAgg;
using UniCloud.Domain.UberModel.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.UberModel.Aggregates.ReceptionAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierCompanyMaterialAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierRoleAgg;
using UniCloud.Domain.UberModel.Aggregates.TradeAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork
{
    public class UberModelUnitOfWork : BaseContext<UberModelUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<ActionCategory> _actionCategories;
        private IDbSet<AircraftType> _aircraftTypes;
        private IDbSet<Aircraft> _aircrafts;
        private IDbSet<BankAccount> _bankAccounts;
        private IDbSet<ContractAircraftBFE> _contractAircraftBfes;
        private IDbSet<ContractAircraft> _contractAircrafts;
        private IDbSet<ContractEngine> _contractEngines;
        private IDbSet<Currency> _currencies;
        private IDbSet<DocumentPath> _documentPaths;
        private IDbSet<Document> _documents;
        private IDbSet<Forwarder> _forwarders;
        private IDbSet<Linkman> _linkmen;
        private IDbSet<MaintainContract> _maintainContracts;
        private IDbSet<Manufacturer> _manufacturers;
        private IDbSet<Material> _materials;
        private IDbSet<Order> _orders;
        private IDbSet<Part> _parts;
        private IDbSet<PlanAircraft> _planAircrafts;
        private IDbSet<Reception> _receptions;
        private IDbSet<SupplierCompany> _supplierCompanies;
        private IDbSet<SupplierCompanyMaterial> _supplierCompanyMaterials;
        private IDbSet<SupplierRole> _supplierRoles;
        private IDbSet<Supplier> _suppliers;
        private IDbSet<Trade> _trades;

        public IDbSet<ActionCategory> ActionCategories
        {
            get { return _actionCategories ?? (_actionCategories = base.Set<ActionCategory>()); }
        }

        public IDbSet<Aircraft> Aircrafts
        {
            get { return _aircrafts ?? (_aircrafts = base.Set<Aircraft>()); }
        }

        public IDbSet<AircraftType> AircraftTypes
        {
            get { return _aircraftTypes ?? (_aircraftTypes = base.Set<AircraftType>()); }
        }

        public IDbSet<BankAccount> BankAccounts
        {
            get { return _bankAccounts ?? (_bankAccounts = base.Set<BankAccount>()); }
        }

        public IDbSet<MaintainContract> MaintainContracts
        {
            get { return _maintainContracts ?? (_maintainContracts = base.Set<MaintainContract>()); }
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

        public IDbSet<Forwarder> Forwarders
        {
            get { return _forwarders ?? (_forwarders = base.Set<Forwarder>()); }
        }

        public IDbSet<Linkman> Linkmen
        {
            get { return _linkmen ?? (_linkmen = base.Set<Linkman>()); }
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

        public IDbSet<PlanAircraft> PlanAircrafts
        {
            get { return _planAircrafts ?? (_planAircrafts = base.Set<PlanAircraft>()); }
        }

        public IDbSet<Reception> Receptions
        {
            get { return _receptions ?? (_receptions = base.Set<Reception>()); }
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

                #region AircraftAgg

                .Add(new AircraftEntityConfiguration())

                #endregion

                #region AircraftTypeAgg

                .Add(new AircraftTypeEntityConfiguration())

                #endregion

                #region BankAccountAgg

                .Add(new BankAccountEntityConfiguration())

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

                #endregion

                #region DocumentPathAgg

                .Add(new DocumentPathEntityConfiguration())

                #endregion

                #region ForwarderAgg

                .Add(new ForwarderEntityConfiguration())

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

                #region PlanAircraftAgg

                .Add(new PlanAircraftEntityConfiguration())

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

                .Add(new AddressConfiguration());
        }

        #endregion
    }
}