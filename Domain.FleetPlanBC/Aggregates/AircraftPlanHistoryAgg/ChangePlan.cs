#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:50:46
// 文件名：ChangePlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg
{
    /// <summary>
    ///     变更计划聚合根
    /// </summary>
    public class ChangePlan : PlanHistory
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ChangePlan()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     商业数据历史外键
        /// </summary>
        public Guid? AircraftBusinessId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     商业数据历史
        /// </summary>
        public virtual AircraftBusiness AircraftBusiness { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置商业数据历史
        /// </summary>
        /// <param name="aircraftBusiness">商业数据历史</param>
        public void SetAircraftBusiness(AircraftBusiness aircraftBusiness)
        {
            AircraftBusiness = AircraftBusiness;
            AircraftBusinessId = aircraftBusiness.Id;
        }

        #endregion
    }
}