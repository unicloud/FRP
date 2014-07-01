#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：10:45
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.ManufacturerAgg
{
    /// <summary>
    ///     制造商工厂
    /// </summary>
    public static class ManufacturerFactory
    {
        /// <summary>
        ///     创建制造商
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="cnShortName">制造商简称</param>
        /// <param name="type">制造商类型</param>
        /// <returns></returns>
        public static Manufacturer CreateManufacturer(Guid id, string cnShortName, int type)
        {
            var manufacturer = new Manufacturer
            {
                CnName = cnShortName,
                CnShortName = cnShortName,
                Type = type,
            };
            manufacturer.ChangeCurrentIdentity(id);

            return manufacturer;
        }
    }
}