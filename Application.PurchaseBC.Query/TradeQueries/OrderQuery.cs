﻿#region 版本信息

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
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.TradeQueries
{
    public class OrderQuery : IOrderQuery
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IQueryableUnitOfWork _unitOfWork;

        public OrderQuery(IQueryableUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }

        #region IOrderQuery 成员

        /// <summary>
        ///     <see cref="IOrderQuery" />
        /// </summary>
        /// <param name="query">
        ///     <see cref="IOrderQuery" />
        /// </param>
        /// <returns>
        ///     <see cref="IOrderQuery" />
        /// </returns>
        public IQueryable<AircraftLeaseOrderDTO> AircraftLeaseOrderQuery(QueryBuilder<Order> query)
        {
            var relatedDocs = _unitOfWork.CreateSet<RelatedDoc>();
            var result = query.ApplyTo(_orderRepository.GetAll().OfType<AircraftLeaseOrder>())
                .Select(o => new AircraftLeaseOrderDTO
                {
                    Id = o.Id,
                    TradeId = o.TradeId,
                    Name = o.Name,
                    Version = o.Version,
                    CurrencyId = o.Currency.Id,
                    OperatorName = o.OperatorName,
                    LinkmanId = o.Linkman.Id,
                    OrderDate = o.OrderDate,
                    Status = (int) o.Status,
                    ContractName = o.ContractName,
                    ContractDocGuid = o.ContractDocGuid,
                    SourceGuid = o.SourceGuid,
                    Note = o.Note,
                    AircraftLeaseOrderLines = o.OrderLines.Select(l => new AircraftLeaseOrderLineDTO
                    {
                        Id = l.Id,
                        UnitPrice = l.UnitPrice,
                        Amount = l.Amount,
                        Discount = l.Discount,
                        EstimateDeliveryDate = l.EstimateDeliveryDate,
                        Note = l.Note
                    }).ToList(),
                    RelatedDocs = relatedDocs.Where(r => r.SourceId == o.SourceGuid).Select(r => new RelatedDocDTO
                    {
                        Id = r.Id,
                        SourceId = r.SourceId,
                        DocumentId = r.DocumentId,
                        DocumentName = r.DocumentName
                    }).ToList()
                });
            return result;
        }

        /// <summary>
        ///     <see cref="IOrderQuery" />
        /// </summary>
        /// <param name="query">
        ///     <see cref="IOrderQuery" />
        /// </param>
        /// <returns>
        ///     <see cref="IOrderQuery" />
        /// </returns>
        public IQueryable<AircraftPurchaseOrderDTO> AircraftPurchaseOrderQuery(QueryBuilder<Order> query)
        {
            var relatedDocs = _unitOfWork.CreateSet<RelatedDoc>();
            var result = query.ApplyTo(_orderRepository.GetAll().OfType<AircraftPurchaseOrder>())
                .Select(o => new AircraftPurchaseOrderDTO
                {
                    Id = o.Id,
                    TradeId = o.TradeId,
                    Name = o.Name,
                    Version = o.Version,
                    CurrencyId = o.Currency.Id,
                    OperatorName = o.OperatorName,
                    LinkmanId = o.Linkman.Id,
                    OrderDate = o.OrderDate,
                    Status = (int) o.Status,
                    ContractName = o.ContractName,
                    ContractDocGuid = o.ContractDocGuid,
                    SourceGuid = o.SourceGuid,
                    Note = o.Note,
                    AircraftPurchaseOrderLines =
                        o.OrderLines.OfType<AircraftPurchaseOrderLine>().Select(l => new AircraftPurchaseOrderLineDTO
                        {
                            Id = l.Id,
                            UnitPrice = l.UnitPrice,
                            Amount = l.Amount,
                            Discount = l.Discount,
                            AirframePrice = l.AirframePrice,
                            RefitCost = l.RefitCost,
                            EnginePrice = l.EnginePrice,
                            EstimateDeliveryDate = l.EstimateDeliveryDate,
                            Note = l.Note
                        }).ToList(),
                    RelatedDocs = relatedDocs.Where(r => r.SourceId == o.SourceGuid).Select(r => new RelatedDocDTO
                    {
                        Id = r.Id,
                        SourceId = r.SourceId,
                        DocumentId = r.DocumentId,
                        DocumentName = r.DocumentName
                    }).ToList()
                });
            return result;
        }

        /// <summary>
        ///     <see cref="IOrderQuery" />
        /// </summary>
        /// <param name="query">
        ///     <see cref="IOrderQuery" />
        /// </param>
        /// <returns>
        ///     <see cref="IOrderQuery" />
        /// </returns>
        public IQueryable<EngineLeaseOrderDTO> EngineLeaseOrderQuery(QueryBuilder<Order> query)
        {
            var relatedDocs = _unitOfWork.CreateSet<RelatedDoc>();
            var result = query.ApplyTo(_orderRepository.GetAll().OfType<EngineLeaseOrder>())
                .Select(o => new EngineLeaseOrderDTO
                {
                    Id = o.Id,
                    TradeId = o.TradeId,
                    Name = o.Name,
                    Version = o.Version,
                    CurrencyId = o.Currency.Id,
                    OperatorName = o.OperatorName,
                    LinkmanId = o.Linkman.Id,
                    OrderDate = o.OrderDate,
                    Status = (int) o.Status,
                    ContractName = o.ContractName,
                    ContractDocGuid = o.ContractDocGuid,
                    SourceGuid = o.SourceGuid,
                    Note = o.Note,
                    EngineLeaseOrderLines = o.OrderLines.Select(l => new EngineLeaseOrderLineDTO
                    {
                        Id = l.Id,
                        UnitPrice = l.UnitPrice,
                        Amount = l.Amount,
                        Discount = l.Discount,
                        EstimateDeliveryDate = l.EstimateDeliveryDate,
                        Note = l.Note
                    }).ToList(),
                    RelatedDocs = relatedDocs.Where(r => r.SourceId == o.SourceGuid).Select(r => new RelatedDocDTO
                    {
                        Id = r.Id,
                        SourceId = r.SourceId,
                        DocumentId = r.DocumentId,
                        DocumentName = r.DocumentName
                    }).ToList()
                });
            return result;
        }

        /// <summary>
        ///     <see cref="IOrderQuery" />
        /// </summary>
        /// <param name="query">
        ///     <see cref="IOrderQuery" />
        /// </param>
        /// <returns>
        ///     <see cref="IOrderQuery" />
        /// </returns>
        public IQueryable<EnginePurchaseOrderDTO> EnginePurchaseOrderQuery(QueryBuilder<Order> query)
        {
            var relatedDocs = _unitOfWork.CreateSet<RelatedDoc>();
            var result = query.ApplyTo(_orderRepository.GetAll().OfType<EnginePurchaseOrder>())
                .Select(o => new EnginePurchaseOrderDTO
                {
                    Id = o.Id,
                    TradeId = o.TradeId,
                    Name = o.Name,
                    Version = o.Version,
                    CurrencyId = o.Currency.Id,
                    OperatorName = o.OperatorName,
                    LinkmanId = o.Linkman.Id,
                    OrderDate = o.OrderDate,
                    Status = (int) o.Status,
                    ContractName = o.ContractName,
                    ContractDocGuid = o.ContractDocGuid,
                    SourceGuid = o.SourceGuid,
                    Note = o.Note,
                    EnginePurchaseOrderLines = o.OrderLines.Select(l => new EnginePurchaseOrderLineDTO
                    {
                        Id = l.Id,
                        UnitPrice = l.UnitPrice,
                        Amount = l.Amount,
                        Discount = l.Discount,
                        EstimateDeliveryDate = l.EstimateDeliveryDate,
                        Note = l.Note
                    }).ToList(),
                    RelatedDocs = relatedDocs.Where(r => r.SourceId == o.SourceGuid).Select(r => new RelatedDocDTO
                    {
                        Id = r.Id,
                        SourceId = r.SourceId,
                        DocumentId = r.DocumentId,
                        DocumentName = r.DocumentName
                    }).ToList()
                });
            return result;
        }

        /// <summary>
        ///     <see cref="IOrderQuery" />
        /// </summary>
        /// <param name="query">
        ///     <see cref="IOrderQuery" />
        /// </param>
        /// <returns>
        ///     <see cref="IOrderQuery" />
        /// </returns>
        public IQueryable<BFEPurchaseOrderDTO> BFEPurchaseOrderQuery(QueryBuilder<Order> query)
        {
            var relatedDocs = _unitOfWork.CreateSet<RelatedDoc>();
            var result = query.ApplyTo(_orderRepository.GetAll().OfType<BFEPurchaseOrder>())
                .Select(o => new BFEPurchaseOrderDTO
                {
                    Id = o.Id,
                    TradeId = o.TradeId,
                    Name = o.Name,
                    Version = o.Version,
                    CurrencyId = o.Currency.Id,
                    OperatorName = o.OperatorName,
                    LinkmanId = o.Linkman.Id,
                    OrderDate = o.OrderDate,
                    Status = (int) o.Status,
                    ContractName = o.ContractName,
                    ContractDocGuid = o.ContractDocGuid,
                    SourceGuid = o.SourceGuid,
                    Note = o.Note,
                    BFEPurchaseOrderLines = o.OrderLines.Select(l => new BFEPurchaseOrderLineDTO
                    {
                        Id = l.Id,
                        UnitPrice = l.UnitPrice,
                        Amount = l.Amount,
                        Discount = l.Discount,
                        EstimateDeliveryDate = l.EstimateDeliveryDate,
                        Note = l.Note
                    }).ToList(),
                    RelatedDocs = relatedDocs.Where(r => r.SourceId == o.SourceGuid).Select(r => new RelatedDocDTO
                    {
                        Id = r.Id,
                        SourceId = r.SourceId,
                        DocumentId = r.DocumentId,
                        DocumentName = r.DocumentName
                    }).ToList()
                });
            return result;
        }

        #endregion
    }
}