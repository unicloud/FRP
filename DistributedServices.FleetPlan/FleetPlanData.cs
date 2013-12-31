//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System.Linq;
using UniCloud.Application.FleetPlanBC.AircraftServices;
using UniCloud.Application.FleetPlanBC.ApprovalDocServices;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.DTO.ApporvalDocDTO;
using UniCloud.Application.FleetPlanBC.DTO.EngineDTO;
using UniCloud.Application.FleetPlanBC.RequestServices;
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
        private readonly IRequestAppService _requestAppService;
        private readonly IApprovalDocAppService _approvalDocAppService;
        public FleetPlanData()
            : base("UniCloud.Application.FleetPlanBC.DTO")
        {
            _aircraftAppService = DefaultContainer.Resolve<IAircraftAppService>();
            _xmlConfigAppService = DefaultContainer.Resolve<IXmlConfigAppService>();
            _xmlSettingAppService = DefaultContainer.Resolve<IXmlSettingAppService>();
            _requestAppService = DefaultContainer.Resolve<IRequestAppService>();
            _approvalDocAppService = DefaultContainer.Resolve<IApprovalDocAppService>();
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

        #region 申请

        /// <summary>
        ///     申请集合
        /// </summary>
        public IQueryable<RequestDTO> Requests
        {
            get { return _requestAppService.GetRequests(); }
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
    }
}