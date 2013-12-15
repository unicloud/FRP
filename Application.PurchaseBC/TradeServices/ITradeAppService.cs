#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/17，20:11
// 方案：FRP
// 项目：Application.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;

#endregion

namespace UniCloud.Application.PurchaseBC.TradeServices
{
    /// <summary>
    ///     应用层交易服务接口
    /// </summary>
    public interface ITradeAppService
    {
        /// <summary>
        ///     获取交易集合
        /// </summary>
        /// <returns></returns>
        IQueryable<TradeDTO> GetTrades();

        /// <summary>
        ///     获取租赁飞机订单集合
        /// </summary>
        /// <returns></returns>
        IQueryable<AircraftLeaseOrderDTO> GetAircraftLeaseOrders();

        /// <summary>
        ///     获取购买飞机订单集合
        /// </summary>
        /// <returns></returns>
        IQueryable<AircraftPurchaseOrderDTO> GetAircraftPurchaseOrders();

        /// <summary>
        ///     获取租赁发动机订单集合
        /// </summary>
        /// <returns></returns>
        IQueryable<EngineLeaseOrderDTO> GetEngineLeaseOrders();

        /// <summary>
        ///     获取购买发动机订单集合
        /// </summary>
        /// <returns></returns>
        IQueryable<EnginePurchaseOrderDTO> GetEnginePurchaseOrders();

        /// <summary>
        ///     获取BFE采购订单集合
        /// </summary>
        /// <returns></returns>
        IQueryable<BFEPurchaseOrderDTO> GetBFEPurchaseOrders();

        /// <summary>
        ///     获取订单文档集合
        /// </summary>
        /// <returns></returns>
        IQueryable<OrderDocumentDTO> GetOrderDocuments();
    }
}