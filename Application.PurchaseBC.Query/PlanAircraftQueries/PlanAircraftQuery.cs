﻿#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/3 10:52:24
// 文件名：PlanAircraftQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftPlanHistoryAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.PlanAircraftAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.PlanAircraftQueries
{
    /// <summary>
    ///     计划飞机查询
    /// </summary>
    public class PlanAircraftQuery : IPlanAircraftQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public PlanAircraftQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     计划飞机查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>PlanAircraftDTO集合</returns>
        public IQueryable<PlanAircraftDTO> PlanAircraftDTOQuery(QueryBuilder<PlanAircraft> query)
        {
            var ps = _unitOfWork.CreateSet<Plan>();
            var phs = _unitOfWork.CreateSet<PlanHistory>();
            return query.ApplyTo(_unitOfWork.CreateSet<PlanAircraft>()).Select(p => new PlanAircraftDTO
            {
                Id = p.Id,
                AircraftId = p.AircraftId,
                ContractAircraftId = p.ContractAircraftId,
                AircraftTypeId = p.AircraftTypeId,
                IsLock = p.IsLock,
                IsOwn = p.IsOwn,
                Status = (int) p.Status,
                Regional = p.AircraftType.AircraftCategory.Regional,
                AircraftTypeName = p.AircraftType.Name,
                PlanHistories = (from ph in phs
                    join pl in ps on ph.PlanId equals pl.Id
                    select new PlanHistoryDTO
                    {
                        Id = ph.Id,
                        PlanAircraftId = ph.PlanAircraftId.Value,
                        PlanYear = pl.Annual.Year,
                        VersionNumber = pl.VersionNumber,
                        PerformAnnual = ph.PerformAnnual.Year,
                        PerformMonth = ph.PerformMonth,
                        ActionType = ph.TargetCategory.ActionType,
                        ActionName = ph.TargetCategory.ActionName
                    }).Where(plh => plh.PlanAircraftId == p.Id).ToList()
            });
        }

        public IQueryable<PlanHistoryDTO> PlanHistoryDTOQuery(QueryBuilder<PlanHistory> query)
        {
            var result = from ph in _unitOfWork.CreateSet<PlanHistory>()
                join p in _unitOfWork.CreateSet<Plan>() on ph.PlanId equals p.Id
                select new PlanHistoryDTO
                {
                    Id = ph.Id,
                    PlanAircraftId = ph.PlanAircraftId.Value,
                    PlanYear = p.Annual.Year,
                    VersionNumber = p.VersionNumber,
                    PerformAnnual = ph.PerformAnnual.Year,
                    PerformMonth = ph.PerformMonth,
                    ActionType = ph.TargetCategory.ActionType,
                    ActionName = ph.TargetCategory.ActionName
                };
            return result;
        }
    }
}