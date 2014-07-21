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
            return query.ApplyTo(_unitOfWork.CreateSet<PlanHistory>()).OfType<OperationPlan>().Select(q => new PlanHistoryDTO
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
                                    CaacAircraftTypeName = q.AircraftType.CaacAircraftType.Name,
                                    Regional = q.AircraftType.AircraftCategory.Regional,
                                    Category = q.AircraftType.AircraftCategory.Category,
                                    AirlinesId = q.AirlinesId,
                                    AirlinesName = q.Airlines.CnShortName,
                                    NeedRequest = q.ActionCategory.NeedRequest,
                                    Year = q.PerformAnnual.Year,
                                    CanRequest = (int)q.CanRequest,
                                    CanDeliver = (int)q.CanDeliver,

                                    ApprovalHistoryId = q.ApprovalHistoryId,
                                    //IsApproved = q.ApprovalHistory.IsApproved,TODO

                                    PlanAircraftId = q.PlanAircraftId,
                                    AircraftId = q.PlanAircraft.AircraftId,
                                    RegNumber = q.PlanAircraft.Aircraft.RegNumber,
                                    AircraftImportCategoryId = q.PlanAircraft.Aircraft.ImportCategoryId,
                                    ManageStatus = q.PlanAircraft == null ? 0 : (int)q.PlanAircraft.Status,
                                    //PaIsLock = q.PlanAircraft.IsLock,

                                    RelatedGuid = q.OperationHistoryId,
                                    RelatedEndDate = q.OperationHistory.EndDate,
                                    RelatedStatus = q.OperationHistory == null ? 0 : (int)q.OperationHistory.Status,
                                    PlanId = q.PlanId,
                                    PlanType = 1,//1表示运营计划
                                    RelatedStartDate = q.OperationHistory.StartDate,
                                }).Union((_unitOfWork.CreateSet<PlanHistory>()).OfType<ChangePlan>().Select(p => new PlanHistoryDTO
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
                                    CaacAircraftTypeName = p.AircraftType.CaacAircraftType.Name,
                                    Regional = p.AircraftType.AircraftCategory.Regional,
                                    Category = p.AircraftType.AircraftCategory.Category,
                                    AirlinesId = p.AirlinesId,
                                    AirlinesName = p.Airlines.CnShortName,
                                    NeedRequest = p.ActionCategory.NeedRequest,
                                    Year = p.PerformAnnual.Year,
                                    CanRequest = (int)p.CanRequest,
                                    CanDeliver = (int)p.CanDeliver,

                                    ApprovalHistoryId = p.ApprovalHistoryId,
                                    //IsApproved = p.ApprovalHistory.IsApproved,TODO

                                    PlanAircraftId = p.PlanAircraftId,
                                    AircraftId = p.PlanAircraft.AircraftId,
                                    RegNumber = p.PlanAircraft.Aircraft.RegNumber,
                                    AircraftImportCategoryId = p.PlanAircraft.Aircraft.ImportCategoryId,
                                    ManageStatus = p.PlanAircraft == null ? 0 : (int)p.PlanAircraft.Status,
                                    //PaIsLock = p.PlanAircraft.IsLock,

                                    RelatedGuid = p.AircraftBusinessId,
                                    RelatedEndDate = p.AircraftBusiness.EndDate,
                                    RelatedStatus = p.AircraftBusiness == null ? 0 : (int)p.AircraftBusiness.Status,
                                    PlanId = p.PlanId,
                                    PlanType = 2,//2表示变更计划
                                    RelatedStartDate = p.AircraftBusiness.StartDate,
                                    }));
        }
    }
}

