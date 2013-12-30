//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System.Linq;
using UniCloud.Application.FleetPlanBC.AircraftServices;
using UniCloud.Application.FleetPlanBC.DTO;
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
        private readonly IAircraftAppService _aircraftAppService;
        private readonly IXmlConfigAppService _xmlConfigAppService;
        private readonly IXmlSettingAppService _xmlSettingAppService;

        public FleetPlanData()
            : base("UniCloud.Application.FleetPlanBC.DTO")
        {
            _aircraftAppService = DefaultContainer.Resolve<IAircraftAppService>();
            _xmlConfigAppService = DefaultContainer.Resolve<IXmlConfigAppService>();
            _xmlSettingAppService = DefaultContainer.Resolve<IXmlSettingAppService>();
        }

        #region 实际飞机
        /// <summary>
        ///     实际飞机集合
        /// </summary>
        public IQueryable<AircraftDTO> Aircrafts
        {
            get { return _aircraftAppService.GetAircrafts(); }
        }
        #endregion

        #region 发动机
        /// <summary>
        ///     发动机集合
        /// </summary>
        public IQueryable<EngineDTO> Engines
        {
            get { return null; }
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