#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/20 11:05:34
// 文件名：PaymentNoticeAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/20 11:05:34
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.Query.PaymentNoticeQueries;
using UniCloud.Domain.PaymentBC.Aggregates.PaymentNoticeAgg;

#endregion

namespace UniCloud.Application.PaymentBC.PaymentNoticeServices
{
    /// <summary>
    ///     实现付款通知接口。
    ///     用于处于付款通知相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class PaymentNoticeAppService : ContextBoundObject, IPaymentNoticeAppService
    {
        private readonly IPaymentNoticeQuery _paymentNoticeQuery;
        private readonly IPaymentNoticeRepository _paymentNoticeRepository;
        private static int _maxInvoiceNumber;
        public PaymentNoticeAppService(IPaymentNoticeQuery paymentNoticeQuery, IPaymentNoticeRepository paymentNoticeRepository)
        {
            _paymentNoticeQuery = paymentNoticeQuery;
            _paymentNoticeRepository = paymentNoticeRepository;
        }

        #region PaymentNoticeDTO
        /// <summary>
        ///     获取所有付款通知。
        /// </summary>
        /// <returns>所有付款通知。</returns>
        public IQueryable<PaymentNoticeDTO> GetPaymentNotices()
        {
            var queryBuilder = new QueryBuilder<PaymentNotice>();
            return _paymentNoticeQuery.PaymentNoticeDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增付款通知。
        /// </summary>
        /// <param name="paymentNotice">付款通知DTO。</param>
        [Insert(typeof(PaymentNoticeDTO))]
        public void InsertPaymentNotice(PaymentNoticeDTO paymentNotice)
        {
            var newPaymentNotice = PaymentNoticeFactory.CreatePaymentNotice();
            var date = DateTime.Now.Date.ToString("yyyyMMdd").Substring(0, 8);
            var noticeNumber = _paymentNoticeRepository.GetAll().Max(p => p.NoticeNumber);
            int seq = 1;
            if (!string.IsNullOrEmpty(noticeNumber) && noticeNumber.StartsWith(date))
            {
                seq = Int32.Parse(noticeNumber.Substring(8)) + 1;
            }
            if (seq <= _maxInvoiceNumber)
            {
                seq = _maxInvoiceNumber;
            }
            else
            {
                _maxInvoiceNumber = seq;
            }
            _maxInvoiceNumber++;
            newPaymentNotice.SetNoticeNumber(seq);
            PaymentNoticeFactory.SetPaymentNotice(newPaymentNotice, paymentNotice.DeadLine, paymentNotice.SupplierName, paymentNotice.SupplierId,
                paymentNotice.OperatorName, paymentNotice.Reviewer, paymentNotice.Status, paymentNotice.CurrencyId, paymentNotice.BankAccountId);
            if (paymentNotice.PaymentNoticeLines != null)
            {
                foreach (var paymentNoticeLine in paymentNotice.PaymentNoticeLines)
                {
                    var newPaymentNoticeLine = PaymentNoticeFactory.CreatePaymentNoticeLine();
                    PaymentNoticeFactory.SetPaymentNoticeLine(newPaymentNoticeLine, paymentNoticeLine.InvoiceType, paymentNoticeLine.InvoiceId, paymentNoticeLine.InvoiceNumber,
                        paymentNoticeLine.Amount, paymentNoticeLine.Note);
                    newPaymentNotice.PaymentNoticeLines.Add(newPaymentNoticeLine);
                }
            }
            _paymentNoticeRepository.Add(newPaymentNotice);
        }


        /// <summary>
        ///     更新付款通知。
        /// </summary>
        /// <param name="paymentNotice">付款通知DTO。</param>
        [Update(typeof(PaymentNoticeDTO))]
        public void ModifyPaymentNotice(PaymentNoticeDTO paymentNotice)
        {
            var updatePaymentNotice = _paymentNoticeRepository.Get(paymentNotice.PaymentNoticeId); //获取需要更新的对象。
            PaymentNoticeFactory.SetPaymentNotice(updatePaymentNotice, paymentNotice.DeadLine, paymentNotice.SupplierName, paymentNotice.SupplierId,
                paymentNotice.OperatorName, paymentNotice.Reviewer, paymentNotice.Status, paymentNotice.CurrencyId, paymentNotice.BankAccountId);
            UpdatePaymentNoticeLines(paymentNotice.PaymentNoticeLines, updatePaymentNotice);
            _paymentNoticeRepository.Modify(updatePaymentNotice);
        }

        /// <summary>
        ///     删除付款通知。
        /// </summary>
        /// <param name="paymentNotice">付款通知DTO。</param>
        [Delete(typeof(PaymentNoticeDTO))]
        public void DeletePaymentNotice(PaymentNoticeDTO paymentNotice)
        {
            var deletePaymentNotice = _paymentNoticeRepository.Get(paymentNotice.PaymentNoticeId); //获取需要删除的对象。
            UpdatePaymentNoticeLines(new List<PaymentNoticeLineDTO>(), deletePaymentNotice);
            _paymentNoticeRepository.Remove(deletePaymentNotice); //删除付款通知。
        }
        #endregion

        #region 更新付款通知行集合
        /// <summary>
        /// 更新付款通知行集合
        /// </summary>
        /// <param name="sourcePaymentNoticeLines">客户端集合</param>
        /// <param name="dstPaymentNotice">数据库集合</param>
        private void UpdatePaymentNoticeLines(IEnumerable<PaymentNoticeLineDTO> sourcePaymentNoticeLines, PaymentNotice dstPaymentNotice)
        {
            var paymentNoticeLines = new List<PaymentNoticeLine>();
            foreach (var sourcePaymentNoticeLine in sourcePaymentNoticeLines)
            {
                var result = dstPaymentNotice.PaymentNoticeLines.FirstOrDefault(p => p.Id == sourcePaymentNoticeLine.PaymentNoticeLineId);
                if (result == null)
                {
                    result = PaymentNoticeFactory.CreatePaymentNoticeLine();
                    result.ChangeCurrentIdentity(sourcePaymentNoticeLine.PaymentNoticeLineId);
                }
                PaymentNoticeFactory.SetPaymentNoticeLine(result, sourcePaymentNoticeLine.InvoiceType, sourcePaymentNoticeLine.InvoiceId, sourcePaymentNoticeLine.InvoiceNumber,
                        sourcePaymentNoticeLine.Amount, sourcePaymentNoticeLine.Note);
                paymentNoticeLines.Add(result);
            }
            dstPaymentNotice.PaymentNoticeLines.ToList().ForEach(p =>
            {
                if (paymentNoticeLines.FirstOrDefault(t => t.Id == p.Id) == null)
                {
                    _paymentNoticeRepository.RemovePaymentNoticeLine(p);
                }
            });
            dstPaymentNotice.PaymentNoticeLines = paymentNoticeLines;
        }
        #endregion
    }
}
