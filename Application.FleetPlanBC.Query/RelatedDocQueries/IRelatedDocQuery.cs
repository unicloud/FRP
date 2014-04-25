#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/10 22:07:24
// 文件名：IRelatedDocQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.RelatedDocAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.RelatedDocQueries
{
    /// <summary>
    ///     关联文档查询接口
    /// </summary>
    public interface IRelatedDocQuery
    {
        /// <summary>
        ///     关联文档查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>关联文档DTO集合。</returns>
        IQueryable<RelatedDocDTO> RelatedDocDTOQuery(
            QueryBuilder<RelatedDoc> query);
    }
}