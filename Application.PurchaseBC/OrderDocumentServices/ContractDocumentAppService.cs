#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.OrderDocumentQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.OrderDocumentServices
{
    public class ContractDocumentAppService:IContractDocumentAppService
    {
        private readonly IContractDocumentQuery _contractDocumentQuery;

        public ContractDocumentAppService(IContractDocumentQuery contractDocumentQuery)
        {
            _contractDocumentQuery = contractDocumentQuery;
        }

        #region OrderDocumentDTO

        /// <summary>
        ///     获取订单文档集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<OrderDocumentDTO> GetOrderDocuments()
        {
            var query = new QueryBuilder<Order>();
            return _contractDocumentQuery.OrderDocumentQuery(query);
        }

        #endregion
    }
}