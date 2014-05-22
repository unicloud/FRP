#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/16 10:01:18
// 文件名：EngineMaintainInvoiceDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/16 10:01:18
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     发动机维修发票DTO
    /// </summary>
    [DataServiceKey("EngineMaintainInvoiceId")]
    public class EngineMaintainInvoiceDTO : MaintainInvoiceDTO
    {
        #region 属性

        /// <summary>
        ///     发动机维修发票主键
        /// </summary>
        public int EngineMaintainInvoiceId { get; set; }
        public int Type { get; set; }
        #endregion

        #region 外键属性

        #endregion
    }
}