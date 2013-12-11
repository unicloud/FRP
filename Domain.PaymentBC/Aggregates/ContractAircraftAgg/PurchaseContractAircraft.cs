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

namespace UniCloud.Domain.PaymentBC.Aggregates.ContractAircraftAgg
{
    /// <summary>
    ///     合同飞机聚合根
    ///     购买合同飞机
    /// </summary>
    public class PurchaseContractAircraft : ContractAircraft
    {
        #region 私有字段

        private HashSet<AircraftPurchaseOrderLine> _aircraftPurchaseOrderLines;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal PurchaseContractAircraft()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     总价
        /// </summary>
        public decimal TotalPrice { get; protected set; }

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

        /// <summary>
        ///     BFE价格
        /// </summary>
        public decimal BFEPrice { get; protected set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        /// <summary>
        ///     租赁飞机订单行
        /// </summary>
        public virtual ICollection<AircraftPurchaseOrderLine> AircraftPurchaseOrderLines
        {
            get
            {
                return _aircraftPurchaseOrderLines ??
                       (_aircraftPurchaseOrderLines = new HashSet<AircraftPurchaseOrderLine>());
            }
            set { _aircraftPurchaseOrderLines = new HashSet<AircraftPurchaseOrderLine>(value); }
        }

        #endregion

        #region 操作

        #endregion
    }
}