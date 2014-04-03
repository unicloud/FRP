#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/04，21:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg
{
    /// <summary>
    ///     采购物料聚合根
    ///     飞机物料
    /// </summary>
    public class AircraftMaterial : Material
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftMaterial()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     机型ID
        /// </summary>
        public Guid AircraftTypeId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     机型
        /// </summary>
        public virtual AircraftType AircraftType { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置机型
        /// </summary>
        /// <param name="aircraftType">机型</param>
        public void SetAircraftType(AircraftType aircraftType)
        {
            if (aircraftType == null || aircraftType.IsTransient())
            {
                throw new ArgumentException("机型参数为空！");
            }

            AircraftType = aircraftType;
            AircraftTypeId = aircraftType.Id;
        }

        /// <summary>
        ///     设置机型外键。
        /// </summary>
        /// <param name="aircraftTypeId">机型外键</param>
        public void SetAircraftTypeId(Guid aircraftTypeId)
        {
            AircraftTypeId = aircraftTypeId;
        }

        #endregion
    }
}