#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，10:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.ContractEngineAgg
{
    /// <summary>
    ///     合同发动机聚合根
    ///     租赁合同发动机
    /// </summary>
    public class LeaseContractEngine : ContractEngine
    {
        #region 私有字段

        private HashSet<EngineLeaseOrderLine> _engineLeaseOrderLines;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal LeaseContractEngine()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        /// <summary>
        ///     租赁发动机订单行
        /// </summary>
        public virtual ICollection<EngineLeaseOrderLine> EngineLeaseOrderLines
        {
            get { return _engineLeaseOrderLines ?? (_engineLeaseOrderLines = new HashSet<EngineLeaseOrderLine>()); }
            set { _engineLeaseOrderLines = new HashSet<EngineLeaseOrderLine>(value); }
        }

        #endregion

        #region 操作

        #endregion
    }
}