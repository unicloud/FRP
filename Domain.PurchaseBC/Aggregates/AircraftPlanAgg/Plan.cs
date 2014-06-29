#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：0:52
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
using UniCloud.Domain.PurchaseBC.Aggregates.AnnualAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.AircraftPlanAgg
{
    /// <summary>
    ///     飞机计划聚合根
    /// </summary>
    public class Plan : EntityGuid
    {
        #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Plan()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     计划标题
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        ///     是否有效版本，通过审核的计划均为有效
        /// </summary>
        public bool IsValid { get; internal set; }

        /// <summary>
        ///     版本号，同年度计划中版本号最高的为那一年度的“当前计划”
        /// </summary>
        public int VersionNumber { get; internal set; }

        /// <summary>
        ///     是否当前版本，民航局系统中设置
        /// </summary>
        public bool IsCurrentVersion { get; internal set; }

        /// <summary>
        ///     提交日期
        /// </summary>
        public DateTime? SubmitDate { get; internal set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     是否完成，计划是否完成评审流程，计划发送后设为完成
        /// </summary>
        public bool IsFinished { get; internal set; }

        /// <summary>
        ///     计划编辑处理状态
        /// </summary>
        public PlanStatus Status { get; internal set; }

        /// <summary>
        ///     发布计划处理状态
        /// </summary>
        public PlanPublishStatus PublishStatus { get; internal set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     计划年度外键
        /// </summary>
        public Guid AnnualId { get; internal set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     计划年度
        /// </summary>
        public virtual Annual Annual { get; internal set; }

        #endregion
    }
}