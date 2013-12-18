#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/27，18:00
// 方案：FRP
// 项目：Application.PurchaseBC.Query
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;
#endregion

namespace UniCloud.Application.PaymentBC.Query.OrderQueries
{
    public interface IOrderQuery
    {
        /// <summary>
        ///     查询所有订单
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>租赁飞机订单集合</returns>
        IQueryable<OrderDTO> OrderDTOQuery(QueryBuilder<Order> query);

        /// <summary>
        ///     查询飞机租赁订单
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>租赁飞机订单集合</returns>
        IQueryable<AircraftLeaseOrderDTO> AircraftLeaseOrderQuery(QueryBuilder<Order> query);

        /// <summary>
        ///     查询飞机购买订单
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>购买飞机订单集合</returns>
        IQueryable<AircraftPurchaseOrderDTO> AircraftPurchaseOrderQuery(QueryBuilder<Order> query);

        /// <summary>
        ///     查询发动机租赁订单
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>租赁发动机订单集合</returns>
        IQueryable<EngineLeaseOrderDTO> EngineLeaseOrderQuery(QueryBuilder<Order> query);

        /// <summary>
        ///     查询发动机购买订单
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>购买发动机订单集合</returns>
        IQueryable<EnginePurchaseOrderDTO> EnginePurchaseOrderQuery(QueryBuilder<Order> query);

        /// <summary>
        ///     查询BFE采购订单
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>采购BFE订单集合</returns>
        IQueryable<BFEPurchaseOrderDTO> BFEPurchaseOrderQuery(QueryBuilder<Order> query);

    }
}