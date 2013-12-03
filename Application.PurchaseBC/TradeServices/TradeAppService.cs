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
        private readonly IOrderRepository _orderRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ITradeQuery _tradeQuery;
        private readonly ITradeRepository _tradeRepository;

        public TradeAppService(ITradeQuery queryTrade, IOrderQuery orderQuery, ITradeRepository tradeRepository,
            IOrderRepository orderRepository, ISupplierRepository supplierRepository)
        {
            _tradeQuery = queryTrade;
            _orderQuery = orderQuery;
            _tradeRepository = tradeRepository;
            _orderRepository = orderRepository;
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

        #endregion

        #region TradeDTO

        [Insert(typeof (TradeDTO))]
        public void InsertTrade(TradeDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var supplier = _supplierRepository.Get(dto.SupplierId);
            var newTrade = TradeFactory.CreateTrade(dto.Name, dto.Description, dto.StartDate);
            newTrade.GenerateNewIdentity();
            // 设置交易编号
            var date = DateTime.Now.Date;
            var seq = _tradeRepository.GetFiltered(t => t.CreateDate > date).Count() + 1;
            newTrade.SetTradeNumber(seq);
            // 设置供应商
            newTrade.SetSupplier(supplier);

            _tradeRepository.Add(newTrade);
        }

        [Update(typeof (TradeDTO))]
        public void UpdateTrade(TradeDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var persisted = _tradeRepository.Get(dto.Id);
            if (persisted != null)
            {
                var current = MaterializeTradeFromDto(dto);
                _tradeRepository.Merge(persisted, current);
            }
        }

        [Delete(typeof (TradeDTO))]
        public void DeleteTrade(TradeDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var deleteTrade = _tradeRepository.Get(dto.Id);
            if (deleteTrade != null)
            {
                _tradeRepository.Remove(deleteTrade);
            }
        }

        private Trade MaterializeTradeFromDto(TradeDTO dto)
        {
            var supplier = _supplierRepository.Get(dto.SupplierId);
            var trade = TradeFactory.CreateTrade(dto.Name, dto.Description, dto.StartDate);
            trade.ChangeCurrentIdentity(dto.Id);
            trade.SetSupplier(supplier);

            return trade;
        }

        #endregion

        #region AircraftLeaseOrderDTO

        [Insert(typeof (AircraftLeaseOrderDTO))]
        public void InsertAircraftLeaseOrder(AircraftLeaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
        }

        [Update(typeof (AircraftLeaseOrderDTO))]
        public void UpdateAircraftLeaseOrder(AircraftLeaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var persisted = _orderRepository.Get(dto.Id);
            if (persisted != null)
            {
                var current = MaterializeAircraftLeaseOrderFromDto(dto);
                _orderRepository.Merge(persisted, current);
            }
        }

        [Delete(typeof (AircraftLeaseOrderDTO))]
        public void DeleteAircraftLeaseOrder(AircraftLeaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var deleteAircraftLeaseOrder = _orderRepository.Get(dto.Id);
            if (deleteAircraftLeaseOrder != null)
            {
                _orderRepository.Remove(deleteAircraftLeaseOrder);
            }
        }

        private AircraftLeaseOrder MaterializeAircraftLeaseOrderFromDto(AircraftLeaseOrderDTO dto)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region AircraftPurchaseOrderDTO

        [Insert(typeof (AircraftPurchaseOrderDTO))]
        public void InsertAircraftPurchaseOrder(AircraftPurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
        }

        [Update(typeof (AircraftPurchaseOrderDTO))]
        public void UpdateAircraftPurchaseOrder(AircraftPurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var persisted = _orderRepository.Get(dto.Id);
            if (persisted != null)
            {
                var current = MaterializeAircraftPurchaseOrderFromDto(dto);
                _orderRepository.Merge(persisted, current);
            }
        }

        [Delete(typeof (AircraftPurchaseOrderDTO))]
        public void DeleteAircraftPurchaseOrder(AircraftPurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var deleteAircraftPurchaseOrder = _orderRepository.Get(dto.Id);
            if (deleteAircraftPurchaseOrder != null)
            {
                _orderRepository.Remove(deleteAircraftPurchaseOrder);
            }
        }

        private AircraftPurchaseOrder MaterializeAircraftPurchaseOrderFromDto(AircraftPurchaseOrderDTO dto)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region BFEPurchaseOrderDTO

        [Insert(typeof (BFEPurchaseOrderDTO))]
        public void InsertBFEPurchaseOrder(BFEPurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
        }

        [Update(typeof (BFEPurchaseOrderDTO))]
        public void UpdateBFEPurchaseOrder(BFEPurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var persisted = _orderRepository.Get(dto.Id);
            if (persisted != null)
            {
                var current = MaterializeBFEPurchaseOrderFromDto(dto);
                _orderRepository.Merge(persisted, current);
            }
        }

        [Delete(typeof (BFEPurchaseOrderDTO))]
        public void DeleteBFEPurchaseOrder(BFEPurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var deleteBFEPurchaseOrder = _orderRepository.Get(dto.Id);
            if (deleteBFEPurchaseOrder != null)
            {
                _orderRepository.Remove(deleteBFEPurchaseOrder);
            }
        }

        private BFEPurchaseOrder MaterializeBFEPurchaseOrderFromDto(BFEPurchaseOrderDTO dto)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region EngineLeaseOrderDTO

        [Insert(typeof (EngineLeaseOrderDTO))]
        public void InsertEngineLeaseOrder(EngineLeaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
        }

        [Update(typeof (EngineLeaseOrderDTO))]
        public void UpdateEngineLeaseOrder(EngineLeaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var persisted = _orderRepository.Get(dto.Id);
            if (persisted != null)
            {
                var current = MaterializeEngineLeaseOrderFromDto(dto);
                _orderRepository.Merge(persisted, current);
            }
        }

        [Delete(typeof (EngineLeaseOrderDTO))]
        public void DeleteEngineLeaseOrder(EngineLeaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var deleteEngineLeaseOrder = _orderRepository.Get(dto.Id);
            if (deleteEngineLeaseOrder != null)
            {
                _orderRepository.Remove(deleteEngineLeaseOrder);
            }
        }

        private EngineLeaseOrder MaterializeEngineLeaseOrderFromDto(EngineLeaseOrderDTO dto)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region EnginePurchaseOrderDTO

        [Insert(typeof (EnginePurchaseOrderDTO))]
        public void InsertEnginePurchaseOrder(EnginePurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
        }

        [Update(typeof (EnginePurchaseOrderDTO))]
        public void UpdateEnginePurchaseOrder(EnginePurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var persisted = _orderRepository.Get(dto.Id);
            if (persisted != null)
            {
                var current = MaterializeEnginePurchaseOrderFromDto(dto);
                _orderRepository.Merge(persisted, current);
            }
        }

        [Delete(typeof (EnginePurchaseOrderDTO))]
        public void DeleteEnginePurchaseOrder(EnginePurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var deleteEnginePurchaseOrder = _orderRepository.Get(dto.Id);
            if (deleteEnginePurchaseOrder != null)
            {
                _orderRepository.Remove(deleteEnginePurchaseOrder);
            }
        }

        private EnginePurchaseOrder MaterializeEnginePurchaseOrderFromDto(EnginePurchaseOrderDTO dto)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}