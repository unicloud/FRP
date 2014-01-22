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

#region 命名空间

using System;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.ManufacturerAgg;

#endregion

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftTypeAgg
{
    /// <summary>
    ///     机型聚合根
    /// </summary>
    public class AircraftType : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftType()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     机型名称
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Description { get; internal set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     系列
        /// </summary>
        public Guid AircraftSeriesId { get; internal set; }

        /// <summary>
        ///     飞机座级
        /// </summary>
        public Guid AircraftCategoryId { get; internal set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public Guid ManufacturerId { get; internal set; }
        #endregion

        #region 导航属性
        /// <summary>
        /// 制造商
        /// </summary>
        public virtual Manufacturer Manufacturer { get; set; }

        /// <summary>
        ///     系列
        /// </summary>
        public virtual AircraftSeries AircraftSeries { get; set; }

        /// <summary>
        ///     飞机座级
        /// </summary>
        public virtual AircraftCategory AircraftCategory { get; set; }

        #endregion

        #region 操作

        #endregion
    }
}