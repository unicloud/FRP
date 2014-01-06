#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：AnnualQuery.cs
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
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.AnnualQueries
{
    public class AnnualQuery : IAnnualQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public AnnualQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     计划年度查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>计划年度DTO集合。</returns>
        public IQueryable<AnnualDTO> AnnualDTOQuery(
            QueryBuilder<Annual> query)
        {
            var plans = _unitOfWork.CreateSet<Plan>();
            var aircraftBusinesses = _unitOfWork.CreateSet<AircraftBusiness>();
            var operationHistories = _unitOfWork.CreateSet<OperationHistory>();
            var result = query.ApplyTo(_unitOfWork.CreateSet<Annual>()).Select(p => new AnnualDTO
            {
                Id = p.Id,
                IsOpen = p.IsOpen,
                ProgrammingId = p.ProgrammingId,
                Year = p.Year,
                ProgrammingName = p.Programming.Name,
                Plans = plans.Where(r => r.AnnualId == p.Id).Select(r => new PlanDTO
                {
                    Id = r.Id,
                    Title = r.Title,
                    VersionNumber = r.VersionNumber,
                    IsValid = r.IsValid,
                    IsCurrentVersion = r.IsCurrentVersion,
                    SubmitDate = r.SubmitDate,
                    CreateDate = r.CreateDate,
                    DocNumber = r.DocNumber,
                    DocName = r.DocName,
                    IsFinished = r.IsFinished,
                    Status = (int)r.Status,
                    PublishStatus = (int)r.PublishStatus,
                    AirlinesId = r.AirlinesId,
                    AnnualId = r.AnnualId,
                    DocumentId = r.DocumentId,
                    AirlinesName = r.Airlines.CnName,
                    Year = r.Annual.Year,
                    PlanHistories = r.PlanHistories.OfType<OperationPlan>().Select(q => new PlanHistoryDTO
                    {
                        Id = q.Id,
                        ActionCategoryId = q.ActionCategoryId,
                        AircraftTypeId = q.AircraftTypeId,
                        AirlinesId = q.AirlinesId,
                        CarryingCapacity = q.CarryingCapacity,
                        SeatingCapacity = q.SeatingCapacity,
                        RelatedGuid = q.OperationHistoryId,
                        //RelatedEndDate = operationHistories.FirstOrDefault(o=>o.Id==q.OperationHistoryId).EndDate,
                        IsSubmit = q.IsSubmit,
                        IsValid = q.IsValid,
                        Note = q.Note,
                        PerformAnnualId = q.PerformAnnualId,
                        PerformMonth = q.PerformMonth,
                        PlanAircraftId = q.PlanAircraftId,
                        PlanId = q.PlanId,
                        PlanType = 1,
                        TargetCategoryId = q.TargetCategoryId,
                        AirlinesName = q.Airlines.CnName,
                        Regional = q.AircraftType.AircraftCategory.Regional,
                        AircraftTypeName = q.AircraftType.Name,
                        ActionType = q.ActionCategory.ActionType + ":" + q.ActionCategory.ActionName,
                        TargetType = q.TargetCategory.ActionName,
                        Year = q.PerformAnnual.Year,
                        ManageStatus = (int)q.PlanAircraft.Status,
                    })
                     .Union(r.PlanHistories.OfType<ChangePlan>().Select(q => new PlanHistoryDTO
                    {
                        Id = q.Id,
                        ActionCategoryId = q.ActionCategoryId,
                        AircraftTypeId = q.AircraftTypeId,
                        AirlinesId = q.AirlinesId,
                        CarryingCapacity = q.CarryingCapacity,
                        SeatingCapacity = q.SeatingCapacity,
                        RelatedGuid = q.AircraftBusinessId,
                        //RelatedEndDate = aircraftBusinesses.FirstOrDefault(o=>o.Id==q.AircraftBusinessId).EndDate,
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
                    })).ToList(),
                }).ToList()
            });
            return result.OrderByDescending(p=>p.Year);
        }

        public IQueryable<PlanYearDTO> PlanYearDTOQuery(QueryBuilder<Annual> query)
        {
            var result = query.ApplyTo(_unitOfWork.CreateSet<Annual>()).Select(p => new PlanYearDTO
            {
                Id = p.Id,
                IsOpen = p.IsOpen,
                ProgrammingId = p.ProgrammingId,
                Year = p.Year,
                ProgrammingName = p.Programming.Name,
            });
            return result.OrderByDescending(p=>p.Year);
        }
    }
}