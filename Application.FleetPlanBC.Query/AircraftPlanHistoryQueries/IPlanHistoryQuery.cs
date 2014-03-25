#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/03/23，23:03
// 文件名：IPlanHistoryQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.PlanHistoryQueries
{
    public interface IPlanHistoryQuery
    {
        /// <summary>
        ///     计划明细查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>计划明细DTO集合</returns>
        IQueryable<PlanHistoryDTO> PlanHistoryDTOQuery(
            QueryBuilder<PlanHistory> query);
    }
}