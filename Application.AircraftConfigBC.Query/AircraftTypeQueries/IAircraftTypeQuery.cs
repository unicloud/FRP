#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：IAircraftTypeQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.CAACAircraftTypeAgg;

#endregion

namespace UniCloud.Application.AircraftConfigBC.Query.AircraftTypeQueries
{
    public interface IAircraftTypeQuery
    {
        /// <summary>
        ///     机型查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>机型DTO集合</returns>
        IQueryable<AircraftTypeDTO> AircraftTypeDTOQuery(
            QueryBuilder<AircraftType> query);

        /// <summary>
        ///     民航机型查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>民航机型DTO集合</returns>
        IQueryable<CAACAircraftTypeDTO> CAACAircraftTypeDTOQuery(
            QueryBuilder<CAACAircraftType> query);
    }
}