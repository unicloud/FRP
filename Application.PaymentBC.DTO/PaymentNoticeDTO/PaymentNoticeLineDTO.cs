#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/20 10:34:03
// 文件名：PaymentNoticeLineDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/20 10:34:03
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Data.Services.Common;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     付款通知行
    /// </summary>
    [DataServiceKey("PaymentNoticeLineId")]
    public class PaymentNoticeLineDTO
    {
        #region 属性

        /// <summary>
        ///     付款通知行主键
        /// </summary>
        public int PaymentNoticeLineId { get; set; }

        /// <summary>
        ///     发票类型
        /// </summary>
        public int InvoiceType { get; set; }

        /// <summary>
        ///     发票ID
        /// </summary>
        public int InvoiceId { get; set; }

        /// <summary>
        ///     发票编号
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        ///     付款金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        #endregion
    }
}