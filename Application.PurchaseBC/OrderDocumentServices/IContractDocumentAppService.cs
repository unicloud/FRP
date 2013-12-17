using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;

namespace UniCloud.Application.PurchaseBC.OrderDocumentServices
{
    public interface IContractDocumentAppService
    {
        /// <summary>
        ///     获取订单文档集合
        /// </summary>
        /// <returns></returns>
        IQueryable<OrderDocumentDTO> GetOrderDocuments();
    }
}