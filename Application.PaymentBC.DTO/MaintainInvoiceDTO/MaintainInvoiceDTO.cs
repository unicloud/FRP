#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/16 9:45:27
// 文件名：MaintainInvoiceDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/16 9:45:27
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///  维修发票基类
    /// </summary>
    public class MaintainInvoiceDTO
    {
        #region 属性
        /// <summary>
        ///     序列号
        /// </summary>
        public string SerialNumber { get;  set; }

        /// <summary>
        ///     发票编号
        /// </summary>
        public string InvoiceNumber { get;  set; }

        /// <summary>
        ///     发票号码
        /// </summary>
        public string InvoideCode { get;  set; }

        /// <summary>
        ///     发票日期
        /// </summary>
        public DateTime InvoiceDate { get;  set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get;  set; }

        /// <summary>
        ///     发票金额
        /// </summary>
        public decimal InvoiceValue { get;  set; }

        /// <summary>
        ///     已付金额
        /// </summary>
        public decimal PaidAmount { get;  set; }

        /// <summary>
        ///     经办人
        /// </summary>
        public string OperatorName { get;  set; }

        /// <summary>
        ///     审核人
        /// </summary>
        public string Reviewer { get;  set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get;  set; }

        /// <summary>
        ///     审核日期
        /// </summary>
        public DateTime? ReviewDate { get;  set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get;  set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsCompleted { get;  set; }

        /// <summary>
        ///     发票状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///  维修发票行集合
        /// </summary>
        public virtual List<MaintainInvoiceLineDTO> MaintainInvoiceLines { get; set; }
        #endregion

        #region 外键属性

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get;  set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get;  set; }

        #endregion
    }
}
