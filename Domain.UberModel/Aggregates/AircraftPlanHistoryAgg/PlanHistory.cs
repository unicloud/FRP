#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 15:55:18
// 文件名：PlanHistory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.UberModel.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.AirlinesAgg;
using UniCloud.Domain.UberModel.Aggregates.AnnualAgg;
using UniCloud.Domain.UberModel.Aggregates.PlanAircraftAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AircraftPlanHistoryAgg
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
        public int SeatingCapacity { get; private set; }

        /// <summary>
        ///     商载量
        /// </summary>
        public decimal CarryingCapacity { get; private set; }

        /// <summary>
        ///     执行月份
        /// </summary>
        public int PerformMonth { get; private set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; internal set; }

        /// <summary>
        ///     是否提交
        /// </summary>
        public bool IsSubmit { get; internal set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; private set; }

        #endregion

        #region 外键属性
        /// <summary>
        ///     计划飞机外键
        /// </summary>
        public Guid? PlanAircraftId { get; private set; }

        /// <summary>
        ///     计划外键
        /// </summary>
        public Guid PlanId { get; internal set; }

        /// <summary>
        ///     活动类别：包括引进、退出、变更
        /// </summary>
        public Guid ActionCategoryId { get; private set; }

        /// <summary>
        /// 目标类别：具体的引进、退出方式
        /// </summary>
        public Guid TargetCategoryId { get; private set; }

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeId { get; private set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; private set; }

        /// <summary>
        ///     执行年度
        /// </summary>
        public Guid PerformAnnualId { get; private set; }

        /// <summary>
        /// 申请明细Id
        /// </summary>
        public Guid? ApprovalHistoryId { get; private set; }
        #endregion

        #region 导航属性

        /// <summary>
        /// 计划飞机
        /// </summary>
        public virtual PlanAircraft PlanAircraft { get; set; }

        /// <summary>
        /// 活动类别：包括引进、退出、变更
        /// </summary>
        public virtual ActionCategory ActionCategory { get; private set; }

        /// <summary>
        /// 目标类别：具体的引进、退出方式
        /// </summary>
        public virtual ActionCategory TargetCategory { get; private set; }

        /// <summary>
        /// 机型
        /// </summary>
        public virtual AircraftType AircraftType { get; private set; }

        /// <summary>
        /// 航空公司
        /// </summary>
        public virtual Airlines Airlines { get; private set; }

        /// <summary>
        /// 执行年度
        /// </summary>
        public virtual Annual PerformAnnual { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        /// 设置座位数
        /// </summary>
        /// <param name="seatingCapacity"></param>
        public void SetSeatingCapacity(int seatingCapacity)
        {
            SeatingCapacity = seatingCapacity;
        }

        /// <summary>
        /// 设置商载量
        /// </summary>
        /// <param name="carryingCapacity"></param>
        public void SetCarryingCapacity(decimal carryingCapacity)
        {
            CarryingCapacity = carryingCapacity;
        }

        /// <summary>
        /// 设置执行时间
        /// </summary>
        /// <param name="annual"></param>
        /// <param name="performMonth"></param>
        public void SetPerformDate(Annual annual, int performMonth)
        {
            if (annual == null || annual.IsTransient())
            {
                throw new ArgumentException("执行年度参数为空！");
            }

            PerformAnnualId = annual.Id;
            PerformAnnual = annual;
            PerformMonth = performMonth;
        }

        /// <summary>
        ///     设置备注
        /// </summary>
        /// <param name="note">备注</param>
        public void SetNote(string note)
        {
            //if (string.IsNullOrWhiteSpace(note))
            //{
            //    throw new ArgumentException("备注参数为空！");
            //}

            Note = note;
        }

        /// <summary>
        ///     设置计划飞机
        /// </summary>
        /// <param name="planAircraftId">计划飞机</param>
        public void SetPlanAircraft(Guid? planAircraftId)
        {
            PlanAircraftId = planAircraftId;
        }

        /// <summary>
        ///     设置活动类型
        /// </summary>
        /// <param name="actionCategory">活动类型</param>
        /// <param name="targetCategory">目标类别</param>
        public void SetActionCategory(ActionCategory actionCategory, ActionCategory targetCategory)
        {
            if (actionCategory == null || actionCategory.IsTransient())
            {
                throw new ArgumentException("活动类型参数为空！");
            }

            ActionCategory = actionCategory;
            ActionCategoryId = actionCategory.Id;

            if (targetCategory == null || targetCategory.IsTransient())
            {
                throw new ArgumentException("目标类别参数为空！");
            }

            TargetCategory = targetCategory;
            TargetCategoryId = targetCategory.Id;
        }

        /// <summary>
        ///     设置机型
        /// </summary>
        /// <param name="aircraftType">机型</param>
        public void SetAircraftType(AircraftType aircraftType)
        {
            if (aircraftType == null || aircraftType.IsTransient())
            {
                throw new ArgumentException("机型参数为空！");
            }

            AircraftType = aircraftType;
            AircraftTypeId = aircraftType.Id;
        }

        /// <summary>
        ///     设置航空公司
        /// </summary>
        /// <param name="airlines">航空公司</param>
        public void SetAirlines(Airlines airlines)
        {
            if (airlines == null || airlines.IsTransient())
            {
                throw new ArgumentException("航空公司参数为空！");
            }

            Airlines = airlines;
            AirlinesId = airlines.Id;
        }

        /// <summary>
        ///     设置申请明细
        /// </summary>
        /// <param name="approvalHistoryId">申请明细</param>
        public void SetApprovalHistory(Guid? approvalHistoryId)
        {
            ApprovalHistoryId = approvalHistoryId;
        }
        #endregion
    }
}
