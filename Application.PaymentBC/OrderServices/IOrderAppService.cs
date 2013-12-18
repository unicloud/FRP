#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/17 9:54:16
// 文件名：IOrderAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Application.PaymentBC.DTO;

#endregion

namespace UniCloud.Application.PaymentBC.OrderServices
{
    /// <summary>
    /// 查询订单服务接口
    /// </summary>
    public interface IOrderAppService
    {
        /// <summary>
        ///     获取所有订单
        /// </summary>
        /// <returns></returns>
        IQueryable<OrderDTO> GetOrders();

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
      
    }
}
