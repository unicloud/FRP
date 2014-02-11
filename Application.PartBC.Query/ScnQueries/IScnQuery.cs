#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/10 14:08:45

// 文件名：IScnQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.ScnAgg;
#endregion

namespace UniCloud.Application.PartBC.Query.ScnQueries
{
    /// <summary>
    /// Scn查询接口
    /// </summary>
    public interface IScnQuery
    {
        /// <summary>
        ///     Scn查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>ScnDTO集合。</returns>
        IQueryable<ScnDTO> ScnDTOQuery(QueryBuilder<Scn> query);
    }
}
