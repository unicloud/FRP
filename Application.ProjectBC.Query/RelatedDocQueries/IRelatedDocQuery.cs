#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/09，10:47
// 方案：FRP
// 项目：Application.ProjectBC.Query
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.ProjectBC.DTO;
using UniCloud.Domain.ProjectBC.Aggregates.RelatedDocAgg;

#endregion

namespace UniCloud.Application.ProjectBC.Query.RelatedDocQueries
{
    public interface IRelatedDocQuery
    {
        /// <summary>
        ///     查询关联文档
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>关联文档集合</returns>
        IQueryable<RelatedDocDTO> RelatedDocDTOQuery(QueryBuilder<RelatedDoc> query);
    }
}