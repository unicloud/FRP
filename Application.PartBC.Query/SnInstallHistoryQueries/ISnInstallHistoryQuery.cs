#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/03，09:04
// 文件名：ISnInstallHistoryQuery.cs
// 程序集：UniCloud.Application.PartBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.SnInstallHistoryAgg;

#endregion

namespace UniCloud.Application.PartBC.Query.SnInstallHistoryQueries
{
    public interface ISnInstallHistoryQuery
    {
        /// <summary>
        ///     序号件装机历史查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>序号件装机历史DTO集合</returns>
        IQueryable<SnInstallHistoryDTO> SnInstallHistoryDTOQuery(
            QueryBuilder<SnInstallHistory> query);
    }
}