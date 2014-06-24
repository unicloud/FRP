#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：DistributedServices.FlightLog
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FlightLogBC.DTO;
using UniCloud.Application.FlightLogBC.FlightLogServices;
using UniCloud.DistributedServices.Data;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DistributedServices.FlightLog
{
    /// <summary>
    ///     飞行日志模块数据类
    /// </summary>
    public class FlightLogData : ServiceData
    {
        private readonly IFlightLogAppService _flightLogAppService;

        public FlightLogData()
            : base("UniCloud.Application.FlightLogBC.DTO", UniContainer.Resolve<IQueryableUnitOfWork>())
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