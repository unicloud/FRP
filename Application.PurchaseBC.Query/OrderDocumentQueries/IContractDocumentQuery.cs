#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.OrderDocumentQueries
{
    public interface IContractDocumentQuery
    {
        /// <summary>
        ///     查询订单的合同文档
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>订单合同集合</returns>
        IQueryable<OrderDocumentDTO> OrderDocumentQuery(QueryBuilder<Order> query);
    }
}