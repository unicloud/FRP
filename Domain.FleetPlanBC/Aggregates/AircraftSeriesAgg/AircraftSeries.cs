#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/4 10:12:06
// 文件名：AircraftSeries
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManufacturerAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftSeriesAgg
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
        public string Name { get; protected set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; protected set; }

        #endregion

        #region 外键属性

        /// <summary>
        /// 制造商外键
        /// </summary>
        public Guid ManufacturerId { get; protected set; }

        #endregion

        #region 导航属性
        /// <summary>
        /// 制造商
        /// </summary>
        public virtual Manufacturer Manufacturer { get; protected set; }
        #endregion

        #region 操作



        #endregion
    }
}
