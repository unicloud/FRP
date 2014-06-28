#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/04，13:11
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Data.Entity;
using UniCloud.Domain.PurchaseBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftPlanHistoryAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.AnnualAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.BankAccountAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftBFEAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractEngineAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.DocumentAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.DocumentPathAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ForwarderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ManufacturerAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.PartAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyMaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierRoleAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork
{
    public class PurchaseBCUnitOfWork : UniContext<PurchaseBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<ActionCategory> _actionCategories;
        private IDbSet<AircraftType> _aircraftTypes;
        private IDbSet<Aircraft> _aircrafts;
        private IDbSet<Annual> _annuals;
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
        private IDbSet<PlanHistory> _planHistories;
        private IDbSet<Reception> _receptions;
        private IDbSet<RelatedDoc> _relatedDocs;
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

        public IDbSet<Annual> Annuals
        {
            get { return _annuals ?? (_annuals = base.Set<Annual>()); }
        }

        public IDbSet<BankAccount> BankAccounts
        {
            get { return _bankAccounts ?? (_bankAccounts = base.Set<BankAccount>()); }
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

        public IDbSet<MaintainContract> MaintainContracts
        {
            get { return _maintainContracts ?? (_maintainContracts = base.Set<MaintainContract>()); }
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

        public IDbSet<PlanHistory> PlanHistories
        {
            get { return _planHistories ?? (_planHistories = base.Set<PlanHistory>()); }
        }

        public IDbSet<Reception> Receptions
        {
            get { return _receptions ?? (_receptions = base.Set<Reception>()); }
        }

        public IDbSet<RelatedDoc> RelatedDocs
        {
            get { return _relatedDocs ?? (_relatedDocs = base.Set<RelatedDoc>()); }
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
    }
}