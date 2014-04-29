#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/29 14:04:14
// 文件名：MaintainPaymentScheduleDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/29 14:04:14
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     维修付款计划
    /// </summary>
    [DataServiceKey("MaintainPaymentScheduleId")]
    public class MaintainPaymentScheduleDTO
    {

        public MaintainPaymentScheduleDTO()
        {
            PaymentScheduleLines = new List<PaymentScheduleLineDTO>();
        }

        /// <summary>
        ///     主键
        /// </summary>
        public int MaintainPaymentScheduleId { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        ///     币种名称
        /// </summary>
        public string CurrencyName { get; set; }


        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///     付款计划行
        /// </summary>
        public List<PaymentScheduleLineDTO> PaymentScheduleLines { get; set; }
    }
}
