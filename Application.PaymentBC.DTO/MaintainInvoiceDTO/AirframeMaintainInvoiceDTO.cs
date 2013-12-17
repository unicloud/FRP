﻿#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/16 10:05:14
// 文件名：AirframeMaintainInvoiceDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/16 10:05:14
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///  机身维修发票DTO
    /// </summary>
    [DataServiceKey("AirframeMaintainInvoiceId")]
    public class AirframeMaintainInvoiceDTO : MaintainInvoiceDTO
    {
        #region 属性
        /// <summary>
        /// 机身维修发票主键
        /// </summary>
        public int AirframeMaintainInvoiceId { get; set; }

        /// <summary>
        ///  机身维修发票行集合
        /// </summary>
        public virtual List<MaintainInvoiceLineDTO> MaintainInvoiceLines { get; set; }
        #endregion

        #region 外键属性

        #endregion
    }
}