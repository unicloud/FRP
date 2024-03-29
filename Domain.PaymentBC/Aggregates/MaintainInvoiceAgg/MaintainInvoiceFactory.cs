﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/15，14:38
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
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg
{
    /// <summary>
    ///     维修发票工厂
    /// </summary>
    public static class MaintainInvoiceFactory
    {
        /// <summary>
        ///     创建APU维修发票
        /// </summary>
        /// <returns></returns>
        public static APUMaintainInvoice CreateApuMaintainInvoice()
        {
            var invoice = new APUMaintainInvoice
            {
                CreateDate = DateTime.Now
            };
            invoice.SetInvoiceType(InvoiceType.维修发票);
            invoice.GenerateNewIdentity();

            return invoice;
        }

        /// <summary>
        ///     创建机身维修发票
        /// </summary>
        /// <returns></returns>
        public static AirframeMaintainInvoice CreateAirframeMaintainInvoice()
        {
            var invoice = new AirframeMaintainInvoice
            {
                CreateDate = DateTime.Now
            };
            invoice.SetInvoiceType(InvoiceType.维修发票);
            invoice.GenerateNewIdentity();

            return invoice;
        }

        /// <summary>
        ///     创建发动机维修发票
        /// </summary>
        /// <returns></returns>
        public static EngineMaintainInvoice CreateEngineMaintainInvoice()
        {
            var invoice = new EngineMaintainInvoice
            {
                CreateDate = DateTime.Now
            };
            invoice.SetInvoiceType(InvoiceType.维修发票);
            invoice.GenerateNewIdentity();

            return invoice;
        }

        /// <summary>
        ///     创建起落架维修发票
        /// </summary>
        /// <returns></returns>
        public static UndercartMaintainInvoice CreateUndercartMaintainInvoice()
        {
            var invoice = new UndercartMaintainInvoice
            {
                CreateDate = DateTime.Now
            };
            invoice.SetInvoiceType(InvoiceType.维修发票);
            invoice.GenerateNewIdentity();

            return invoice;
        }

        /// <summary>
        ///     创建特修改装发票
        /// </summary>
        /// <returns>特修改装发票</returns>
        public static SpecialRefitInvoice CreateSpecialRefitInvoice()
        {
            var invoice = new SpecialRefitInvoice
                          {
                              CreateDate = DateTime.Now
                          };
            invoice.SetInvoiceType(InvoiceType.特修改装发票);
            invoice.GenerateNewIdentity();

            return invoice;
        }

        /// <summary>
        ///     设置维修发票属性
        /// </summary>
        /// <param name="maintainInvoice">维修发票</param>
        /// <param name="serialNumber">序列号</param>
        /// <param name="invoideCode">发票号码</param>
        /// <param name="invoiceDate">发票日期</param>
        /// <param name="supplierName">供应商名称</param>
        /// <param name="supplierId">供应商ID</param>
        /// <param name="invoiceValue">发票金额</param>
        /// <param name="paidAmount">已付金额</param>
        /// <param name="operatorName">经办人</param>
        /// <param name="reviewer">审核人</param>
        /// <param name="status">发票状态</param>
        /// <param name="currencyId">币种ID</param>
        /// <param name="documentName">文档名称</param>
        /// <param name="documentId">文档ID</param>
        /// <param name="paymentScheduleLineId">付款计划行</param>
        /// <param name="inMaintainTime"></param>
        /// <param name="outMaintainTime"></param>
        public static void SetMaintainInvoice(MaintainInvoice maintainInvoice, string serialNumber, string invoideCode,
            DateTime invoiceDate, string supplierName, int supplierId, decimal invoiceValue, decimal paidAmount,
            string operatorName, string reviewer, int status, int currencyId, string documentName, Guid documentId, int? paymentScheduleLineId,
            DateTime inMaintainTime, DateTime outMaintainTime)
        {
            maintainInvoice.SetSerialNumber(serialNumber);
            maintainInvoice.InvoideCode = invoideCode;
            maintainInvoice.InvoiceDate = invoiceDate;
            maintainInvoice.SetInvoiceValue();
            maintainInvoice.SetOperator(operatorName);
            maintainInvoice.SetInvoiceStatus((InvoiceStatus)status);
            maintainInvoice.SetSupplier(supplierId, supplierName);
            maintainInvoice.SetCurrency(currencyId);
            maintainInvoice.SetPaymentScheduleLine(paymentScheduleLineId);
            maintainInvoice.DocumentName = documentName;
            maintainInvoice.DocumentId = documentId;
            maintainInvoice.InMaintainTime = inMaintainTime;
            maintainInvoice.OutMaintainTime = outMaintainTime;
            maintainInvoice.TotalDays = (maintainInvoice.OutMaintainTime.Date - maintainInvoice.InMaintainTime.Date).Days + 1;
            if (!String.IsNullOrEmpty(reviewer))
            {
                maintainInvoice.Review(reviewer);
            }
            if (maintainInvoice is EngineMaintainInvoice)
            {
            }
            else if (maintainInvoice is APUMaintainInvoice)
            {
            }
            else if (maintainInvoice is UndercartMaintainInvoice)
            {
            }
            else if (maintainInvoice is AirframeMaintainInvoice)
            {
            }
        }

        /// <summary>
        ///     设置发票行属性
        /// </summary>
        /// <param name="invoiceLine">发票行</param>
        /// <param name="price">单价</param>
        /// <param name="amount">金额</param>
        /// <param name="note">备注</param>
        /// <param name="maintainItem">维修项</param>
        /// <param name="itemName">项名称</param>
        public static void SetInvoiceLine(MaintainInvoiceLine invoiceLine, int maintainItem, int itemName, decimal price, decimal amount, string note)
        {
            invoiceLine.SetMaintainItem((MaintainItem)maintainItem);
            invoiceLine.SetItemName((ItemNameType)itemName);
            invoiceLine.SetUnitPrice(price);
            invoiceLine.SetAmount(amount);
            invoiceLine.SetNote(note);
        }

        /// <summary>
        ///     创建发票行
        /// </summary>
        /// <returns></returns>
        public static MaintainInvoiceLine CreateInvoiceLine()
        {
            var invoiceLine = new MaintainInvoiceLine();
            invoiceLine.GenerateNewIdentity();
            return invoiceLine;
        }
    }
}