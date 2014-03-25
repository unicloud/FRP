#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/23 21:52:22
// 文件名：PlanHistoryFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg
{
    /// <summary>
    ///     飞机计划明细工厂
    /// </summary>
    public static class PlanHistoryFactory
    {
        /// <summary>
        ///     创建运力增减计划明细
        /// </summary>
        /// <param name="planId">计划</param>
        /// <returns>飞机计划明细</returns>
        public static PlanHistory CreatePlanHistory(Guid planId)
        {
            var planHistory = new PlanHistory
            {
                PlanId = planId,
            };

            planHistory.GenerateNewIdentity();
            return planHistory;
        }

        /// <summary>
        ///     新增飞机变更计划明细
        /// </summary>
        /// <param name="planId">计划</param>
        /// <returns></returns>
        public static PlanHistory CreateChangePlan(Guid planId)
        {
            var changePlan = new ChangePlan
            {
                PlanId = planId,
            };

            changePlan.GenerateNewIdentity();

            return changePlan;
        }

        /// <summary>
        ///     新增飞机运营计划明细
        /// </summary>
        /// <param name="planId">计划</param>
        /// <returns></returns>
        public static PlanHistory CreateOperationPlan(Guid planId)
        {
            var operationPlan = new OperationPlan
            {
                PlanId = planId,
            };

            operationPlan.GenerateNewIdentity();

            return operationPlan;
        }
    }
}
