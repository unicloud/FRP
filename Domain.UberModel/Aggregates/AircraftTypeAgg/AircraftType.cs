﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
// 文件名：AircraftType.cs
// 程序集：UniCloud.Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.UberModel.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.UberModel.Aggregates.CAACAircraftTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.ManufacturerAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AircraftTypeAgg
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
        public string Name { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Description { get; set; }


        #endregion

        #region 外键属性

        /// <summary>
        ///     系列
        /// </summary>
        public Guid AircraftSeriesId { get; set; }

        /// <summary>
        ///     飞机类别
        /// </summary>
        public Guid AircraftCategoryId { get; set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public Guid ManufacturerId { get;  set; }

        /// <summary>
        /// 民航机型
        /// </summary>
        public Guid CaacAircraftTypeId { get; internal set; }
        #endregion

        #region 导航属性
        /// <summary>
        /// 民航机型
        /// </summary>
        public virtual CAACAircraftType CaacAircraftType { get; set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public virtual Manufacturer Manufacturer { get; set; }
        /// <summary>
        /// 系列
        /// </summary>
        public virtual AircraftSeries AircraftSeries { get; set; }

        /// <summary>
        /// 飞机类别
        /// </summary>
        public virtual AircraftCategory AircraftCategory { get; set; }

        #endregion

        #region 操作

        #endregion
    }
}