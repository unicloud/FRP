#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/20 10:50:33
// 文件名：PaymentNoticeQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/20 10:50:33
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Domain.PaymentBC.Aggregates.PaymentNoticeAgg;

#endregion

namespace UniCloud.Application.PaymentBC.Query.PaymentNoticeQueries
{
    public class PaymentNoticeQuery : IPaymentNoticeQuery
    {
        private readonly IPaymentNoticeRepository _paymentNoticeRepository;

        public PaymentNoticeQuery(IPaymentNoticeRepository paymentNoticeRepository)
        {
            _paymentNoticeRepository = paymentNoticeRepository;
        }

        /// <summary>
        ///  付款通知查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>付款通知DTO集合。</returns>
        public IQueryable<PaymentNoticeDTO> PaymentNoticeDTOQuery(QueryBuilder<PaymentNotice> query)
        {
            return
                query.ApplyTo(_paymentNoticeRepository.GetAll()).Select(p => new PaymentNoticeDTO
                                 {
                                     PaymentNoticeId = p.Id,
                                     NoticeNumber = p.NoticeNumber,
                                     CreateDate = p.CreateDate,
                                     DeadLine = p.DeadLine,
                                     SupplierName = p.SupplierName,
                                     OperatorName = p.OperatorName,
                                     Reviewer = p.Reviewer,
                                     ReviewDate = p.ReviewDate,
                                     Status = (int)p.Status,
                                     SupplierId = p.SupplierId,
                                     CurrencyId = p.CurrencyId,
                                     BankAccountId = p.BankAccountId,
                                     PaymentNoticeLines =
                                         p.PaymentNoticeLines.Select(q => new PaymentNoticeLineDTO
                                                                            {
                                                                                PaymentNoticeLineId = q.Id,
                                                                                InvoiceType = (int)q.InvoiceType,
                                                                                InvoiceId = q.InvoiceId,
                                                                                InvoiceNumber = q.InvoiceNumber,
                                                                                Amount = q.Amount,
                                                                                Note = q.Note
                                                                            }).ToList(),
                                 });
        }
    }
}
