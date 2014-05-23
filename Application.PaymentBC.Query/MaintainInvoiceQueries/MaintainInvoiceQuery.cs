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
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg;

#endregion

namespace UniCloud.Application.PaymentBC.Query.MaintainInvoiceQueries
{
    public class MaintainInvoiceQuery : IMaintainInvoiceQuery
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public MaintainInvoiceQuery(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        /// <summary>
        ///     发票查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>发票DTO集合。</returns>
        public IQueryable<BaseInvoiceDTO> InvoiceDTOQuery(
            QueryBuilder<Invoice> query)
        {
            return
                query.ApplyTo(_invoiceRepository.GetAll().OfType<Invoice>())
                    .Select(p => new BaseInvoiceDTO
                    {
                        InvoiceId = p.Id,
                        InvoiceType = (int)p.InvoiceType,
                        InvoiceNumber = p.InvoiceNumber,
                        InvoideCode = p.InvoideCode,
                        InvoiceDate = p.InvoiceDate,
                        SupplierName = p.SupplierName,
                        InvoiceValue = p.InvoiceValue,
                        PaidAmount = p.PaidAmount,
                        OperatorName = p.OperatorName,
                        Reviewer = p.Reviewer,
                        CreateDate = p.CreateDate,
                        ReviewDate = p.ReviewDate,
                        IsValid = p.IsValid,
                        IsCompleted = p.IsCompleted,
                        Status = (int)p.Status,
                        SupplierId = p.SupplierId,
                        CurrencyId = p.CurrencyId,
                    });
        }

        /// <summary>
        ///     发动机维修发票查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>发动机维修发票DTO集合。</returns>
        public IQueryable<EngineMaintainInvoiceDTO> EngineMaintainInvoiceDTOQuery(
            QueryBuilder<EngineMaintainInvoice> query)
        {
            return
                query.ApplyTo(_invoiceRepository.GetAll().OfType<EngineMaintainInvoice>())
                    .Select(p => new EngineMaintainInvoiceDTO
                                 {
                                     EngineMaintainInvoiceId = p.Id,
                                     Type = (int)p.Type,
                                     SerialNumber = p.SerialNumber,
                                     InvoiceNumber = p.InvoiceNumber,
                                     InvoideCode = p.InvoideCode,
                                     InvoiceDate = p.InvoiceDate,
                                     SupplierName = p.SupplierName,
                                     InvoiceValue = p.InvoiceValue,
                                     PaidAmount = p.PaidAmount,
                                     OperatorName = p.OperatorName,
                                     InMaintainTime = p.InMaintainTime,
                                     OutMaintainTime = p.OutMaintainTime,
                                     Reviewer = p.Reviewer,
                                     CreateDate = p.CreateDate,
                                     ReviewDate = p.ReviewDate,
                                     IsValid = p.IsValid,
                                     IsCompleted = p.IsCompleted,
                                     Status = (int)p.Status,
                                     SupplierId = p.SupplierId,
                                     CurrencyId = p.CurrencyId,
                                     DocumentName = p.DocumentName,
                                     DocumentId = p.DocumentId,
                                     PaymentScheduleLineId = p.PaymentScheduleLineId,
                                     MaintainInvoiceLines =
                                         p.InvoiceLines.Select(q => new MaintainInvoiceLineDTO
                                                                            {
                                                                                MaintainInvoiceLineId = q.Id,
                                                                                MaintainItem = (int)q.MaintainItem,
                                                                                ItemName = (int)q.ItemName,
                                                                                UnitPrice = q.UnitPrice,
                                                                                Amount = q.Amount,
                                                                                Note = q.Note
                                                                            }).ToList(),
                                 });
        }


        /// <summary>
        ///     APU维修发票查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>APU维修发票DTO集合。</returns>
        public IQueryable<APUMaintainInvoiceDTO> APUMaintainInvoiceDTOQuery(
           QueryBuilder<MaintainInvoice> query)
        {
            return
                query.ApplyTo(_invoiceRepository.GetAll().OfType<APUMaintainInvoice>())
                    .Select(p => new APUMaintainInvoiceDTO
                                 {
                                     APUMaintainInvoiceId = p.Id,
                                     SerialNumber = p.SerialNumber,
                                     InvoiceNumber = p.InvoiceNumber,
                                     InvoideCode = p.InvoideCode,
                                     InvoiceDate = p.InvoiceDate,
                                     SupplierName = p.SupplierName,
                                     InvoiceValue = p.InvoiceValue,
                                     PaidAmount = p.PaidAmount,
                                     OperatorName = p.OperatorName,
                                     InMaintainTime = p.InMaintainTime,
                                     OutMaintainTime = p.OutMaintainTime,
                                     Reviewer = p.Reviewer,
                                     CreateDate = p.CreateDate,
                                     ReviewDate = p.ReviewDate,
                                     IsValid = p.IsValid,
                                     IsCompleted = p.IsCompleted,
                                     Status = (int)p.Status,
                                     SupplierId = p.SupplierId,
                                     CurrencyId = p.CurrencyId,
                                     DocumentName = p.DocumentName,
                                     DocumentId = p.DocumentId,
                                     PaymentScheduleLineId = p.PaymentScheduleLineId,
                                     MaintainInvoiceLines =
                                         p.InvoiceLines.Select(q => new MaintainInvoiceLineDTO
                                                                    {
                                                                        MaintainInvoiceLineId = q.Id,
                                                                        MaintainItem = (int)q.MaintainItem,
                                                                        ItemName = (int)q.ItemName,
                                                                        UnitPrice = q.UnitPrice,
                                                                        Amount = q.Amount,
                                                                        Note = q.Note
                                                                    }).ToList(),
                                 });
        }

        /// <summary>
        ///     机身维修发票查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>机身维修发票DTO集合</returns>
        public IQueryable<AirframeMaintainInvoiceDTO> AirframeMaintainInvoiceDTOQuery(
             QueryBuilder<MaintainInvoice> query)
        {
            return
                query.ApplyTo(_invoiceRepository.GetAll().OfType<AirframeMaintainInvoice>())
                    .Select(p => new AirframeMaintainInvoiceDTO
                                 {
                                     AirframeMaintainInvoiceId = p.Id,
                                     SerialNumber = p.SerialNumber,
                                     InvoiceNumber = p.InvoiceNumber,
                                     InvoideCode = p.InvoideCode,
                                     InvoiceDate = p.InvoiceDate,
                                     SupplierName = p.SupplierName,
                                     InvoiceValue = p.InvoiceValue,
                                     InMaintainTime = p.InMaintainTime,
                                     OutMaintainTime = p.OutMaintainTime,
                                     PaidAmount = p.PaidAmount,
                                     OperatorName = p.OperatorName,
                                     Reviewer = p.Reviewer,
                                     CreateDate = p.CreateDate,
                                     ReviewDate = p.ReviewDate,
                                     IsValid = p.IsValid,
                                     IsCompleted = p.IsCompleted,
                                     Status = (int)p.Status,
                                     SupplierId = p.SupplierId,
                                     CurrencyId = p.CurrencyId,
                                     DocumentName = p.DocumentName,
                                     DocumentId = p.DocumentId,
                                     PaymentScheduleLineId = p.PaymentScheduleLineId,
                                     MaintainInvoiceLines =
                                         p.InvoiceLines.Select(q => new MaintainInvoiceLineDTO
                                                                    {
                                                                        MaintainInvoiceLineId = q.Id,
                                                                        MaintainItem = (int)q.MaintainItem,
                                                                        ItemName = (int)q.ItemName,
                                                                        UnitPrice = q.UnitPrice,
                                                                        Amount = q.Amount,
                                                                        Note = q.Note
                                                                    }).ToList(),
                                 });
        }

        /// <summary>
        ///     起落架维修发票查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>起落架维修发票DTO集合。</returns>
        public IQueryable<UndercartMaintainInvoiceDTO> UndercartMaintainInvoiceDTOQuery(
            QueryBuilder<MaintainInvoice> query)
        {
            return
                query.ApplyTo(_invoiceRepository.GetAll().OfType<UndercartMaintainInvoice>())
                    .Select(p => new UndercartMaintainInvoiceDTO
                                 {
                                     UndercartMaintainInvoiceId
                                         = p.Id,
                                     SerialNumber = p.SerialNumber,
                                     InvoiceNumber = p.InvoiceNumber,
                                     InvoideCode = p.InvoideCode,
                                     InvoiceDate = p.InvoiceDate,
                                     SupplierName = p.SupplierName,
                                     InvoiceValue = p.InvoiceValue,
                                     PaidAmount = p.PaidAmount,
                                     InMaintainTime = p.InMaintainTime,
                                     OutMaintainTime = p.OutMaintainTime,
                                     OperatorName = p.OperatorName,
                                     Reviewer = p.Reviewer,
                                     CreateDate = p.CreateDate,
                                     ReviewDate = p.ReviewDate,
                                     IsValid = p.IsValid,
                                     IsCompleted = p.IsCompleted,
                                     Status = (int)p.Status,
                                     SupplierId = p.SupplierId,
                                     CurrencyId = p.CurrencyId,
                                     DocumentName = p.DocumentName,
                                     DocumentId = p.DocumentId,
                                     PaymentScheduleLineId = p.PaymentScheduleLineId,
                                     MaintainInvoiceLines =
                                         p.InvoiceLines.Select(q => new MaintainInvoiceLineDTO
                                                                    {
                                                                        MaintainInvoiceLineId = q.Id,
                                                                        MaintainItem = (int)q.MaintainItem,
                                                                        ItemName = (int)q.ItemName,
                                                                        UnitPrice = q.UnitPrice,
                                                                        Amount = q.Amount,
                                                                        Note = q.Note
                                                                    }).ToList(),
                                 });
        }

        /// <summary>
        ///    特修改装发票查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>特修改装发票DTO集合。</returns>
        public IQueryable<SpecialRefitInvoiceDTO> SpecialRefitInvoiceDTOQuery(
            QueryBuilder<SpecialRefitInvoice> query)
        {
            return
                query.ApplyTo(_invoiceRepository.GetAll().OfType<SpecialRefitInvoice>())
                     .Select(p => new SpecialRefitInvoiceDTO
                     {
                         SpecialRefitId = p.Id,
                         InvoiceNumber = p.InvoiceNumber,
                         InvoideCode = p.InvoideCode,
                         InvoiceDate = p.InvoiceDate,
                         SupplierName = p.SupplierName,
                         SupplierId = p.SupplierId,
                         InvoiceValue = p.InvoiceValue,
                         PaidAmount = p.PaidAmount,
                         OperatorName = p.OperatorName,
                         Reviewer = p.Reviewer,
                         CreateDate = p.CreateDate,
                         ReviewDate = p.ReviewDate,
                         IsValid = p.IsValid,
                         IsCompleted = p.IsCompleted,
                         Status = (int)p.Status,
                         CurrencyId = p.CurrencyId,
                         PaymentScheduleLineId = p.PaymentScheduleLineId,
                         MaintainInvoiceLines = p.InvoiceLines.Select(q => new MaintainInvoiceLineDTO
                         {
                             MaintainInvoiceLineId = q.Id,
                             ItemName = (int)q.ItemName,
                             Amount = q.Amount,
                             InvoiceId = q.InvoiceId,
                             Note = q.Note,
                         }).ToList(),

                     });
        }
    }
}
