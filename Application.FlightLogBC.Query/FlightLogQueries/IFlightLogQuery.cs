#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/26 14:10:22
// 文件名：IFlightLogQuery
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/26 14:10:22
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Application.FlightLogBC.DTO;
using UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg;

#endregion

namespace UniCloud.Application.FlightLogBC.Query.FlightLogQueries
{
    public interface IFlightLogQuery
    {
        /// <summary>
        ///    飞行日志查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>飞行日志DTO集合</returns>
        IQueryable<FlightLogDTO> FlightLogDTOQuery(
            QueryBuilder<FlightLog> query);

        /// <summary>
        ///    查询近一个月飞机的飞行数据
        /// </summary>
        /// <param name="regNumber"></param>
        /// <param name="flightDate"></param>
        /// <returns></returns>
        List<AcFlightDataDTO> QueryAcFlightData(string regNumber, DateTime flightDate);

    }
}
