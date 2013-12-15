#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 10:32:47
// 文件名：PrepaymentInvoiceLineDTO
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

namespace UniCloud.Application.PaymentBC.DTO.InvoiceDTO
{
    /// <summary>
    /// 预付款发票
    /// 预付款发票行DTO
    /// </summary>
    [DataServiceKey("PrepaymentInvoiceLineId")]
    public class PrepaymentInvoiceLineDTO
    {
        #region 属性
        /// <summary>
        ///     主键
        /// </summary>
        public int PrepaymentInvoiceLineId { get; set; }

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
