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
           var result= query.ApplyTo(_unitOfWork.CreateSet<Plan>()).Select(p => new PlanDTO
            {
                Id = p.Id,
                Title = p.Title,
                VersionNumber = p.VersionNumber,
                IsValid = p.IsValid,
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
                AirlinesName = p.Airlines.CnShortName,
                Year = p.Annual.Year,
                PlanHistories = p.PlanHistories.OfType<OperationPlan>().Select(q => new PlanHistoryDTO
                                {
                                    Id=q.Id,
                                    CarryingCapacity = q.CarryingCapacity,
                                    SeatingCapacity = q.SeatingCapacity,
                                    PerformAnnualId = q.PerformAnnualId,
                                    PerformMonth = q.PerformMonth,
                                    IsSubmit = q.IsSubmit,
                                    IsValid = q.IsValid,
                                    Note = q.Note,
                                    ActionCategoryId = q.ActionCategoryId,
                                    ActionType = q.ActionCategory.ActionType,
                                    ActionName=q.ActionCategory.ActionName,
                                    TargetCategoryId = q.TargetCategoryId,
                                    TargetType = q.TargetCategory.ActionName,
                                    AircraftTypeId = q.AircraftTypeId,
                                    AircraftTypeName = q.AircraftType.Name,
                                    Regional = q.AircraftType.AircraftCategory.Regional,
                                    AirlinesId = q.AirlinesId,
                                    AirlinesName = q.Airlines.CnShortName,
                                    NeedRequest = q.ActionCategory.NeedRequest,
                                    Year = q.PerformAnnual.Year,
                                    ApprovalHistoryId = q.ApprovalHistoryId,

                                    PlanAircraftId = q.PlanAircraftId,
                                    AircraftId = q.PlanAircraft.AircraftId,
                                    RegNumber = q.PlanAircraft.Aircraft.RegNumber,
                                    ManageStatus = q.PlanAircraft == null ? 0 : (int)q.PlanAircraft.Status,
                                    PaIsLock = q.PlanAircraft.IsLock,

                                    RelatedGuid = q.OperationHistoryId,
                                    RelatedEndDate = q.OperationHistory.EndDate,
                                    RelatedStatus = q.OperationHistory == null ? 0 : (int)q.OperationHistory.Status,
                                    PlanId = q.PlanId,
                                    PlanType = 1,//1表示运营计划
                                    
                                })
                                .Union(p.PlanHistories.OfType<ChangePlan>().Select(q => new PlanHistoryDTO
                                {
                                    Id = q.Id,
                                    CarryingCapacity = q.CarryingCapacity,
                                    SeatingCapacity = q.SeatingCapacity,
                                    PerformAnnualId = q.PerformAnnualId,
                                    PerformMonth = q.PerformMonth,
                                    IsSubmit = q.IsSubmit,
                                    IsValid = q.IsValid,
                                    Note = q.Note,
                                    ActionCategoryId = q.ActionCategoryId,
                                    ActionType = q.ActionCategory.ActionType,
                                    ActionName = q.ActionCategory.ActionName,
                                    TargetCategoryId = q.TargetCategoryId,
                                    TargetType = q.TargetCategory.ActionName,
                                    AircraftTypeId = q.AircraftTypeId,
                                    AircraftTypeName = q.AircraftType.Name,
                                    Regional = q.AircraftType.AircraftCategory.Regional,
                                    AirlinesId = q.AirlinesId,
                                    AirlinesName = q.Airlines.CnShortName,
                                    NeedRequest = q.ActionCategory.NeedRequest,
                                    Year = q.PerformAnnual.Year,
                                    ApprovalHistoryId = q.ApprovalHistoryId,

                                    PlanAircraftId = q.PlanAircraftId,
                                    AircraftId = q.PlanAircraft.AircraftId,
                                    RegNumber = q.PlanAircraft.Aircraft.RegNumber,
                                    ManageStatus = q.PlanAircraft == null ? 0 : (int)q.PlanAircraft.Status,
                                    PaIsLock = q.PlanAircraft.IsLock,

                                    RelatedGuid = q.AircraftBusinessId,
                                    RelatedEndDate = q.AircraftBusiness.EndDate,
                                    RelatedStatus = q.AircraftBusiness == null ? 0 : (int)q.AircraftBusiness.Status,
                                    PlanId = q.PlanId,
                                    PlanType = 2,//2表示变更计划
                                })
                                ).ToList(),
            });
            var a = result.ToList();
            return result;
        }
    }
}