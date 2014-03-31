#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：IPlanEngineQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanEngineAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.PlanEngineQueries
{
    public interface IPlanEngineQuery
    {
        /// <summary>
        ///     计划发动机查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>计划发动机DTO集合</returns>
        IQueryable<PlanEngineDTO> PlanEngineDTOQuery(
            QueryBuilder<PlanEngine> query);
    }
}