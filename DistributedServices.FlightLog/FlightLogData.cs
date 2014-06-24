//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

#region 命名空间

using System.Linq;
using UniCloud.Application.FlightLogBC.DTO;
using UniCloud.Application.FlightLogBC.FlightLogServices;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DistributedServices.FlightLog
{
    /// <summary>
    ///     飞行日志模块数据类
    /// </summary>
    public class FlightLogData : ExposeData.ExposeData
    {
        private readonly IFlightLogAppService _flightLogAppService;

        public FlightLogData()
            : base("UniCloud.Application.FlightLogBC.DTO")
        {
            _flightLogAppService = UniContainer.Resolve<IFlightLogAppService>();
        }

        #region 飞行日志

        /// <summary>
        ///     飞行日志集合
        /// </summary>
        public IQueryable<FlightLogDTO> FlightLogs
        {
            get { return _flightLogAppService.GetFlightLogs(); }
        }

        #endregion
    }
}