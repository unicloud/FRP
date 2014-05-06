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
            QueryBuilder<MaintainInvoice> query)
        {
            return
                query.ApplyTo(_invoiceRepository.GetAll().OfType<EngineMaintainInvoice>())
                    .Select(p => new EngineMaintainInvoiceDTO
                                 {
                                     EngineMaintainInvoiceId = p.Id,
                                     SerialNumber = p.SerialNumber,
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
                                     DocumentName = p.DocumentName,
                                     DocumentId = p.DocumentId,
                                     PaymentScheduleLineId = p.PaymentScheduleLineId,
                                     MaintainInvoiceLines =
                                         p.InvoiceLines.Select(q => new MaintainInvoiceLineDTO
                                                                            {
                                                                                MaintainInvoiceLineId = q.Id,
                                                                                MaintainItem = (int)q.MaintainItem,
                                                                                ItemName = q.ItemName,
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
                                                                        ItemName = q.ItemName,
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
                                                                        ItemName = q.ItemName,
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
                                                                        ItemName = q.ItemName,
                                                                        UnitPrice = q.UnitPrice,
                                                                        Amount = q.Amount,
                                                                        Note = q.Note
                                                                    }).ToList(),
                                 });
        }
    }
}
