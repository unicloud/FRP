#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 11:04:45
// 文件名：CreditNoteAppService
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
    public class CreditNoteAppService : ICreditNoteAppService
    {
        private readonly ICreditNoteQuery _creditNoteQuery;
        private readonly IInvoiceRepository _invoiceRepository;

        public CreditNoteAppService(ICreditNoteQuery creditNoteQuery,
            IInvoiceRepository invoiceRepository)
        {
            _creditNoteQuery = creditNoteQuery;
            _invoiceRepository = invoiceRepository;
        }
    }
}
