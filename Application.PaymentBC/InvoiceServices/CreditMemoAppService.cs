#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 11:04:45
// 文件名：CreditMemoAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.Query.InvoiceQueries;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;

#endregion

namespace UniCloud.Application.PaymentBC.InvoiceServices
{
    /// <summary>
    /// 贷项单服务实现
    /// </summary>
    public class CreditMemoAppService : ICreditMemoAppService
    {
        private readonly ICreditMemoQuery _creditMemoQuery;
        private readonly IInvoiceRepository _invoiceRepository;

        public CreditMemoAppService(ICreditMemoQuery creditMemoQuery,
            IInvoiceRepository invoiceRepository)
        {
            _creditMemoQuery = creditMemoQuery;
            _invoiceRepository = invoiceRepository;
        }
    }
}
