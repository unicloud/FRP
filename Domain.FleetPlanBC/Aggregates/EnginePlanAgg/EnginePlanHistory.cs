#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:44:54
// 文件名：EnginePlanHistory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanEngineAgg;
using UniCloud.Domain.FleetPlanBC.Enums;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg
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
        public virtual EngineType EngineType { get; set; }

        /// <summary>
        /// 执行年度
        /// </summary>
        public virtual Annual PerformAnnual { get; set; }

        /// <summary>
        /// 计划发动机
        /// </summary>
        public virtual PlanEngine PlanEngine { get; set; }

        /// <summary>
        ///   活动类型
        /// </summary>
        public virtual ActionCategory ActionCategory { get; set; }

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
        /// <param name="annualId"></param>
        /// <param name="performMonth"></param>
        public void SetPerformDate(Guid annualId, int performMonth)
        {
            if (annualId == null)
            {
                throw new ArgumentException("执行年度Id参数为空！");
            }
            PerformAnnualId = annualId;
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
        ///     设置发动机型号
        /// </summary>
        /// <param name="engineTypeId">发动机型号</param>
        public void SetEngineType(Guid engineTypeId)
        {
            if (engineTypeId == null)
            {
                throw new ArgumentException("发动机型号Id参数为空！");
            }

            EngineTypeId = engineTypeId;
        }

        /// <summary>
        ///     设置计划发动机
        /// </summary>
        /// <param name="planEngineId">计划发动机</param>
        public void SetPlanEngine(Guid? planEngineId)
        {
            PlanEngineId = planEngineId;
        }

        /// <summary>
        ///     设置活动类型
        /// </summary>
        /// <param name="actionCategoryId">活动类型</param>
        public void SetActionCategory(Guid actionCategoryId)
        {
            if (actionCategoryId == null)
            {
                throw new ArgumentException("活动类型Id参数为空！");
            }
            ActionCategoryId = actionCategoryId;
        }
        #endregion
    }
}
