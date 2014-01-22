#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：IActionCategoryQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Domain.AircraftConfigBC.Aggregates.ActionCategoryAgg;

#endregion

namespace UniCloud.Application.AircraftConfigBC.Query.ActionCategoryQueries
{
    public interface IActionCategoryQuery
    {
        /// <summary>
        ///     活动类型查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>活动类型DTO集合</returns>
        IQueryable<ActionCategoryDTO> ActionCategoryDTOQuery(
            QueryBuilder<ActionCategory> query);
    }
}