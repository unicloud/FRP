#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/26，15:53
// 方案：FRP
// 项目：Application.PurchaseBC.DTO
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     购买发动机订单DTO
    /// </summary>
    [DataServiceKey("OrderId")]
    public class EnginePurchaseOrderDTO
    {
        /// <summary>
        ///     订单ID
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        ///     版本号
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        ///     币种
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        ///     总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     经办人
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        ///     联系人
        /// </summary>
        public string Linkman { get; set; }

        /// <summary>
        ///     生效日期
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        ///     订单状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     购买发动机订单行集合
        /// </summary>
        public List<EnginePurchaseOrderLineDTO> EnginePurchaseOrderLines { get; set; }
    }
}