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
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PaymentBC.Aggregates.TradeAgg;using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PaymentBC.Query.OrderQueries
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
        public IQueryable<OrderDTO> OrderDTOQuery(QueryBuilder<Order> query)
        {
            var trades = _unitOfWork.CreateSet<Trade>();
            var result = query.ApplyTo(_orderRepository.GetAll().Where(p => p.IsValid == true))
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    Version = o.Version,
                    CurrencyId = o.Currency.Id,
                    OperatorName = o.OperatorName,
                    OrderDate = o.OrderDate,
                    Status = (int)o.Status,
                    Note = o.Note,
                    SupplierId = trades.FirstOrDefault(p => p.Id == o.TradeId).SupplierId,
                    SupplierName = trades.FirstOrDefault(p => p.Id == o.TradeId).Supplier.Name,
                    OrderLines = 
                        o.OrderLines.OfType<AircraftLeaseOrderLine>().Select(l => new OrderLineDTO
                        {
                            Id = l.Id,
                            UnitPrice = l.UnitPrice,
                            Amount = l.Amount,
                            Discount = l.Discount,
                            EstimateDeliveryDate = l.EstimateDeliveryDate,
                            Note = l.Note,
                            MaterialName = l.LeaseContractAircraft.SerialNumber,
                            OrderId = l.OrderId,
                            TotalLine = (l.UnitPrice * l.Amount) * (1 - (l.Discount / 100M)),
                        })
                        .Union(o.OrderLines.OfType<AircraftPurchaseOrderLine>().Select(l => new OrderLineDTO
                        {
                            Id = l.Id,
                            UnitPrice = l.UnitPrice,
                            Amount = l.Amount,
                            Discount = l.Discount,
                            EstimateDeliveryDate = l.EstimateDeliveryDate,
                            Note = l.Note,
                            MaterialName = l.PurchaseContractAircraft.SerialNumber,
                            OrderId = l.OrderId,
                            TotalLine = (l.UnitPrice * l.Amount) * (1 - (l.Discount / 100M)),
                        }))
                        .Union(o.OrderLines.OfType<EnginePurchaseOrderLine>().Select(l => new OrderLineDTO
                        {
                            Id = l.Id,
                            UnitPrice = l.UnitPrice,
                            Amount = l.Amount,
                            Discount = l.Discount,
                            EstimateDeliveryDate = l.EstimateDeliveryDate,
                            Note = l.Note,
                            MaterialName = l.PurchaseContractEngine.SerialNumber,
                            OrderId = l.OrderId,
                            TotalLine = (l.UnitPrice * l.Amount) * (1 - (l.Discount / 100M)),
                        }))
                        .Union(o.OrderLines.OfType<EngineLeaseOrderLine>().Select(l => new OrderLineDTO
                        {
                            Id = l.Id,
                            UnitPrice = l.UnitPrice,
                            Amount = l.Amount,
                            Discount = l.Discount,
                            EstimateDeliveryDate = l.EstimateDeliveryDate,
                            Note = l.Note,
                            MaterialName = l.LeaseContractEngine.SerialNumber,
                            OrderId = l.OrderId,
                            TotalLine = (l.UnitPrice * l.Amount) * (1 - (l.Discount / 100M)),
                        }))
                        .Union(o.OrderLines.OfType<BFEPurchaseOrderLine>().Select(l => new OrderLineDTO
                        {
                            Id = l.Id,
                            UnitPrice = l.UnitPrice,
                            Amount = l.Amount,
                            Discount = l.Discount,
                            EstimateDeliveryDate = l.EstimateDeliveryDate,
                            Note = l.Note,
                            MaterialName = "",
                            OrderId = l.OrderId,
                            TotalLine = (l.UnitPrice * l.Amount) * (1 - (l.Discount / 100M)),
                        })
                        ).ToList(),

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
        public IQueryable<PurchaseOrderDTO> PurchaseOrderQuery(QueryBuilder<Order> query)
        {
            var trades = _unitOfWork.CreateSet<Trade>();
            var result = query.ApplyTo(_orderRepository.GetAll().Where(p => p.IsValid == true))
                .Select(o => new PurchaseOrderDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    Version = o.Version,
                    CurrencyId = o.Currency.Id,
                    OperatorName = o.OperatorName,
                    OrderDate = o.OrderDate,
                    Status = (int)o.Status,
                    Note = o.Note,
                    SupplierId = trades.FirstOrDefault(p => p.Id == o.TradeId).SupplierId,
                    SupplierName = trades.FirstOrDefault(p => p.Id == o.TradeId).Supplier.Name,
                    OrderLines =
                        o.OrderLines.OfType<AircraftPurchaseOrderLine>().Select(l => new OrderLineDTO
                        {
                            Id = l.Id,
                            UnitPrice = l.UnitPrice,
                            Amount = l.Amount,
                            Discount = l.Discount,
                            EstimateDeliveryDate = l.EstimateDeliveryDate,
                            Note = l.Note,
                            MaterialName = l.PurchaseContractAircraft.SerialNumber,
                            OrderId = l.OrderId,
                            TotalLine = (l.UnitPrice * l.Amount) * (1 - (l.Discount / 100M)),
                        })
                        .Union(o.OrderLines.OfType<EnginePurchaseOrderLine>().Select(l => new OrderLineDTO
                        {
                            Id = l.Id,
                            UnitPrice = l.UnitPrice,
                            Amount = l.Amount,
                            Discount = l.Discount,
                            EstimateDeliveryDate = l.EstimateDeliveryDate,
                            Note = l.Note,
                            MaterialName = l.PurchaseContractEngine.SerialNumber,
                            OrderId = l.OrderId,
                            TotalLine = (l.UnitPrice * l.Amount) * (1 - (l.Discount / 100M)),
                        }))
                        .Union(o.OrderLines.OfType<BFEPurchaseOrderLine>().Select(l => new OrderLineDTO
                        {
                            Id = l.Id,
                            UnitPrice = l.UnitPrice,
                            Amount = l.Amount,
                            Discount = l.Discount,
                            EstimateDeliveryDate = l.EstimateDeliveryDate,
                            Note = l.Note,
                            MaterialName = "",
                            OrderId = l.OrderId,
                            TotalLine = (l.UnitPrice * l.Amount) * (1 - (l.Discount / 100M)),
                        })).ToList(),
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
        public IQueryable<AircraftLeaseOrderDTO> AircraftLeaseOrderQuery(QueryBuilder<Order> query)
        {
            var trades = _unitOfWork.CreateSet<Trade>();
            var result = query.ApplyTo(_orderRepository.GetAll().OfType<AircraftLeaseOrder>().Where(p => p.IsValid == true))
                .Select(o => new AircraftLeaseOrderDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    Version = o.Version,
                    CurrencyId = o.Currency.Id,
                    OperatorName = o.OperatorName,
                    OrderDate = o.OrderDate,
                    Status = (int)o.Status,
                    Note = o.Note,
                    SupplierId = trades.FirstOrDefault(p => p.Id == o.TradeId).SupplierId,
                    SupplierName = trades.FirstOrDefault(p => p.Id == o.TradeId).Supplier.Name,
                    AircraftLeaseOrderLines =
                        o.OrderLines.OfType<AircraftLeaseOrderLine>().Select(l => new AircraftLeaseOrderLineDTO
                        {
                            Id = l.Id,
                            UnitPrice = l.UnitPrice,
                            Amount = l.Amount,
                            Discount = l.Discount,
                            EstimateDeliveryDate = l.EstimateDeliveryDate,
                            Note = l.Note,
                            ContractAircraftId = l.ContractAircraftId,
                            OrderId = l.OrderId,
                            TotalLine = (l.UnitPrice * l.Amount) * (1 - (l.Discount / 100M)),
                        }).ToList(),
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
            var trades = _unitOfWork.CreateSet<Trade>();
            var result = query.ApplyTo(_orderRepository.GetAll().OfType<AircraftPurchaseOrder>().Where(p => p.IsValid == true))
                .Select(o => new AircraftPurchaseOrderDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    Version = o.Version,
                    CurrencyId = o.Currency.Id,
                    OperatorName = o.OperatorName,
                    OrderDate = o.OrderDate,
                    Status = (int)o.Status,
                    Note = o.Note,
                    SupplierId = trades.FirstOrDefault(p => p.Id == o.TradeId).SupplierId,
                    SupplierName = trades.FirstOrDefault(p => p.Id == o.TradeId).Supplier.Name,
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
                            Note = l.Note,
                            ContractAircraftId = l.ContractAircraftId,
                            OrderId = l.OrderId,
                            TotalLine = (l.UnitPrice * l.Amount) * (1 - (l.Discount / 100M)),
                        }).ToList(),
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
            var trades = _unitOfWork.CreateSet<Trade>();
            var result = query.ApplyTo(_orderRepository.GetAll().OfType<EngineLeaseOrder>().Where(p => p.IsValid == true))
                .Select(o => new EngineLeaseOrderDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    Version = o.Version,
                    CurrencyId = o.Currency.Id,
                    OperatorName = o.OperatorName,
                    LinkmanId = o.Linkman.Id,
                    OrderDate = o.OrderDate,
                    Status = (int)o.Status,
                    Note = o.Note,
                    SupplierId = trades.FirstOrDefault(p => p.Id == o.TradeId).SupplierId,
                    SupplierName = trades.FirstOrDefault(p => p.Id == o.TradeId).Supplier.Name,
                    EngineLeaseOrderLines =
                        o.OrderLines.OfType<EngineLeaseOrderLine>().Select(l => new EngineLeaseOrderLineDTO
                        {
                            Id = l.Id,
                            UnitPrice = l.UnitPrice,
                            Amount = l.Amount,
                            Discount = l.Discount,
                            EstimateDeliveryDate = l.EstimateDeliveryDate,
                            Note = l.Note,
                            ContractEngineId = l.ContractEngineId,
                            OrderId = l.OrderId,
                            TotalLine = (l.UnitPrice * l.Amount) * (1 - (l.Discount / 100M)),
                        }).ToList(),
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
            var trades = _unitOfWork.CreateSet<Trade>();
            var result = query.ApplyTo(_orderRepository.GetAll().OfType<EnginePurchaseOrder>().Where(p => p.IsValid == true))
                .Select(o => new EnginePurchaseOrderDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    Version = o.Version,
                    CurrencyId = o.Currency.Id,
                    OperatorName = o.OperatorName,
                    OrderDate = o.OrderDate,
                    Status = (int)o.Status,
                    Note = o.Note,
                    SupplierId = trades.FirstOrDefault(p => p.Id == o.TradeId).SupplierId,
                    SupplierName = trades.FirstOrDefault(p => p.Id == o.TradeId).Supplier.Name,
                    EnginePurchaseOrderLines =
                        o.OrderLines.OfType<EnginePurchaseOrderLine>().Select(l => new EnginePurchaseOrderLineDTO
                        {
                            Id = l.Id,
                            UnitPrice = l.UnitPrice,
                            Amount = l.Amount,
                            Discount = l.Discount,
                            EstimateDeliveryDate = l.EstimateDeliveryDate,
                            Note = l.Note,
                            ContractEngineId = l.ContractEngineId,
                            OrderId = l.OrderId,
                            TotalLine = (l.UnitPrice * l.Amount) * (1 - (l.Discount / 100M)),
                        }).ToList(),
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
            var trades = _unitOfWork.CreateSet<Trade>();
            var result = query.ApplyTo(_orderRepository.GetAll().OfType<BFEPurchaseOrder>().Where(p => p.IsValid == true))
                .Select(o => new BFEPurchaseOrderDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    Version = o.Version,
                    CurrencyId = o.Currency.Id,
                    OperatorName = o.OperatorName,
                    OrderDate = o.OrderDate,
                    Status = (int)o.Status,
                    Note = o.Note,
                    SupplierId = trades.FirstOrDefault(p => p.Id == o.TradeId).SupplierId,
                    SupplierName = trades.FirstOrDefault(p => p.Id == o.TradeId).Supplier.Name,
                    BFEPurchaseOrderLines =
                        o.OrderLines.OfType<BFEPurchaseOrderLine>().Select(l => new BFEPurchaseOrderLineDTO
                        {
                            Id = l.Id,
                            UnitPrice = l.UnitPrice,
                            Amount = l.Amount,
                            Discount = l.Discount,
                            EstimateDeliveryDate = l.EstimateDeliveryDate,
                            Note = l.Note,
                            BFEMaterialId = l.BFEMaterialId,
                            OrderId = l.OrderId,
                            TotalLine = (l.UnitPrice * l.Amount) * (1 - (l.Discount / 100M)),
                        }).ToList(),
                });
            return result;
        }
        /// <summary>
        /// 查询标准订单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<StandardOrderDTO> StandardOrderQuery(QueryBuilder<Order> query)
        {
            var dbTrade = _unitOfWork.CreateSet<Trade>();
            var result = query.ApplyTo(_orderRepository.GetAll().OfType<BFEPurchaseOrder>().Where(p => p.IsValid == true))
               .Select(o => new StandardOrderDTO
               {
                   StandardOrderId = o.Id,
                   Name = o.Name,
                   ContractNumber = o.ContractNumber,
                   CurrencyId = o.Currency.Id,
                   SupplierId = dbTrade.FirstOrDefault(c=>c.SupplierId==o.TradeId).SupplierId,
                   SupplierName = dbTrade.FirstOrDefault(c => c.SupplierId == o.TradeId).Supplier.Name,
                   Note = o.Note,
               
               });
            return result;
        }

        #endregion
    }
}