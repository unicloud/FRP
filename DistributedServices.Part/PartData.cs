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
using UniCloud.Application.PartBC.AcDailyUtilizationServices;
using UniCloud.Application.PartBC.AircraftServices;
using UniCloud.Application.PartBC.AircraftTypeServices;
using UniCloud.Application.PartBC.BasicConfigGroupServices;
using UniCloud.Application.PartBC.ContractAircraftServices;
using UniCloud.Application.PartBC.CtrlUnitServices;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.MaintainCtrlServices;
using UniCloud.Application.PartBC.MaintainWorkServices;
using UniCloud.Application.PartBC.ModServices;
using UniCloud.Application.PartBC.PnRegServices;
using UniCloud.Application.PartBC.ScnServices;
using UniCloud.Application.PartBC.SnRegServices;
using UniCloud.Application.PartBC.SpecialConfigServices;
using UniCloud.Application.PartBC.TechnicalSolutionServices;
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
        private readonly IAircraftAppService _aircraftAppService;
        private readonly IAircraftTypeAppService _aircraftTypeAppService;
        private readonly IBasicConfigGroupAppService _basicConfigGroupAppService;
        private readonly IContractAircraftAppService _contractAircraftAppService;
        private readonly ICtrlUnitAppService _ctrlUnitAppService;
        private readonly IMaintainCtrlAppService _maintainCtrlAppService;
        private readonly IMaintainWorkAppService _maintainWorkAppService;
        private readonly IModAppService _modAppService;
        private readonly IPnRegAppService _pnRegAppService;
        private readonly IScnAppService _scnAppService;
        private readonly ISnRegAppService _snRegAppService;
        private readonly ISpecialConfigAppService _specialConfigAppService;
        private readonly ITechnicalSolutionAppService _technicalSolutionAppService;

        public PartData()
            : base("UniCloud.Application.PartBC.DTO")
        {
            _acDailyUtilizationAppService = DefaultContainer.Resolve<IAcDailyUtilizationAppService>();
            _aircraftAppService = DefaultContainer.Resolve<IAircraftAppService>();
            _aircraftTypeAppService = DefaultContainer.Resolve<IAircraftTypeAppService>();
            _basicConfigGroupAppService = DefaultContainer.Resolve<IBasicConfigGroupAppService>();
            _contractAircraftAppService = DefaultContainer.Resolve<IContractAircraftAppService>();
            _ctrlUnitAppService = DefaultContainer.Resolve<ICtrlUnitAppService>();
            _maintainCtrlAppService = DefaultContainer.Resolve<IMaintainCtrlAppService>();
            _maintainWorkAppService = DefaultContainer.Resolve<IMaintainWorkAppService>();
            _modAppService = DefaultContainer.Resolve<IModAppService>();
            _pnRegAppService = DefaultContainer.Resolve<IPnRegAppService>();
            _scnAppService = DefaultContainer.Resolve<IScnAppService>();
            _snRegAppService = DefaultContainer.Resolve<ISnRegAppService>();
            _specialConfigAppService = DefaultContainer.Resolve<ISpecialConfigAppService>();
            _technicalSolutionAppService = DefaultContainer.Resolve<ITechnicalSolutionAppService>();
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

        #endregion

        #region 序号件集合

        /// <summary>
        ///     序号件集合
        /// </summary>
        public IQueryable<SnRegDTO> SnRegs
        {
            get { return _snRegAppService.GetSnRegs(); }
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

        #region 技术解决方案集合

        /// <summary>
        ///     技术解决方案集合
        /// </summary>
        public IQueryable<TechnicalSolutionDTO> TechnicalSolutions
        {
            get { return _technicalSolutionAppService.GetTechnicalSolutions(); }
        }

        #endregion


    }
}