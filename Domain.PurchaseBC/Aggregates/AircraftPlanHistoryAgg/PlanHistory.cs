#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/06/28，17:06
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
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PurchaseBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.AnnualAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.AircraftPlanHistoryAgg
{
    /// <summary>
    ///     飞机计划明细
    /// </summary>
    public class PlanHistory : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal PlanHistory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     座位数
        /// </summary>
        public int SeatingCapacity { get; internal set; }

        /// <summary>
        ///     商载量
        /// </summary>
        public decimal CarryingCapacity { get; internal set; }

        /// <summary>
        ///     执行月份
        /// </summary>
        public int PerformMonth { get; internal set; }

        /// <summary>
        ///     是否有效，确认计划时将计划相关条目置为有效，只有有效的条目才能执行。已有申请、批文的始终有效。
        /// </summary>
        public bool IsValid { get; internal set; }

        /// <summary>
        ///     是否提交
        /// </summary>
        public bool IsSubmit { get; internal set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; internal set; }

        /// <summary>
        ///     能否执行申请操作
        /// </summary>
        public CanRequest CanRequest { get; internal set; }

        /// <summary>
        ///     能否执行交付操作
        /// </summary>
        public CanDeliver CanDeliver { get; internal set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     计划外键
        /// </summary>
        public Guid PlanId { get; internal set; }

        /// <summary>
        ///     计划飞机外键
        /// </summary>
        public Guid? PlanAircraftId { get; internal set; }

        /// <summary>
        ///     目标类别：具体的引进、退出方式
        /// </summary>
        public Guid TargetCategoryId { get; internal set; }

        /// <summary>
        ///     执行年度
        /// </summary>
        public Guid PerformAnnualId { get; internal set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     目标类别：具体的引进、退出方式
        /// </summary>
        public virtual ActionCategory TargetCategory { get; internal set; }

        /// <summary>
        ///     执行年度
        /// </summary>
        public virtual Annual PerformAnnual { get; internal set; }

        #endregion
    }
}