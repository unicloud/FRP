#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：IEngineQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.EngineQueries
{
    public interface IEngineQuery
    {
        /// <summary>
        ///     发动机查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>发动机DTO集合</returns>
        IQueryable<EngineDTO> EngineDTOQuery(
            QueryBuilder<Engine> query);
    }
}