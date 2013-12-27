#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 10:14:38
// 文件名：AirProgrammingFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg
{
    /// <summary>
    ///     航空公司五年规划工厂
    /// </summary>
    public static class AirProgrammingFactory
    {
        /// <summary>
        ///     创建航空公司五年规划
        /// </summary>
        /// <returns>航空公司五年规划</returns>
        public static AirProgramming CreateAirProgramming()
        {
            var airProgramming = new AirProgramming
            {
                CreateDate = DateTime.Now,
            };

            return airProgramming;
        }
    }
}
