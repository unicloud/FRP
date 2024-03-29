﻿#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:21:43
// 文件名：FleetPlanBCUnitOfWork
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Data.Entity;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftConfigurationAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.CAACAircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.CaacProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.DocumentAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.IssuedUnitAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.MailAddressAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManagerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManufacturerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanEngineAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingFileAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.RelatedDocAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierRoleAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork
{
    public class FleetPlanBCUnitOfWork : UniContext<FleetPlanBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<ActionCategory> _actionCategories;
        private IDbSet<AirProgramming> _airProgrammings;
        private IDbSet<AircraftCategory> _aircraftCategories;
        private IDbSet<AircraftConfiguration> _aircraftConfigurations;
        private IDbSet<AircraftSeries> _aircraftSeries;
        private IDbSet<AircraftType> _aircraftTypes;
        private IDbSet<Aircraft> _aircrafts;
        private IDbSet<Airlines> _airlineses;
        private IDbSet<Annual> _annuals;
        private IDbSet<ApprovalDoc> _approvalDocs;
        private IDbSet<CAACAircraftType> _caacAircraftTypes;
        private IDbSet<CaacProgramming> _caacProgrammings;
        private IDbSet<Document> _documents;
        private IDbSet<EnginePlan> _enginePlans;
        private IDbSet<EngineType> _engineTypes;
        private IDbSet<Engine> _engines;
        private IDbSet<IssuedUnit> _issuedUnits;
        private IDbSet<MailAddress> _mailAddresses;
        private IDbSet<Manager> _managers;
        private IDbSet<Manufacturer> _manufacturers;
        private IDbSet<PlanAircraft> _planAircrafts;
        private IDbSet<PlanEngine> _planEngines;
        private IDbSet<PlanHistory> _planHistories;
        private IDbSet<Plan> _plans;
        private IDbSet<ProgrammingFile> _programmingFiles;
        private IDbSet<Programming> _programmings;
        private IDbSet<RelatedDoc> _relatedDocs;
        private IDbSet<Request> _requests;
        private IDbSet<Supplier> _suppliers;
        private IDbSet<SupplierCompany> _supplierCompanies;
        private IDbSet<SupplierRole> _supplierRoles;
        private IDbSet<XmlConfig> _xmlConfigs;

        public IDbSet<ActionCategory> ActionCategories
        {
            get { return _actionCategories ?? (_actionCategories = base.Set<ActionCategory>()); }
        }

        public IDbSet<AircraftSeries> AircraftSeries
        {
            get { return _aircraftSeries ?? (_aircraftSeries = base.Set<AircraftSeries>()); }
        }

        public IDbSet<AircraftCategory> AircraftCategories
        {
            get { return _aircraftCategories ?? (_aircraftCategories = base.Set<AircraftCategory>()); }
        }

        public IDbSet<AircraftConfiguration> AircraftConfigurations
        {
            get { return _aircraftConfigurations ?? (_aircraftConfigurations = base.Set<AircraftConfiguration>()); }
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

        public IDbSet<CAACAircraftType> CaacAircraftTypes
        {
            get { return _caacAircraftTypes ?? (_caacAircraftTypes = base.Set<CAACAircraftType>()); }
        }

        public IDbSet<Document> Documents
        {
            get { return _documents ?? (_documents = base.Set<Document>()); }
        }

        public IDbSet<CaacProgramming> CaacProgrammings
        {
            get { return _caacProgrammings ?? (_caacProgrammings = base.Set<CaacProgramming>()); }
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

        public IDbSet<IssuedUnit> IssuedUnits
        {
            get { return _issuedUnits ?? (_issuedUnits = base.Set<IssuedUnit>()); }
        }

        public IDbSet<MailAddress> MailAddresses
        {
            get { return _mailAddresses ?? (_mailAddresses = base.Set<MailAddress>()); }
        }

        public IDbSet<Manager> Managers
        {
            get { return _managers ?? (_managers = base.Set<Manager>()); }
        }

        public IDbSet<Manufacturer> Manufacturers
        {
            get { return _manufacturers ?? (_manufacturers = base.Set<Manufacturer>()); }
        }

        public IDbSet<Plan> Plans
        {
            get { return _plans ?? (_plans = base.Set<Plan>()); }
        }

        public IDbSet<PlanHistory> PlanHistories
        {
            get { return _planHistories ?? (_planHistories = base.Set<PlanHistory>()); }
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

        public IDbSet<ProgrammingFile> ProgrammingFiles
        {
            get { return _programmingFiles ?? (_programmingFiles = base.Set<ProgrammingFile>()); }
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

        public IDbSet<SupplierRole> SupplierRoles
        {
            get { return _supplierRoles ?? (_supplierRoles = base.Set<SupplierRole>()); }
        }
        public IDbSet<XmlConfig> XmlConfigs
        {
            get { return _xmlConfigs ?? (_xmlConfigs = base.Set<XmlConfig>()); }
        }

        #endregion
    }
}