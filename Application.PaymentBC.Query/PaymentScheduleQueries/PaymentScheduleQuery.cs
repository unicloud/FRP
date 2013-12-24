#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/16 10:39:38
// 文件名：MaintainInvoiceQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/16 10:39:38
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PaymentBC.Aggregates.PaymentScheduleAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PaymentBC.Query.PaymentScheduleQueries
{
    public class PaymentScheduleQuery : IPaymentScheduleQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public PaymentScheduleQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<PaymentScheduleDTO> PaymentSchedulesQuery(QueryBuilder<PaymentSchedule> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<PaymentSchedule>()).Select(p => new PaymentScheduleDTO
            {
                PaymentScheduleId = p.Id,
                CreateDate = p.CreateDate,
                CurrencyId = p.CurrencyId,
                IsCompleted = p.IsCompleted,
                SupplierId = p.SupplierId,
                SupplierName = p.SupplierName,
                PaymentScheduleLines = p.PaymentScheduleLines.Select(c => new PaymentScheduleLineDTO
                {
                    PaymentScheduleLineId = c.Id,
                    Amount = c.Amount,
                    Start = c.Start,
                    End = c.End,
                    Body = c.Body,
                    Subject = c.Subject,
                    PaymentScheduleId = c.PaymentScheduleId,
                    ScheduleDate = c.ScheduleDate,
                    Status = (int) c.Status,
                    Importance = c.Importance,
                    ProcessStatus = c.Tempo,
                    InvoiceId = c.InvoiceId,
                    IsAllDayEvent = c.IsAllDayEvent
                }).ToList(),
            });
        }

        public IQueryable<AcPaymentScheduleDTO> AcPaymentSchedulesQuery(QueryBuilder<PaymentSchedule> query)
        {
            var dbCurrency = _unitOfWork.CreateSet<Currency>();
            return
                query.ApplyTo(_unitOfWork.CreateSet<PaymentSchedule>())
                    .OfType<AircraftPaymentSchedule>()
                    .Select(p => new AcPaymentScheduleDTO
                    {
                        AcPaymentScheduleId = p.Id,
                        CreateDate = p.CreateDate,
                        CurrencyId = p.CurrencyId,
                        IsCompleted = p.IsCompleted,
                        SupplierId = p.SupplierId,
                        SupplierName = p.SupplierName,
                        ContractAcId = p.ContractAircraftId,
                        CurrencyName = dbCurrency.FirstOrDefault(c => c.Id == p.CurrencyId).CnName,
                        PaymentScheduleLines = p.PaymentScheduleLines.Select(c => new PaymentScheduleLineDTO
                        {
                            PaymentScheduleLineId = c.Id,
                            Amount = c.Amount,
                            Start = c.Start,
                            End = c.End,
                            Body = c.Body,
                            Subject = c.Subject,
                            PaymentScheduleId = c.PaymentScheduleId,
                            ScheduleDate = c.ScheduleDate,
                            Status = (int) c.Status,
                            Importance = c.Importance,
                            ProcessStatus = c.Tempo,
                            InvoiceId = c.InvoiceId,
                            IsAllDayEvent = c.IsAllDayEvent
                        }).ToList(),
                    });
        }

        public IQueryable<EnginePaymentScheduleDTO> EnginePaymentSchedulesQuery(QueryBuilder<PaymentSchedule> query)
        {
            var dbCurrency = _unitOfWork.CreateSet<Currency>();
            return
                query.ApplyTo(_unitOfWork.CreateSet<PaymentSchedule>())
                    .OfType<EnginePaymentSchedule>()
                    .Select(p => new EnginePaymentScheduleDTO
                    {
                        EnginePaymentScheduleId = p.Id,
                        CreateDate = p.CreateDate,
                        CurrencyId = p.CurrencyId,
                        IsCompleted = p.IsCompleted,
                        SupplierId = p.SupplierId,
                        SupplierName = p.SupplierName,
                        ContractEngineId = p.ContractEngineId,
                        CurrencyName = dbCurrency.FirstOrDefault(c => c.Id == p.CurrencyId).CnName,
                        PaymentScheduleLines = p.PaymentScheduleLines.Select(c => new PaymentScheduleLineDTO
                        {
                            PaymentScheduleLineId = c.Id,
                            Amount = c.Amount,
                            Start = c.Start,
                            End = c.End,
                            Body = c.Body,
                            Subject = c.Subject,
                            PaymentScheduleId = c.PaymentScheduleId,
                            ScheduleDate = c.ScheduleDate,
                            Status = (int) c.Status,
                            Importance = c.Importance,
                            ProcessStatus = c.Tempo,
                            InvoiceId = c.InvoiceId,
                            IsAllDayEvent = c.IsAllDayEvent
                        }).ToList(),
                    });
        }

        public IQueryable<StandardPaymentScheduleDTO> StandardPaymentSchedulesQuery(QueryBuilder<PaymentSchedule> query)
        {
            var dbCurrency = _unitOfWork.CreateSet<Currency>();
            return
                query.ApplyTo(_unitOfWork.CreateSet<PaymentSchedule>())
                    .OfType<StandardPaymentSchedule>()
                    .Select(p => new StandardPaymentScheduleDTO
                    {
                        StandardPaymentScheduleId = p.Id,
                        CreateDate = p.CreateDate,
                        CurrencyId = p.CurrencyId,
                        IsCompleted = p.IsCompleted,
                        SupplierId = p.SupplierId,
                        SupplierName = p.SupplierName,
                        OrderId = p.OrderId,
                        CurrencyName = dbCurrency.FirstOrDefault(c => c.Id == p.CurrencyId).CnName,
                        PaymentScheduleLines = p.PaymentScheduleLines.Select(c => new PaymentScheduleLineDTO
                        {
                            PaymentScheduleLineId = c.Id,
                            Amount = c.Amount,
                            Start = c.Start,
                            End = c.End,
                            Body = c.Body,
                            Subject = c.Subject,
                            PaymentScheduleId = c.PaymentScheduleId,
                            ScheduleDate = c.ScheduleDate,
                            Status = (int) c.Status,
                            Importance = c.Importance,
                            ProcessStatus = c.Tempo,
                            InvoiceId = c.InvoiceId,
                            IsAllDayEvent = c.IsAllDayEvent
                        }).ToList(),
                    });
        }
    }
}