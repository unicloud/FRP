#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 10:22:15
// 文件名：PrepaymentInvoiceDTO
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
    /// 预付款发票
    /// </summary>
    [DataServiceKey("PrepaymentInvoiceId")]
    public class PrepaymentInvoiceDTO
    {
        public PrepaymentInvoiceDTO()
        {
            InvoiceLines=new List<PrepaymentInvoiceLineDTO>();
        }

        #region 属性
        /// <summary>
        /// 预付款发票主键
        /// </summary>
        public int PrepaymentInvoiceId { get; set; }

        /// <summary>
        ///     发票编号
        /// </summary>
        public string InvoiceNumber { get; private set; }

        /// <summary>
        ///     发票号码
        /// </summary>
        public string InvoideCode { get; internal set; }

        /// <summary>
        ///     发票日期
        /// </summary>
        public DateTime InvoiceDate { get; internal set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; private set; }

        /// <summary>
        ///     发票金额
        /// </summary>
        public decimal InvoiceValue { get; private set; }

        /// <summary>
        ///     已付金额
        /// </summary>
        public decimal PaidAmount { get; private set; }

        /// <summary>
        ///     经办人
        /// </summary>
        public string OperatorName { get; private set; }

        /// <summary>
        ///     审核人
        /// </summary>
        public string Reviewer { get; private set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     审核日期
        /// </summary>
        public DateTime? ReviewDate { get; private set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        ///     发票状态
        /// </summary>
        public int Status { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     订单ID
        /// </summary>
        public int OrderId { get; private set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; private set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; private set; }

        /// <summary>
        ///     付款计划行ID
        /// </summary>
        public int? PaymentScheduleLineId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///    预付款发票行集合
        /// </summary>
        public List<PrepaymentInvoiceLineDTO> InvoiceLines { get; set; }

        #endregion
    }
}
