#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/29 9:25:58
// 文件名：MaintainInvoiceLineDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/29 9:25:58
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     维修发票行DTO
    /// </summary>
    [DataServiceKey("MaintainInvoiceLineId")]
    public class MaintainInvoiceLineDTO
    {
        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int MaintainInvoiceLineId { get; set; }

        /// <summary>
        ///     维修项
        /// </summary>
        public int MaintainItem { get; set; }

        /// <summary>
        ///     项名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        ///     单价
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        ///    数量
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     发票ID
        /// </summary>
        public int InvoiceId { get; set; }

        #endregion

        #region 导航属性

        #endregion
    }
}
