#region 版本信息

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
using UniCloud.Domain.Common.Enums;

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
        public static void SetMaintainInvoice(MaintainInvoice maintainInvoice, string serialNumber, string invoideCode,
            DateTime invoiceDate, string supplierName, int supplierId, decimal invoiceValue, decimal paidAmount,
            string operatorName,
            string reviewer, int status, int currencyId, string documentName, Guid documentId)
        {
            maintainInvoice.SetSerialNumber(serialNumber);
            maintainInvoice.InvoideCode = invoideCode;
            maintainInvoice.InvoiceDate = invoiceDate;
            maintainInvoice.SetInvoiceValue(invoiceValue);
            //maintainInvoice.SetPaidAmount(paidAmount);
            maintainInvoice.SetOperator(operatorName);
            maintainInvoice.SetInvoiceStatus((InvoiceStatus) status);
            maintainInvoice.SetSupplier(supplierId, supplierName);
            maintainInvoice.SetCurrency(currencyId);
            maintainInvoice.DocumentName = documentName;
            maintainInvoice.DocumentId = documentId;
            if (!string.IsNullOrEmpty(reviewer))
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
        ///     创建维修发票行
        /// </summary>
        /// <returns></returns>
        public static MaintainInvoiceLine CreateMaintainInvoiceLine()
        {
            var maintainInvoiceLine = new MaintainInvoiceLine();
            maintainInvoiceLine.GenerateNewIdentity();
            return maintainInvoiceLine;
        }

        /// <summary>
        ///     设置维修发票行属性
        /// </summary>
        /// <param name="maintainInvoiceLine">维修发票行</param>
        /// <param name="maintainItem">维修项</param>
        /// <param name="itemName">项名称</param>
        /// <param name="unitPrice">单价</param>
        /// <param name="amount">数量</param>
        /// <param name="note">备注</param>
        public static void SetMaintainInvoiceLine(MaintainInvoiceLine maintainInvoiceLine, int maintainItem,
            string itemName, decimal unitPrice, decimal amount, string note)
        {
            maintainInvoiceLine.SetMaintainItem((MaintainItem) maintainItem);
            maintainInvoiceLine.ItemName = itemName;
            maintainInvoiceLine.UnitPrice = unitPrice;
            maintainInvoiceLine.Amount = amount;
            maintainInvoiceLine.SetNote(note);
        }
    }
}