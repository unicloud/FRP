#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/08，11:54
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.PaymentNoticeAgg
{
    /// <summary>
    ///     付款通知工厂
    /// </summary>
    public static class PaymentNoticeFactory
    {
        /// <summary>
        ///     创建付款通知
        /// </summary>
        /// <returns></returns>
        public static PaymentNotice CreatePaymentNotice()
        {
            var invoice = new PaymentNotice
            {
                CreateDate = DateTime.Now
            };
            invoice.GenerateNewIdentity();

            return invoice;
        }

        /// <summary>
        ///     设置付款通知属性
        /// </summary>
        /// <param name="paymentNotice">付款通知</param>
        /// <param name="deadLine">付款期限</param>
        /// <param name="supplierName">供应商名称</param>
        /// <param name="supplierId">供应商ID</param>
        /// <param name="operatorName">经办人</param>
        /// <param name="reviewer">审核人</param>
        /// <param name="status">付款通知状态</param>
        /// <param name="currencyId">币种ID</param>
        /// <param name="bankAccountId">银行账户ID</param>
        public static void SetPaymentNotice(PaymentNotice paymentNotice, DateTime deadLine, string supplierName,
            int supplierId, string operatorName,
            string reviewer, int status, int currencyId, int bankAccountId)
        {
            paymentNotice.DeadLine = deadLine;
            paymentNotice.SetSupplier(supplierId, supplierName);
            paymentNotice.SetOperator(operatorName);
            paymentNotice.SetPaymentNoticeStatus((PaymentNoticeStatus) status);
            paymentNotice.SetCurrency(currencyId);
            paymentNotice.SetBankAccount(bankAccountId);
            if (!string.IsNullOrEmpty(reviewer))
            {
                paymentNotice.Review(reviewer);
            }
        }

        /// <summary>
        ///     创建维修发票行
        /// </summary>
        /// <returns></returns>
        public static PaymentNoticeLine CreatePaymentNoticeLine()
        {
            var maintainInvoiceLine = new PaymentNoticeLine();
            maintainInvoiceLine.GenerateNewIdentity();
            return maintainInvoiceLine;
        }

        /// <summary>
        ///     设置维修发票行属性
        /// </summary>
        /// <param name="paymentNoticeLine">维修发票行</param>
        /// <param name="invoiceType">发票类型</param>
        /// <param name="invoiceId">发票Id</param>
        /// <param name="invoiceNumber">发票编号</param>
        /// <param name="amount">数量</param>
        /// <param name="note">备注</param>
        public static void SetPaymentNoticeLine(PaymentNoticeLine paymentNoticeLine, int invoiceType, int invoiceId,
            string invoiceNumber, decimal amount, string note)
        {
            paymentNoticeLine.SetInvoice(invoiceId, invoiceNumber, (InvoiceType) invoiceType);
            paymentNoticeLine.Amount = amount;
            paymentNoticeLine.Note = note;
        }
    }
}