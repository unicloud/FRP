#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/27 14:05:24
// 文件名：FlightLogFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg
{
    /// <summary>
    ///     FlightLog工厂。
    /// </summary>
    public static class FlightLogFactory
    {
        /// <summary>
        ///     创建飞行日志。
        /// </summary>
        /// <param name="acReg">机号</param>
        /// <param name="msn">飞机序列号</param>
        /// <param name="flightNum">航班号</param>
        /// <param name="flightDate">航班日期</param>
        /// <param name="departure">出发机场</param>
        /// <param name="arrival">到达机场</param>
        /// <returns>飞行日志</returns>
        public static FlightLog CreateFlightLog(string acReg, string msn, string flightNum, DateTime flightDate,
            string departure, string arrival)
        {
            var flightLog = new FlightLog
            {
                AcReg = acReg,
                MSN = msn,
                FlightNum = flightNum,
                FlightDate = flightDate,
                DepartureAirport = departure,
                ArrivalAirport = arrival,
                CreateDate = DateTime.Now
            };
            flightLog.GenerateNewIdentity();
            return flightLog;
        }
    }
}