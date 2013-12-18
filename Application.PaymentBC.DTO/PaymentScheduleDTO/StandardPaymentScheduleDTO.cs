#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/18，13:12
// 文件名：StandardPaymentScheduleDTO.cs
// 程序集：UniCloud.Application.PaymentBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System;
using System.Collections.Generic;
using System.Data.Services.Common;


namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    /// 标准付款计划
    /// </summary>
    [DataServiceKey("StandardPaymentScheduleId")]
    public class StandardPaymentScheduleDTO
    {
        public StandardPaymentScheduleDTO()
        {
            PaymentScheduleLines = new List<PaymentScheduleLineDTO>();
        }
        /// <summary>
        /// 主键
        /// </summary>
        public int StandardPaymentScheduleId { get; set; }

        /// <summary>
        ///  创建时间
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
        public int CurrencyId { get;  set; }

        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        /// 合同发动机主键
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        ///     付款计划行
        /// </summary>
        public List<PaymentScheduleLineDTO> PaymentScheduleLines { get; set; }
    }
}
