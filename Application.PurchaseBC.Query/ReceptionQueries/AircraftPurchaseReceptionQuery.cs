﻿#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/16 14:44:51
// 文件名：AircraftPurchaseReceptionQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;

namespace UniCloud.Application.PurchaseBC.Query.ReceptionQueries
{
    /// <summary>
    /// 实现采购飞机接收项目查询接口
    /// </summary>
    public class AircraftPurchaseReceptionQuery : IAircraftPurchaseReceptionQuery
    {
        private readonly IReceptionRepository _receptionRepository;
        public AircraftPurchaseReceptionQuery(IReceptionRepository receptionRepository)
        {
            _receptionRepository = receptionRepository;
        }

        /// <summary>
        ///    采购飞机接收项目查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>采购飞机接收项目DTO集合。</returns>
        public IQueryable<AircraftPurchaseReceptionDTO> AircraftPurchaseReceptionDTOQuery(
            QueryBuilder<AircraftPurchaseReception> query)
        {
            return
                query.ApplyTo(_receptionRepository.GetAll().OfType<AircraftPurchaseReception>()).Select(p => new AircraftPurchaseReceptionDTO
                {
                    AircraftPurchaseReceptionId = p.Id,
                    CreateDate = p.CreateDate,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    CloseDate = p.CloseDate,
                    IsClosed = p.IsClosed,
                    ReceptionLines = p.ReceptionLines.OfType<AircraftPurchaseReceptionLine>().Select(q => new AircraftPurchaseReceptionLineDTO
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
