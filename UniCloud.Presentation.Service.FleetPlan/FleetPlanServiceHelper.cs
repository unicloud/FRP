#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/2 15:29:17
// 文件名：FleetPlanServiceHelper
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition.ReflectionModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Data;
using Telerik.Windows.Controls;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.Service.FleetPlan
{
    public class FleetPlanServiceHelper : IDisposable
    {
        public void Dispose()
        {
        }

        #region 计划

        /// <summary>
        /// 创建新飞机
        /// </summary>
        /// <param name="planDetail">计划明细</param>
        /// <param name="service"></param>
        private static AircraftDTO CreateAircraft(PlanHistoryDTO planDetail, IFleetPlanService service)
        {
            var aircraft = new AircraftDTO
            {
                AircraftId = Guid.NewGuid(),
                AircraftTypeId = planDetail.AircraftTypeId,
                AircraftTypeName = planDetail.AircraftTypeName,
                AirlinesId = planDetail.AirlinesId,
                AirlinesName = planDetail.AirlinesName,
                CreateDate = DateTime.Now,
                IsOperation = true,
                SeatingCapacity = planDetail.SeatingCapacity,
                CarryingCapacity = planDetail.CarryingCapacity,
            };
            planDetail.AircraftId = aircraft.AircraftId;
            CreateOperationHistory(planDetail, ref aircraft, service);
            CreateAircraftBusiness(planDetail, ref aircraft, service);
            return aircraft;
        }

        /// <summary>
        /// 创建新的运营历史
        /// </summary>
        /// <param name="planDetail">计划明细</param>
        /// <param name="aircraft">飞机</param>
        /// <param name="service"></param>
        private static void CreateOperationHistory(PlanHistoryDTO planDetail, ref AircraftDTO aircraft, IFleetPlanService service)
        {
            var id = planDetail.ApprovalHistoryId == null ? Guid.Empty : Guid.Parse(planDetail.ActionCategoryId.ToString());
            var operationHistory = new OperationHistoryDTO
            {
                OperationHistoryId = id,
                AirlinesId = planDetail.AirlinesId,
                AirlinesName = planDetail.AirlinesName,
                AircraftId = aircraft.AircraftId,
                ImportCategoryId = aircraft.ImportCategoryId,
                ImportActionType = aircraft.ImportCategoryName,
                Status = (int)OperationStatus.草稿,
            };
            if (planDetail.PlanType == 1) planDetail.RelatedGuid = operationHistory.OperationHistoryId;
            aircraft.OperationHistories.Add(operationHistory);
            // 更改运营历史状态
            operationHistory.Status = (int)OperationStatus.草稿;
        }

        /// <summary>
        /// 创建新的商业数据历史
        /// </summary>
        /// <param name="aircraft">飞机</param>
        /// <param name="service"></param>
        private static void CreateAircraftBusiness(PlanHistoryDTO planDetail, ref AircraftDTO aircraft, IFleetPlanService service)
        {
            var aircraftBusiness = new AircraftBusinessDTO
            {
                AircraftBusinessId = Guid.NewGuid(),
                AircraftId = aircraft.AircraftId,
                AircraftTypeId = aircraft.AircraftTypeId,
                ImportCategoryId = aircraft.ImportCategoryId,
                SeatingCapacity = aircraft.SeatingCapacity,
                CarryingCapacity = aircraft.CarryingCapacity,
                Status = (int)OperationStatus.草稿,
            };

            if (planDetail.PlanType == 2) planDetail.RelatedGuid = aircraftBusiness.AircraftBusinessId;
            aircraft.AircraftBusinesses.Add(aircraftBusiness);
            // 更改商业数据历史状态
            aircraftBusiness.Status = (int)OperationStatus.草稿;
        }


        /// <summary>
        /// 创建新年度计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <param name="allPlanHistories"></param>
        /// <param name="newAnnual"></param>
        /// <returns></returns>
        internal PlanDTO CreateNewYearPlan(PlanDTO lastPlan, QueryableDataServiceCollectionView<PlanHistoryDTO> allPlanHistories, AnnualDTO newAnnual)
        {
            var title = newAnnual.Year + "年度运力规划";
            // 从当前计划复制生成新年度计划
            var newPlan = new PlanDTO
            {
                Id = Guid.NewGuid(),
                Title = title,
                CreateDate = DateTime.Now,
                AnnualId = newAnnual.Id,
                Year = newAnnual.Year,
                AirlinesId = lastPlan.AirlinesId,
                AirlinesName = lastPlan.AirlinesName,
                VersionNumber = 1,
                Status = (int)PlanStatus.草稿,
                IsValid = false,
                IsFinished = false,
                PublishStatus = (int)PlanPublishStatus.待发布,
            };
            // 获取需要滚动到下一年度的计划明细项(可再次申请的计划明细不滚动到下一年度，并将对应的计划飞机置为预备状态)
            var lastPhs = allPlanHistories.Where(p => p.PlanId == lastPlan.Id).ToList();
            var planHistories = (lastPhs == null) ? null : lastPhs.Where(o => o.PlanAircraftId == null ||
                                           (o.PlanAircraftId != null && (o.ManageStatus != (int)ManageStatus.预备
                                                                       && o.ManageStatus != (int)ManageStatus.运营
                                                                       && o.ManageStatus != (int)ManageStatus.退役))).ToList();

            var resultphs = new List<PlanHistoryDTO>();
            if (planHistories != null)
                // 从当前计划往新版本计划复制运营计划
                resultphs = planHistories.Where(ph => ph.RelatedGuid == null || ph.RelatedEndDate == null)
                      .Select(q => new PlanHistoryDTO
                      {
                          PlanId = newPlan.Id,
                          Id = Guid.NewGuid(),
                          ActionCategoryId = q.ActionCategoryId,
                          AircraftTypeId = q.AircraftTypeId,
                          AirlinesId = q.AirlinesId,
                          CarryingCapacity = q.CarryingCapacity,
                          SeatingCapacity = q.SeatingCapacity,
                          RelatedGuid = q.RelatedGuid,
                          RelatedEndDate = q.RelatedEndDate,
                          IsSubmit = q.IsSubmit,
                          IsValid = q.IsValid,
                          Note = q.Note,
                          PerformAnnualId = q.PerformAnnualId,
                          PerformMonth = q.PerformMonth,
                          PlanAircraftId = q.PlanAircraftId,
                          PlanType = q.PlanType,
                          TargetCategoryId = q.TargetCategoryId,
                          AirlinesName = q.AirlinesName,
                          Regional = q.Regional,
                          AircraftTypeName = q.AircraftTypeName,
                          ActionType = q.ActionType,
                          ActionName = q.ActionName,
                          TargetType = q.TargetType,
                          Year = q.Year,
                          ManageStatus = q.ManageStatus,
                      }).ToList();
            resultphs.ForEach(allPlanHistories.AddNew);
            return newPlan;
        }

        /// <summary>
        /// 创建当前新版本计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <returns></returns>
        internal PlanDTO CreateNewVersionPlan(PlanDTO lastPlan, QueryableDataServiceCollectionView<PlanHistoryDTO> allPlanHistories)
        {
            var title = lastPlan.Year + "年度运力规划";
            // 从当前计划复制生成新版本计划
            var newPlan = new PlanDTO
            {
                Id = Guid.NewGuid(),
                Title = title,
                CreateDate = DateTime.Now,
                AnnualId = lastPlan.AnnualId,
                Year = lastPlan.Year,
                AirlinesId = lastPlan.AirlinesId,
                AirlinesName = lastPlan.AirlinesName,
                VersionNumber = lastPlan.VersionNumber + 1,
                Status = 0,
                IsValid = false,
                IsFinished = false,
                PublishStatus = 0,
            };
            // 从当前计划往新版本计划复制计划明细
            var lastPhs = allPlanHistories.Where(p => p.PlanId == lastPlan.Id).ToList();
            var resultphs = new List<PlanHistoryDTO>();
            resultphs = lastPhs.Select(q => new PlanHistoryDTO
            {
                PlanId = newPlan.Id,
                Id = Guid.NewGuid(),
                ActionCategoryId = q.ActionCategoryId,
                AircraftTypeId = q.AircraftTypeId,
                AirlinesId = q.AirlinesId,
                CarryingCapacity = q.CarryingCapacity,
                SeatingCapacity = q.SeatingCapacity,
                RelatedGuid = q.RelatedGuid,
                RelatedEndDate = q.RelatedEndDate,
                IsSubmit = q.IsSubmit,
                IsValid = q.IsValid,
                Note = q.Note,
                PerformAnnualId = q.PerformAnnualId,
                PerformMonth = q.PerformMonth,
                PlanAircraftId = q.PlanAircraftId,
                PlanType = q.PlanType,
                TargetCategoryId = q.TargetCategoryId,
                AirlinesName = q.AirlinesName,
                CaacAircraftTypeName = q.CaacAircraftTypeName,
                Regional = q.Regional,
                AircraftTypeName = q.AircraftTypeName,
                ActionType = q.ActionType,
                ActionName = q.ActionName,
                TargetType = q.TargetType,
                Year = q.Year,
                ManageStatus = q.ManageStatus,
                CanRequest = q.CanRequest,
                CanDeliver = q.CanDeliver,
            }).ToList();

            resultphs.ForEach(allPlanHistories.AddNew);
            return newPlan;
        }

        /// <summary>
        /// 创建运营计划明细
        /// 同一架计划飞机在一份计划中的明细项不得超过两条，且两条不得为同种操作类型（含运营计划与变更计划）
        /// </summary>
        /// <param name="plan"></param>
        /// <param name="planAircraft"></param>
        /// <param name="aircraft"></param>
        /// <param name="actionType"></param>
        /// <param name="planType"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        internal PlanHistoryDTO CreatePlanHistory(PlanDTO plan, QueryableDataServiceCollectionView<PlanHistoryDTO> allPlanHistories, ref PlanAircraftDTO planAircraft, AircraftDTO aircraft, string actionType, int planType, IFleetPlanService service)
        {
            if (plan == null) return null;
            // 创建新的计划历史
            var planDetail = new PlanHistoryDTO
            {
                Id = Guid.NewGuid(),
                PlanId = plan.Id,
                AirlinesId = plan.AirlinesId,
                AirlinesName = plan.AirlinesName,
                PerformAnnualId = plan.AnnualId,
                PerformMonth = 1,
                PlanType = planType,
                ActionType = actionType,
                ManageStatus = (int)ManageStatus.计划,
                CanRequest = (int)CanRequest.未报计划,
                CanDeliver = (int)CanDeliver.未申请,
            };

            planDetail.ActionCategories = service.GetActionCategoriesForPlanHistory(planDetail);
            planDetail.AircraftCategories = service.GetAircraftCategoriesForPlanHistory(planDetail);
            planDetail.AircraftTypes = service.GetAircraftTypesForPlanHistory(planDetail);

            // 1、计划飞机为空
            if (planAircraft == null)
            {
                planAircraft = new PlanAircraftDTO
                {
                    Id = Guid.NewGuid(),
                    AirlinesId = plan.AirlinesId,
                    AirlinesName = plan.AirlinesName,
                    Status = (int)ManageStatus.计划,
                    IsOwn = true
                };
                planDetail.PlanAircraftId = planAircraft.Id;
            }
            // 2、计划飞机非空
            else
            {
                // 获取计划飞机的所有计划明细集合
                Guid planAcId = planAircraft.Id;
                var planAcPhs = allPlanHistories.Where(p => p.PlanAircraftId == planAcId).ToList();
                // 获取计划飞机在当前计划中的计划明细集合
                PlanAircraftDTO dto = planAircraft;
                var currentPhs = allPlanHistories.Where(p => p.PlanId == plan.Id).ToList();
                var planDetails = currentPhs.Where(ph => ph.PlanAircraftId == dto.Id).ToList();
                // 2.1、不是针对现有飞机的计划明细
                if (planAircraft.AircraftId == null && aircraft == null)
                {
                    if (planAcPhs.Any())
                    {
                        // 获取计划飞机的最后一条计划明细，用于复制数据
                        var planHistory =
                            planAcPhs.OrderBy(ph => ph.Year)
                               .ThenBy(ph => ph.PerformMonth)
                               .LastOrDefault();
                        if (planHistory != null)
                        {
                            // 1、计划飞机在当前计划中没有明细项，（20140106补充）是预备状态的计划飞机
                            if (!planDetails.Any())
                            {
                                planDetail.AircraftTypeId = planAircraft.AircraftTypeId;
                                planDetail.ActionCategoryId = planHistory.ActionCategoryId;
                                planDetail.ActionType = planHistory.ActionType;
                                planDetail.TargetCategoryId = planHistory.TargetCategoryId;
                                planDetail.TargetType = planHistory.TargetType;
                                planDetail.SeatingCapacity = planHistory.SeatingCapacity;
                                planDetail.CarryingCapacity = planHistory.CarryingCapacity;
                                if (planHistory.NeedRequest)
                                {
                                    planDetail.CanRequest = (int)CanRequest.未报计划;
                                    planDetail.CanDeliver = (int)CanDeliver.未申请;
                                }
                                else
                                {
                                    planDetail.CanRequest = (int)CanRequest.无需申请;
                                    planDetail.CanDeliver = (int)CanDeliver.可交付;
                                }
                                planAircraft.Status = (int)ManageStatus.计划;
                            }
                            // 2、计划飞机在当前计划中已有明细项
                            else
                            {
                                planDetail.Regional = planAircraft.Regional;
                                planDetail.AircraftTypeName = planAircraft.AircraftTypeName;
                                planDetail.CaacAircraftTypeName = planAircraft.AircraftTypeName;
                                planDetail.AircraftTypeId = planAircraft.AircraftTypeId;
                                planDetail.SeatingCapacity = -planHistory.SeatingCapacity;
                                planDetail.CarryingCapacity = -planHistory.CarryingCapacity;
                                planDetail.CanRequest = (int)CanRequest.无需申请;
                                planDetail.CanDeliver = (int)CanDeliver.可交付;
                            }
                        }
                    }
                }
                // 2.2、是针对现有飞机的计划明细，退出或变更计划，无需改变计划飞机管理状态
                else
                {
                    if (planType == 1) //为退出计划
                    {
                        planDetail.Regional = aircraft.Regional;
                        planDetail.RegNumber = aircraft.RegNumber;
                        planDetail.AircraftTypeId = planAircraft.AircraftTypeId;
                        planDetail.AircraftTypeName = planAircraft.AircraftTypeName;
                        planDetail.CaacAircraftTypeName = planAircraft.AircraftTypeName;
                        planDetail.SeatingCapacity = -aircraft.SeatingCapacity;
                        planDetail.CarryingCapacity = -aircraft.CarryingCapacity;
                        planDetail.AircraftId = aircraft.AircraftId;
                        planDetail.CanRequest = (int)CanRequest.无需申请;
                        planDetail.CanDeliver = (int)CanDeliver.可交付;
                        planDetail.ManageStatus = (int) ManageStatus.运营;
                    }
                    else if (planType == 2) //为变更计划
                    {
                        // 获取飞机的当前商业数据，赋予新创建的变更计划明细
                        var abs = aircraft.AircraftBusinesses;
                        if (abs.Any())
                        {
                            var aircraftBusiness = abs.FirstOrDefault(a => a.EndDate == null);
                            if (aircraftBusiness != null)
                            {
                                planDetail.Regional = aircraft.Regional;
                                planDetail.RegNumber = aircraft.RegNumber;
                                planDetail.TargetCategoryId = aircraftBusiness.ImportCategoryId;
                                planDetail.SeatingCapacity = aircraftBusiness.SeatingCapacity;
                                planDetail.CarryingCapacity = aircraftBusiness.CarryingCapacity;
                                planDetail.AircraftId = aircraft.AircraftId;
                                planDetail.ManageStatus = (int)ManageStatus.运营;
                            }
                        }
                    }
                }
                planDetail.PlanAircraftId = planAircraft.Id;
            }
            return planDetail;
        }

        /// <summary>
        /// 完成计划项
        /// </summary>
        /// <param name="planDetail"></param>
        /// <param name="aircraft"></param>
        /// <param name="editAircraft"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        internal void CompletePlan(PlanHistoryDTO planDetail, AircraftDTO aircraft, ref AircraftDTO editAircraft, IFleetPlanService service)
        {
            OperationHistoryDTO operationHistory;
            if (planDetail == null)
            {
                throw new ArgumentNullException("planDetail");
            }
            var actionName = planDetail.ActionName;
            if (actionName == null)
            {
                return;
            }
            // 根据引进方式调用不同的操作
            switch (actionName)
            {
                case "购买":
                    // 创建新飞机
                    editAircraft = CreateAircraft(planDetail, service);
                    break;
                case "融资租赁":
                    // 创建新飞机
                    editAircraft = CreateAircraft(planDetail, service);
                    break;
                case "经营租赁":
                    // 创建新飞机
                    editAircraft = CreateAircraft(planDetail, service);
                    break;
                case "湿租":
                    // 创建新飞机
                    editAircraft = CreateAircraft(planDetail, service);
                    break;
                case "经营租赁续租":
                    // 创建新运营历史
                    if (aircraft != null)
                    {
                        editAircraft = aircraft;
                        CreateOperationHistory(planDetail, ref editAircraft, service);
                    }
                    break;
                case "湿租续租":
                    // 创建新运营历史
                    if (aircraft != null)
                    {
                        editAircraft = aircraft;
                        CreateOperationHistory(planDetail, ref editAircraft, service);
                    }
                    break;
                case "出售":
                    // 更改运营历史状态
                    if (aircraft != null)
                    {
                        editAircraft = aircraft;
                        operationHistory = editAircraft.OperationHistories.LastOrDefault(oh => oh.EndDate == null);
                        if (operationHistory != null)
                        {
                            operationHistory.Status = (int)OperationStatus.草稿;
                            var actionCategoryDTO =
                                service.GetActionCategories(null)
                                    .SourceCollection.Cast<ActionCategoryDTO>()
                                    .FirstOrDefault(ac => ac.ActionName == "出售");
                            if (actionCategoryDTO != null)
                                operationHistory.ExportCategoryId = actionCategoryDTO.Id;

                            if (planDetail.PlanType == 1) planDetail.RelatedGuid = operationHistory.OperationHistoryId;
                        }
                    }
                    break;
                case "出租":
                    // 更改运营历史状态
                    if (aircraft != null)
                    {
                        editAircraft = aircraft;
                        operationHistory = editAircraft.OperationHistories.LastOrDefault(oh => oh.EndDate == null);
                        if (operationHistory != null)
                        {
                            operationHistory.Status = (int)OperationStatus.草稿;
                            var actionCategoryDTO =
                                service.GetActionCategories(null)
                                    .SourceCollection.Cast<ActionCategoryDTO>()
                                    .FirstOrDefault(ac => ac.ActionName == "出租");
                            if (actionCategoryDTO != null)
                                operationHistory.ExportCategoryId = actionCategoryDTO.Id;

                            if (planDetail.PlanType == 1) planDetail.RelatedGuid = operationHistory.OperationHistoryId;
                        }
                    }
                    break;
                case "退租":
                    // 更改运营历史状态
                    if (aircraft != null)
                    {
                        editAircraft = aircraft;
                        operationHistory = editAircraft.OperationHistories.LastOrDefault(oh => oh.EndDate == null);
                        if (operationHistory != null)
                        {
                            operationHistory.Status = (int)OperationStatus.草稿;
                            var actionCategoryDTO =
                                service.GetActionCategories(null)
                                    .SourceCollection.Cast<ActionCategoryDTO>()
                                    .FirstOrDefault(ac => ac.ActionName == "退租");
                            if (actionCategoryDTO != null)
                                operationHistory.ExportCategoryId = actionCategoryDTO.Id;

                            if (planDetail.PlanType == 1) planDetail.RelatedGuid = operationHistory.OperationHistoryId;
                        }
                    }
                    break;
                case "退役":
                    // 更改运营历史状态
                    if (aircraft != null)
                    {
                        editAircraft = aircraft;
                        operationHistory = editAircraft.OperationHistories.LastOrDefault(oh => oh.EndDate == null);
                        if (operationHistory != null)
                        {
                            operationHistory.Status = (int)OperationStatus.草稿;
                            var actionCategoryDTO =
                                service.GetActionCategories(null)
                                    .SourceCollection.Cast<ActionCategoryDTO>()
                                    .FirstOrDefault(ac => ac.ActionName == "退役");
                            if (actionCategoryDTO != null)
                                operationHistory.ExportCategoryId = actionCategoryDTO.Id;

                            if (planDetail.PlanType == 1) planDetail.RelatedGuid = operationHistory.OperationHistoryId;
                        }
                    }
                    break;
                case "货改客":
                    // 创建商业数据历史
                    if (aircraft != null)
                    {
                        editAircraft = aircraft;
                        CreateAircraftBusiness(planDetail, ref editAircraft, service);
                    }
                    break;
                case "客改货":
                    // 创建商业数据历史
                    if (aircraft != null)
                    {
                        editAircraft = aircraft;
                        CreateAircraftBusiness(planDetail, ref editAircraft, service);
                    }
                    break;
                case "售后经营租赁":
                    // 创建商业数据历史
                    if (aircraft != null)
                    {
                        editAircraft = aircraft;
                        CreateAircraftBusiness(planDetail, ref editAircraft, service);
                    }
                    break;
                case "售后融资租赁":
                    // 创建商业数据历史
                    if (aircraft != null)
                    {
                        editAircraft = aircraft;
                        CreateAircraftBusiness(planDetail, ref editAircraft, service);
                    }
                    break;
                case "一般改装":
                    // 创建商业数据历史
                    if (aircraft != null)
                    {
                        editAircraft = aircraft;
                        CreateAircraftBusiness(planDetail, ref editAircraft, service);
                    }
                    break;
                case "租转购":
                    // 创建商业数据历史
                    if (aircraft != null)
                    {
                        editAircraft = aircraft;
                        CreateAircraftBusiness(planDetail, ref editAircraft, service);
                    }
                    break;
            }
            //更改计划飞机状态
            planDetail.ManageStatus = (int)ManageStatus.运营; //TODO 修改计划飞机的状态（前台或后台处理）
        }

        /// <summary>
        /// 获取所有有效的计划
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        internal ObservableCollection<PlanDTO> GetAllValidPlan(IFleetPlanService service)
        {
            //var validPlans = service.EntityContainer.GetEntitySet<Plan>().Where(p => p.IsValid);
            //return validPlans;
            return null;
        }

        #endregion

        #region 批文管理
        /// <summary>
        /// 创建新申请明细
        /// </summary>
        /// <param name="request"></param>
        /// <param name="planHistory"></param>
        /// <returns></returns>
        internal ApprovalHistoryDTO CreateNewRequestDetail(RequestDTO request, PlanHistoryDTO planHistory)
        {
            // 创建新的申请明细
            var requestDetail = new ApprovalHistoryDTO
            {
                Id = Guid.NewGuid(),
                RequestId = request.Id,
                ImportCategoryId = planHistory.TargetCategoryId,
                AirlinesId = planHistory.AirlinesId,
                RequestDeliverAnnualId = planHistory.PerformAnnualId,
                RequestDeliverMonth = planHistory.PerformMonth,
                SeatingCapacity = planHistory.SeatingCapacity,
                CarryingCapacity = planHistory.CarryingCapacity,
            };
            if (planHistory.PlanAircraftId != null)
                requestDetail.PlanAircraftId = Guid.Parse(planHistory.PlanAircraftId.ToString());
            // 把申请明细赋给关联的计划明细
            planHistory.ApprovalHistoryId = requestDetail.Id;
            // 计划飞机管理状态修改为申请:TODO
            //requestDetail.PlanAircraftId.Status = (int)ManageStatus.Request;
            return requestDetail;
        }

        /// <summary>
        /// 移除申请明细
        /// </summary>
        /// <param name="requestDetail"></param>
        /// <returns></returns>
        internal void RemoveRequestDetail(ApprovalHistoryDTO requestDetail)
        {
            //// 获取相关的计划明细
            //var planHistories =
            //    service.EntityContainer.GetEntitySet<PlanHistoryDTO>()
            //           .Where(ph => ph.ApprovalHistoryID == requestDetail.ApprovalHistoryID).ToList();
            //// 相关计划明细的申请明细置为空
            //planHistories.ForEach(ph => ph.ApprovalHistory = null);
            //// 相关计划飞机的管理状态改为计划
            //requestDetail.PlanAircraft.Status = (int)ManageStatus.Plan;
            //// 移除申请明细
            //service.EntityContainer.GetEntitySet<ApprovalHistory>().Remove(requestDetail);
        }

        #endregion

        #region 运营管理

        /// <summary>
        /// 创建新的所有权历史记录
        /// </summary>
        /// <param name="aircraft"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        internal OwnershipHistoryDTO CreateNewOwnership(AircraftDTO aircraft, IFleetPlanService service)
        {
            //var ownership = new OwnershipHistory
            //{
            //    OwnershipHistoryID = Guid.NewGuid(),
            //    Aircraft = aircraft,
            //    Owner = service.CurrentAirlines,
            //    StartDate = DateTime.Now,
            //    Status = (int)OpStatus.Draft,
            //};
            //service.EntityContainer.GetEntitySet<OwnershipHistory>().Add(ownership);
            //return ownership;
            return null;
        }

        /// <summary>
        /// 移除所有权历史记录
        /// </summary>
        /// <param name="ownership"></param>
        /// <param name="service"></param>
        internal void RemoveOwnership(OwnershipHistoryDTO ownership, IFleetPlanService service)
        {
            //var ownweships =
            //    service.EntityContainer.GetEntitySet<OwnershipHistory>()
            //           .Where(os => os.Aircraft == ownership.Aircraft)
            //           .OrderBy(os => os.StartDate)
            //           .ToList();
            //var count = ownweships.Count;
            //// 所有权历史至少要保留一条
            //if (count > 1)
            //{
            //    service.EntityContainer.GetEntitySet<OwnershipHistory>().Remove(ownership);
            //    // 修改之前记录的结束日期
            //    ownweships[count - 2].EndDate = null;
            //}
        }
        #endregion

    }
}
