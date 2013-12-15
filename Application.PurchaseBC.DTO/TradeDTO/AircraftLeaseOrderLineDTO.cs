#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/26，12:01
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
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     租赁飞机订单行DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class AircraftLeaseOrderLineDTO
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
        ///     预计交付日期
        /// </summary>
        public DateTime EstimateDeliveryDate { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     合同飞机ID
        /// </summary>
        public int ContractAircraftId { get; set; }

        /// <summary>
        ///     飞机物料ID
        /// </summary>
        public int AircraftMaterialId { get; set; }
    }
}