#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/10，10:12
// 文件名：ISupplierQuery.cs
// 程序集：UniCloud.Application.CommonServiceBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion


#region 命名空间

using System.Linq;
using UniCloud.Application.CommonServiceBC.DTO;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentPathAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentTypeAgg;

#endregion

namespace UniCloud.Application.CommonServiceBC.Query.DocumentQueries
{
    /// <summary>
    ///    文档查询接口。
    /// </summary>
    public interface IDocumentQuery
    {
        /// <summary>
        ///     查询文档。
        /// </summary>
        /// <param name="query">查询条件。</param>
        /// <returns>文档。</returns>
        IQueryable<DocumentDTO> DocumentsQuery(
            QueryBuilder<Document> query);

        /// <summary>
        ///     查询文档类型。
        /// </summary>
        /// <param name="query">查询条件。</param>
        /// <returns>文档类型。</returns>
        IQueryable<DocumentTypeDTO> DocumentTypesQuery(
            QueryBuilder<DocumentType> query);
    }
}