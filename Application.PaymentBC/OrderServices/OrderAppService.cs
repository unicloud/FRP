#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/17 9:53:58
// 文件名：OrderAppService
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
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.Query.OrderQueries;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Application.PaymentBC.OrderServices
{
    /// <summary>
    /// 查询订单服务实现
    /// </summary>
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderQuery _orderQuery;

        public OrderAppService(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
        }

        #region IOrderAppService 成员

        public IQueryable<OrderDTO> GetOrders()
        {
            var query = new QueryBuilder<Order>();
            return _orderQuery.OrderDTOQuery(query);
        }

        public IQueryable<PurchaseOrderDTO> GetPurchaseOrders()
        {
            var query = new QueryBuilder<Order>();
            return _orderQuery.PurchaseOrderQuery(query);
        }

        public IQueryable<LeaseOrderDTO> GetLeaseOrders()
        {
            var query = new QueryBuilder<Order>();
            return _orderQuery.LeaseOrderQuery(query);
        }

        public IQueryable<AircraftLeaseOrderDTO> GetAircraftLeaseOrders()
        {
            var query = new QueryBuilder<Order>();
            return _orderQuery.AircraftLeaseOrderQuery(query);
        }

        public IQueryable<AircraftPurchaseOrderDTO> GetAircraftPurchaseOrders()
        {
            var query = new QueryBuilder<Order>();
            return _orderQuery.AircraftPurchaseOrderQuery(query);
        }

        public IQueryable<EngineLeaseOrderDTO> GetEngineLeaseOrders()
        {
            var query = new QueryBuilder<Order>();
            return _orderQuery.EngineLeaseOrderQuery(query);
        }

        public IQueryable<EnginePurchaseOrderDTO> GetEnginePurchaseOrders()
        {
            var query = new QueryBuilder<Order>();
            return _orderQuery.EnginePurchaseOrderQuery(query);
        }

        public IQueryable<BFEPurchaseOrderDTO> GetBFEPurchaseOrders()
        {
            var query = new QueryBuilder<Order>();
            return _orderQuery.BFEPurchaseOrderQuery(query);
        }

        /// <summary>
        ///     获取标准采购订单集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<StandardOrderDTO> GetStandardOrders()
        {
            var query = new QueryBuilder<Order>();
            return _orderQuery.StandardOrderQuery(query);
        }

        #endregion
    }
}
