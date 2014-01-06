#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：PlanQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.AircraftPlanQueries
{
    public class PlanQuery : IPlanQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public PlanQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     运力增减计划查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>运力增减计划DTO集合。</returns>
        public IQueryable<PlanDTO> PlanDTOQuery(
            QueryBuilder<Plan> query)
        {
            var aircraftBusinesses = _unitOfWork.CreateSet<AircraftBusiness>();
            var operationHistories = _unitOfWork.CreateSet<OperationHistory>();

            return query.ApplyTo(_unitOfWork.CreateSet<Plan>()).Select(p => new PlanDTO
            {
                Id = p.Id,
                Title = p.Title,
                VersionNumber = p.VersionNumber,
                IsValid = p.IsValid,
                IsCurrentVersion = p.IsCurrentVersion,
                SubmitDate = p.SubmitDate,
                CreateDate = p.CreateDate,
                DocNumber = p.DocNumber,
                DocName = p.DocName,
                IsFinished = p.IsFinished,
                Status = (int)p.Status,
                PublishStatus = (int)p.PublishStatus,
                AirlinesId = p.AirlinesId,
                AnnualId = p.AnnualId,
                DocumentId = p.DocumentId,
                AirlinesName = p.Airlines.CnName,
                Year = p.Annual.Year,
                PlanHistories = p.PlanHistories.OfType<OperationPlan>().Select(q => new PlanHistoryDTO
                                {
                                    Id=q.Id,
                                    ActionCategoryId = q.ActionCategoryId,
                                    AircraftTypeId = q.AircraftTypeId,
                                    AirlinesId = q.AirlinesId,
                                    CarryingCapacity = q.CarryingCapacity,
                                    SeatingCapacity = q.SeatingCapacity,
                                    RelatedGuid = q.OperationHistoryId,
                                    RelatedEndDate = operationHistories.FirstOrDefault(o => o.Id == q.OperationHistoryId).EndDate,
                                    IsSubmit = q.IsSubmit,
                                    IsValid = q.IsValid,
                                    Note = q.Note,
                                    PerformAnnualId = q.PerformAnnualId,
                                    PerformMonth = q.PerformMonth,
                                    PlanAircraftId = q.PlanAircraftId,
                                    PlanId = q.PlanId,
                                    PlanType = 1,
                                    TargetCategoryId = q.TargetCategoryId,
                                    AirlinesName=q.Airlines.CnName,
                                    Regional=q.AircraftType.AircraftCategory.Regional,
                                    AircraftTypeName=q.AircraftType.Name,
                                    ActionType=q.ActionCategory.ActionType+":"+q.ActionCategory.ActionName,
                                    TargetType = q.TargetCategory.ActionName,
                                    Year = q.PerformAnnual.Year,
                                    ManageStatus = (int)q.PlanAircraft.Status,
                                })
                                .Union(p.PlanHistories.OfType<ChangePlan>().Select(q => new PlanHistoryDTO
                                {
                                    Id = q.Id,
                                    ActionCategoryId = q.ActionCategoryId,
                                    AircraftTypeId = q.AircraftTypeId,
                                    AirlinesId = q.AirlinesId,
                                    CarryingCapacity = q.CarryingCapacity,
                                    SeatingCapacity = q.SeatingCapacity,
                                    RelatedGuid = q.AircraftBusinessId,
                                    RelatedEndDate = aircraftBusinesses.FirstOrDefault(o => o.Id == q.AircraftBusinessId).EndDate,
                                    IsSubmit = q.IsSubmit,
                                    IsValid = q.IsValid,
                                    Note = q.Note,
                                    PerformAnnualId = q.PerformAnnualId,
                                    PerformMonth = q.PerformMonth,
                                    PlanAircraftId = q.PlanAircraftId,
                                    PlanId = q.PlanId,
                                    PlanType = 2,
                                    TargetCategoryId = q.TargetCategoryId,
                                    AirlinesName = q.Airlines.CnName,
                                    Regional = q.AircraftType.AircraftCategory.Regional,
                                    AircraftTypeName = q.AircraftType.Name,
                                    ActionType = q.ActionCategory.ActionType + ":" + q.ActionCategory.ActionName,
                                    TargetType = q.TargetCategory.ActionName,
                                    Year = q.PerformAnnual.Year,
                                    ManageStatus = (int)q.PlanAircraft.Status,
                                })
                                ).ToList(),
            });
        }
    }
}