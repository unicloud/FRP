#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.OrderDocumentQueries
{
    public class ContractDocumentQuery : IContractDocumentQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public ContractDocumentQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     查询订单的合同文档
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>订单合同集合</returns>
        public IQueryable<OrderDocumentDTO> OrderDocumentQuery(QueryBuilder<Order> query)
        {
            var dbTrade = _unitOfWork.CreateSet<Trade>();
            var result = query.ApplyTo(_unitOfWork.CreateSet<Order>())
                              .Select(o => new OrderDocumentDTO
                                  {
                                      OrderDocumentId = o.Id,
                                      SupplierName = dbTrade.Where(p => p.Id == o.TradeId)
                                                            .Select(p => p.Supplier.Name).FirstOrDefault(),
                                      Name = o.Name,
                                      ContractName = o.ContractName,
                                      ContractDocGuid = o.ContractDocGuid,
                                  });
            return result;
        }
    }
}