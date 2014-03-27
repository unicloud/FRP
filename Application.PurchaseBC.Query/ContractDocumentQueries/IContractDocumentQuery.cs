#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;

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

        IEnumerable<ContractDocumentDTO> GetContractDocuments();
    }
}