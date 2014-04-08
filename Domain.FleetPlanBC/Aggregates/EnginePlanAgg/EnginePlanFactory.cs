#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 11:28:39
// 文件名：EnginePlanFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;

namespace UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg
{
    /// <summary>
    ///     备发计划工厂
    /// </summary>
    public static class EnginePlanFactory
    {
        /// <summary>
        ///     创建备发计划
        /// </summary>
        /// <param name="versionNumber">版本号</param>
        /// <returns>备发计划</returns>
        public static EnginePlan CreateEnginePlan(int versionNumber)
        {
            var enginePlan = new EnginePlan
            {
                VersionNumber = versionNumber,
                CreateDate = DateTime.Now,
            };

            enginePlan.GenerateNewIdentity();
            return enginePlan;
        }
    }
}
