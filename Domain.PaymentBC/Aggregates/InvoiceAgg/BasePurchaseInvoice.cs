#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/28 15:04:12
// 文件名：BasePurchaseInvoice
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/28 15:04:12
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg
{
    /// <summary>
    ///     发票聚合根
    /// </summary>
    public class BasePurchaseInvoice: Invoice
    {
        #region 私有字段

        private HashSet<PurchaseInvoiceLine> _lines;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal BasePurchaseInvoice()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性
        /// <summary>
        ///     发票行
        /// </summary>
        public virtual ICollection<PurchaseInvoiceLine> InvoiceLines
        {
            get { return _lines ?? (_lines = new HashSet<PurchaseInvoiceLine>()); }
            set { _lines = new HashSet<PurchaseInvoiceLine>(value); }
        }
        #endregion

        #region 操作
        /// <summary>
        ///     设置发票金额
        /// </summary>
        public void SetInvoiceValue()
        {
            SetInvoiceValue(InvoiceLines.Sum(i => i.Amount));
        }

        /// <summary>
        ///     添加发票行
        /// </summary>
        /// <param name="itemName">项名称</param>
        /// <param name="amount">金额</param>
        /// <param name="orderLine">订单行</param>
        /// <param name="note">备注</param>
        /// <returns>发票行</returns>
        public PurchaseInvoiceLine AddInvoiceLine(int itemName, decimal amount, OrderLine orderLine, string note)
        {
            var invoiceLine = new PurchaseInvoiceLine();
            invoiceLine.SetItemName((ItemNameType)itemName);
            invoiceLine.SetAmount(amount);
            invoiceLine.SetNote(note);
            invoiceLine.GenerateNewIdentity();
            invoiceLine.SetOrderLine(orderLine);

            InvoiceLines.Add(invoiceLine);

            return invoiceLine;
        }
        #endregion
    }
}
