#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 9:39:20
// 文件名：AircraftType
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManufacturerAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg
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
        public string Name { get; protected set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Description { get; protected set; }


        #endregion

        #region 外键属性

        /// <summary>
        ///     制造商
        /// </summary>
        public Guid ManufacturerId { get; protected set; }

        /// <summary>
        ///     飞机类别
        /// </summary>
        public Guid AircraftCategoryId { get; protected set; }

        #endregion

        #region 导航属性
        /// <summary>
        /// 制造商
        /// </summary>
        public virtual Manufacturer Manufacturer { get; set; }

        /// <summary>
        /// 飞机类别
        /// </summary>
        public virtual AircraftCategory AircraftCategory { get; set; }

        #endregion

        #region 操作

        #endregion
    }
}
