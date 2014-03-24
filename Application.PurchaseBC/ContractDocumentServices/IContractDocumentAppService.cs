using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;

namespace UniCloud.Application.PurchaseBC.ContractDocumentServices
{
    public interface IContractDocumentAppService
    {
        /// <summary>
        ///     获取文档集合
        /// </summary>
        /// <returns></returns>
        IQueryable<ContractDocumentDTO> GetContractDocumentList();

        IQueryable<ContractDocumentDTO> GetContractDocuments();
    }
}