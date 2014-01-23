#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：Domain.AircraftConfigBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System;

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftTypeAgg
{
    /// <summary>
    ///     机型工厂
    /// </summary>
    public static class AircraftTypeFactory
    {
        /// <summary>
        /// 新建机型
        /// </summary>
        /// <returns></returns>
        public static AircraftType CreateAircraftType()
        {
            var aircraftType = new AircraftType();
            aircraftType.ChangeCurrentIdentity(Guid.NewGuid());
            return aircraftType;
        }

        /// <summary>
        /// 设置机型属性
        /// </summary>
        /// <param name="aircraftType">当前机型</param>
        /// <param name="name">机型名称</param>
        /// <param name="description">描述</param>
        /// <param name="aircraftSeriesId">飞机系列</param>
        /// <param name="manufacturerId">制造商</param>
        /// <param name="aircraftCategoryId">座级</param>
        public static void SetAircraftType(AircraftType aircraftType, string name, string description, Guid aircraftCategoryId, Guid aircraftSeriesId, Guid manufacturerId)
        {
            aircraftType.Name = name;
            aircraftType.Description = description;
            aircraftType.AircraftCategoryId = aircraftCategoryId;
            aircraftType.AircraftSeriesId = aircraftSeriesId;
            aircraftType.ManufacturerId = manufacturerId;
        }
    }
}