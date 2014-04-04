#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/22 22:12:46
// 文件名：PurchaseOrderDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
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
    ///     采购订单DTO，包含飞机采购订单、发动机采购订单、BFE采购订单
    /// </summary>
    [DataServiceKey("Id")]
    public class PurchaseOrderDTO
    {
        public PurchaseOrderDTO()
        {
            OrderLines = new List<OrderLineDTO>();
        }

        /// <summary>
        ///     订单ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     合同名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     版本号
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        ///     经办人
        /// </summary>
        public string OperatorName { get; set; }


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
        ///     供应商外键
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        ///     订单行集合
        /// </summary>
        public List<OrderLineDTO> OrderLines { get; set; }
    }
}