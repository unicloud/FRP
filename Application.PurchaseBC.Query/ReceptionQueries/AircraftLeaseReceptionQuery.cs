#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/16 14:40:26
// 文件名：AircraftLeaseReceptionQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.RelatedDocQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;
using UniCloud.Infrastructure.Data;

namespace UniCloud.Application.PurchaseBC.Query.ReceptionQueries
{
    /// <summary>
    /// 实现租赁飞机接收项目查询接口
    /// </summary>
    public class AircraftLeaseReceptionQuery : IAircraftLeaseReceptionQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public AircraftLeaseReceptionQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///    租赁飞机接收项目查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>租赁飞机接收项目DTO集合。</returns>
        public IQueryable<AircraftLeaseReceptionDTO> AircraftLeaseReceptionDTOQuery(
            QueryBuilder<AircraftLeaseReception> query)
        {
            return
            query.ApplyTo(_unitOfWork.CreateSet<Reception>().OfType<AircraftLeaseReception>())
                     .Select(p => new AircraftLeaseReceptionDTO
                     {
                         AircraftLeaseReceptionId = p.Id,
                         ReceptionNumber = p.ReceptionNumber,
                         CreateDate = p.CreateDate,
                         StartDate = p.StartDate,
                         EndDate = p.EndDate,
                         CloseDate = p.CloseDate,
                         IsClosed = p.IsClosed,
                         SupplierId = p.SupplierId,
                         SupplierName = p.Supplier.Name,
                         SourceId = p.SourceId,
                         ReceptionLines = p.ReceptionLines.OfType<AircraftLeaseReceptionLine>()
                         .Select(q => new AircraftLeaseReceptionLineDTO
                         {
                             ReceptionId = q.ReceptionId,
                             ReceivedAmount = q.ReceivedAmount,
                             AcceptedAmount = q.AcceptedAmount,
                             IsCompleted = q.IsCompleted,
                             Note = q.Note,
                             MSN = q.LeaseContractAircraft.SerialNumber,
                             ContractNumber = q.LeaseContractAircraft.ContractNumber,
                             AircraftType = q.LeaseContractAircraft.AircraftType.Name,
                             ImportCategoryId = q.LeaseContractAircraft.ImportCategory.ActionName,
                             ContractAircraftId = q.ContractAircraftId,
                             RankNumber = q.LeaseContractAircraft.RankNumber,
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
                             Location = q.Location,
                             UniqueId = q.UniqueId,
                             Url = q.Url,
                         }).ToList(),
                     });
        }
    }
}
