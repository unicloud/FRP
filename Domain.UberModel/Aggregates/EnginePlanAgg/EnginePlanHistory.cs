#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 16:39:22
// 文件名：EnginePlanHistory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.UberModel.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.UberModel.Aggregates.AnnualAgg;
using UniCloud.Domain.UberModel.Aggregates.EngineTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.PlanEngineAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.EnginePlanAgg
{
    /// <summary>
    ///     发动机计划明细
    /// </summary>
    public class EnginePlanHistory : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EnginePlanHistory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     执行月份
        /// </summary>
        public int PerformMonth { get; private set; }

        /// <summary>
        ///     执行情况
        /// </summary>
        public EnginePlanDeliverStatus Status { get; private set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsFinished { get; private set; }

        /// <summary>
        ///     最大推力
        /// </summary>
        public decimal MaxThrust { get; private set; }

        /// <summary>
        /// 实际引进日期
        /// </summary>
        public DateTime? ImportDate { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; private set; }
        #endregion

        #region 外键属性

        /// <summary>
        ///     发动机型号
        /// </summary>
        public Guid EngineTypeId { get; private set; }

        /// <summary>
        ///     发动机计划外键
        /// </summary>
        public Guid EnginePlanId { get; internal set; }

        /// <summary>
        ///     执行年度
        /// </summary>
        public Guid PerformAnnualId { get; private set; }

        /// <summary>
        ///     计划发动机ID
        /// </summary>
        public Guid? PlanEngineId { get; private set; }

        /// <summary>
        ///     活动类型
        /// </summary>
        public Guid ActionCategoryId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///   发动机型号
        /// </summary>
        public virtual EngineType EngineType { get; private set; }

        /// <summary>
        /// 执行年度
        /// </summary>
        public virtual Annual PerformAnnual { get; private set; }

        /// <summary>
        /// 计划发动机
        /// </summary>
        public virtual PlanEngine PlanEngine { get; set; }

        /// <summary>
        ///   活动类型
        /// </summary>
        public virtual ActionCategory ActionCategory { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置执行情况
        /// </summary>
        /// <param name="status">执行情况</param>
        public void SetPlanStatus(EnginePlanDeliverStatus status)
        {
            switch (status)
            {
                case EnginePlanDeliverStatus.预备:
                    Status = EnginePlanDeliverStatus.预备;
                    break;
                case EnginePlanDeliverStatus.签约:
                    Status = EnginePlanDeliverStatus.签约;
                    break;
                case EnginePlanDeliverStatus.接收:
                    Status = EnginePlanDeliverStatus.接收;
                    IsFinished = true;
                    break;
                case EnginePlanDeliverStatus.运营:
                    Status = EnginePlanDeliverStatus.运营;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
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

            PerformAnnual = annual;
            PerformAnnualId = annual.Id;
            PerformMonth = performMonth;
        }

        /// <summary>
        /// 设置发动机最大推力
        /// </summary>
        /// <param name="maxThrust"></param>
        public void SetMaxThrust(decimal maxThrust)
        {
            MaxThrust = maxThrust;
        }

        /// <summary>
        ///     设置发动机实际引进日期
        /// </summary>
        /// <param name="date"></param>
        public void SetImportDate(DateTime? date)
        {
            ImportDate = date;
        }


        /// <summary>
        ///     设置备注
        /// </summary>
        /// <param name="note"></param>
        public void SetNote(string note)
        {
            Note = note;
        }

        /// <summary>
        ///     设置发动机型号
        /// </summary>
        /// <param name="engineType">发动机型号</param>
        public void SetEngineType(EngineType engineType)
        {
            if (engineType == null || engineType.IsTransient())
            {
                throw new ArgumentException("发动机型号参数为空！");
            }

            EngineType = engineType;
            EngineTypeId = engineType.Id;
        }

        /// <summary>
        ///     设置计划发动机
        /// </summary>
        /// <param name="planEngineId">计划发动机Id</param>
        public void SetPlanEngine(Guid? planEngineId)
        {
            PlanEngineId = planEngineId;
        }

        /// <summary>
        ///     设置活动类型
        /// </summary>
        /// <param name="actionCategory">活动类型</param>
        public void SetActionCategory(ActionCategory actionCategory)
        {
            if (actionCategory == null)
            {
                throw new ArgumentException("活动类型Id参数为空！");
            }
            ActionCategory = actionCategory;
            ActionCategoryId = actionCategory.Id;
        }
        #endregion
    }
}
