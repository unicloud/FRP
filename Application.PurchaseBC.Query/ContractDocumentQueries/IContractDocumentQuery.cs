#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.ContractDocumentQueries
{
    public interface IContractDocumentQuery
    {
        /// <summary>
        ///     查询合同文档
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>合同文档集合</returns>
        IQueryable<ContractDocumentDTO> ContractDocumentsQuery(QueryBuilder<Order> query);

        IEnumerable<ContractDocumentDTO> GetContractDocuments(Expression<Func<Order, bool>> orderDocument, Expression<Func<RelatedDoc, bool>> relateDocument, Expression<Func<MaintainContract, bool>> maintainContractDocument);
    }
}