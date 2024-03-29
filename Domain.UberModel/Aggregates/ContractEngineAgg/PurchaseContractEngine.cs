﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using UniCloud.Domain.UberModel.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.ContractEngineAgg
{
    /// <summary>
    ///     合同发动机聚合根
    ///     购买合同发动机
    /// </summary>
    public class PurchaseContractEngine : ContractEngine
    {
        #region 私有字段

        private HashSet<EnginePurchaseOrderLine> _enginePurchaseOrderLines;

        #endregion

        #region 属性

        /// <summary>
        ///     总价
        /// </summary>
        public decimal TotalPrice { get; set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        /// <summary>
        ///     购买发动机订单行
        /// </summary>
        public virtual ICollection<EnginePurchaseOrderLine> EnginePurchaseOrderLines
        {
            get
            {
                return _enginePurchaseOrderLines ?? (_enginePurchaseOrderLines = new HashSet<EnginePurchaseOrderLine>());
            }
            set { _enginePurchaseOrderLines = new HashSet<EnginePurchaseOrderLine>(value); }
        }

        #endregion

        #region 操作

        #endregion
    }
}