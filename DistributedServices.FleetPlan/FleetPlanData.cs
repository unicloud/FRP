//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System.Linq;
using UniCloud.Application.FleetPlanBC.AircraftServices;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.DistributedServices.FleetPlan
{
    /// <summary>
    /// 运力规划模块数据类
    /// </summary>
    public class FleetPlanData : ExposeData.ExposeData
    {
        private readonly IAircraftAppService _aircraftAppService;

        public FleetPlanData()
            : base("UniCloud.Application.FleetPlanBC.DTO")
        {
            _aircraftAppService = DefaultContainer.Resolve<IAircraftAppService>();
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
    }
}