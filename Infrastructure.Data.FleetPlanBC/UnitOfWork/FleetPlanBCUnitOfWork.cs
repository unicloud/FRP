#region 版本信息
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
using System.Data.Entity.ModelConfiguration.Conventions;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AcTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.CaacProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.MailAddressAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManagerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManufacturerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanEngineAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlSettingAgg;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork
{
    public class FleetPlanBCUnitOfWork : BaseContext<FleetPlanBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<ActionCategory> _actionCategories;
        private IDbSet<AcType> _acTypes;
        private IDbSet<AircraftCategory> _aircraftCategories;
        private IDbSet<Aircraft> _aircrafts;
        private IDbSet<AircraftType> _aircraftTypes;
        private IDbSet<Airlines> _airlineses;
        private IDbSet<AirProgramming> _airProgrammings;
        private IDbSet<Annual> _annuals;
        private IDbSet<ApprovalDoc> _approvalDocs;
        private IDbSet<CaacProgramming> _caacProgrammings;
        private IDbSet<Engine> _engines;
        private IDbSet<EnginePlan> _enginePlans;
        private IDbSet<EngineType> _engineTypes;
        private IDbSet<MailAddress> _mailAddresses;
        private IDbSet<Manager> _managers;
        private IDbSet<Manufacturer> _manufacturers;
        private IDbSet<Plan> _plans;
        private IDbSet<PlanAircraft> _planAircrafts;
        private IDbSet<PlanEngine> _planEngines;
        private IDbSet<Programming> _programmings;
        private IDbSet<Request> _requests;
        private IDbSet<Supplier> _suppliers;
        private IDbSet<XmlConfig> _xmlConfigs;
        private IDbSet<XmlSetting> _xmlSettings;

        public IDbSet<ActionCategory> ActionCategories
        {
            get { return _actionCategories ?? (_actionCategories = base.Set<ActionCategory>()); }
        }

        public IDbSet<AcType> AcTypes
        {
            get { return _acTypes ?? (_acTypes = base.Set<AcType>()); }
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

        public IDbSet<Request> Requests
        {
            get { return _requests ?? (_requests = base.Set<Request>()); }
        }

        public IDbSet<Supplier> Suppliers
        {
            get { return _suppliers ?? (_suppliers = base.Set<Supplier>()); }
        }

        public IDbSet<XmlConfig> XmlConfigs
        {
            get { return _xmlConfigs ?? (_xmlConfigs = base.Set<XmlConfig>()); }
        }

        public IDbSet<XmlSetting> XmlSettings
        {
            get { return _xmlSettings ?? (_xmlSettings = base.Set<XmlSetting>()); }
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

                .Add(new AcTypeEntityConfiguration())

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

                #region CaacProgrammingAgg

                .Add(new CaacProgrammingEntityConfiguration())
                .Add(new CaacProgrammingLineEntityConfiguration())

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

                #region MailAddressAgg

                .Add(new MailAddressEntityConfiguration())

                #endregion

                #region ManagerAgg

                .Add(new ManagerEntityConfiguration())

                #endregion

                #region ManufacturerAgg

                .Add(new ManufacturerEntityConfiguration())

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

                #region RequestAgg

                .Add(new RequestEntityConfiguration())
                .Add(new ApprovalHistoryEntityConfiguration())

                #endregion

                #region SupplierAgg

                .Add(new SupplierEntityConfiguration())

                #endregion

                #region XmlConfigAgg

                .Add(new XmlConfigEntityConfiguration())

                #endregion

                #region XmlSettingAgg

                .Add(new XmlSettingEntityConfiguration())

                #endregion

                ;
        }

        #endregion
    }
}
