#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/4 10:44:06
// 文件名：AircraftSeriesFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AircraftSeriesAgg
{
    /// <summary>
    ///     飞机系列工厂
    /// </summary>
    public static class AircraftSeriesFactory
    {
        /// <summary>
        ///     创建飞机系列
        /// </summary>
        /// <param name="id">飞机系列ID</param>
        /// <param name="name">飞机系列名称</param>
        /// <param name="manufacturerId">制造商</param>
        /// <returns></returns>
        public static AircraftSeries CreateAircraftSeries(Guid id, string name, Guid manufacturerId)
        {
            var aircraftSerise = new AircraftSeries { Name = name };
            aircraftSerise.ChangeCurrentIdentity(id);
            aircraftSerise.ManufacturerId = manufacturerId;

            return aircraftSerise;
        }
    }
}
