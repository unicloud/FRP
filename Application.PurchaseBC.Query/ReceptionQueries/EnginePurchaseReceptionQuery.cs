#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/16 14:45:30
// 文件名：EnginePurchaseReceptionQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg;
using UniCloud.Infrastructure.Data;

namespace UniCloud.Application.PurchaseBC.Query.ReceptionQueries
{
    /// <summary>
    /// 实现采购发动机接收项目查询接口
    /// </summary>
    public class EnginePurchaseReceptionQuery : IEnginePurchaseReceptionQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public EnginePurchaseReceptionQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///    采购发动机接收项目查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>采购发动机接收项目DTO集合。</returns>
        public IQueryable<EnginePurchaseReceptionDTO> EnginePurchaseReceptionDTOQuery(
            QueryBuilder<EnginePurchaseReception> query)
        {
            var relatedDocs = _unitOfWork.CreateSet<RelatedDoc>();
            
            return
                query.ApplyTo(_unitOfWork.CreateSet<Reception>().OfType<EnginePurchaseReception>()).Select(p => new EnginePurchaseReceptionDTO
                {
                    EnginePurchaseReceptionId = p.Id,
                    ReceptionNumber = p.ReceptionNumber,
                    CreateDate = p.CreateDate,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    CloseDate = p.CloseDate,
                    IsClosed = p.IsClosed,
                    SupplierId = p.SupplierId,
                    SupplierName = p.Supplier.CnName,
                    SourceId = p.SourceId,
                    ReceptionLines = p.ReceptionLines.OfType<EnginePurchaseReceptionLine>()
                    .Select(q => new EnginePurchaseReceptionLineDTO
                    {
                        ReceptionId = q.ReceptionId,
                        ReceivedAmount = q.ReceivedAmount,
                        AcceptedAmount = q.AcceptedAmount,
                        IsCompleted = q.IsCompleted,
                        Note = q.Note,
                        SerialNumber = q.PurchaseContractEngine.SerialNumber,
                        ContractNumber = q.PurchaseContractEngine.ContractNumber,
                        ContractName = q.PurchaseContractEngine.ContractName,
                        ImportCategoryId = q.PurchaseContractEngine.ImportCategory.ActionName,
                        ContractEngineId = q.ContractEngineId,
                        DeliverDate = q.DeliverDate,
                        DeliverPlace = q.DeliverPlace,
                        RankNumber = q.PurchaseContractEngine.RankNumber,
                    }).ToList(),
                    ReceptionSchedules = p.ReceptionSchedules.Select(q => new ReceptionScheduleDTO
                    {
                        ReceptionScheduleId = q.Id,
                        ReceptionId = q.ReceptionId,
                        Subject = q.Subject,
                        Body = q.Body,
                        Importance = q.Importance,
                        Start = q.Start,
                        End = q.End,
                        IsAllDayEvent = q.IsAllDayEvent,
                        Group = q.Group,
                        Tempo = q.Tempo,
                    }).ToList(),
                    RelatedDocs = relatedDocs.Where(r => r.SourceId == p.SourceId).Select(r => new RelatedDocDTO
                    {
                        Id = r.Id,
                        SourceId = r.SourceId,
                        DocumentId = r.DocumentId,
                        DocumentName = r.DocumentName
                    }).ToList()
                });
        }
    }
}