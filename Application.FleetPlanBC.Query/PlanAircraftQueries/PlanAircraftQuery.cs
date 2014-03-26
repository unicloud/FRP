#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：PlanAircraftQuery.cs
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
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.PlanAircraftQueries
{
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
        /// <param name="query">查询表达式。</param>
        /// <returns>计划飞机DTO集合。</returns>
        public IQueryable<PlanAircraftDTO> PlanAircraftDTOQuery(
            QueryBuilder<PlanAircraft> query)
        {
            //var operationPlans = _unitOfWork.CreateSet<OperationPlan>();
            //var changePlans = _unitOfWork.CreateSet<ChangePlan>();
            //var approvalHistories = _unitOfWork.CreateSet<ApprovalHistory>();

            return query.ApplyTo(_unitOfWork.CreateSet<PlanAircraft>()).Select(p => new PlanAircraftDTO
            {
                Id = p.Id,
                AirlinesId = p.AirlinesId,
                AircraftId = p.AircraftId,
                AircraftTypeId = p.AircraftTypeId,
                IsLock = p.IsLock,
                IsOwn = p.IsOwn,
                Status = (int)p.Status,
                AirlinesName = p.Airlines.CnShortName,
                AircraftTypeName = p.AircraftType.Name,
                Regional = p.AircraftType.AircraftCategory.Regional,
                //PlanHistories = operationPlans.Where(o=>o.PlanAircraftId==p.Id).Select(q => new PlanHistoryDTO
                //{
                //    Id = q.Id,
                //    CarryingCapacity = q.CarryingCapacity,
                //    SeatingCapacity = q.SeatingCapacity,
                //    PerformAnnualId = q.PerformAnnualId,
                //    PerformMonth = q.PerformMonth,
                //    IsSubmit = q.IsSubmit,
                //    IsValid = q.IsValid,
                //    Note = q.Note,
                //    ActionCategoryId = q.ActionCategoryId,
                //    ActionType = q.ActionCategory.ActionType,
                //    ActionName = q.ActionCategory.ActionName,
                //    TargetCategoryId = q.TargetCategoryId,
                //    TargetType = q.TargetCategory.ActionName,
                //    AircraftTypeId = q.AircraftTypeId,
                //    AircraftTypeName = q.AircraftType.Name,
                //    Regional = q.AircraftType.AircraftCategory.Regional,
                //    Category = q.AircraftType.AircraftCategory.Category,
                //    AirlinesId = q.AirlinesId,
                //    AirlinesName = q.Airlines.CnShortName,
                //    NeedRequest = q.ActionCategory.NeedRequest,
                //    Year = q.PerformAnnual.Year,
                //    ApprovalHistoryId = q.ApprovalHistoryId,

                //    PlanAircraftId = q.PlanAircraftId,
                //    AircraftId = q.PlanAircraft.AircraftId,
                //    RegNumber = q.PlanAircraft.Aircraft.RegNumber,
                //    AircraftImportCategoryId = q.PlanAircraft.Aircraft.ImportCategoryId,
                //    ManageStatus = (int)p.Status,
                //    PaIsLock = q.PlanAircraft.IsLock,

                //    RelatedGuid = q.OperationHistoryId,
                //    RelatedEndDate = q.OperationHistory.EndDate,
                //    RelatedStatus = q.OperationHistory == null ? 0 : (int)q.OperationHistory.Status,
                //    PlanId = q.PlanId,
                //    PlanType = 1,//1表示运营计划
                //})
                //.Union(changePlans.Where(o => o.PlanAircraftId == p.Id).Select(q => new PlanHistoryDTO
                //{
                //    Id = q.Id,
                //    CarryingCapacity = q.CarryingCapacity,
                //    SeatingCapacity = q.SeatingCapacity,
                //    PerformAnnualId = q.PerformAnnualId,
                //    PerformMonth = q.PerformMonth,
                //    IsSubmit = q.IsSubmit,
                //    IsValid = q.IsValid,
                //    Note = q.Note,
                //    ActionCategoryId = q.ActionCategoryId,
                //    ActionType = q.ActionCategory.ActionType,
                //    ActionName = q.ActionCategory.ActionName,
                //    TargetCategoryId = q.TargetCategoryId,
                //    TargetType = q.TargetCategory.ActionName,
                //    AircraftTypeId = q.AircraftTypeId,
                //    AircraftTypeName = q.AircraftType.Name,
                //    Regional = q.AircraftType.AircraftCategory.Regional,
                //    Category = q.AircraftType.AircraftCategory.Category,
                //    AirlinesId = q.AirlinesId,
                //    AirlinesName = q.Airlines.CnShortName,
                //    NeedRequest = q.ActionCategory.NeedRequest,
                //    Year = q.PerformAnnual.Year,
                //    ApprovalHistoryId = q.ApprovalHistoryId,

                //    PlanAircraftId = q.PlanAircraftId,
                //    AircraftId = q.PlanAircraft.AircraftId,
                //    RegNumber = q.PlanAircraft.Aircraft.RegNumber,
                //    AircraftImportCategoryId = q.PlanAircraft.Aircraft.ImportCategoryId,
                //    ManageStatus = (int)p.Status,
                //    PaIsLock = q.PlanAircraft.IsLock,

                //    RelatedGuid = q.AircraftBusinessId,
                //    RelatedEndDate = q.AircraftBusiness.EndDate,
                //    RelatedStatus = q.AircraftBusiness == null ? 0 : (int)q.AircraftBusiness.Status,
                //    PlanId = q.PlanId,
                //    PlanType = 2,//2表示变更计划
                //})
                //).ToList(),
                //ApprovalHistories = approvalHistories.Where(o=>o.PlanAircraftId==p.Id).Select(q=>new ApprovalHistoryDTO
                //{
                //    Id = q.Id,
                //    IsApproved = q.IsApproved,
                //    RequestId = q.RequestId,
                //    PlanAircraftId = q.PlanAircraftId,
                //}).ToList(),
            });
        }
    }
}