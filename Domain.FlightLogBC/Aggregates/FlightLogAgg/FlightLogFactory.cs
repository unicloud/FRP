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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg
{
    /// <summary>
    /// FlightLog工厂。
    /// </summary>
    public static class FlightLogFactory
    {
        /// <summary>
        /// 创建FlightLog。
        /// </summary>
        ///  <returns>FlightLog</returns>
        public static FlightLog CreateFlightLog()
        {
            var flightLog = new FlightLog
            {
            };
            flightLog.GenerateNewIdentity();
            return flightLog;
        }

        ///// <summary>
        ///// 创建飞机日利用率
        ///// </summary>
        ///// <param name="aircraft">运营飞机</param>
        ///// <param name="amendValue">修正日利用率</param>
        ///// <param name="calculatedValue">计算日利用率</param>
        ///// <param name="isCurrent">是否当前</param>
        ///// <param name="month">月份</param>
        ///// <param name="regNumber">飞机注册号</param>
        ///// <param name="year">年度</param>
        ///// <returns></returns>
        //public static FlightLog CreateFlightLog(string acReg, int apuCycle, int apuMm,
        //    bool isCurrent,int month,string regNumber,int year)
        //{
        //    var flightLog = new FlightLog
        //    {
        //    };
        //    flightLog.GenerateNewIdentity();
        //    flightLog.AcReg = acReg;
        //    flightLog.ApuCycle=1;
        //    flightLog.ApuMM = apuMm;
        //    flightLog.ApuOilArr(isCurrent);
        //    flightLog.ApuOilDep(month);
        //    flightLog.ArrivalAirport(regNumber);
        //    flightLog.DepartureAirport(year);
        //    flightLog.BlockHours=
        //    return flightLog;
        //}
    }
}
