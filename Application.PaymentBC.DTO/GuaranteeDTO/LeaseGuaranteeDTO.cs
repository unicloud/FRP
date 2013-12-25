#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/25，16:12
// 文件名：LeaseGuaranteeDTO.cs
// 程序集：UniCloud.Application.PaymentBC.DTO
// 版本：V`1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO.GuaranteeDTO
{
    /// <summary>
    ///     租赁保证金
    /// </summary>
    [DataServiceKey("GuaranteeId")]
    public class LeaseGuaranteeDTO
    {
        /// <summary>
        ///     主键
        /// </summary>
        public int GuaranteeId { get; set; }

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        ///     支付金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        ///     币种名称
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        ///     经办人
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        ///     审核人
        /// </summary>
        public string Reviewer { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     审核日期
        /// </summary>
        public DateTime ReviewDate { get; set; }

        /// <summary>
        ///     保函状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///     订单ID
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; set; }
    }
}