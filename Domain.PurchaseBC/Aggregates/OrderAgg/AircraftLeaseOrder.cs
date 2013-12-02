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

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg
{
    /// <summary>
    ///     订单聚合根
    ///     飞机租赁订单
    /// </summary>
    public class AircraftLeaseOrder : Order
    {
                #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能通过工厂方法去创建新实例
        /// </summary>
        internal AircraftLeaseOrder()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     添加飞机租赁订单行
        /// </summary>
        /// <param name="price">单价</param>
        /// <param name="amount">数量</param>
        /// <param name="discount">折扣</param>
        /// <param name="delivery">预计交付日期</param>
        /// <returns>飞机租赁订单行</returns>
        public AircraftLeaseOrderLine AddNewAircraftLeaseOrderLine(decimal price, int amount, decimal discount,
            DateTime delivery)
        {
            var aircraftLeaseOrderLine = new AircraftLeaseOrderLine
            {
                OrderId = Id,
                UnitPrice = price,
                Amount = amount,
                Discount = discount,
                EstimateDeliveryDate = delivery
            };

            aircraftLeaseOrderLine.GenerateNewIdentity();
            OrderLines.Add(aircraftLeaseOrderLine);

            return aircraftLeaseOrderLine;
        }

        #endregion
    }
}