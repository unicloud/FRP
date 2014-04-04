#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/16 9:58:02
// 文件名：APUMaintainInvoiceDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/16 9:58:02
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     APU维修发票DTO
    /// </summary>
    [DataServiceKey("APUMaintainInvoiceId")]
    public class APUMaintainInvoiceDTO : MaintainInvoiceDTO
    {
        #region 属性

        /// <summary>
        ///     APU维修发票主键
        /// </summary>
        public int APUMaintainInvoiceId { get; set; }

        #endregion

        #region 外键属性

        #endregion
    }
}