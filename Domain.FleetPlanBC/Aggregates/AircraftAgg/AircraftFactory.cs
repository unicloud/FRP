#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:39:02
// 文件名：AircraftFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg
{
    /// <summary>
    ///     “实际运营飞机”工厂
    /// </summary>
    public static class AircraftFactory
    {
        /// <summary>
        ///     创建运营飞机
        /// </summary>
        /// <returns>运营飞机</returns>
        public static Aircraft CreateAircraft()
        {
            var aircraft = new Aircraft
            {
                CreateDate = DateTime.Now,
            };

            return aircraft;
        }

    }
}
