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
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Domain.PurchaseBC.Enums;

#endregion

namespace UniCloud.Application.PurchaseBC.TradeServices
{
    public class TradeAppService : ITradeAppService
    {
        private readonly IOrderQuery _orderQuery;
        private readonly IOrderRepository _orderRepository;
        private readonly IRelatedDocRepository _relatedDocRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ITradeQuery _tradeQuery;
        private readonly ITradeRepository _tradeRepository;

        public TradeAppService(ITradeQuery queryTrade, ITradeRepository tradeRepository,
            IOrderQuery orderQuery, IOrderRepository orderRepository, ISupplierRepository supplierRepository,
            IRelatedDocRepository relatedDocRepository)
        {
            _tradeQuery = queryTrade;
            _tradeRepository = tradeRepository;
            _orderQuery = orderQuery;
            _orderRepository = orderRepository;
            _supplierRepository = supplierRepository;
            _relatedDocRepository = relatedDocRepository;
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
            // 修改状态
            newTrade.SetStatus(TradeStatus.进行中);

            _tradeRepository.Add(newTrade);
        }

        [Update(typeof (TradeDTO))]
        public void UpdateTrade(TradeDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var current = _tradeRepository.Get(dto.Id);
            if (current != null)
            {
                // 更新当前记录
                var supplier = _supplierRepository.Get(dto.SupplierId);
                current.UpdateTrade(dto.Name, dto.Description, dto.StartDate);
                current.SetSupplier(supplier);

                _tradeRepository.Modify(current);
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

            var current = _orderRepository.Get(dto.Id);
            if (current != null)
            {
                // 更新当前记录


                _orderRepository.Modify(current);
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

        #endregion

        #region AircraftPurchaseOrderDTO

        [Insert(typeof (AircraftPurchaseOrderDTO))]
        public void InsertAircraftPurchaseOrder(AircraftPurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            // 获取版本号
            var version = _orderRepository.GetFiltered(o => o.TradeId == dto.TradeId).Count() + 1;

            // 创建订单
            var order = OrderFactory.CreateAircraftPurchaseOrder(version, dto.OperatorName, dto.OrderDate);
            order.SetTrade(dto.TradeId);
            order.SetCurrency(dto.CurrencyId);
            order.SetLinkman(dto.LinkmanId);
            order.SetSourceGuid(dto.SourceGuid);
            order.SetName(dto.Name);
            if (!string.IsNullOrWhiteSpace(dto.LogWriter))
            {
                order.SetNote(dto.LogWriter);
            }
            if (dto.ContractDocGuid != Guid.Empty)
            {
                order.SetContractDoc(dto.ContractDocGuid, dto.ContractName);
            }

            // TODO : 获取机型ID和引进方式ID，需要移除
            var aircraftTypeId = Guid.Parse("EF5DD798-C16D-47CD-A588-ABD257A6B6B6");
            var imortTypeId = Guid.Parse("8C58622E-01E3-4F61-B34D-619D3FB432AF");

            // 处理订单行
            if (dto.AircraftPurchaseOrderLines != null)
            {
                dto.AircraftPurchaseOrderLines.ToList().ForEach(line =>
                {
                    var orderLine = order.AddNewAircraftPurchaseOrderLine(line.UnitPrice, line.Amount, line.Discount,
                        line.EstimateDeliveryDate);
                    orderLine.SetCost(line.AirframePrice, line.RefitCost, line.EnginePrice);
                    // 创建合同飞机
                    var contractAircraft = ContractAircraftFactory.CreatePurchaseContractAircraft(dto.Name, "0001");
                    contractAircraft.GenerateNewIdentity();
                    contractAircraft.SetAircraftType(aircraftTypeId);
                    contractAircraft.SetImportCategory(imortTypeId);
                    orderLine.SetContractAircraft(contractAircraft);
                });
            }

            // 处理关联文档
            if (dto.RelatedDocs != null)
            {
                dto.RelatedDocs.ToList().ForEach(doc =>
                {
                    var relatedDoc = RelatedDocFactory.CreateRelatedDoc(doc.SourceId, doc.DocumentId, doc.DocumentName);
                    _relatedDocRepository.Add(relatedDoc);
                });
            }
            _orderRepository.Add(order);
        }

        [Update(typeof (AircraftPurchaseOrderDTO))]
        public void UpdateAircraftPurchaseOrder(AircraftPurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var order = _orderRepository.Get(dto.Id);
            if (order != null)
            {
                // 更新当前记录
                order.SetCurrency(dto.CurrencyId);
                order.SetLinkman(dto.LinkmanId);
                order.SetName(dto.Name);
                order.SetOperatorName(dto.OperatorName);
                if (!string.IsNullOrWhiteSpace(dto.LogWriter))
                {
                    order.SetNote(dto.LogWriter);
                }
                if (dto.ContractDocGuid != Guid.Empty)
                {
                    order.SetContractDoc(dto.ContractDocGuid, dto.ContractName);
                }

                _orderRepository.Modify(order);
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

            var current = _orderRepository.Get(dto.Id);
            if (current != null)
            {
                // 更新当前记录


                _orderRepository.Modify(current);
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

            var current = _orderRepository.Get(dto.Id);
            if (current != null)
            {
                // 更新当前记录


                _orderRepository.Modify(current);
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

            var current = _orderRepository.Get(dto.Id);
            if (current != null)
            {
                // 更新当前记录


                _orderRepository.Modify(current);
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

        #endregion

     
    }
}