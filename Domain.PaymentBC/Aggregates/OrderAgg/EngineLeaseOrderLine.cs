﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/07，16:12
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using UniCloud.Domain.PaymentBC.Aggregates.ContractEngineAgg;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.OrderAgg
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
        ///     发动机物料ID
        /// </summary>
        public int EngineMaterialId { get; protected set; }

        /// <summary>
        ///     租赁合同发动机ID
        /// </summary>
        public int ContractEngineId { get; protected set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     租赁合同发动机
        /// </summary>
        public virtual LeaseContractEngine LeaseContractEngine { get; protected set; }

        #endregion

        #region 操作

        #endregion
    }
}