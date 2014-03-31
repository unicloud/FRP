#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/01/04，11:01
// 文件名：IAircraftSeriesQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftSeriesAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.AircraftSeriesQueries
{
    public interface IAircraftSeriesQuery
    {
        /// <summary>
        ///     飞机系列查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>飞机系列DTO集合</returns>
        IQueryable<AircraftSeriesDTO> AircraftSeriesDTOQuery(
            QueryBuilder<AircraftSeries> query);
    }
}