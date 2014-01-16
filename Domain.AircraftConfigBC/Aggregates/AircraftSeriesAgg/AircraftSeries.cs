#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/4 10:26:13
// 文件名：AircraftSeries
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AtaAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.ManufacturerAgg;

#endregion

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftSeriesAgg
{
    /// <summary>
    ///     飞机系列聚合根
    /// </summary>
    public class AircraftSeries : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftSeries()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 系列
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// 章节
        /// </summary>
        private HashSet<Ata> _atas;
        public virtual ICollection<Ata> Atas
        {
            get { return _atas ?? (_atas = new HashSet<Ata>()); }
            set { _atas = new HashSet<Ata>(value); }
        }
        #endregion

        #region 外键属性

        /// <summary>
        /// 制造商外键
        /// </summary>
        public Guid ManufacturerId { get; internal set; }

        #endregion

        #region 导航属性
        /// <summary>
        /// 制造商
        /// </summary>
        public virtual Manufacturer Manufacturer { get; set; }

        #endregion

        #region 操作



        #endregion
    }
}
