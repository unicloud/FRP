#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/4 10:26:36
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



namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftSeriesAgg
{
    /// <summary>
    ///     飞机系列工厂
    /// </summary>
    public static class AircraftSeriesFactory
    {
        /// <summary>
        /// 新建飞机系列
        /// </summary>
        /// <returns></returns>
        public static AircraftSeries CreateAircraftSeries()
        {
            var aircraftSeries = new AircraftSeries();
            return aircraftSeries;
        }

        /// <summary>
        /// 设置系列属性
        /// </summary>
        /// <param name="aircraftSeries">当前系列</param>
        /// <param name="name">系列名称</param>
        /// <param name="description">描述</param>
        /// <param name="manufacturerId">制造商</param>
        public static void SetAircraftSeries(AircraftSeries aircraftSeries, string name, string description, Guid manufacturerId)
        {
            aircraftSeries.Name = name;
            aircraftSeries.Description = description;
            aircraftSeries.ManufacturerId = manufacturerId;
        }
    }
}
