#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/13 15:06:44
// 文件名：MaintainInvoiceDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/13 15:06:44
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    /// 维修发票DTO基类
    /// </summary>
    [DataServiceKey("MaintainInvoiceId")]
    public class MaintainInvoiceDTO
    {
        public int MaintainInvoiceId { get; set; }
    }
}
