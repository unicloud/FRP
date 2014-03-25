#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/03/23，23:03
// 文件名：PlanHistoryQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.PlanHistoryQueries
{
    public class PlanHistoryQuery : IPlanHistoryQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public PlanHistoryQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     计划明细查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>计划明细DTO集合。</returns>
        public IQueryable<PlanHistoryDTO> PlanHistoryDTOQuery(
            QueryBuilder<PlanHistory> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<PlanHistory>())
                .Select(p => new PlanHistoryDTO
                {
                    Id = p.Id,
                    CarryingCapacity = p.CarryingCapacity,
                    SeatingCapacity = p.SeatingCapacity,
                    PerformAnnualId = p.PerformAnnualId,
                    PerformMonth = p.PerformMonth,
                    IsSubmit = p.IsSubmit,
                    IsValid = p.IsValid,
                    Note = p.Note,
                    ActionCategoryId = p.ActionCategoryId,
                    ActionType = p.ActionCategory.ActionType,
                    ActionName = p.ActionCategory.ActionName,
                    TargetCategoryId = p.TargetCategoryId,
                    TargetType = p.TargetCategory.ActionName,
                    AircraftTypeId = p.AircraftTypeId,
                    AircraftTypeName = p.AircraftType.Name,
                    Regional = p.AircraftType.AircraftCategory.Regional,
                    Category = p.AircraftType.AircraftCategory.Category,
                    AirlinesId = p.AirlinesId,
                    AirlinesName = p.Airlines.CnShortName,
                    NeedRequest = p.ActionCategory.NeedRequest,
                    Year = p.PerformAnnual.Year,
                    PlanId = p.PlanId,
                });
        }
    }
}

