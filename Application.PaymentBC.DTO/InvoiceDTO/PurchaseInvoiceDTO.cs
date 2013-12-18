#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 10:23:29
// 文件名：PurchaseInvoiceDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    /// 采购发票
    /// </summary>
    [DataServiceKey("PurchaseInvoiceId")]
    public class PurchaseInvoiceDTO
    {
        #region 私有字段

        private List<InvoiceLineDTO> _lines;

        #endregion

        #region 属性
        /// <summary>
        /// 采购发票主键
        /// </summary>
        public int PurchaseInvoiceId { get; set; }

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

        #endregion

        #region 外键属性

        /// <summary>
        ///     订单ID
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        ///     付款计划行ID
        /// </summary>
        public int? PaymentScheduleLineId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///    发票行集合
        /// </summary>
        public List<InvoiceLineDTO> InvoiceLines
        {
            get { return _lines ?? (_lines = new List<InvoiceLineDTO>()); }
            set { _lines = value; }
        }
        #endregion
    }
}
