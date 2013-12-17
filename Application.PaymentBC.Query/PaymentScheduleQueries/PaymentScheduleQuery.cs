﻿#region Version Info
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
using UniCloud.Application.PaymentBC.Query.MaintainInvoiceQueries;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg;
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

        public IQueryable<AcPaymentScheduleDTO> AcPaymentSchedulesQuery(QueryBuilder<PaymentSchedule> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<PaymentSchedule>()).OfType<AircraftPaymentSchedule>().Select(p => new AcPaymentScheduleDTO
                {
                    AcPaymentScheduleId = p.Id,
                    CreateDate = p.CreateDate,
                    CurrencyId = p.CurrencyId,
                    IsCompleted = p.IsCompleted,
                    SupplierId = p.SupplierId,
                    SupplierName = p.SupplierName,
                    ContractAcId = p.ContractAircraftId,
                    PaymentScheduleLines = p.PaymentScheduleLines.Select(c => new PaymentScheduleLineDTO
                        {
                            PaymentScheduleLineId = c.Id,
                            Amount = c.Amount,
                            Note = c.Note,
                            PaymentScheduleId = c.PaymentScheduleId,
                            ScheduleDate = c.ScheduleDate,
                            Status = (int) c.Status
                        }).ToList(),
                });
        }

        public IQueryable<EnginePaymentScheduleDTO> EnginePaymentSchedulesQuery(QueryBuilder<PaymentSchedule> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<PaymentSchedule>()).OfType<EnginePaymentSchedule>().Select(p => new EnginePaymentScheduleDTO
            {
                EnginePaymentScheduleId = p.Id,
                CreateDate = p.CreateDate,
                CurrencyId = p.CurrencyId,
                IsCompleted = p.IsCompleted,
                SupplierId = p.SupplierId,
                SupplierName = p.SupplierName,
                ContractEngineId = p.ContractEngineId,
                PaymentScheduleLines = p.PaymentScheduleLines.Select(c => new PaymentScheduleLineDTO
                {
                    PaymentScheduleLineId = c.Id,
                    Amount = c.Amount,
                    Note = c.Note,
                    PaymentScheduleId = c.PaymentScheduleId,
                    ScheduleDate = c.ScheduleDate,
                    Status = (int)c.Status
                }).ToList(),
            });
        }
    }
}