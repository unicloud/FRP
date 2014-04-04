#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/08，11:52
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
using System.Linq;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg
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


        /// <summary>
        ///     设置发票属性
        /// </summary>
        /// <param name="invoice">发票</param>
        /// <param name="invoideCode">发票代码</param>
        /// <param name="invoiceDate">发票日期</param>
        /// <param name="operatorName">经办人</param>
        /// <param name="invoiceNumber">发票号</param>
        /// <param name="supplier">供应商</param>
        /// <param name="order">订单</param>
        /// <param name="paidAmount">已付金额</param>
        /// <param name="currency">币种</param>
        /// <param name="paymentScheduleLineId">付款计划行ID</param>
        /// <param name="status">发票状态</param>
        /// <returns>发票</returns>
        public static void SetInvoice(Invoice invoice, string invoideCode, DateTime invoiceDate, string operatorName,
            string invoiceNumber, Supplier supplier, Order order,
            decimal paidAmount, Currency currency, int? paymentScheduleLineId, int status)
        {
            invoice.InvoideCode = invoideCode;
            invoice.InvoiceDate = invoiceDate;
            invoice.SetOperator(operatorName);
            invoice.SetInvoiceNumber(invoiceNumber);
            invoice.SetSupplier(supplier);
            invoice.SetOrder(order);
            invoice.SetPaidAmount(paidAmount);
            invoice.SetCurrency(currency);
            invoice.SetPaymentScheduleLine(paymentScheduleLineId);
            invoice.SetInvoiceStatus((InvoiceStatus) status);
        }

        /// <summary>
        ///     设置发票行属性
        /// </summary>
        /// <param name="invoiceLine">发票行</param>
        /// <param name="itemName">项名称</param>
        /// <param name="amount">金额</param>
        /// <param name="order">订单</param>
        /// <param name="orderLineId">订单行Id</param>
        /// <param name="note">备注</param>
        public static void SetInvoiceLine(InvoiceLine invoiceLine, string itemName, decimal amount, Order order,
            int orderLineId, string note)
        {
            if (order != null)
            {
                var orderLine = order.OrderLines.FirstOrDefault(p => p.Id == orderLineId);
                invoiceLine.SetOrderLine(orderLine);
            }
            invoiceLine.ItemName = itemName;
            invoiceLine.Amount = amount;
            invoiceLine.SetNote(note);
        }

        /// <summary>
        ///     创建发票行
        /// </summary>
        /// <returns></returns>
        public static InvoiceLine CreateInvoiceLine()
        {
            var invoiceLine = new InvoiceLine();
            invoiceLine.GenerateNewIdentity();
            return invoiceLine;
        }
    }
}