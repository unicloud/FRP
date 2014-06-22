#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/06/21，09:06
// 方案：FRP
// 项目：Domain.PortalBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.PortalBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.PortalBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.PortalBC.Aggregates.ManufacturerAgg;

#endregion

namespace UniCloud.Domain.PortalBC.Aggregates.CAACAircraftTypeAgg
{
    /// <summary>
    ///     民航机型聚合根
    /// </summary>
    public class CAACAircraftType : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal CAACAircraftType()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     机型名称
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Description { get; protected set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     系列
        /// </summary>
        public Guid AircraftSeriesId { get; protected set; }

        /// <summary>
        ///     飞机类别
        /// </summary>
        public Guid AircraftCategoryId { get; protected set; }

        /// <summary>
        ///     制造商
        /// </summary>
        public Guid ManufacturerId { get; protected set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     制造商
        /// </summary>
        public virtual Manufacturer Manufacturer { get; protected set; }

        /// <summary>
        ///     系列
        /// </summary>
        public virtual AircraftSeries AircraftSeries { get; protected set; }

        /// <summary>
        ///     飞机类别
        /// </summary>
        public virtual AircraftCategory AircraftCategory { get; protected set; }

        #endregion

        #region 操作

        #endregion
    }
}