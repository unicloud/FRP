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
using UniCloud.Domain.PurchaseBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.TradeServices
{
    public class TradeAppService : ITradeAppService
    {
        private readonly IActionCategoryRepository _actionCategoryRepository;
        private readonly IContractAircraftRepository _contractAircraftRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IOrderQuery _orderQuery;
        private readonly IOrderRepository _orderRepository;
        private readonly IRelatedDocRepository _relatedDocRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ITradeQuery _tradeQuery;
        private readonly ITradeRepository _tradeRepository;

        public TradeAppService(ITradeQuery queryTrade, ITradeRepository tradeRepository, IOrderQuery orderQuery,
            IOrderRepository orderRepository, ISupplierRepository supplierRepository,
            IMaterialRepository materialRepository, IActionCategoryRepository actionCategoryRepository,
            IContractAircraftRepository contractAircraftRepository, IRelatedDocRepository relatedDocRepository)
        {
            _tradeQuery = queryTrade;
            _tradeRepository = tradeRepository;
            _orderQuery = orderQuery;
            _orderRepository = orderRepository;
            _supplierRepository = supplierRepository;
            _materialRepository = materialRepository;
            _actionCategoryRepository = actionCategoryRepository;
            _contractAircraftRepository = contractAircraftRepository;
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
            // TODO:设置交易编号,如果当天的记录被删除过，本方法导致会出现重复交易号
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

        #region 订单行处理

        /// <summary>
        ///     插入新订单行
        /// </summary>
        /// <param name="order">订单</param>
        /// <param name="dto">订单DTO</param>
        /// <param name="line">订单行DTO</param>
        /// <param name="importType">引进方式</param>
        /// <param name="supplierId">供应商ID</param>
        private void InsertOrderLine(AircraftPurchaseOrder order, AircraftPurchaseOrderDTO dto,
            AircraftPurchaseOrderLineDTO line, ActionCategory importType, int supplierId)
        {
            // 获取飞机物料机型
            var material =
                _materialRepository.GetFiltered(m => m.Id == line.AircraftMaterialId)
                    .OfType<AircraftMaterial>()
                    .FirstOrDefault();
            if (material == null)
            {
                throw new ArgumentException("未能获取飞机物料！");
            }
            var aircraftTypeId = material.AircraftTypeId;

            // 添加订单行
            var orderLine = order.AddNewAircraftPurchaseOrderLine(line.UnitPrice, line.Amount, line.Discount,
                line.EstimateDeliveryDate);
            orderLine.SetCost(line.AirframePrice, line.RefitCost, line.EnginePrice);
            orderLine.SetAircraftMaterial(line.AircraftMaterialId);

            // 创建合同飞机
            var contractAircraft = ContractAircraftFactory.CreatePurchaseContractAircraft(dto.Name, line.RankNumber);
            contractAircraft.GenerateNewIdentity();
            contractAircraft.SetAircraftType(aircraftTypeId);
            contractAircraft.SetImportCategory(importType);
            contractAircraft.SetCSCNumber(line.CSCNumber);
            contractAircraft.SetSerialNumber(line.SerialNumber);
            contractAircraft.SetSupplier(supplierId);
            orderLine.SetContractAircraft(contractAircraft);
        }

        /// <summary>
        ///     更新订单行
        /// </summary>
        /// <param name="line">订单行DTO</param>
        /// <param name="orderLine">订单行</param>
        private void UpdateOrderLine(AircraftPurchaseOrderLineDTO line, AircraftPurchaseOrderLine orderLine)
        {
            // 获取飞机物料机型
            var material =
                _materialRepository.GetFiltered(m => m.Id == line.AircraftMaterialId)
                    .OfType<AircraftMaterial>()
                    .FirstOrDefault();
            if (material == null)
            {
                throw new ArgumentException("未能获取飞机物料！");
            }
            var aircraftTypeId = material.AircraftTypeId;

            // 更新订单行
            orderLine.UpdateOrderLine(line.UnitPrice, line.Amount, line.Discount, line.EstimateDeliveryDate);
            orderLine.SetCost(line.AirframePrice, line.RefitCost, line.EnginePrice);
            orderLine.SetAircraftMaterial(line.AircraftMaterialId);

            // 更新合同飞机
            var contractAircraft = _contractAircraftRepository.Get(orderLine.ContractAircraftId);
            contractAircraft.SetAircraftType(aircraftTypeId);
            contractAircraft.SetRankNumber(line.RankNumber);
            contractAircraft.SetCSCNumber(line.CSCNumber);
            contractAircraft.SetSerialNumber(line.SerialNumber);
        }

        /// <summary>
        ///     插入合同内容
        /// </summary>
        /// <param name="order">订单</param>
        /// <param name="content">合同内容</param>
        private void InsertContractContent(AircraftPurchaseOrder order, ContractContentDTO content)
        {
            var contractContent = order.AddNewContractContent(content.ContentDoc);
            contractContent.SetDescription(content.Description);
            contractContent.SetContentTag(content.ContentTags);
        }

        /// <summary>
        ///     更新合同内容
        /// </summary>
        /// <param name="dtoContent">合同内容DTO</param>
        /// <param name="content">合同内容</param>
        private void UpdateContractContent(ContractContentDTO dtoContent, ContractContent content)
        {
            content.UpdateContent(dtoContent.ContentDoc);
            content.SetContentTag(dtoContent.ContentTags);
            content.SetDescription(dtoContent.Description);
        }

        #endregion

        [Insert(typeof (AircraftPurchaseOrderDTO))]
        public void InsertAircraftPurchaseOrder(AircraftPurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            // 获取版本号
            var id = dto.TradeId;
            var version = _orderRepository.GetFiltered(o => o.TradeId == id).Count() + 1;

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

            // 获取引进方式
            var importType =
                _actionCategoryRepository.GetFiltered(a => a.ActionType == "引进" && a.ActionName == "购买")
                    .FirstOrDefault();

            //// 修改交易状态
            //var trade = _tradeRepository.Get(order.TradeId);
            //trade.SetStatus(TradeStatus.进行中);

            // 处理订单行
            dto.AircraftPurchaseOrderLines.ToList()
                .ForEach(line => InsertOrderLine(order, dto, line, importType, dto.SupplierId));

            // 处理分解合同
            dto.ContractContents.ToList().ForEach(c => InsertContractContent(order, c));

            _orderRepository.Add(order);
        }

        [Update(typeof (AircraftPurchaseOrderDTO))]
        public void UpdateAircraftPurchaseOrder(AircraftPurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var order = _orderRepository.Get(dto.Id) as AircraftPurchaseOrder;
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

                // 获取引进方式
                var importType =
                    _actionCategoryRepository.GetFiltered(a => a.ActionType == "引进" && a.ActionName == "购买")
                        .FirstOrDefault();

                var trade = _tradeRepository.Get(order.TradeId);

                // 处理订单行
                var dtoOrderLines = dto.AircraftPurchaseOrderLines;
                var orderLines = order.OrderLines;
                DataHelper.DetailHandle(dtoOrderLines.ToArray(),
                    orderLines.OfType<AircraftPurchaseOrderLine>().ToArray(),
                    c => c.Id, p => p.Id,
                    i => InsertOrderLine(order, dto, i, importType, trade.SupplierId),
                    UpdateOrderLine,
                    d => _orderRepository.RemoveOrderLine(d));

                // 处理分解合同
                var dtoContents = dto.ContractContents;
                var contents = order.ContractContents;
                DataHelper.DetailHandle(dtoContents.ToArray(), contents.ToArray(),
                    c => c.Id, p => p.Id,
                    i => InsertContractContent(order, i),
                    UpdateContractContent,
                    d => _orderRepository.RemoveContractContent(d));
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
                _relatedDocRepository.GetFiltered(r => r.SourceId == deleteAircraftPurchaseOrder.SourceGuid)
                    .ToList()
                    .ForEach(r => _relatedDocRepository.Remove(r));
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