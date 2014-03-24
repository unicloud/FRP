#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.ContractDocumentQueries
{
    public class ContractDocumentQuery : IContractDocumentQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        private readonly IMaintainContractRepository _contractRepository;

        public ContractDocumentQuery(IQueryableUnitOfWork unitOfWork, IMaintainContractRepository contractRepository)
        {
            _unitOfWork = unitOfWork;
            _contractRepository = contractRepository;
        }

        /// <summary>
        ///     查询合同文档
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>合同文档集合</returns>
        public IQueryable<ContractDocumentDTO> ContractDocumentsQuery(QueryBuilder<Order> query)
        {
            var dbTrade = _unitOfWork.CreateSet<Trade>();
            var result = query.ApplyTo(_unitOfWork.CreateSet<Order>())
                              .Select(o => new ContractDocumentDTO
                                  {
                                      Id = o.ContractDocGuid,
                                      SupplierName = dbTrade.Where(p => p.Id == o.TradeId)
                                                            .Select(p => p.Supplier.CnName).FirstOrDefault(),
                                      ContractName = o.Name,
                                      DocumentName = o.ContractName,
                                      DocumentId = o.ContractDocGuid,
                                  });
            return result;
        }

        public IEnumerable<ContractDocumentDTO> GetContractDocuments()
        {
            var documents = new List<ContractDocumentDTO>();
            var dbTrade = _unitOfWork.CreateSet<Trade>();
            _unitOfWork.CreateSet<Order>()
                .Select(o => new ContractDocumentDTO
                             {
                                 Id = o.ContractDocGuid,
                                 SupplierName = dbTrade.Where(p => p.Id == o.TradeId)
                                     .Select(p => p.Supplier.CnName).FirstOrDefault(),
                                 ContractName = o.Name,
                                 ContractNumber = o.ContractNumber,
                                 DocumentName = o.ContractName,
                                 DocumentId = o.ContractDocGuid,
                             }).ToList().ForEach(documents.Add);
            _unitOfWork.CreateSet<Order>().ToList().ForEach(o => _unitOfWork.CreateSet<RelatedDoc>()
                .Where(r => r.SourceId == o.SourceGuid)
                .Select(p => new ContractDocumentDTO
                         {
                             Id = p.DocumentId,
                             SupplierName = dbTrade.Where(
                                     t => t.Id == o.TradeId)
                                 .Select(t => t.Supplier.CnName)
                                 .FirstOrDefault(),
                             ContractName = o.Name,
                             ContractNumber = o.ContractNumber,
                             DocumentName = p.DocumentName,
                             DocumentId = p.DocumentId,
                         }).ToList().ForEach(documents.Add));
            _contractRepository.GetAll()
                .Select(o => new ContractDocumentDTO
                             {
                                 Id = o.DocumentId,
                                 SupplierName = o.Signatory,
                                 ContractNumber = o.Number,
                                 ContractName = o.Name,
                                 DocumentName = o.DocumentName,
                                 DocumentId = o.DocumentId,
                             }).ToList().ForEach(documents.Add);
            return documents;
        }
    }
}