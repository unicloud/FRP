#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/28 9:20:59
// 文件名：PurchaseInvoiceLine
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/28 9:20:59
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.UberModel.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.InvoiceAgg
{
    /// <summary>
    ///     发票聚合根
    ///     采购发票行
    /// </summary>
    public class PurchaseInvoiceLine : InvoiceLine
    {
        #region 外键属性
        /// <summary>
        ///     订单行ID
        /// </summary>
        public int? OrderLineId { get; private set; }

        /// <summary>
        ///     发票ID
        /// </summary>
        public int InvoiceId { get; internal set; }
        #endregion

        #region 操作

        /// <summary>
        ///     设置订单行
        /// </summary>
        /// <param name="orderLine">订单行</param>
        public void SetOrderLine(OrderLine orderLine)
        {
            if (orderLine != null)
            {
                OrderLineId = orderLine.Id;
            }
        }
        #endregion
    }
}
