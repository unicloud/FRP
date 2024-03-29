﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，14:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractEngineAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg
{
    /// <summary>
    ///     接收聚合根
    ///     购买发动机接收行
    /// </summary>
    public class EnginePurchaseReceptionLine : ReceptionLine
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EnginePurchaseReceptionLine()
        {
        }

        #endregion

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