#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.ContractDocumentQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.ContractDocumentServices
{
    public class ContractDocumentAppService:IContractDocumentAppService
    {
        private readonly IContractDocumentQuery _contractDocumentQuery;

        public ContractDocumentAppService(IContractDocumentQuery contractDocumentQuery)
        {
            _contractDocumentQuery = contractDocumentQuery;
        }
        
        #region DocumentDTO

        /// <summary>
        ///     获取订单文档集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<ContractDocumentDTO> GetContractDocumentList()
        {
            var query = new QueryBuilder<Order>();
            return _contractDocumentQuery.ContractDocumentsQuery(query);
        }

        public IQueryable<ContractDocumentDTO> GetContractDocuments()
        {
            return _contractDocumentQuery.GetContractDocuments().AsQueryable();
        }
        #endregion
    }
}