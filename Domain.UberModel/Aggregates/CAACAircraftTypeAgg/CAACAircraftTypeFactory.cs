﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/15，11:12
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.CAACAircraftTypeAgg
{
    /// <summary>
    ///     民航机型工厂
    /// </summary>
    public static class CAACAircraftTypeFactory
    {
        /// <summary>
        ///     创建民航机型
        /// </summary>
        /// <param name="id">机型ID</param>
        /// <param name="name">机型名称</param>
        /// <param name="manufacturerId">制造商</param>
        /// <param name="aircraftCategoryId">座级</param>
        /// <returns></returns>
        public static CAACAircraftType CreateAircraftType(Guid id, string name, Guid manufacturerId, Guid aircraftCategoryId)
        {
            var aircraftType = new CAACAircraftType { Name = name };
            aircraftType.ChangeCurrentIdentity(id);
            aircraftType.ManufacturerId = manufacturerId;
            aircraftType.AircraftCategoryId = aircraftCategoryId;
            return aircraftType;
        }
    }
}