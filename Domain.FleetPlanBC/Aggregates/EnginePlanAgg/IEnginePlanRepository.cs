﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:07:48
// 文件名：IEnginePlanRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg
{
    /// <summary>
    ///     备发计划仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{EnginePlan}" />
    /// </summary>
    public interface IEnginePlanRepository : IRepository<EnginePlan>
    {
        /// <summary>
        /// 删除备发计划
        /// </summary>
        /// <param name="enginePlan"></param>
        void DeleteEnginePlan(EnginePlan enginePlan);

        /// <summary>
        /// 移除备发计划明细    
        /// </summary>
        /// <param name="eph"></param>
        void RemoveEnginePlanHistory(EnginePlanHistory eph);
    }
}
