﻿#region 版本信息

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

using System;
using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;
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
            var planHistories = _unitOfWork.CreateSet<PlanHistory>();
            var actionCategories = _unitOfWork.CreateSet<ActionCategory>();
            var aircraftCategories = _unitOfWork.CreateSet<AircraftCategory>();
            var aircraftTypes = _unitOfWork.CreateSet<AircraftType>();

            var result = query.ApplyTo(_unitOfWork.CreateSet<Plan>()).Select(p => new PlanDTO
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
                //PlanHistories = planHistories.OfType<OperationPlan>().Select(q => new PlanHistoryDTO
                //                {
                //                    Id = q.Id,
                //                    CarryingCapacity = q.CarryingCapacity,
                //                    SeatingCapacity = q.SeatingCapacity,
                //                    PerformAnnualId = q.PerformAnnualId,
                //                    PerformMonth = q.PerformMonth,
                //                    IsSubmit = q.IsSubmit,
                //                    IsValid = q.IsValid,
                //                    Note = q.Note,
                //                    ActionCategoryId = q.ActionCategoryId,
                //                    ActionType = q.ActionCategory.ActionType,
                //                    ActionName = q.ActionCategory.ActionName,
                //                    TargetCategoryId = q.TargetCategoryId,
                //                    TargetType = q.TargetCategory.ActionName,
                //                    AircraftTypeId = q.AircraftTypeId,
                //                    AircraftTypeName = q.AircraftType.Name,
                //                    CaacAircraftTypeName = q.AircraftType.CaacAircraftType.Name,
                //                    Regional = q.AircraftType.AircraftCategory.Regional,
                //                    Category = q.AircraftType.AircraftCategory.Category,
                //                    AirlinesId = q.AirlinesId,
                //                    AirlinesName = q.Airlines.CnShortName,
                //                    NeedRequest = q.ActionCategory.NeedRequest,
                //                    Year = q.PerformAnnual.Year,

                //                    ApprovalHistoryId = q.ApprovalHistoryId,
                //                    //IsApproved = q.ApprovalHistory.IsApproved,TODO

                //                    PlanAircraftId = q.PlanAircraftId,
                //                    AircraftId = q.PlanAircraft.AircraftId,
                //                    RegNumber = q.PlanAircraft.Aircraft.RegNumber,
                //                    AircraftImportCategoryId = q.PlanAircraft.Aircraft.ImportCategoryId,
                //                    ManageStatus = q.PlanAircraft == null ? 0 : (int)q.PlanAircraft.Status,
                //                    PaIsLock = q.PlanAircraft.IsLock,

                //                    RelatedGuid = q.OperationHistoryId,
                //                    RelatedEndDate = q.OperationHistory.EndDate,
                //                    RelatedStatus = q.OperationHistory == null ? 0 : (int)q.OperationHistory.Status,
                //                    PlanId = q.PlanId,
                //                    PlanType = 1,//1表示运营计划
                //                    RelatedStartDate = q.OperationHistory.StartDate,
                //                    //ActionCategories = actionCategories.Select(r => new ActionCateDTO
                //                    //{
                //                    //    Id = r.Id,
                //                    //    ActionName = r.ActionName,
                //                    //    ActionType = r.ActionType
                //                    //}).ToList(),
                //                    //AircraftCategories = aircraftCategories.Select(r => new AircraftCateDTO
                //                    //{
                //                    //    Id = r.Id,
                //                    //    Category = r.Category,
                //                    //    Regional = r.Regional,
                //                    //}).ToList(),
                //                    //AircraftTypes = aircraftTypes.Select(r => new AircraftTyDTO
                //                    //{
                //                    //    Id = r.Id,
                //                    //    Name = r.Name,
                //                    //    AircraftCategoryId = r.AircraftCategoryId,
                //                    //    Regional = r.AircraftCategory.Regional,
                //                    //}).ToList(),
                //                })
                //                .Union(planHistories.OfType<ChangePlan>().Select(q => new PlanHistoryDTO
                //                {
                //                    Id = q.Id,
                //                    CarryingCapacity = q.CarryingCapacity,
                //                    SeatingCapacity = q.SeatingCapacity,
                //                    PerformAnnualId = q.PerformAnnualId,
                //                    PerformMonth = q.PerformMonth,
                //                    IsSubmit = q.IsSubmit,
                //                    IsValid = q.IsValid,
                //                    Note = q.Note,
                //                    ActionCategoryId = q.ActionCategoryId,
                //                    ActionType = q.ActionCategory.ActionType,
                //                    ActionName = q.ActionCategory.ActionName,
                //                    TargetCategoryId = q.TargetCategoryId,
                //                    TargetType = q.TargetCategory.ActionName,
                //                    AircraftTypeId = q.AircraftTypeId,
                //                    AircraftTypeName = q.AircraftType.Name,
                //                    CaacAircraftTypeName = q.AircraftType.CaacAircraftType.Name,
                //                    Regional = q.AircraftType.AircraftCategory.Regional,
                //                    Category = q.AircraftType.AircraftCategory.Category,
                //                    AirlinesId = q.AirlinesId,
                //                    AirlinesName = q.Airlines.CnShortName,
                //                    NeedRequest = q.ActionCategory.NeedRequest,
                //                    Year = q.PerformAnnual.Year,

                //                    ApprovalHistoryId = q.ApprovalHistoryId,
                //                    //IsApproved = q.ApprovalHistory.IsApproved,TODO

                //                    PlanAircraftId = q.PlanAircraftId,
                //                    AircraftId = q.PlanAircraft.AircraftId,
                //                    RegNumber = q.PlanAircraft.Aircraft.RegNumber,
                //                    AircraftImportCategoryId = q.PlanAircraft.Aircraft.ImportCategoryId,
                //                    ManageStatus = q.PlanAircraft == null ? 0 : (int)q.PlanAircraft.Status,
                //                    PaIsLock = q.PlanAircraft.IsLock,

                //                    RelatedGuid = q.AircraftBusinessId,
                //                    RelatedEndDate = q.AircraftBusiness.EndDate,
                //                    RelatedStatus = q.AircraftBusiness == null ? 0 : (int)q.AircraftBusiness.Status,
                //                    PlanId = q.PlanId,
                //                    PlanType = 2,//2表示变更计划
                //                    RelatedStartDate = q.AircraftBusiness.StartDate,
                //                    //ActionCategories = actionCategories.Select(r => new ActionCateDTO
                //                    //{
                //                    //    Id = r.Id,
                //                    //    ActionName = r.ActionName,
                //                    //    ActionType = r.ActionType
                //                    //}).ToList(),
                //                    //AircraftCategories = aircraftCategories.Select(r => new AircraftCateDTO
                //                    //{
                //                    //    Id = r.Id,
                //                    //    Category = r.Category,
                //                    //    Regional = r.Regional,
                //                    //}).ToList(),
                //                    //AircraftTypes = aircraftTypes.Select(r => new AircraftTyDTO
                //                    //{
                //                    //    Id = r.Id,
                //                    //    Name = r.Name,
                //                    //    AircraftCategoryId = r.AircraftCategoryId,
                //                    //    Regional = r.AircraftCategory.Regional,
                //                    //}).ToList(),
                //                })
                //                ).ToList(),
            });
            var a = result.ToList();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="planHistoryId"></param>
        /// <param name="approvalHistoryId"></param>
        /// <param name="planType"></param>
        /// <param name="relatedGuid"></param>
        /// <returns></returns>
        public PerformPlan PerformPlanQuery(string planHistoryId, string approvalHistoryId, int planType, string relatedGuid)
        {
            var dbAirline = _unitOfWork.CreateSet<Airlines>();
            var dbImportCategory = _unitOfWork.CreateSet<ActionCategory>();
            var dbAnnaul = _unitOfWork.CreateSet<Annual>();
            var dbPlanAircraft = _unitOfWork.CreateSet<PlanAircraft>();

            var performPlan = new PerformPlan
            {
                Id = Guid.NewGuid(),
            };
            if (!string.IsNullOrEmpty(approvalHistoryId))
            {
                var appId = Guid.Parse(approvalHistoryId);
                performPlan.ApprovalHistory =
                    _unitOfWork.CreateSet<ApprovalHistory>()
                        .Where(p => p.Id == appId)
                        .Select(c => new ApprovalHistoryDTO
                        {
                            Id = c.Id,
                            IsApproved = c.IsApproved,
                            SeatingCapacity = c.SeatingCapacity,
                            CarryingCapacity = c.CarryingCapacity,
                            RequestDeliverMonth = c.RequestDeliverMonth,
                            Note = c.Note,
                            RequestId = c.RequestId,
                            PlanAircraftId = c.PlanAircraftId,
                            ImportCategoryId = c.ImportCategoryId,
                            AircraftRegional = c.PlanAircraft.AircraftType.AircraftCategory.Regional,
                            AircraftType = c.PlanAircraft.AircraftType.Name,
                            ImportCategoryName =
                                dbImportCategory.FirstOrDefault(a => a.Id == c.ImportCategoryId).ActionType + "-"
                                + dbImportCategory.FirstOrDefault(a => a.Id == c.ImportCategoryId).ActionName,
                            RequestDeliverAnnualId = c.RequestDeliverAnnualId,
                            RequestDeliverAnnualName =
                                dbAnnaul.FirstOrDefault(a => a.Id == c.RequestDeliverAnnualId).Year,
                            AirlinesId = c.AirlinesId,
                            AirlineName = dbAirline.FirstOrDefault(a => a.Id == c.AirlinesId).CnShortName,
                            PlanAircraftStatus =
                                (int)dbPlanAircraft.FirstOrDefault(a => a.Id == c.PlanAircraftId).Status,
                        }).FirstOrDefault();
            }
            if (!string.IsNullOrEmpty(relatedGuid))
            {
                var relatedId = Guid.Parse(relatedGuid);
                if (planType == 1)
                {
                    performPlan.OperationHistory =
                        _unitOfWork.CreateSet<OperationHistory>().Where(p => p.Id == relatedId).Select(q => new OperationHistoryDTO
                        {
                            OperationHistoryId = q.Id,
                            RegNumber = q.RegNumber,
                            TechReceiptDate = q.TechReceiptDate,
                            ReceiptDate = q.ReceiptDate,
                            StartDate = q.StartDate,
                            StopDate = q.StopDate,
                            TechDeliveryDate = q.TechDeliveryDate,
                            EndDate = q.EndDate,
                            OnHireDate = q.OnHireDate,
                            Note = q.Note,
                            AircraftId = q.AircraftId,
                            AirlinesId = q.AirlinesId,
                            AirlinesName = q.Airlines.CnName,
                            ImportCategoryId = q.ImportCategoryId,
                            ImportActionType = q.ImportCategory.ActionType,
                            ImportActionName = q.ImportCategory.ActionName,
                            ExportCategoryId = q.ExportCategoryId,
                            ExportCategoryName = q.ExportCategory.ActionName,
                            Status = (int)q.Status,
                        }).FirstOrDefault();
                }
                else
                {
                    performPlan.AircraftBusiness =
                        _unitOfWork.CreateSet<AircraftBusiness>().Where(p => p.Id == relatedId).Select(q => new AircraftBusinessDTO
                        {
                            AircraftBusinessId = q.Id,
                            SeatingCapacity = q.SeatingCapacity,
                            CarryingCapacity = q.CarryingCapacity,
                            StartDate = q.StartDate,
                            EndDate = q.EndDate,
                            Status = (int)q.Status,
                            AircraftId = q.AircraftId,
                            AircraftTypeId = q.AircraftTypeId,
                            AircraftTypeName = q.AircraftType.Name,
                            Regional = q.AircraftType.AircraftCategory.Regional,
                            Category = q.AircraftType.AircraftCategory.Category,
                            ImportCategoryId = q.ImportCategoryId,
                            ImportCategoryName = q.ImportCategory.ActionName,
                        }).FirstOrDefault();
                }
            }

            return performPlan;
        }
    }
}