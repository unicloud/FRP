#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，11:11
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

namespace UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg
{
    /// <summary>
    ///     订单聚合根
    ///     发动机租赁订单行
    /// </summary>
    public class EngineLeaseOrderLine : OrderLine
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EngineLeaseOrderLine()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     租赁合同发动机ID
        /// </summary>
        public int ContractEngineId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     租赁合同发动机
        /// </summary>
        public virtual LeaseContractEngine LeaseContractEngine { get; set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置租赁合同发动机
        /// </summary>
        /// <param name="leaseContractEngine">租赁合同发动机</param>
        public void SetContractEngine(LeaseContractEngine leaseContractEngine)
        {
            if (leaseContractEngine == null || leaseContractEngine.IsTransient())
            {
                throw new ArgumentException("租赁合同发动机参数为空！");
            }

            LeaseContractEngine = leaseContractEngine;
            ContractEngineId = leaseContractEngine.Id;
        }

        #endregion
    }
}