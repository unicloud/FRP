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

using UniCloud.Domain.UberModel.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.InvoiceAgg
{
    /// <summary>
    ///     发票聚合根
    ///     租赁发票
    /// </summary>
    public class LeaseInvoice : Invoice
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal LeaseInvoice()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     添加租赁发票行
        /// </summary>
        /// <param name="itemName">项名称</param>
        /// <param name="amount">金额</param>
        /// <param name="orderLine">订单行</param>
        /// <returns>租赁发票行</returns>
        public LeaseInvoiceLine AddLeaseInvoiceLine(string itemName, decimal amount, OrderLine orderLine)
        {
            var invoiceLine = new LeaseInvoiceLine
            {
                ItemName = itemName,
                Amount = amount,
            };
            invoiceLine.GenerateNewIdentity();
            invoiceLine.SetOrderLine(orderLine);

            InvoiceLines.Add(invoiceLine);

            return invoiceLine;
        }

        #endregion
    }
}