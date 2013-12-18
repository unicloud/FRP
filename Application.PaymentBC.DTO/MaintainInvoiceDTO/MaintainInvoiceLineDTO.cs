#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/16 9:55:20
// 文件名：MaintainInvoiceLineDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/16 9:55:20
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Data.Services.Common;
using UniCloud.Domain.PaymentBC.Enums;

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    /// 维修发票行基类
    /// </summary>
   [DataServiceKey("MaintainInvoiceLineId")]
    public class MaintainInvoiceLineDTO
    {
        #region 属性

        /// <summary>
        /// 维修发票行主键
        /// </summary>
        public int MaintainInvoiceLineId { get; set; }

        /// <summary>
        ///     维修项
        /// </summary>
        public int MaintainItem { get; set; }

        /// <summary>
        ///     维修项
        /// </summary>
        public string MaintainItemString
        {
            get { return ((MaintainItem) MaintainItem).ToString(); }
            set { MaintainItem =(int)(MaintainItem)Enum.Parse(typeof (MaintainItem), value, true); }
        }

        /// <summary>
        ///     项名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        ///     单价
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        ///     数量
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     附件ID
        /// </summary>
        public int? PartId { get; set; }

        #endregion
    }
}
