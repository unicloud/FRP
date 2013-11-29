#region 版本信息

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
    public class LeaseContractEngine : ContractEngine
    {
        #region 私有字段

        private HashSet<EngineLeaseOrderLine> _engineLeaseOrderLines;

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