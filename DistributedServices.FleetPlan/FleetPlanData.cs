//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.ActionCategoryServices;
using UniCloud.Application.FleetPlanBC.AircraftCategoryServices;
using UniCloud.Application.FleetPlanBC.AircraftConfigurationServices;
using UniCloud.Application.FleetPlanBC.AircraftPlanHistoryServices;
using UniCloud.Application.FleetPlanBC.AircraftPlanServices;
using UniCloud.Application.FleetPlanBC.AircraftSeriesServices;
using UniCloud.Application.FleetPlanBC.AircraftServices;
using UniCloud.Application.FleetPlanBC.AircraftTypeServices;
using UniCloud.Application.FleetPlanBC.AirlinesServices;
using UniCloud.Application.FleetPlanBC.AirProgrammingServices;
using UniCloud.Application.FleetPlanBC.AnnualServices;
using UniCloud.Application.FleetPlanBC.ApprovalDocServices;
using UniCloud.Application.FleetPlanBC.CAACAircraftTypeServices;
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
using UniCloud.Application.FleetPlanBC.ProgrammingFileServices;
using UniCloud.Application.FleetPlanBC.ProgrammingServices;
using UniCloud.Application.FleetPlanBC.RequestServices;
using UniCloud.Application.FleetPlanBC.SupplierServices;
using UniCloud.Application.FleetPlanBC.XmlConfigServices;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.FleetPlan
{
    /// <summary>
    ///     运力规划模块数据类
    /// </summary>
    public class FleetPlanData : ExposeData.ExposeData
    {
        private readonly IAircraftSeriesAppService _aircraftSeriesAppService;
        private readonly IActionCategoryAppService _actionCategoryAppService;
        private readonly IAirProgrammingAppService _airProgrammingAppService;
        private readonly IAircraftAppService _aircraftAppService;
        private readonly IAircraftCategoryAppService _aircraftCategoryAppService;
        private readonly IAircraftConfigurationAppService _aircraftConfigurationAppService;
        private readonly IAircraftTypeAppService _aircraftTypeAppService;
        private readonly IAirlinesAppService _airlinesAppService;
        private readonly IAnnualAppService _annualAppService;
        private readonly IApprovalDocAppService _approvalDocAppService;
        private readonly ICAACAircraftTypeAppService _caacAircraftTypeAppService;
        private readonly ICaacProgrammingAppService _caacProgrammingAppService;
        private readonly IEngineAppService _engineAppService;
        private readonly IEnginePlanAppService _enginePlanAppService;
        private readonly IEngineTypeAppService _engineTypeAppService;
        private readonly IMailAddressAppService _mailAddressAppService;
        private readonly IManagerAppService _managerAppService;
        private readonly IManufacturerAppService _manufacturerAppService;
        private readonly IPlanAircraftAppService _planAircraftAppService;
        private readonly IPlanAppService _planAppService;
        private readonly IPlanHistoryAppService _planHistoryAppService;
        private readonly IPlanEngineAppService _planEngineAppService;
        private readonly IProgrammingAppService _programmingAppService;
        private readonly IProgrammingFileAppService _programmingFileAppService;
        private readonly IRequestAppService _requestAppService;
        private readonly ISupplierAppService _supplierAppService;
        private readonly IXmlConfigAppService _xmlConfigAppService;

        public FleetPlanData()
            : base("UniCloud.Application.FleetPlanBC.DTO")
        {
            _actionCategoryAppService = DefaultContainer.Resolve<IActionCategoryAppService>();
            _aircraftSeriesAppService = DefaultContainer.Resolve<IAircraftSeriesAppService>();
            _aircraftCategoryAppService = DefaultContainer.Resolve<IAircraftCategoryAppService>();
            _aircraftConfigurationAppService = DefaultContainer.Resolve<IAircraftConfigurationAppService>();
            _aircraftAppService = DefaultContainer.Resolve<IAircraftAppService>();
            _aircraftTypeAppService = DefaultContainer.Resolve<IAircraftTypeAppService>();
            _airlinesAppService = DefaultContainer.Resolve<IAirlinesAppService>();
            _airProgrammingAppService = DefaultContainer.Resolve<IAirProgrammingAppService>();
            _annualAppService = DefaultContainer.Resolve<IAnnualAppService>();
            _approvalDocAppService = DefaultContainer.Resolve<IApprovalDocAppService>();
            _caacAircraftTypeAppService = DefaultContainer.Resolve<ICAACAircraftTypeAppService>();
            _caacProgrammingAppService = DefaultContainer.Resolve<ICaacProgrammingAppService>();
            _engineAppService = DefaultContainer.Resolve<IEngineAppService>();
            _enginePlanAppService = DefaultContainer.Resolve<IEnginePlanAppService>();
            _engineTypeAppService = DefaultContainer.Resolve<IEngineTypeAppService>();
            _mailAddressAppService = DefaultContainer.Resolve<IMailAddressAppService>();
            _managerAppService = DefaultContainer.Resolve<IManagerAppService>();
            _manufacturerAppService = DefaultContainer.Resolve<IManufacturerAppService>();
            _planAppService = DefaultContainer.Resolve<IPlanAppService>();
            _planHistoryAppService = DefaultContainer.Resolve<IPlanHistoryAppService>();
            _planAircraftAppService = DefaultContainer.Resolve<IPlanAircraftAppService>();
            _planEngineAppService = DefaultContainer.Resolve<IPlanEngineAppService>();
            _programmingAppService = DefaultContainer.Resolve<IProgrammingAppService>();
            _programmingFileAppService = DefaultContainer.Resolve<IProgrammingFileAppService>();
            _requestAppService = DefaultContainer.Resolve<IRequestAppService>();
            _supplierAppService = DefaultContainer.Resolve<ISupplierAppService>();
            _xmlConfigAppService = DefaultContainer.Resolve<IXmlConfigAppService>();
            _requestAppService = DefaultContainer.Resolve<IRequestAppService>();
            _approvalDocAppService = DefaultContainer.Resolve<IApprovalDocAppService>();
        }

        #region 活动类型

        /// <summary>
        ///     活动类型集合
        /// </summary>
        public IQueryable<ActionCategoryDTO> ActionCategories
        {
            get
            {
                return GetStaticData("actionCategoriesFleetPlan", () => _actionCategoryAppService.GetActionCategories());
            }
        }

        #endregion

        #region 飞机配置

        /// <summary>
        ///     飞机配置集合
        /// </summary>
        public IQueryable<AircraftConfigurationDTO> AircraftConfigurations
        {
            get
            {
                return _aircraftConfigurationAppService.GetAircraftConfigurations();
            }
        }

        #endregion

        #region 飞机系列集合

        /// <summary>
        ///     活动类型集合
        /// </summary>
        public IQueryable<AircraftSeriesDTO> AircraftSeries
        {
            get { return GetStaticData("AircraftSeriesFleetPlan", () => _aircraftSeriesAppService.GetAircraftSeries()); }
        }

        #endregion

        #region 座级

        /// <summary>
        ///     座级集合
        /// </summary>
        public IQueryable<AircraftCategoryDTO> AircraftCategories
        {
            get
            {
                return GetStaticData("aircraftCategoriesFleetPlan",
                    () => _aircraftCategoryAppService.GetAircraftCategories());
            }
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
        ///     川航机型集合
        /// </summary>
        public IQueryable<AircraftTypeDTO> AircraftTypes
        {
            get { return GetStaticData("aircraftTypesFleetPlan", () => _aircraftTypeAppService.GetAircraftTypes()); }
        }


        /// <summary>
        ///     民航机型集合
        /// </summary>
        public IQueryable<CAACAircraftTypeDTO> CaacAircraftTypes
        {
            get { return GetStaticData("caacAircraftTypesFleetPlan", () => _caacAircraftTypeAppService.GetCAACAircraftTypes()); }
        }
        #endregion

        #region 航空公司

        /// <summary>
        ///     航空公司集合
        /// </summary>
        public IQueryable<AirlinesDTO> Airlineses
        {
            get { return GetStaticData("airlinesFleetPlan", () => _airlinesAppService.GetAirlineses()); }
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
            get { return GetStaticData("engineTypes", () => _engineTypeAppService.GetEngineTypes()); }
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
            get { return GetStaticData("managersFleetPlan", () => _managerAppService.GetManagers()); }
        }

        #endregion

        #region 制造商

        /// <summary>
        ///     制造商集合
        /// </summary>
        public IQueryable<ManufacturerDTO> Manufacturers
        {
            get { return GetStaticData("manufactoriesFleetPlan", () => _manufacturerAppService.GetManufacturers()); }
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

        /// <summary>
        ///     飞机计划明细集合
        /// </summary>
        public IQueryable<PlanHistoryDTO> PlanHistories
        {
            get { return _planHistoryAppService.GetPlanHistories(); }
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
            get { return GetStaticData("programmingsFleetPlan", () => _programmingAppService.GetProgrammings()); }
        }

        #endregion

        #region 规划文档

        /// <summary>
        ///     规划文档集合
        /// </summary>
        public IQueryable<ProgrammingFileDTO> ProgrammingFiles
        {
            get { return _programmingFileAppService.GetProgrammingFiles(); }
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

        /// <summary>
        /// 获取带有申请的批文
        /// </summary>
        public IQueryable<ApprovalRequestDTO> ApprovalRequests
        {
            get
            {
                return _requestAppService.GetApprovalRequests();
            }
        }

        #endregion

        #region 供应商

        /// <summary>
        ///     供应商集合
        /// </summary>
        public IQueryable<SupplierDTO> Suppliers
        {
            get { return GetStaticData("suppliersFleetPlan", () => _supplierAppService.GetSuppliers()); }
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

    }
}