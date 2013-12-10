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
    ///     飞机租赁订单行
    /// </summary>
    public class AircraftLeaseOrderLine : OrderLine
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftLeaseOrderLine()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     租赁合同飞机ID
        /// </summary>
        public int ContractAircraftId { get; protected set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     租赁合同飞机
        /// </summary>
        public virtual LeaseContractAircraft LeaseContractAircraft { get; protected set; }

        #endregion

        #region 操作

        #endregion
    }
}