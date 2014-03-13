#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/03/13，15:03
// 文件名：IAircraftConfigurationQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftConfigurationAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.AircraftConfigurationQueries
{
    public interface IAircraftConfigurationQuery
    {
        /// <summary>
        ///     飞机配置查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>飞机配置DTO集合</returns>
        IQueryable<AircraftConfigurationDTO> AircraftConfigurationDTOQuery(
            QueryBuilder<AircraftConfiguration> query);
    }
}