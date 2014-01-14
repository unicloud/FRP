#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/4 10:32:14
// 文件名：AcType
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.UberModel.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.UberModel.Aggregates.ManufacturerAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AircraftSeriesAgg
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
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        /// 制造商外键
        /// </summary>
        public Guid ManufacturerId { get; set; }

        /// <summary>
        /// 座级外键
        /// </summary>
        public Guid AircraftCategoryId { get; set; }

        #endregion

        #region 导航属性
        /// <summary>
        /// 制造商
        /// </summary>
        public virtual Manufacturer Manufacturer { get; set; }

        /// <summary>
        /// 座级
        /// </summary>
        public virtual AircraftCategory AircraftCategory { get; set; }

        #endregion

        #region 操作



        #endregion
    }
}
