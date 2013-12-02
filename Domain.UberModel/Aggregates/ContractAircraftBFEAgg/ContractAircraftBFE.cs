#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/02，17:12
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using UniCloud.Domain.UberModel.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.UberModel.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.ContractAircraftBFEAgg
{
    /// <summary>
    ///     合同飞机BFE
    /// </summary>
    public class ContractAircraftBFE
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制创建新实例
        /// </summary>
        internal ContractAircraftBFE()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     合同飞机ID
        /// </summary>
        public int ContractAircraftId { get; internal set; }

        /// <summary>
        ///     BFE采购订单ID
        /// </summary>
        public int BFEPurchaseOrderId { get; internal set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     物料
        /// </summary>
        public virtual ContractAircraft ContractAircraft { get; internal set; }

        /// <summary>
        ///     供应商公司
        /// </summary>
        public virtual BFEPurchaseOrder BFEPurchaseOrder { get; internal set; }

        #endregion

        #region 操作

        #endregion
    }
}