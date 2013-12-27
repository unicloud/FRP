#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 11:25:08
// 文件名：PlanFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg
{
    /// <summary>
    ///     飞机计划工厂
    /// </summary>
    public static class PlanFactory
    {
        /// <summary>
        ///     创建运力增减计划
        /// </summary>
        /// <param name="versionNumber">版本号</param>
        /// <param name="submitDate">提交日期</param>
        /// <returns>飞机计划</returns>
        public static Plan CreatePlan(int versionNumber,DateTime submitDate)
        {
            var plan = new Plan
            {
                VersionNumber = versionNumber,
                CreateDate = DateTime.Now,
                SubmitDate = submitDate,
            };

            return plan;
        }
    }
}
