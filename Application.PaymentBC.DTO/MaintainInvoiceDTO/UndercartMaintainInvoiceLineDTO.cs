#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/16 10:15:52
// 文件名：UndercartMaintainInvoiceLineDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/16 10:15:52
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///  起落架维修发票行DTO
    /// </summary>
    [DataServiceKey("UndercartMaintainInvoiceLineId")]
    public class UndercartMaintainInvoiceLineDTO : MaintainInvoiceLineDTO
    {
        #region 属性
        /// <summary>
        /// 起落架维修发票行主键
        /// </summary>
        public int UndercartMaintainInvoiceLineId { get; set; }
        #endregion

        #region 外键属性

        #endregion
    }
}
