#region 版本信息

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

using UniCloud.Domain.PaymentBC.Aggregates.ContractAircraftAgg;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.OrderAgg
{
    /// <summary>
    ///     订单聚合根
    ///     飞机购买订单行
    /// </summary>
    public class AircraftPurchaseOrderLine : OrderLine
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftPurchaseOrderLine()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     机身价格
        /// </summary>
        public decimal AirframePrice { get; protected set; }

        /// <summary>
        ///     机身改装费用
        /// </summary>
        public decimal RefitCost { get; protected set; }

        /// <summary>
        ///     发动机价格
        /// </summary>
        public decimal EnginePrice { get; protected set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机物料ID
        /// </summary>
        public int AircraftMaterialId { get; protected set; }

        /// <summary>
        ///     购买合同飞机ID
        /// </summary>
        public int ContractAircraftId { get; protected set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     购买合同飞机
        /// </summary>
        public virtual PurchaseContractAircraft PurchaseContractAircraft { get; protected set; }

        #endregion

        #region 操作

        #endregion
    }
}