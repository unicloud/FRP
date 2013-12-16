#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 10:24:10
// 文件名：PurchaseInvoiceLineDTO
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
    /// 采购发票行DTO
    /// </summary>
    [DataServiceKey("PurchaseInvoiceLineId")]
    public class PurchaseInvoiceLineDTO
    {
        #region 属性
        /// <summary>
        ///     主键
        /// </summary>
        public int PurchaseInvoiceLineId { get; set; }

        /// <summary>
        ///     项名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        ///     金额
        /// </summary>
        public decimal Amount { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     发票ID
        /// </summary>
        public int InvoiceId { get; set; }

        /// <summary>
        ///     订单行ID
        /// </summary>
        public int OrderLineId { get; set; }

        #endregion

        #region 导航属性

        #endregion
    }
}
