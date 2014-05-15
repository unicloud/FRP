#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/28 9:17:34
// 文件名：MaintainInvoiceLine
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/28 9:17:34
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg
{
    /// <summary>
    /// 维修发票聚合根
    /// 维修发票行
    /// </summary>
    public class MaintainInvoiceLine : InvoiceLine
    {
        #region 属性
        /// <summary>
        ///     维修项
        /// </summary>
        public MaintainItem MaintainItem { get; private set; }

        /// <summary>
        ///     单价
        /// </summary>
        public decimal UnitPrice { get; private set; }
        #endregion

        #region 外键属性
        /// <summary>
        ///     发票ID
        /// </summary>
        public int InvoiceId { get; internal set; }
        #endregion

        #region 操作
        /// <summary>
        /// 设置维修项
        /// </summary>
        /// <param name="maintainItem">维修项</param>
        public void SetMaintainItem(MaintainItem maintainItem)
        {
            MaintainItem = maintainItem;
        }

        /// <summary>
        /// 设置单价
        /// </summary>
        /// <param name="unitPrice">单价</param>
        public void SetUnitPrice(decimal unitPrice)
        {
            if (unitPrice == 0)
                unitPrice = 1;
            UnitPrice = unitPrice;
        }
        #endregion
    }
}
