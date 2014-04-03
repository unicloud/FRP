#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/03，09:04
// 文件名：IBasicConfigHistoryQuery.cs
// 程序集：UniCloud.Application.PartBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigHistoryAgg;

#endregion

namespace UniCloud.Application.PartBC.Query.BasicConfigHistoryQueries
{
    public interface IBasicConfigHistoryQuery
    {
        /// <summary>
        ///     BasicConfigHistory查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>BasicConfigHistoryDTO集合</returns>
        IQueryable<BasicConfigHistoryDTO> BasicConfigHistoryDTOQuery(
            QueryBuilder<BasicConfigHistory> query);
    }
}