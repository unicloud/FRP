#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：IAircraftCategoryQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.AircraftCategoryQueries
{
    public interface IAircraftCategoryQuery
    {
        /// <summary>
        ///     座级查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>座级DTO集合</returns>
        IQueryable<AircraftCategoryDTO> AircraftCategoryDTOQuery(
            QueryBuilder<AircraftCategory> query);
    }
}