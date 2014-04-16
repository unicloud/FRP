#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/15，23:04
// 文件名：ISnRemInstRecordQuery.cs
// 程序集：UniCloud.Application.PartBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.SnRemInstRecordAgg;

#endregion

namespace UniCloud.Application.PartBC.Query.SnRemInstRecordQueries
{
    public interface ISnRemInstRecordQuery
    {
        /// <summary>
        ///     拆装记录查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>拆装记录DTO集合</returns>
        IQueryable<SnRemInstRecordDTO> SnRemInstRecordDTOQuery(
            QueryBuilder<SnRemInstRecord> query);
    }
}