#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/7 17:49:09
// 文件名：BaseInvoice
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/7 17:49:09
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
    ///     发票
    /// </summary>
    [DataServiceKey("InvoiceId")]
    public class BaseInvoiceDTO
    {
        #region 属性

        /// <summary>
        ///  发票主键
        /// </summary>
        public int InvoiceId { get; set; }

        /// <summary>
        ///     发票编号
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        ///     发票类型
        /// </summary>
        public int InvoiceType { get; set; }

        /// <summary>
        ///     发票类型
        /// </summary>
        public string InvoiceTypeString
        {
            get { return ((InvoiceType)InvoiceType).ToString(); }
            set { InvoiceType = (int)(InvoiceType)Enum.Parse(typeof(InvoiceType), value, true); }
        }
        #endregion
    }
}
