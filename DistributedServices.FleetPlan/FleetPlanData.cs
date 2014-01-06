//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System.Linq;
using UniCloud.Application.FleetPlanBC;
using UniCloud.Application.FleetPlanBC.ActionCategoryServices;
using UniCloud.Application.FleetPlanBC.AircraftCategoryServices;
using UniCloud.Application.FleetPlanBC.AircraftServices;
using UniCloud.Application.FleetPlanBC.AircraftTypeServices;
using UniCloud.Application.FleetPlanBC.AirlinesServices;
using UniCloud.Application.FleetPlanBC.AirProgrammingServices;
using UniCloud.Application.FleetPlanBC.AnnualServices;
using UniCloud.Application.FleetPlanBC.ApprovalDocServices;
using UniCloud.Application.FleetPlanBC.CaacProgrammingServices;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.EnginePlanServices;
using UniCloud.Application.FleetPlanBC.EngineServices;
using UniCloud.Application.FleetPlanBC.EngineTypeServices;
using UniCloud.Application.FleetPlanBC.MailAddressServices;
using UniCloud.Application.FleetPlanBC.ManagerServices;
using UniCloud.Application.FleetPlanBC.ManufacturerServices;
using UniCloud.Application.FleetPlanBC.PlanAircraftServices;
using UniCloud.Application.FleetPlanBC.PlanEngineServices;
using UniCloud.Application.FleetPlanBC.AircraftPlanServices;
using UniCloud.Application.FleetPlanBC.ProgrammingServices;
using UniCloud.Application.FleetPlanBC.RequestServices;
using UniCloud.Application.FleetPlanBC.SupplierServices;
using UniCloud.Application.FleetPlanBC.XmlConfigServices;
using UniCloud.Application.FleetPlanBC.XmlSettingServices;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.DistributedServices.FleetPlan
{
    /// <summary>
    /// 运力规划模块数据类
    /// </summary>
    public class FleetPlanData : ExposeData.ExposeData
    {
        private readonly IActionCategoryAppService _actionCategoryAppService;
        private readonly IAircraftCategoryAppService _aircraftCategoryAppService;
        private readonly IAircraftAppService _aircraftAppService;
        private readonly IAircraftTypeAppService _aircraftTypeAppService;
        private readonly IAirlinesAppService _airlinesAppService;
        private readonly IAirProgrammingAppService _airProgrammingAppService;
        private readonly IAnnualAppService _annualAppService;
        private readonly IApprovalDocAppService _approvalDocAppService;
        private readonly ICaacProgrammingAppService _caacProgrammingAppService;
        private readonly IEngineAppService _engineAppService;
        private readonly IEnginePlanAppService _enginePlanAppService;
        private readonly IEngineTypeAppService _engineTypeAppService;
        private readonly IMailAddressAppService _mailAddressAppService;
        private readonly IManagerAppService _managerAppService;
        private readonly IManufacturerAppService _manufacturerAppService;
        private readonly IPlanAppService _planAppService;
        private readonly IPlanAircraftAppService _planAircraftAppService;
        private readonly IPlanEngineAppService _planEngineAppService;
        private readonly IProgrammingAppService _programmingAppService;
        private readonly IStaticLoad _staticLoad;
        private readonly ISupplierAppService _supplierAppService;
        private readonly IXmlConfigAppService _xmlConfigAppService;
        private readonly IXmlSettingAppService _xmlSettingAppService;
        private readonly IRequestAppService _requestAppService;
        public FleetPlanData()
            : base("UniCloud.Application.FleetPlanBC.DTO")
        {
            _actionCategoryAppService = DefaultContainer.Resolve<IActionCategoryAppService>();
            _aircraftCategoryAppService = DefaultContainer.Resolve<IAircraftCategoryAppService>();
            _aircraftAppService = DefaultContainer.Resolve<IAircraftAppService>();
            _aircraftTypeAppService = DefaultContainer.Resolve<IAircraftTypeAppService>();
            _airlinesAppService = DefaultContainer.Resolve<IAirlinesAppService>();
            _airProgrammingAppService = DefaultContainer.Resolve<IAirProgrammingAppService>();
            _annualAppService = DefaultContainer.Resolve<IAnnualAppService>();
            _approvalDocAppService = DefaultContainer.Resolve<IApprovalDocAppService>();
            _caacProgrammingAppService = DefaultContainer.Resolve<ICaacProgrammingAppService>();
            _engineAppService = DefaultContainer.Resolve<IEngineAppService>();
            _enginePlanAppService = DefaultContainer.Resolve<IEnginePlanAppService>();
            _engineTypeAppService = DefaultContainer.Resolve<IEngineTypeAppService>();
            _mailAddressAppService = DefaultContainer.Resolve<IMailAddressAppService>();
            _managerAppService = DefaultContainer.Resolve<IManagerAppService>();
            _manufacturerAppService = DefaultContainer.Resolve<IManufacturerAppService>();
            _planAppService = DefaultContainer.Resolve<IPlanAppService>();
            _planAircraftAppService = DefaultContainer.Resolve<IPlanAircraftAppService>();
            _planEngineAppService = DefaultContainer.Resolve<IPlanEngineAppService>();
            _programmingAppService = DefaultContainer.Resolve<IProgrammingAppService>();
            _requestAppService = DefaultContainer.Resolve<IRequestAppService>();
            _staticLoad = DefaultContainer.Resolve<IStaticLoad>();
            _supplierAppService = DefaultContainer.Resolve<ISupplierAppService>();
            _xmlConfigAppService = DefaultContainer.Resolve<IXmlConfigAppService>();
            _xmlSettingAppService = DefaultContainer.Resolve<IXmlSettingAppService>();
            _requestAppService = DefaultContainer.Resolve<IRequestAppService>();
            _approvalDocAppService = DefaultContainer.Resolve<IApprovalDocAppService>();
        }
        #region 活动类型
        /// <summary>
        ///     活动类型集合
        /// </summary>
        public IQueryable<ActionCategoryDTO> ActionCategories
        {
            get { return _staticLoad.GetActionCategories(); }
        }
        #endregion

        #region 飞机系列集合
        /// <summary>
        ///     活动类型集合
        /// </summary>
        public IQueryable<AcTypeDTO> AcTypes
        {
            get { return _staticLoad.GetAcTypes(); }
        }
        #endregion

        #region 座级
        /// <summary>
        ///     座级集合
        /// </summary>
        public IQueryable<AircraftCategoryDTO> AircraftCategories
        {
            get { return _staticLoad.GetAircraftCategories(); }
        }
        #endregion

        #region 实际飞机
        /// <summary>
        ///     实际飞机集合
        /// </summary>
        public IQueryable<AircraftDTO> Aircrafts
        {
            get { return _aircraftAppService.GetAircrafts(); }
        }
        #endregion

        #region 机型
        /// <summary>
        ///     机型集合
        /// </summary>
        public IQueryable<AircraftTypeDTO> AircraftTypes
        {
            get { return _staticLoad.GetAircraftTypes(); }
        }
        #endregion

        #region 航空公司
        /// <summary>
        ///     航空公司集合
        /// </summary>
        public IQueryable<AirlinesDTO> Airlineses
        {
            get { return _staticLoad.GetAirlineses(); }
        }
        #endregion

        #region 航空公司五年规划
        /// <summary>
        ///     航空公司五年规划集合
        /// </summary>
        public IQueryable<AirProgrammingDTO> AirProgrammings
        {
            get { return _airProgrammingAppService.GetAirProgrammings(); }
        }
        #endregion

        #region 计划年度

        /// <summary>
        ///     计划年度集合
        /// </summary>
        public IQueryable<AnnualDTO> Annuals
        {
            get { return _annualAppService.GetAnnuals(); }
        }

        /// <summary>
        ///     计划年度集合
        /// </summary>
        public IQueryable<PlanYearDTO> PlanYears
        {
            get { return _annualAppService.GetPlanYears(); }
        }
        #endregion

        #region 批文
        /// <summary>
        ///     批文集合
        /// </summary>
        public IQueryable<ApprovalDocDTO> ApprovalDocs
        {
            get { return _approvalDocAppService.GetApprovalDocs(); }
        }
        #endregion

        #region 民航局五年规划
        /// <summary>
        ///     民航局五年规划集合
        /// </summary>
        public IQueryable<CaacProgrammingDTO> CaacProgrammings
        {
            get { return _caacProgrammingAppService.GetCaacProgrammings(); }
        }
        #endregion

        #region 发动机

        /// <summary>
        ///     发动机集合
        /// </summary>
        public IQueryable<EngineDTO> Engines
        {
            get { return _engineAppService.GetEngines(); }
        }
        #endregion

        #region 备发计划
        /// <summary>
        ///     备发计划集合
        /// </summary>
        public IQueryable<EnginePlanDTO> EnginePlans
        {
            get { return _enginePlanAppService.GetEnginePlans(); }
        }
        #endregion

        #region 发动机型号

        /// <summary>
        ///     发动机型号集合
        /// </summary>
        public IQueryable<EngineTypeDTO> EngineTypes
        {
            get { return _staticLoad.GetEngineTypes(); }
        }
        #endregion

        #region 邮件账号
        /// <summary>
        ///     邮件账号集合
        /// </summary>
        public IQueryable<MailAddressDTO> MailAddresses
        {
            get { return _mailAddressAppService.GetMailAddresses(); }
        }
        #endregion

        #region 管理者
        /// <summary>
        ///     管理者集合
        /// </summary>
        public IQueryable<ManagerDTO> Managers
        {
            get { return _staticLoad.GetManagers(); }
        }
        #endregion

        #region 制造商
        /// <summary>
        ///     制造商集合
        /// </summary>
        public IQueryable<ManufacturerDTO> Manufacturers
        {
            get { return _staticLoad.GetManufacturers(); }
        }
        #endregion

        #region 飞机计划
        /// <summary>
        ///     飞机计划集合
        /// </summary>
        public IQueryable<PlanDTO> Plans
        {
            get { return _planAppService.GetPlans(); }
        }
        #endregion

        #region 计划飞机
        /// <summary>
        ///     计划飞机集合
        /// </summary>
        public IQueryable<PlanAircraftDTO> PlanAircrafts
        {
            get { return _planAircraftAppService.GetPlanAircrafts(); }
        }
        #endregion

        #region 计划发动机
        /// <summary>
        ///     计划发动机集合
        /// </summary>
        public IQueryable<PlanEngineDTO> PlanEngines
        {
            get { return _planEngineAppService.GetPlanEngines(); }
        }
        #endregion

        #region 规划期间
        /// <summary>
        ///     规划期间集合
        /// </summary>
        public IQueryable<ProgrammingDTO> Programmings
        {
            get { return _staticLoad.GetProgrammings(); }
        }
        #endregion

        #region 申请
        /// <summary>
        ///     申请集合
        /// </summary>
        public IQueryable<RequestDTO> Requests
        {
            get { return _requestAppService.GetRequests(); }
        }
        #endregion

        #region 供应商
        /// <summary>
        ///     供应商集合
        /// </summary>
        public IQueryable<SupplierDTO> Suppliers
        {
            get { return _staticLoad.GetSuppliers(); }
        }
        #endregion

        #region 分析数据相关的xml
        /// <summary>
        ///     分析数据相关的xml集合
        /// </summary>
        public IQueryable<XmlConfigDTO> XmlConfigs
        {
            get { return _xmlConfigAppService.GetXmlConfigs(); }
        }
        #endregion

        #region 配置相关的xml
        /// <summary>
        ///     配置相关的xml集合
        /// </summary>
        public IQueryable<XmlSettingDTO> XmlSettings
        {
            get { return _xmlSettingAppService.GetXmlSettings(); }
        }
        #endregion
    }
}