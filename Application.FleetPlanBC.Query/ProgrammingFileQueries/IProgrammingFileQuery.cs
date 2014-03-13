#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/03/13，14:03
// 文件名：IProgrammingFileQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingFileAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.ProgrammingFileQueries
{
    public interface IProgrammingFileQuery
    {
        /// <summary>
        ///     ProgrammingFile查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>ProgrammingFileDTO集合</returns>
        IQueryable<ProgrammingFileDTO> ProgrammingFileDTOQuery(
            QueryBuilder<ProgrammingFile> query);
    }
}