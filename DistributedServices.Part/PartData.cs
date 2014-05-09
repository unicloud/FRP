#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/10，13:11
// 方案：FRP
// 项目：DistributedServices.Part
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.AcConfigServices;
using UniCloud.Application.PartBC.AcDailyUtilizationServices;
using UniCloud.Application.PartBC.AdSbServices;
using UniCloud.Application.PartBC.AircraftServices;
using UniCloud.Application.PartBC.AircraftTypeServices;
using UniCloud.Application.PartBC.AirStructureDamageServices;
using UniCloud.Application.PartBC.AnnualMaintainPlanServices;
using UniCloud.Application.PartBC.BasicConfigGroupServices;
using UniCloud.Application.PartBC.BasicConfigHistoryServices;
using UniCloud.Application.PartBC.BasicConfigServices;
using UniCloud.Application.PartBC.ContractAircraftServices;
using UniCloud.Application.PartBC.CtrlUnitServices;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.InstallControllerServices;
using UniCloud.Application.PartBC.ItemServices;
using UniCloud.Application.PartBC.MaintainCtrlServices;
using UniCloud.Application.PartBC.MaintainWorkServices;
using UniCloud.Application.PartBC.ModServices;
using UniCloud.Application.PartBC.OilMonitorServices;
using UniCloud.Application.PartBC.PnRegServices;
using UniCloud.Application.PartBC.ScnServices;
using UniCloud.Application.PartBC.SnHistoryServices;
using UniCloud.Application.PartBC.SnRegServices;
using UniCloud.Application.PartBC.SnRemInstRecordServices;
using UniCloud.Application.PartBC.SpecialConfigServices;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.Part
{
    /// <summary>
    ///     附件管理模块数据类
    /// </summary>
    public class PartData : ExposeData.ExposeData
    {
        private readonly IAcDailyUtilizationAppService _acDailyUtilizationAppService;
        private readonly IAdSbAppService _adSbAppService;
        private readonly IAcConfigAppService _acConfigAppService;
        private readonly IAirStructureDamageAppService _airStructureDamageAppService;
        private readonly IAircraftAppService _aircraftAppService;
        private readonly IAircraftTypeAppService _aircraftTypeAppService;
        private readonly IBasicConfigAppService _basicConfigAppService;
        private readonly IBasicConfigGroupAppService _basicConfigGroupAppService;
        private readonly IBasicConfigHistoryAppService _basicConfigHistoryAppService;
        private readonly IContractAircraftAppService _contractAircraftAppService;
        private readonly ICtrlUnitAppService _ctrlUnitAppService;
        private readonly IItemAppService _itemAppService;
        private readonly IInstallControllerAppService _installControllerAppService;
        private readonly IMaintainCtrlAppService _maintainCtrlAppService;
        private readonly IMaintainWorkAppService _maintainWorkAppService;
        private readonly IModAppService _modAppService;
        private readonly IOilMonitorAppService _oilMonitorAppService;
        private readonly IPnRegAppService _pnRegAppService;
        private readonly IScnAppService _scnAppService;
        private readonly ISnHistoryAppService _snHistoryAppService;
        private readonly ISnRemInstRecordAppService _snRemInstRecordAppService;
        private readonly ISnRegAppService _snRegAppService;
        private readonly ISpecialConfigAppService _specialConfigAppService;
        private readonly IAnnualMaintainPlanAppService _annualMaintainPlanAppService;

        public PartData()
            : base("UniCloud.Application.PartBC.DTO")
        {
            _acDailyUtilizationAppService = DefaultContainer.Resolve<IAcDailyUtilizationAppService>();
            _acConfigAppService = DefaultContainer.Resolve<IAcConfigAppService>();
            _aircraftAppService = DefaultContainer.Resolve<IAircraftAppService>();
            _aircraftTypeAppService = DefaultContainer.Resolve<IAircraftTypeAppService>();
            _basicConfigAppService = DefaultContainer.Resolve<IBasicConfigAppService>();
            _basicConfigGroupAppService = DefaultContainer.Resolve<IBasicConfigGroupAppService>();
            _basicConfigHistoryAppService = DefaultContainer.Resolve<IBasicConfigHistoryAppService>();
            _contractAircraftAppService = DefaultContainer.Resolve<IContractAircraftAppService>();
            _ctrlUnitAppService = DefaultContainer.Resolve<ICtrlUnitAppService>();
            _itemAppService = DefaultContainer.Resolve<IItemAppService>();
            _installControllerAppService = DefaultContainer.Resolve<IInstallControllerAppService>();
            _maintainCtrlAppService = DefaultContainer.Resolve<IMaintainCtrlAppService>();
            _maintainWorkAppService = DefaultContainer.Resolve<IMaintainWorkAppService>();
            _modAppService = DefaultContainer.Resolve<IModAppService>();
            _oilMonitorAppService = DefaultContainer.Resolve<IOilMonitorAppService>();
            _pnRegAppService = DefaultContainer.Resolve<IPnRegAppService>();
            _scnAppService = DefaultContainer.Resolve<IScnAppService>();
            _snRegAppService = DefaultContainer.Resolve<ISnRegAppService>();
            _snHistoryAppService = DefaultContainer.Resolve<ISnHistoryAppService>();
            _snRemInstRecordAppService = DefaultContainer.Resolve<ISnRemInstRecordAppService>();
            _specialConfigAppService = DefaultContainer.Resolve<ISpecialConfigAppService>();
            _airStructureDamageAppService = DefaultContainer.Resolve<IAirStructureDamageAppService>();
            _adSbAppService = DefaultContainer.Resolve<IAdSbAppService>();
            _annualMaintainPlanAppService = DefaultContainer.Resolve<IAnnualMaintainPlanAppService>();
        }

        #region 飞机日利用率集合

        /// <summary>
        ///     飞机日利用率集合
        /// </summary>
        public IQueryable<AcDailyUtilizationDTO> AcDailyUtilizations
        {
            get { return _acDailyUtilizationAppService.GetAcDailyUtilizations(); }
        }

        #endregion

        #region 运营飞机集合

        /// <summary>
        ///     运营飞机集合
        /// </summary>
        public IQueryable<AircraftDTO> Aircrafts
        {
            get { return _aircraftAppService.GetAircrafts(); }
        }

        #endregion

        #region 机型集合

        /// <summary>
        ///     机型集合
        /// </summary>
        public IQueryable<AircraftTypeDTO> AircraftTypes
        {
            get { return _aircraftTypeAppService.GetAircraftTypes(); }
        }

        /// <summary>
        ///     系列集合
        /// </summary>
        public IQueryable<AircraftSeriesDTO> AircraftSeriess
        {
            get { return _aircraftTypeAppService.GetAircraftSeriess(); }
        }

        #endregion

        #region 功能构型集合

        /// <summary>
        ///     功能构型集合
        /// </summary>
        public IQueryable<AcConfigDTO> AcConfigs
        {
            get { return _acConfigAppService.GetAcConfigs(); }
        }

        #endregion

        #region 基本构型组集合

        /// <summary>
        ///     基本构型组集合
        /// </summary>
        public IQueryable<BasicConfigGroupDTO> BasicConfigGroups
        {
            get { return _basicConfigGroupAppService.GetBasicConfigGroups(); }
        }

        #endregion

        #region 基本构型集合

        /// <summary>
        ///     基本构型集合
        /// </summary>
        public IQueryable<BasicConfigDTO> BasicConfigs
        {
            get { return _basicConfigAppService.GetBasicConfigs(); }
        }

        #endregion

        #region 基本构型历史集合

        /// <summary>
        ///     基本构型历史集合
        /// </summary>
        public IQueryable<BasicConfigHistoryDTO> BasicConfigHistories
        {
            get { return _basicConfigHistoryAppService.GetBasicConfigHistories(); }
        }

        #endregion

        #region 合同飞机集合

        /// <summary>
        ///     合同飞机集合
        /// </summary>
        public IQueryable<ContractAircraftDTO> ContractAircrafts
        {
            get { return _contractAircraftAppService.GetContractAircrafts(); }
        }

        #endregion

        #region 控制单位集合

        /// <summary>
        ///     控制单位集合
        /// </summary>
        public IQueryable<CtrlUnitDTO> CtrlUnits
        {
            get { return _ctrlUnitAppService.GetCtrlUnits(); }
        }

        #endregion

        #region 附件项集合

        /// <summary>
        ///     附件项集合
        /// </summary>
        public IQueryable<ItemDTO> Items
        {
            get { return _itemAppService.GetItems(); }
        }

        #endregion

        #region 装机控制集合

        /// <summary>
        ///     装机控制集合
        /// </summary>
        public IQueryable<InstallControllerDTO> InstallControllers
        {
            get { return _installControllerAppService.GetInstallControllers(); }
        }

        #endregion

        #region 维修控制组集合

        /// <summary>
        ///     项维修控制组集合
        /// </summary>
        public IQueryable<ItemMaintainCtrlDTO> ItemMaintainCtrls
        {
            get { return _maintainCtrlAppService.GetItemMaintainCtrls(); }
        }

        /// <summary>
        ///     附件维修控制组集合
        /// </summary>
        public IQueryable<PnMaintainCtrlDTO> PnMaintainCtrls
        {
            get { return _maintainCtrlAppService.GetPnMaintainCtrls(); }
        }

        /// <summary>
        ///     序号件维修控制组集合
        /// </summary>
        public IQueryable<SnMaintainCtrlDTO> SnMaintainCtrls
        {
            get { return _maintainCtrlAppService.GetSnMaintainCtrls(); }
        }

        #endregion

        #region 维修工作集合

        /// <summary>
        ///     维修工作集合
        /// </summary>
        public IQueryable<MaintainWorkDTO> MaintainWorks
        {
            get { return _maintainWorkAppService.GetMaintainWorks(); }
        }

        #endregion

        #region Mod号集合

        /// <summary>
        ///     Mod号集合
        /// </summary>
        public IQueryable<ModDTO> Mods
        {
            get { return _modAppService.GetMods(); }
        }

        #endregion

        #region 滑油监控集合

        /// <summary>
        ///     发动机滑油集合
        /// </summary>
        public IQueryable<EngineOilDTO> EngineOils
        {
            get { return _oilMonitorAppService.GetEngineOils(); }
        }

        /// <summary>
        ///     APU滑油集合
        /// </summary>
        public IQueryable<APUOilDTO> APUOils
        {
            get { return _oilMonitorAppService.GetAPUOils(); }
        }

        /// <summary>
        ///     滑油消耗数据
        /// </summary>
        public IQueryable<OilMonitorDTO> OilMonitors
        {
            get { return _oilMonitorAppService.GetOilMonitors(); }
        }

        #endregion

        #region 附件集合

        /// <summary>
        ///     附件集合
        /// </summary>
        public IQueryable<PnRegDTO> PnRegs
        {
            get { return _pnRegAppService.GetPnRegs(); }
        }

        #endregion

        #region Scn相关集合

        /// <summary>
        ///     Scn信息。
        /// </summary>
        public IQueryable<ScnDTO> Scns
        {
            get { return _scnAppService.GetScns(); }
        }

        /// <summary>
        ///     AirBusScn信息。
        /// </summary>
        public IQueryable<AirBusScnDTO> AirBusScns
        {
            get { return _scnAppService.GetAirBusScns(); }
        }

        #endregion

        #region 序号件集合

        /// <summary>
        ///     序号件集合
        /// </summary>
        public IQueryable<SnRegDTO> SnRegs
        {
            get { return _snRegAppService.GetSnRegs(); }
        }

        /// <summary>
        ///     Apu、Engine的序号件集合
        /// </summary>
        public IQueryable<ApuEngineSnRegDTO> ApuEngineSnRegs
        {
            get { return _snRegAppService.GetApuEngineSnRegs(); }
        }

        #endregion

        #region 序号件装机历史集合

        /// <summary>
        ///     序号件装机历史集合
        /// </summary>
        public IQueryable<SnHistoryDTO> SnHistories
        {
            get { return _snHistoryAppService.GetSnHistories(); }
        }

        #endregion

        #region 序号件拆换记录集合

        /// <summary>
        ///     序号件拆换记录集合
        /// </summary>
        public IQueryable<SnRemInstRecordDTO> SnRemInstRecords
        {
            get { return _snRemInstRecordAppService.GetSnRemInstRecords(); }
        }

        #endregion

        #region 特定选型集合

        /// <summary>
        ///     特定选型集合
        /// </summary>
        public IQueryable<SpecialConfigDTO> SpecialConfigs
        {
            get { return _specialConfigAppService.GetSpecialConfigs(); }
        }

        #endregion

        #region 结构损伤集合

        /// <summary>
        ///     结构损伤集合
        /// </summary>
        public IQueryable<AirStructureDamageDTO> AirStructureDamages
        {
            get { return _airStructureDamageAppService.GetAirStructureDamages(); }
        }

        #endregion

        #region AdSb集合

        /// <summary>
        ///     AdSb集合
        /// </summary>
        public IQueryable<AdSbDTO> AdSbs
        {
            get { return _adSbAppService.GetAdSbs(); }
        }

        #endregion

        #region 年度送修计划集合
        public IQueryable<EngineMaintainPlanDTO> EngineMaintainPlans
        {
            get
            {
                return _annualMaintainPlanAppService.GetEngineMaintainPlans();
            }
        }

        public IQueryable<AircraftMaintainPlanDTO> AircraftMaintainPlans
        {
            get
            {
                return _annualMaintainPlanAppService.GetAircraftMaintainPlans();
                
            }
        }
        #endregion

        public IQueryable<UtilizationReportDTO> UtilizationReports
        {
            get { return null; }
        }
    }
}