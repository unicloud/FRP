#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/15，21:12
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

namespace UniCloud.Domain.UberModel.Aggregates.MaintainInvoiceAgg
{
    /// <summary>
    ///     维修发票工厂
    /// </summary>
    public static class MaintainInvoiceFactory
    {
        /// <summary>
        ///     创建APU维修发票
        /// </summary>
        /// <param name="serialNumber">序列号</param>
        /// <param name="invoiceCode">发票代码</param>
        /// <param name="invoiceDate">发票日期</param>
        /// <param name="operatorName">经办人</param>
        /// <returns></returns>
        public static APUMaintainInvoice CreateAPUMaintainInvoice(string serialNumber, string invoiceCode,
            DateTime invoiceDate, string operatorName)
        {
            var invoice = new APUMaintainInvoice
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
        ///     创建机身维修发票
        /// </summary>
        /// <param name="serialNumber">序列号</param>
        /// <param name="invoiceCode">发票代码</param>
        /// <param name="invoiceDate">发票日期</param>
        /// <param name="operatorName">经办人</param>
        /// <returns></returns>
        public static AirframeMaintainInvoice CreateAirframeMaintainInvoice(string serialNumber, string invoiceCode,
            DateTime invoiceDate, string operatorName)
        {
            var invoice = new AirframeMaintainInvoice
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
        ///     创建发动机维修发票
        /// </summary>
        /// <param name="serialNumber">序列号</param>
        /// <param name="invoiceCode">发票代码</param>
        /// <param name="invoiceDate">发票日期</param>
        /// <param name="operatorName">经办人</param>
        /// <returns></returns>
        public static EngineMaintainInvoice CreateEngineMaintainInvoice(string serialNumber, string invoiceCode,
            DateTime invoiceDate, string operatorName)
        {
            var invoice = new EngineMaintainInvoice
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
        ///     创建起落架维修发票
        /// </summary>
        /// <param name="serialNumber">序列号</param>
        /// <param name="invoiceCode">发票代码</param>
        /// <param name="invoiceDate">发票日期</param>
        /// <param name="operatorName">经办人</param>
        /// <returns></returns>
        public static UndercartMaintainInvoice CreateUndercartMaintainInvoice(string serialNumber, string invoiceCode,
            DateTime invoiceDate, string operatorName)
        {
            var invoice = new UndercartMaintainInvoice
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