//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.FlightLogBC.DTO;
using UniCloud.Application.FlightLogBC.Query.FlightLogQueries;
using UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg;

namespace UniCloud.Application.FlightLogBC.FlightLogServices
{
    /// <summary>
    ///     实现飞行日志服务接口。
    ///     用于处理飞行日志相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class FlightLogAppService : ContextBoundObject, IFlightLogAppService
    {
        private readonly IFlightLogQuery _flightLogQuery;

        public FlightLogAppService(IFlightLogQuery flightLogQuery)
        {
            _flightLogQuery = flightLogQuery;
        }

        #region FlightLogDTO

        /// <summary>
        ///     获取所有飞行日志
        /// </summary>
        /// <returns></returns>
        public IQueryable<FlightLogDTO> GetFlightLogs()
        {
            var queryBuilder =
                new QueryBuilder<FlightLog>();
            return _flightLogQuery.FlightLogDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     查询近一个月飞机的飞行数据
        /// </summary>
        /// <param name="regNumber"></param>
        /// <param name="flightDate"></param>
        /// <returns></returns>
        public List<AcFlightDataDTO> QueryAcFlightData(string regNumber, DateTime flightDate)
        {
            return _flightLogQuery.QueryAcFlightData(regNumber, flightDate);
        }

        #endregion
    }
}