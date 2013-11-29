#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，11:11
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.UberModel.Aggregates.ContractEngineAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.OrderAgg
{
    /// <summary>
    ///     订单聚合根
    ///     发动机购买订单行
    /// </summary>
    public class EnginePurchaseOrderLine : OrderLine
    {
        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     购买合同发动机ID
        /// </summary>
        public int ContractEngineId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     购买合同发动机
        /// </summary>
        public virtual PurchaseContractEngine PurchaseContractEngine { get; set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置购买合同发动机
        /// </summary>
        /// <param name="purchaseContractEngine">购买合同发动机</param>
        public void SetContractEngine(PurchaseContractEngine purchaseContractEngine)
        {
            if (purchaseContractEngine == null || purchaseContractEngine.IsTransient())
            {
                throw new ArgumentException("购买合同发动机参数为空！");
            }

            PurchaseContractEngine = purchaseContractEngine;
            ContractEngineId = purchaseContractEngine.Id;
        }

        #endregion
    }
}