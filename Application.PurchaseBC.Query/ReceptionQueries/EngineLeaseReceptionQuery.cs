#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/16 14:45:54
// 文件名：EngineLeaseReceptionQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;
using UniCloud.Infrastructure.Data;

namespace UniCloud.Application.PurchaseBC.Query.ReceptionQueries
{
    /// <summary>
    /// 实现租赁发动机接收项目查询接口
    /// </summary>
    public class EngineLeaseReceptionQuery : IEngineLeaseReceptionQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public EngineLeaseReceptionQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///    租赁发动机接收项目查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>租赁发动机接收项目DTO集合。</returns>
        public IQueryable<EngineLeaseReceptionDTO> EngineLeaseReceptionDTOQuery(
            QueryBuilder<EngineLeaseReception> query)
        {
            return
                query.ApplyTo(_unitOfWork.CreateSet<Reception>().OfType<EngineLeaseReception>()).Select(p => new EngineLeaseReceptionDTO
                {
                    EngineLeaseReceptionId = p.Id,
                    CreateDate = p.CreateDate,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    CloseDate = p.CloseDate,
                    IsClosed = p.IsClosed,
                    ReceptionLines = p.ReceptionLines.OfType<EngineLeaseReceptionLine>().Select(q => new EngineLeaseReceptionLineDTO()
                    {
                        ReceptionId = q.ReceptionId,
                        ReceivedAmount = q.ReceivedAmount,
                        AcceptedAmount = q.AcceptedAmount,
                        IsCompleted = q.IsCompleted,
                        Note = q.Note,
                    }).ToList(),
                    ReceptionSchedules = p.ReceptionSchedules.Select(q => new ReceptionScheduleDTO
                    {
                        ReceptionId = q.ReceptionId,
                        Subject = q.Subject,
                        Body = q.Body,
                        Importance = q.Importance,
                        Start = q.Start,
                        End = q.End,
                        IsAllDayEvent = q.IsAllDayEvent,
                        Group = q.Group,
                    }).ToList(),
                });
        }

    }
}

