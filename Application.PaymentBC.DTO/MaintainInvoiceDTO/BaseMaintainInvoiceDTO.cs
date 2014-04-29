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

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     发票
    /// </summary>
    [DataServiceKey("MaintainInvoiceId")]
    public class BaseMaintainInvoiceDTO
    {
        #region 属性

        /// <summary>
        ///  发票主键
        /// </summary>
        public int MaintainInvoiceId { get; set; }

        /// <summary>
        ///     序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        ///     发票编号
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        ///     发票号码
        /// </summary>
        public string InvoideCode { get; set; }

        /// <summary>
        ///     发票日期
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        ///     发票金额
        /// </summary>
        public decimal InvoiceValue { get; set; }

        /// <summary>
        ///     已付金额
        /// </summary>
        public decimal PaidAmount { get; set; }

        /// <summary>
        ///     经办人
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        ///     审核人
        /// </summary>
        public string Reviewer { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        ///     发票状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///  文档名称
        /// </summary>
        public string DocumentName { get; set; }

        #endregion

        #region 外键属性
        /// <summary>
        ///  文档ID
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; set; }

        #endregion
    }
}
