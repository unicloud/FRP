﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/26，15:45
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

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     租赁发动机订单DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class EngineLeaseOrderDTO
    {
        #region 私有字段

        private HashSet<EngineLeaseOrderLineDTO> _lines;

        #endregion

        /// <summary>
        ///     订单ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     交易ID
        /// </summary>
        public int TradeId { get; set; }

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
        ///     联系人ID
        /// </summary>
        public int LinkmanId { get; set; }

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
        ///     租赁发动机订单行集合
        /// </summary>
        public virtual ICollection<EngineLeaseOrderLineDTO> EngineLeaseOrderLines
        {
            get { return _lines ?? (_lines = new HashSet<EngineLeaseOrderLineDTO>()); }
            set { _lines = new HashSet<EngineLeaseOrderLineDTO>(value); }
        }
    }
}