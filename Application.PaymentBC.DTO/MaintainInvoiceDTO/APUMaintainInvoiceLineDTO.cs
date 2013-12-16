#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/16 10:13:41
// 文件名：APUMaintainInvoiceLineDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/16 10:13:41
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///  APU维修发票行DTO
    /// </summary>
    [DataServiceKey("APUMaintainInvoiceLineId")]
    public class APUMaintainInvoiceLineDTO : MaintainInvoiceLineDTO
    {
        #region 属性
        /// <summary>
        /// APU维修发票行主键
        /// </summary>
        public int APUMaintainInvoiceLineId { get; set; }
        #endregion

        #region 外键属性

        #endregion
    }
}
