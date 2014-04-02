#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.LuceneSearch;
using UniCloud.Application.PurchaseBC.DocumentPathServices;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.ContractDocumentQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.PurchaseBC.ContractDocumentServices
{
    [LogAOP]
    public class ContractDocumentAppService : ContextBoundObject, IContractDocumentAppService
    {
        private readonly IContractDocumentQuery _contractDocumentQuery;

        public ContractDocumentAppService(IContractDocumentQuery contractDocumentQuery)
        {
            _contractDocumentQuery = contractDocumentQuery;
        }

        #region ContractDocumentDTO

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
            Expression<Func<Order, bool>> orderDocument = p => true;
            Expression<Func<RelatedDoc, bool>> relateDocument = p => true;
            Expression<Func<MaintainContract, bool>> maintainContractDocument = p => true;
            return _contractDocumentQuery.GetContractDocuments(orderDocument, relateDocument, maintainContractDocument).AsQueryable();
        }

        public List<ContractDocumentDTO> Search(string keyword)
        {
            var documentPathAppService = DefaultContainer.Resolve<IDocumentPathAppService>();
            var queryResults = LuceneSearch.LuceneSearch.PanguQuery(keyword, "3");
            if (queryResults == null) return new List<ContractDocumentDTO>();
            var multiSearcher = IndexManager.GenerateMultiSearcher("3");
            var documents = queryResults.scoreDocs.Select(a => multiSearcher.Doc(a.doc)).Select(result =>
                                                                                      {
                                                                                          Guid documentId = Guid.Parse(result.Get("ID"));
                                                                                          Expression<Func<Order, bool>> orderDocument = p => p.ContractDocGuid == documentId;
                                                                                          Expression<Func<RelatedDoc, bool>> relateDocument = p => p.DocumentId == documentId;
                                                                                          Expression<Func<MaintainContract, bool>> maintainContractDocument = p => p.DocumentId == documentId;
                                                                                          var document = _contractDocumentQuery.GetContractDocuments(orderDocument, relateDocument, maintainContractDocument).FirstOrDefault();
                                                                                          var contractDocument = new ContractDocumentDTO
                                                                                                 {
                                                                                                     DocumentId = documentId,
                                                                                                     Abstract = ResultHighlighter.HighlightContent(keyword, result.Get("fileContent")),
                                                                                                     DocumentName = result.Get("fileName"),
                                                                                                 };
                                                                                          if (document != null)
                                                                                          {
                                                                                              contractDocument.ContractName = document.ContractName;
                                                                                              contractDocument.ContractNumber = document.ContractName;
                                                                                              contractDocument.SupplierName = document.SupplierName;
                                                                                          }
                                                                                          documentPathAppService.GetDocumentPaths().Where(p => p.DocumentGuid == documentId).ToList()
                                                                                              .ForEach(p => contractDocument.DocumentPath += p.Path + "\r\n");
                                                                                          return contractDocument;
                                                                                      }).ToList();
            return documents;
        }
        #endregion
    }
}