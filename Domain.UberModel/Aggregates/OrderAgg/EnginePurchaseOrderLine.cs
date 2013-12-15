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
using UniCloud.Domain.UberModel.Aggregates.MaterialAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.OrderAgg
{
    /// <summary>
    ///     订单聚合根
    ///     发动机购买订单行
    /// </summary>
    public class EnginePurchaseOrderLine : OrderLine
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EnginePurchaseOrderLine()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     发动机物料ID
        /// </summary>
        public int EngineMaterialId { get; private set; }

        /// <summary>
        ///     购买合同发动机ID
        /// </summary>
        public int ContractEngineId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     发动机物料
        /// </summary>
        public virtual EngineMaterial EngineMaterial { get; private set; }

        /// <summary>
        ///     购买合同发动机
        /// </summary>
        public virtual PurchaseContractEngine PurchaseContractEngine { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置发动机物料
        /// </summary>
        /// <param name="engineMaterial">发动机物料</param>
        public void SetEngineMaterial(EngineMaterial engineMaterial)
        {
            if (engineMaterial == null || engineMaterial.IsTransient())
            {
                throw new ArgumentException("发动机物料参数为空！");
            }

            EngineMaterial = engineMaterial;
            EngineMaterialId = engineMaterial.Id;
        }

        /// <summary>
        ///     设置发动机物料
        /// </summary>
        /// <param name="engineMaterialId">发动机物料ID</param>
        public void SetEngineMaterial(int engineMaterialId)
        {
            if (engineMaterialId == 0)
            {
                throw new ArgumentException("发动机物料ID参数为空！");
            }

            EngineMaterialId = engineMaterialId;
        }

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