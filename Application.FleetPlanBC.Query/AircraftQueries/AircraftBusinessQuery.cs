#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 11:52:42
// 文件名：AircraftBusinessQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 11:52:42
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Application.FleetPlanBC.Query.AircraftQueries
{
   public class AircraftBusinessQuery:IAircraftBusinessQuery
    {
       //private readonly IAircraftBusinessRepository _invoiceRepository;

       // public AircraftBusinessQuery(IAircraftBusinessRepository invoiceRepository)
       // {
       //     _invoiceRepository = invoiceRepository;
       // }

       // /// <summary>
       // ///     发动机维修发票查询。
       // /// </summary>
       // /// <param name="query">查询表达式。</param>
       // /// <returns>发动机维修发票DTO集合。</returns>
       // public IQueryable<AircraftBusinessDTO> AircraftBusinessDTOQuery(
       //     QueryBuilder<AircraftBusiness> query)
       // {
       //     return
       //         query.ApplyTo(_invoiceRepository.GetAll().OfType<EngineAircraftBusiness>())
       //             .Select(p => new EngineAircraftBusinessDTO
       //                          {
       //                              EngineAircraftBusinessId = p.Id,
       //                              SerialNumber = p.SerialNumber,
       //                              InvoiceNumber = p.InvoiceNumber,
       //                              InvoideCode = p.InvoideCode,
       //                              InvoiceDate = p.InvoiceDate,
       //                              SupplierName = p.SupplierName,
       //                              InvoiceValue = p.InvoiceValue,
       //                              PaidAmount = p.PaidAmount,
       //                              OperatorName = p.OperatorName,
       //                              Reviewer = p.Reviewer,
       //                              CreateDate = p.CreateDate,
       //                              ReviewDate = p.ReviewDate,
       //                              IsValid = p.IsValid,
       //                              IsCompleted = p.IsCompleted,
       //                              Status = (int)p.Status,
       //                              SupplierId = p.SupplierId,
       //                              CurrencyId = p.CurrencyId,
       //                              DocumentName = p.DocumentName,
       //                              DocumentId = p.DocumentId,
       //                              AircraftBusinessLines =
       //                                  p.AircraftBusinessLines.Select(q => new AircraftBusinessLineDTO
       //                                                                     {
       //                                                                         AircraftBusinessLineId = q.Id,
       //                                                                         MaintainItem = (int)q.MaintainItem,
       //                                                                         ItemName = q.ItemName,
       //                                                                         UnitPrice = q.UnitPrice,
       //                                                                         Amount = q.Amount,
       //                                                                         Note = q.Note
       //                                                                     }).ToList(),
       //                          });
       // }
    }
}
