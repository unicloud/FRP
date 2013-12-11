#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，11:12
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.UberModel.Aggregates.InvoiceAgg;
using UniCloud.Domain.UberModel.Enums;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.PaymentNoticeAgg
{
    /// <summary>
    ///     付款通知聚合根
    ///     付款通知行
    /// </summary>
    public class PaymentNoticeLine : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal PaymentNoticeLine()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     发票类型
        /// </summary>
        public InvoiceType InvoiceType { get; private set; }

        /// <summary>
        ///     发票编号
        /// </summary>
        public string InvoiceNumber { get; private set; }

        /// <summary>
        ///     付款金额
        /// </summary>
        public decimal Amount { get; internal set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; internal set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     付款通知ID
        /// </summary>
        public int PaymentNoticeId { get; internal set; }

        /// <summary>
        ///     发票ID
        /// </summary>
        public int InvoiceId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     发票
        /// </summary>
        public virtual Invoice Invoice { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置发票
        /// </summary>
        /// <param name="invoice">发票</param>
        public void SetInvoice(Invoice invoice)
        {
            if (invoice == null || invoice.IsTransient())
            {
                throw new ArgumentException("发票参数为空！");
            }

            Invoice = invoice;
            InvoiceId = invoice.Id;
            InvoiceNumber = invoice.InvoiceNumber;
            if (invoice is PurchaseInvoice)
            {
                InvoiceType = InvoiceType.采购发票;
            }
            else if (invoice is PrepaymentInvoice)
            {
                InvoiceType = InvoiceType.预付款发票;
            }
            else if (invoice is LeaseInvoice)
            {
                InvoiceType = InvoiceType.租赁发票;
            }
        }

        #endregion

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}