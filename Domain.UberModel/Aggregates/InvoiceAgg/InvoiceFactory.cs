#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/09，22:12
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.InvoiceAgg
{
    /// <summary>
    ///     发票工厂
    /// </summary>
    public static class InvoiceFactory
    {
        /// <summary>
        ///     创建贷项单发票
        /// </summary>
        /// <param name="invoiceCode">发票代码</param>
        /// <param name="invoiceDate">发票日期</param>
        /// <param name="operatorName">经办人</param>
        /// <returns>贷项单</returns>
        public static CreditNoteInvoice CreateCreditNoteInvoice(string invoiceCode, DateTime invoiceDate,
            string operatorName)
        {
            var invoice = new CreditNoteInvoice
            {
                InvoideCode = invoiceCode,
                InvoiceDate = invoiceDate,
                CreateDate = DateTime.Now
            };
            invoice.GenerateNewIdentity();
            invoice.SetOperator(operatorName);

            return invoice;
        }

        /// <summary>
        ///     创建租赁发票
        /// </summary>
        /// <param name="invoiceCode">发票代码</param>
        /// <param name="invoiceDate">发票日期</param>
        /// <param name="operatorName">经办人</param>
        /// <returns>租赁发票</returns>
        public static LeaseInvoice CreateLeaseInvoice(string invoiceCode, DateTime invoiceDate,
            string operatorName)
        {
            var invoice = new LeaseInvoice
            {
                InvoideCode = invoiceCode,
                InvoiceDate = invoiceDate,
                CreateDate = DateTime.Now
            };
            invoice.GenerateNewIdentity();
            invoice.SetOperator(operatorName);

            return invoice;
        }

        /// <summary>
        ///     创建预付款发票
        /// </summary>
        /// <param name="invoiceCode">发票代码</param>
        /// <param name="invoiceDate">发票日期</param>
        /// <param name="operatorName">经办人</param>
        /// <returns>预付款发票</returns>
        public static PrepaymentInvoice CreatePrepaymentInvoice(string invoiceCode, DateTime invoiceDate,
            string operatorName)
        {
            var invoice = new PrepaymentInvoice
            {
                InvoideCode = invoiceCode,
                InvoiceDate = invoiceDate,
                CreateDate = DateTime.Now
            };
            invoice.GenerateNewIdentity();
            invoice.SetOperator(operatorName);

            return invoice;
        }

        /// <summary>
        ///     创建采购发票
        /// </summary>
        /// <param name="invoiceCode">发票代码</param>
        /// <param name="invoiceDate">发票日期</param>
        /// <param name="operatorName">经办人</param>
        /// <returns>采购发票</returns>
        public static PurchaseInvoice CreatePurchaseInvoice(string invoiceCode, DateTime invoiceDate,
            string operatorName)
        {
            var invoice = new PurchaseInvoice
            {
                InvoideCode = invoiceCode,
                InvoiceDate = invoiceDate,
                CreateDate = DateTime.Now
            };
            invoice.GenerateNewIdentity();
            invoice.SetOperator(operatorName);

            return invoice;
        }
    }
}