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

using System;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.TradeQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.TradeServices
{
    public class TradeAppService : ITradeAppService
    {
        private readonly IOrderQuery _orderQuery;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ITradeQuery _tradeQuery;
        private readonly ITradeRepository _tradeRepository;

        public TradeAppService(ITradeQuery queryTrade, IOrderQuery orderQuery, ITradeRepository tradeRepository,
            ISupplierRepository supplierRepository)
        {
            _tradeQuery = queryTrade;
            _orderQuery = orderQuery;
            _tradeRepository = tradeRepository;
            _supplierRepository = supplierRepository;
        }

        #region ITradeAppService 成员

        public IQueryable<TradeDTO> GetTrades()
        {
            var query = new QueryBuilder<Trade>();
            return _tradeQuery.TradesQuery(query);
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

        [Insert(typeof (TradeDTO))]
        public void InsertTrade(TradeDTO trade)
        {
            var supplier = _supplierRepository.GetAll().FirstOrDefault();
            var newTrade = TradeFactory.CreateTrade(null, null, DateTime.Now, 1);
            newTrade.SetSupplier(supplier);
            _tradeRepository.Add(newTrade);
        }

        [Update(typeof (TradeDTO))]
        public void UpdateTrade(TradeDTO trade)
        {
            var updateTrade = _tradeRepository.GetFiltered(t => t.TradeNumber == trade.TradeNumber).FirstOrDefault();
            updateTrade.Name = trade.Name;
            _tradeRepository.Modify(updateTrade);
        }

        [Delete(typeof (TradeDTO))]
        public void DeleteTrade(TradeDTO trade)
        {
            var deleteTrade = _tradeRepository.GetFiltered(t => t.TradeNumber == trade.TradeNumber).FirstOrDefault();
            _tradeRepository.Remove(deleteTrade);
        }

        #endregion
    }
}