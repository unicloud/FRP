#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/20 10:33:46
// 文件名：PaymentNoticeDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/20 10:33:46
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     付款通知
    /// </summary>
    [DataServiceKey("PaymentNoticeId")]
    public class PaymentNoticeDTO
    {
        #region 属性

        /// <summary>
        ///     付款通知行
        /// </summary>
        private List<PaymentNoticeLineDTO> _paymentNoticeLines;

        /// <summary>
        ///     付款通知主键
        /// </summary>
        public int PaymentNoticeId { get; set; }

        /// <summary>
        ///     通知编号
        /// </summary>
        public string NoticeNumber { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     付款期限
        /// </summary>
        public DateTime DeadLine { get; set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        ///     币种
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        ///     银行账户
        /// </summary>
        public string BankAccountName { get; set; }

        /// <summary>
        ///     经办人
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        ///     审核人
        /// </summary>
        public string Reviewer { get; set; }

        /// <summary>
        ///     审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        ///     付款通知状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     付款通知状态
        /// </summary>
        public string StatusString
        {
            get { return ((PaymentNoticeStatus) Status).ToString(); }
            set { Status = (int) (PaymentNoticeStatus) Enum.Parse(typeof (PaymentNoticeStatus), value, true); }
        }

        public List<PaymentNoticeLineDTO> PaymentNoticeLines
        {
            get { return _paymentNoticeLines ?? new List<PaymentNoticeLineDTO>(); }
            set { _paymentNoticeLines = value; }
        }

        #endregion

        #region 外键属性

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///     银行账户ID
        /// </summary>
        public int BankAccountId { get; set; }

        #endregion
    }
}