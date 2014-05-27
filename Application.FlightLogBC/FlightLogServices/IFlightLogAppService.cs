//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.FlightLogBC.DTO;

namespace UniCloud.Application.FlightLogBC.FlightLogServices
{
    /// <summary>
    ///     飞行日志服务接口。
    /// </summary>
    public interface IFlightLogAppService
    {
        /// <summary>
        ///     获取所有飞行日志
        /// </summary>
        /// <returns></returns>
        IQueryable<FlightLogDTO> GetFlightLogs();


        /// <summary>
        ///    查询近一个月飞机的飞行数据
        /// </summary>
        /// <param name="regNumber"></param>
        /// <param name="flightDate"></param>
        /// <returns></returns>
        List<AcFlightDataDTO> QueryAcFlightData(string regNumber, DateTime flightDate);
    }
}