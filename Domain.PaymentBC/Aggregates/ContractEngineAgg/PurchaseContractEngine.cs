#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/07，17:12
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.ContractEngineAgg
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

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal PurchaseContractEngine()
        {
        }

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