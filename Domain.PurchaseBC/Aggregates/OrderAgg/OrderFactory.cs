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
    ///     订单工厂
    /// </summary>
    public static class OrderFactory
    {
        /// <summary>
        ///     创建飞机租赁订单
        /// </summary>
        /// <param name="version">版本</param>
        /// <param name="totalAmount">总金额</param>
        /// <param name="operatorName">经办人</param>
        /// <param name="orderDate">订单日期</param>
        /// <returns>飞机租赁订单</returns>
        public static AircraftLeaseOrder CreateAircraftLeaseOrder(int version, decimal totalAmount,
            string operatorName, DateTime orderDate)
        {
            var aircraftLeaseOrder = new AircraftLeaseOrder
            {
                Version = version,
                TotalAmount = totalAmount,
                OperatorName = operatorName,
                CreateDate = DateTime.Now,
                OrderDate = orderDate,
                IsValid = true
            };

            aircraftLeaseOrder.GenerateNewIdentity();

            return aircraftLeaseOrder;
        }

        /// <summary>
        ///     创建飞机购买订单
        /// </summary>
        /// <param name="version">版本</param>
        /// <param name="totalAmount">总金额</param>
        /// <param name="operatorName">经办人</param>
        /// <param name="orderDate">订单日期</param>
        /// <returns>飞机购买订单</returns>
        public static AircraftPurchaseOrder CreateAircraftPurchaseOrder(int version, decimal totalAmount,
            string operatorName, DateTime orderDate)
        {
            var aircraftPurchaseOrder = new AircraftPurchaseOrder
            {
                Version = version,
                TotalAmount = totalAmount,
                OperatorName = operatorName,
                CreateDate = DateTime.Now,
                OrderDate = orderDate,
                IsValid = true
            };

            aircraftPurchaseOrder.GenerateNewIdentity();

            return aircraftPurchaseOrder;
        }

        /// <summary>
        ///     创建BFE购买订单
        /// </summary>
        /// <param name="version">版本</param>
        /// <param name="totalAmount">总金额</param>
        /// <param name="operatorName">经办人</param>
        /// <param name="orderDate">订单日期</param>
        /// <returns>BFE购买订单</returns>
        public static BFEPurchaseOrder CreateBFEPurchaseOrder(int version, decimal totalAmount,
            string operatorName, DateTime orderDate)
        {
            var bfePurchaseOrder = new BFEPurchaseOrder
            {
                Version = version,
                TotalAmount = totalAmount,
                OperatorName = operatorName,
                CreateDate = DateTime.Now,
                OrderDate = orderDate,
                IsValid = true
            };

            bfePurchaseOrder.GenerateNewIdentity();

            return bfePurchaseOrder;
        }
    }
}