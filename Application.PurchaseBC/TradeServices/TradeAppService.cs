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
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PurchaseBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractEngineAgg;
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
        private readonly IContractEngineRepository _contractEngineRepository;
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
            IContractAircraftRepository contractAircraftRepository, IContractEngineRepository contractEngineRepository,
            IRelatedDocRepository relatedDocRepository)
        {
            _tradeQuery = queryTrade;
            _tradeRepository = tradeRepository;
            _orderQuery = orderQuery;
            _orderRepository = orderRepository;
            _supplierRepository = supplierRepository;
            _materialRepository = materialRepository;
            _actionCategoryRepository = actionCategoryRepository;
            _contractAircraftRepository = contractAircraftRepository;
            _contractEngineRepository = contractEngineRepository;
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
            var newTrade = TradeFactory.CreateTrade(dto.Name, dto.Description, dto.StartDate, dto.TradeType);
            newTrade.ChangeCurrentIdentity(dto.Id);
            newTrade.SetStatus((TradeStatus) dto.Status);
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
                current.SetStatus((TradeStatus) dto.Status);
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

        #region 共享方法

        /// <summary>
        ///     插入合同内容
        /// </summary>
        /// <param name="order">订单</param>
        /// <param name="content">合同内容</param>
        private void InsertContractContent(Order order, ContractContentDTO content)
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

        #region AircraftLeaseOrderDTO

        /// <summary>
        ///     插入新订单行
        /// </summary>
        /// <param name="order">订单</param>
        /// <param name="dto">订单DTO</param>
        /// <param name="line">订单行DTO</param>
        /// <param name="importType">引进方式</param>
        /// <param name="supplierId">供应商ID</param>
        private void InsertOrderLine(AircraftLeaseOrder order, AircraftLeaseOrderDTO dto,
            AircraftLeaseOrderLineDTO line, ActionCategory importType, int supplierId)
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
            var orderLine = order.AddNewAircraftLeaseOrderLine(line.UnitPrice, line.Amount, line.Discount,
                line.EstimateDeliveryDate);
            orderLine.SetAircraftMaterial(line.AircraftMaterialId);

            // 创建或引用合同飞机
            var persist = _contractAircraftRepository.Get(line.ContractAircraftId) as LeaseContractAircraft;
            var contractAircraft = ContractAircraftFactory.CreateLeaseContractAircraft(dto.Name, line.RankNumber);
            contractAircraft.ChangeCurrentIdentity(line.ContractAircraftId);
            contractAircraft.SetAircraftType(aircraftTypeId);
            contractAircraft.SetImportCategory(importType);
            contractAircraft.SetCSCNumber(line.CSCNumber);
            contractAircraft.SetSerialNumber(line.SerialNumber);
            contractAircraft.SetSupplier(supplierId);
            if (persist == null)
            {
                orderLine.SetContractAircraft(contractAircraft);
            }
            else
            {
                orderLine.SetContractAircraft(persist);
                _contractAircraftRepository.Merge(persist, contractAircraft);
            }
        }

        /// <summary>
        ///     更新订单行
        /// </summary>
        /// <param name="line">订单行DTO</param>
        /// <param name="orderLine">订单行</param>
        private void UpdateOrderLine(AircraftLeaseOrderLineDTO line, AircraftLeaseOrderLine orderLine)
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
            orderLine.SetAircraftMaterial(line.AircraftMaterialId);

            // 更新合同飞机
            var contractAircraft = _contractAircraftRepository.Get(orderLine.ContractAircraftId);
            contractAircraft.SetAircraftType(aircraftTypeId);
            contractAircraft.SetRankNumber(line.RankNumber);
            contractAircraft.SetCSCNumber(line.CSCNumber);
            contractAircraft.SetSerialNumber(line.SerialNumber);
        }

        [Insert(typeof (AircraftLeaseOrderDTO))]
        public void InsertAircraftLeaseOrder(AircraftLeaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            // 获取版本号
            var id = dto.TradeId;
            var version = _orderRepository.GetFiltered(o => o.TradeId == id).Count() + 1;

            // 创建订单
            var order = OrderFactory.CreateAircraftLeaseOrder(version, dto.OperatorName, dto.OrderDate);
            order.SetTrade(dto.TradeId);
            order.SetCurrency(dto.CurrencyId);
            order.SetLinkman(dto.LinkmanId);
            order.SetSourceGuid(dto.SourceGuid);
            order.SetName(dto.Name);
            order.SetOrderStatus((OrderStatus) dto.Status);
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

            // 处理订单行
            dto.AircraftLeaseOrderLines.ToList()
                .ForEach(line => InsertOrderLine(order, dto, line, importType, dto.SupplierId));

            // 处理分解合同
            dto.ContractContents.ToList().ForEach(c => InsertContractContent(order, c));

            _orderRepository.Add(order);
        }

        [Update(typeof (AircraftLeaseOrderDTO))]
        public void UpdateAircraftLeaseOrder(AircraftLeaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var order = _orderRepository.Get(dto.Id) as AircraftLeaseOrder;
            if (order != null)
            {
                // 更新当前记录
                order.SetCurrency(dto.CurrencyId);
                order.SetLinkman(dto.LinkmanId);
                order.SetName(dto.Name);
                order.SetOperatorName(dto.OperatorName);
                order.SetOrderStatus((OrderStatus) dto.Status);
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
                var dtoOrderLines = dto.AircraftLeaseOrderLines;
                var orderLines = order.OrderLines;
                DataHelper.DetailHandle(dtoOrderLines.ToArray(),
                    orderLines.OfType<AircraftLeaseOrderLine>().ToArray(),
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
                _relatedDocRepository.GetFiltered(r => r.SourceId == deleteAircraftLeaseOrder.SourceGuid)
                    .ToList()
                    .ForEach(r => _relatedDocRepository.Remove(r));
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

            // 创建或引用合同飞机
            var persist = _contractAircraftRepository.Get(line.ContractAircraftId) as PurchaseContractAircraft;
            var contractAircraft = ContractAircraftFactory.CreatePurchaseContractAircraft(dto.Name, line.RankNumber);
            contractAircraft.ChangeCurrentIdentity(line.ContractAircraftId);
            contractAircraft.SetAircraftType(aircraftTypeId);
            contractAircraft.SetImportCategory(importType);
            contractAircraft.SetCSCNumber(line.CSCNumber);
            contractAircraft.SetSerialNumber(line.SerialNumber);
            contractAircraft.SetSupplier(supplierId);
            if (persist == null)
            {
                orderLine.SetContractAircraft(contractAircraft);
            }
            else
            {
                orderLine.SetContractAircraft(persist);
                _contractAircraftRepository.Merge(persist, contractAircraft);
            }
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
            order.SetOrderStatus((OrderStatus) dto.Status);
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
                order.SetOrderStatus((OrderStatus) dto.Status);
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

        /// <summary>
        ///     插入新订单行
        /// </summary>
        /// <param name="order">订单</param>
        /// <param name="dto">订单DTO</param>
        /// <param name="line">订单行DTO</param>
        /// <param name="importType">引进方式</param>
        /// <param name="supplierId">供应商ID</param>
        private void InsertOrderLine(BFEPurchaseOrder order, BFEPurchaseOrderDTO dto,
            BFEPurchaseOrderLineDTO line, ActionCategory importType, int supplierId)
        {
            // 获取飞机物料机型
            var material =
                _materialRepository.GetFiltered(m => m.Id == line.BFEMaterialId)
                    .OfType<BFEMaterial>()
                    .FirstOrDefault();
            if (material == null)
            {
                throw new ArgumentException("未能获取飞机物料！");
            }

            // 添加订单行
            var orderLine = order.AddNewBFEPurchaseOrderLine(line.UnitPrice, line.Amount, line.Discount,
                line.EstimateDeliveryDate);
            orderLine.SetBFEMaterial(line.BFEMaterialId);
        }

        /// <summary>
        ///     更新订单行
        /// </summary>
        /// <param name="line">订单行DTO</param>
        /// <param name="orderLine">订单行</param>
        private void UpdateOrderLine(BFEPurchaseOrderLineDTO line, BFEPurchaseOrderLine orderLine)
        {
            // 获取飞机物料机型
            var material =
                _materialRepository.GetFiltered(m => m.Id == line.BFEMaterialId)
                    .OfType<BFEMaterial>()
                    .FirstOrDefault();
            if (material == null)
            {
                throw new ArgumentException("未能获取飞机物料！");
            }

            // 更新订单行
            orderLine.UpdateOrderLine(line.UnitPrice, line.Amount, line.Discount, line.EstimateDeliveryDate);
            orderLine.SetBFEMaterial(line.BFEMaterialId);
        }

        [Insert(typeof (BFEPurchaseOrderDTO))]
        public void InsertBFEPurchaseOrder(BFEPurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            // 获取版本号
            var id = dto.TradeId;
            var version = _orderRepository.GetFiltered(o => o.TradeId == id).Count() + 1;

            // 创建订单
            var order = OrderFactory.CreateBFEPurchaseOrder(version, dto.OperatorName, dto.OrderDate);
            order.SetTrade(dto.TradeId);
            order.SetCurrency(dto.CurrencyId);
            order.SetLinkman(dto.LinkmanId);
            order.SetSourceGuid(dto.SourceGuid);
            order.SetName(dto.Name);
            order.SetOrderStatus((OrderStatus) dto.Status);
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

            // 处理订单行
            dto.BFEPurchaseOrderLines.ToList()
                .ForEach(line => InsertOrderLine(order, dto, line, importType, dto.SupplierId));

            // 处理分解合同
            dto.ContractContents.ToList().ForEach(c => InsertContractContent(order, c));

            _orderRepository.Add(order);
        }

        [Update(typeof (BFEPurchaseOrderDTO))]
        public void UpdateBFEPurchaseOrder(BFEPurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var order = _orderRepository.Get(dto.Id) as BFEPurchaseOrder;
            if (order != null)
            {
                // 更新当前记录
                order.SetCurrency(dto.CurrencyId);
                order.SetLinkman(dto.LinkmanId);
                order.SetName(dto.Name);
                order.SetOperatorName(dto.OperatorName);
                order.SetOrderStatus((OrderStatus) dto.Status);
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
                var dtoOrderLines = dto.BFEPurchaseOrderLines;
                var orderLines = order.OrderLines;
                DataHelper.DetailHandle(dtoOrderLines.ToArray(),
                    orderLines.OfType<BFEPurchaseOrderLine>().ToArray(),
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
                _relatedDocRepository.GetFiltered(r => r.SourceId == deleteBFEPurchaseOrder.SourceGuid)
                    .ToList()
                    .ForEach(r => _relatedDocRepository.Remove(r));
                _orderRepository.Remove(deleteBFEPurchaseOrder);
            }
        }

        #endregion

        #region EngineLeaseOrderDTO

        /// <summary>
        ///     插入新订单行
        /// </summary>
        /// <param name="order">订单</param>
        /// <param name="dto">订单DTO</param>
        /// <param name="line">订单行DTO</param>
        /// <param name="importType">引进方式</param>
        /// <param name="supplierId">供应商ID</param>
        private void InsertOrderLine(EngineLeaseOrder order, EngineLeaseOrderDTO dto,
            EngineLeaseOrderLineDTO line, ActionCategory importType, int supplierId)
        {
            // 获取飞机物料机型
            var material =
                _materialRepository.GetFiltered(m => m.Id == line.EngineMaterialId)
                    .OfType<EngineMaterial>()
                    .FirstOrDefault();
            if (material == null)
            {
                throw new ArgumentException("未能获取飞机物料！");
            }

            // 添加订单行
            var orderLine = order.AddNewEngineLeaseOrderLine(line.UnitPrice, line.Amount, line.Discount,
                line.EstimateDeliveryDate);
            orderLine.SetEngineMaterial(line.EngineMaterialId);

            // 创建或引用合同发动机
            var persist = _contractEngineRepository.Get(line.ContractEngineId) as LeaseContractEngine;
            var contractEngine = ContractEngineFactory.CreateLeaseContractEngine(dto.Name, line.RankNumber);
            contractEngine.ChangeCurrentIdentity(line.ContractEngineId);
            contractEngine.SetImportCategory(importType);
            contractEngine.SetSerialNumber(line.SerialNumber);
            contractEngine.SetSupplier(supplierId);
            if (persist == null)
            {
                orderLine.SetContractEngine(contractEngine);
            }
            else
            {
                orderLine.SetContractEngine(persist);
                _contractEngineRepository.Merge(persist, contractEngine);
            }
        }

        /// <summary>
        ///     更新订单行
        /// </summary>
        /// <param name="line">订单行DTO</param>
        /// <param name="orderLine">订单行</param>
        private void UpdateOrderLine(EngineLeaseOrderLineDTO line, EngineLeaseOrderLine orderLine)
        {
            // 获取飞机物料机型
            var material =
                _materialRepository.GetFiltered(m => m.Id == line.EngineMaterialId)
                    .OfType<EngineMaterial>()
                    .FirstOrDefault();
            if (material == null)
            {
                throw new ArgumentException("未能获取飞机物料！");
            }

            // 更新订单行
            orderLine.UpdateOrderLine(line.UnitPrice, line.Amount, line.Discount, line.EstimateDeliveryDate);
            orderLine.SetEngineMaterial(line.EngineMaterialId);

            // 更新合同发动机
            var contractEngine = _contractEngineRepository.Get(orderLine.ContractEngineId);
            contractEngine.SetRankNumber(line.RankNumber);
            contractEngine.SetSerialNumber(line.SerialNumber);
        }

        [Insert(typeof (EngineLeaseOrderDTO))]
        public void InsertEngineLeaseOrder(EngineLeaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            // 获取版本号
            var id = dto.TradeId;
            var version = _orderRepository.GetFiltered(o => o.TradeId == id).Count() + 1;

            // 创建订单
            var order = OrderFactory.CreateEngineLeaseOrder(version, dto.OperatorName, dto.OrderDate);
            order.SetTrade(dto.TradeId);
            order.SetCurrency(dto.CurrencyId);
            order.SetLinkman(dto.LinkmanId);
            order.SetSourceGuid(dto.SourceGuid);
            order.SetName(dto.Name);
            order.SetOrderStatus((OrderStatus) dto.Status);
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

            // 处理订单行
            dto.EngineLeaseOrderLines.ToList()
                .ForEach(line => InsertOrderLine(order, dto, line, importType, dto.SupplierId));

            // 处理分解合同
            dto.ContractContents.ToList().ForEach(c => InsertContractContent(order, c));

            _orderRepository.Add(order);
        }

        [Update(typeof (EngineLeaseOrderDTO))]
        public void UpdateEngineLeaseOrder(EngineLeaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var order = _orderRepository.Get(dto.Id) as EngineLeaseOrder;
            if (order != null)
            {
                // 更新当前记录
                order.SetCurrency(dto.CurrencyId);
                order.SetLinkman(dto.LinkmanId);
                order.SetName(dto.Name);
                order.SetOperatorName(dto.OperatorName);
                order.SetOrderStatus((OrderStatus) dto.Status);
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
                var dtoOrderLines = dto.EngineLeaseOrderLines;
                var orderLines = order.OrderLines;
                DataHelper.DetailHandle(dtoOrderLines.ToArray(),
                    orderLines.OfType<EngineLeaseOrderLine>().ToArray(),
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
                _relatedDocRepository.GetFiltered(r => r.SourceId == deleteEngineLeaseOrder.SourceGuid)
                    .ToList()
                    .ForEach(r => _relatedDocRepository.Remove(r));
                _orderRepository.Remove(deleteEngineLeaseOrder);
            }
        }

        #endregion

        #region EnginePurchaseOrderDTO

        /// <summary>
        ///     插入新订单行
        /// </summary>
        /// <param name="order">订单</param>
        /// <param name="dto">订单DTO</param>
        /// <param name="line">订单行DTO</param>
        /// <param name="importType">引进方式</param>
        /// <param name="supplierId">供应商ID</param>
        private void InsertOrderLine(EnginePurchaseOrder order, EnginePurchaseOrderDTO dto,
            EnginePurchaseOrderLineDTO line, ActionCategory importType, int supplierId)
        {
            // 获取飞机物料机型
            var material =
                _materialRepository.GetFiltered(m => m.Id == line.EngineMaterialId)
                    .OfType<EngineMaterial>()
                    .FirstOrDefault();
            if (material == null)
            {
                throw new ArgumentException("未能获取飞机物料！");
            }

            // 添加订单行
            var orderLine = order.AddNewEnginePurchaseOrderLine(line.UnitPrice, line.Amount, line.Discount,
                line.EstimateDeliveryDate);
            orderLine.SetEngineMaterial(line.EngineMaterialId);

            // 创建或引用合同发动机
            var persist = _contractEngineRepository.Get(line.ContractEngineId) as PurchaseContractEngine;
            var contractEngine = ContractEngineFactory.CreatePurchaseContractEngine(dto.Name, line.RankNumber);
            contractEngine.ChangeCurrentIdentity(line.ContractEngineId);
            contractEngine.SetImportCategory(importType);
            contractEngine.SetSerialNumber(line.SerialNumber);
            contractEngine.SetSupplier(supplierId);
            if (persist == null)
            {
                orderLine.SetContractEngine(contractEngine);
            }
            else
            {
                orderLine.SetContractEngine(persist);
                _contractEngineRepository.Merge(persist, contractEngine);
            }
        }

        /// <summary>
        ///     更新订单行
        /// </summary>
        /// <param name="line">订单行DTO</param>
        /// <param name="orderLine">订单行</param>
        private void UpdateOrderLine(EnginePurchaseOrderLineDTO line, EnginePurchaseOrderLine orderLine)
        {
            // 获取飞机物料机型
            var material =
                _materialRepository.GetFiltered(m => m.Id == line.EngineMaterialId)
                    .OfType<EngineMaterial>()
                    .FirstOrDefault();
            if (material == null)
            {
                throw new ArgumentException("未能获取飞机物料！");
            }

            // 更新订单行
            orderLine.UpdateOrderLine(line.UnitPrice, line.Amount, line.Discount, line.EstimateDeliveryDate);
            orderLine.SetEngineMaterial(line.EngineMaterialId);

            // 更新合同发动机
            var contractEngine = _contractEngineRepository.Get(orderLine.ContractEngineId);
            contractEngine.SetRankNumber(line.RankNumber);
            contractEngine.SetSerialNumber(line.SerialNumber);
        }

        [Insert(typeof (EnginePurchaseOrderDTO))]
        public void InsertEnginePurchaseOrder(EnginePurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            // 获取版本号
            var id = dto.TradeId;
            var version = _orderRepository.GetFiltered(o => o.TradeId == id).Count() + 1;

            // 创建订单
            var order = OrderFactory.CreateEnginePurchaseOrder(version, dto.OperatorName, dto.OrderDate);
            order.SetTrade(dto.TradeId);
            order.SetCurrency(dto.CurrencyId);
            order.SetLinkman(dto.LinkmanId);
            order.SetSourceGuid(dto.SourceGuid);
            order.SetName(dto.Name);
            order.SetOrderStatus((OrderStatus) dto.Status);
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

            // 处理订单行
            dto.EnginePurchaseOrderLines.ToList()
                .ForEach(line => InsertOrderLine(order, dto, line, importType, dto.SupplierId));

            // 处理分解合同
            dto.ContractContents.ToList().ForEach(c => InsertContractContent(order, c));

            _orderRepository.Add(order);
        }

        [Update(typeof (EnginePurchaseOrderDTO))]
        public void UpdateEnginePurchaseOrder(EnginePurchaseOrderDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }

            var order = _orderRepository.Get(dto.Id) as EnginePurchaseOrder;
            if (order != null)
            {
                // 更新当前记录
                order.SetCurrency(dto.CurrencyId);
                order.SetLinkman(dto.LinkmanId);
                order.SetName(dto.Name);
                order.SetOperatorName(dto.OperatorName);
                order.SetOrderStatus((OrderStatus) dto.Status);
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
                var dtoOrderLines = dto.EnginePurchaseOrderLines;
                var orderLines = order.OrderLines;
                DataHelper.DetailHandle(dtoOrderLines.ToArray(),
                    orderLines.OfType<EnginePurchaseOrderLine>().ToArray(),
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
                _relatedDocRepository.GetFiltered(r => r.SourceId == deleteEnginePurchaseOrder.SourceGuid)
                    .ToList()
                    .ForEach(r => _relatedDocRepository.Remove(r));
                _orderRepository.Remove(deleteEnginePurchaseOrder);
            }
        }

        #endregion
    }
}