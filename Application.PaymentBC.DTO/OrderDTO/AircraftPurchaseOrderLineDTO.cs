﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/17，19:11
// 方案：FRP
// 项目：Application.PurchaseBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     购买飞机订单行DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class AircraftPurchaseOrderLineDTO
    {
        /// <summary>
        ///     订单行ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     单价
        ///     <remarks>
        ///         单价不能小于0
        ///     </remarks>
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        ///     数量
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        ///     折扣
        ///     <remarks>
        ///         取值范围为 [0-100]
        ///     </remarks>
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        ///     机身价格
        /// </summary>
        public decimal AirframePrice { get; set; }

        /// <summary>
        ///     机身改装费用
        /// </summary>
        public decimal RefitCost { get; set; }

        /// <summary>
        ///     发动机价格
        /// </summary>
        public decimal EnginePrice { get; set; }

        /// <summary>
        ///     预计交付日期
        /// </summary>
        public DateTime EstimateDeliveryDate { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     行金额
        /// </summary>
        public decimal TotalLine { get; set; }

        /// <summary>
        ///     订单Id
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        ///     合同飞机ID
        /// </summary>
        public int ContractAircraftId { get; set; }
    }
}