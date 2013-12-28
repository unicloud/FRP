#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 14:13:46
// 文件名：PlanEngineFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.PlanEngineAgg
{
    /// <summary>
    ///     计划发动机工厂
    /// </summary>
    public static class PlanEngineFactory
    {
        /// <summary>
        ///     创建计划发动机
        /// </summary>
        /// <returns>计划发动机</returns>
        public static PlanEngine CreatePlanEngine()
        {
            var planEngine = new PlanEngine
            {
            };

            return planEngine;
        }
    }
}
