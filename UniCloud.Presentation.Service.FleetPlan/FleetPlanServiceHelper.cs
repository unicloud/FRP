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
        private static void CreateAircraft(PlanHistoryDTO planDetail, IFleetPlanService service)
        {
            //var aircraft = new AircraftDTO
            //{
            //    AircraftId = Guid.NewGuid(),
            //    AircraftTypeId = planDetail.PlanAircraftId.AircraftType,
            //    AirlinesId = planDetail.AirlinesId,
            //    CreateDate = DateTime.Now,
            //    IsOperation = true,
            //    SeatingCapacity = planDetail.SeatingCapacity,
            //    CarryingCapacity = planDetail.CarryingCapacity,
            //};
            //service.EntityContainer.GetEntitySet<Aircraft>().Add(aircraft);
            //CreateOperationHistory(planDetail, aircraft, service);
            //CreateAircraftBusiness(planDetail, aircraft, service);
        }

        /// <summary>
        /// 创建新的运营历史
        /// </summary>
        /// <param name="approvalHistory">批文明细</param>
        /// <param name="aircraft">飞机</param>
        /// <param name="service"></param>
        private static void CreateOperationHistory(PlanHistoryDTO planDetail, AircraftDTO aircraft, IFleetPlanService service)
        {
            //var operationHistory = new OperationHistory
            //{
            //    ApprovalHistory = planDetail.ApprovalHistory,
            //    Airlines = service.CurrentAirlines,
            //    Aircraft = aircraft,
            //    ImportCategory = planDetail.ApprovalHistory.ImportCategory,
            //    Status = (int)OpStatus.Draft,
            //};
            //if (planDetail is OperationPlan) (planDetail as OperationPlan).OperationHistory = operationHistory;
            //service.EntityContainer.GetEntitySet<OperationHistory>().Add(operationHistory);
            //// 更改运营历史状态
            //operationHistory.Status = (int)OpStatus.Draft;
        }

        /// <summary>
        /// 创建新的商业数据历史
        /// </summary>
        /// <param name="aircraft">飞机</param>
        /// <param name="service"></param>
        private static void CreateAircraftBusiness(PlanHistoryDTO planDetail, AircraftDTO aircraft, IFleetPlanService service)
        {
            //var aircraftBusiness = new AircraftBusiness
            //{
            //    AircraftBusinessID = Guid.NewGuid(),
            //    Aircraft = aircraft,
            //    AircraftType = aircraft.AircraftType,
            //    ImportCategory = aircraft.ImportCategory,
            //    SeatingCapacity = aircraft.SeatingCapacity,
            //    CarryingCapacity = aircraft.CarryingCapacity,
            //    Status = (int)OpStatus.Draft,
            //};

            //if (planDetail is ChangePlan) (planDetail as ChangePlan).AircraftBusiness = aircraftBusiness;
            //service.EntityContainer.GetEntitySet<AircraftBusiness>().Add(aircraftBusiness);
            //// 更改商业数据历史状态
            //aircraftBusiness.Status = (int)OpStatus.Draft;
        }


        /// <summary>
        /// 创建新年度计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <param name="newAnnual"></param>
        /// <param name="newYear"></param>
        /// <param name="curAirline"></param>
        /// <returns></returns>
        internal PlanDTO CreateNewYearPlan(PlanDTO lastPlan, Guid newAnnual, int newYear, AirlinesDTO curAirline)
        {
            var title = newYear + "年度机队资源规划";
            // 从当前计划复制生成新年度计划
            var newPlan = new PlanDTO
            {
                Id = Guid.NewGuid(),
                Title = title,
                CreateDate = DateTime.Now,
                AnnualId = newAnnual,
                Year = newYear,
                AirlinesId = curAirline.Id,
                AirlinesName = curAirline.CnShortName,
                VersionNumber = 1,
                Status = 1,
                IsValid = false,
                IsFinished = false,
                PublishStatus = 0,
            };
            // 获取需要滚动到下一年度的计划明细项
            var planHistories = (lastPlan == null || lastPlan.PlanHistories == null) ? null : lastPlan.PlanHistories.Where(o => o.PlanAircraftId == null ||
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
                          TargetType = q.TargetType,
                          Year = q.Year,
                          ManageStatus = q.ManageStatus,
                      }).ToList();
            resultphs.ForEach(ph => newPlan.PlanHistories.Add(ph));
            return newPlan;
        }

        /// <summary>
        /// 创建当前新版本计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <returns></returns>
        internal PlanDTO CreateNewVersionPlan(PlanDTO lastPlan)
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
            lastPlan.PlanHistories.Select(q => new PlanHistoryDTO
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
                TargetType = q.TargetType,
                Year = q.Year,
                ManageStatus = q.ManageStatus,
            }).ToList().ForEach(op => newPlan.PlanHistories.Add(op));

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
        internal PlanHistoryDTO CreatePlanHistory(PlanDTO plan, ref PlanAircraftDTO planAircraft, AircraftDTO aircraft, string actionType, int planType, IFleetPlanService service)
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
                ManageStatus = (int)ManageStatus.计划,
            };
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
                var phs = planAircraft.PlanHistories;
                // 获取计划飞机在当前计划中的计划明细集合
                PlanAircraftDTO dto = planAircraft;
                var planDetails = plan.PlanHistories.Where(ph => ph.PlanAircraftId == dto.Id).ToList();
                // 2.1、不是针对现有飞机的计划明细
                if (planAircraft.AircraftId == null && aircraft == null)
                {
                    if (phs.Any())
                    {
                        // 获取计划飞机的最后一条计划明细，用于复制数据
                        var planHistory =
                            phs.OrderBy(ph => ph.Year)
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
                                planAircraft.Status = (int)ManageStatus.计划;
                            }
                            // 2、计划飞机在当前计划中已有明细项
                            else
                            {
                                planDetail.AircraftTypeId = planAircraft.AircraftTypeId;
                                planDetail.SeatingCapacity = -planHistory.SeatingCapacity;
                                planDetail.CarryingCapacity = -planHistory.CarryingCapacity;
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
                        planDetail.SeatingCapacity = -aircraft.SeatingCapacity;
                        planDetail.CarryingCapacity = -aircraft.CarryingCapacity;
                        planDetail.AircraftId = aircraft.AircraftId;
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
                                planDetail.ActionCategoryId = aircraftBusiness.ImportCategoryId;
                                planDetail.TargetCategoryId = aircraftBusiness.ImportCategoryId;
                                planDetail.SeatingCapacity = aircraftBusiness.SeatingCapacity;
                                planDetail.CarryingCapacity = aircraftBusiness.CarryingCapacity;
                                planDetail.AircraftId = aircraft.AircraftId;
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
        /// <param name="service"></param>
        /// <returns></returns>
        internal AircraftDTO CompletePlan(PlanHistoryDTO planDetail, IFleetPlanService service)
        {
            AircraftDTO aircraft;
            //OperationHistoryDTO operationHistory;
            //if (planDetail == null)
            //{
            //    throw new ArgumentNullException("planDetail");
            //}
            //var actionName = planDetail.ActionName;
            //if (actionName == null)
            //{
            //    return null;
            //}
            //// 根据引进方式调用不同的操作
            //switch (actionName)
            //{
            //    case "购买":
            //        // 创建新飞机
            //        CreateAircraft(planDetail, service);
            //        break;
            //    case "融资租赁":
            //        // 创建新飞机
            //        CreateAircraft(planDetail, service);
            //        break;
            //    case "经营租赁":
            //        // 创建新飞机
            //        CreateAircraft(planDetail, service);
            //        break;
            //    case "湿租":
            //        // 创建新飞机
            //        CreateAircraft(planDetail, service);
            //        break;
            //    case "经营租赁续租":
            //        // 创建新运营历史
            //        CreateOperationHistory(planDetail, aircraftId, service);
            //        break;
            //    case "湿租续租":
            //        // 创建新运营历史
            //        CreateOperationHistory(planDetail, aircraftId, service);
            //        break;
            //    case "出售":
            //        // 更改运营历史状态
            //        aircraft = planDetail.PlanAircraft.Aircraft;
            //        operationHistory = aircraft.OperationHistories.LastOrDefault(oh => oh.EndDate == null);
            //        if (operationHistory != null)
            //        {
            //            operationHistory.Status = (int)OpStatus.Draft;
            //            operationHistory.ExportCategory =
            //                service.AllActionCategories.FirstOrDefault(ac => ac.ActionName == "出售");

            //            if (planDetail is OperationPlan) (planDetail as OperationPlan).OperationHistory = operationHistory;
            //        }
            //        break;
            //    case "出租":
            //        // 更改运营历史状态
            //        aircraft = planDetail.PlanAircraft.Aircraft;
            //        operationHistory = aircraft.OperationHistories.LastOrDefault(oh => oh.EndDate == null);
            //        if (operationHistory != null)
            //        {
            //            operationHistory.Status = (int)OpStatus.Draft;
            //            operationHistory.ExportCategory =
            //                service.AllActionCategories.FirstOrDefault(ac => ac.ActionName == "出租");

            //            if (planDetail is OperationPlan) (planDetail as OperationPlan).OperationHistory = operationHistory;
            //        }
            //        break;
            //    case "退租":
            //        // 更改运营历史状态
            //        aircraft = planDetail.PlanAircraft.Aircraft;
            //        operationHistory = aircraft.OperationHistories.LastOrDefault(oh => oh.EndDate == null);
            //        if (operationHistory != null)
            //        {
            //            operationHistory.Status = (int)OpStatus.Draft;
            //            operationHistory.ExportCategory =
            //                service.AllActionCategories.FirstOrDefault(ac => ac.ActionName == "退租");

            //            if (planDetail is OperationPlan) (planDetail as OperationPlan).OperationHistory = operationHistory;
            //        }
            //        break;
            //    case "退役":
            //        // 更改运营历史状态
            //        aircraft = planDetail.PlanAircraft.Aircraft;
            //        operationHistory = aircraft.OperationHistories.LastOrDefault(oh => oh.EndDate == null);
            //        if (operationHistory != null)
            //        {
            //            operationHistory.Status = (int)OpStatus.Draft;
            //            operationHistory.ExportCategory =
            //                service.AllActionCategories.FirstOrDefault(ac => ac.ActionName == "退役");

            //            if (planDetail is OperationPlan) (planDetail as OperationPlan).OperationHistory = operationHistory;
            //        }
            //        break;
            //    case "货改客":
            //        // 创建商业数据历史
            //        CreateAircraftBusiness(planDetail, planDetail.PlanAircraft.Aircraft, service);
            //        break;
            //    case "客改货":
            //        // 创建商业数据历史
            //        CreateAircraftBusiness(planDetail, planDetail.PlanAircraft.Aircraft, service);
            //        break;
            //    case "售后经营租赁":
            //        // 创建商业数据历史
            //        CreateAircraftBusiness(planDetail, planDetail.PlanAircraft.Aircraft, service);
            //        break;
            //    case "售后融资租赁":
            //        // 创建商业数据历史
            //        CreateAircraftBusiness(planDetail, planDetail.PlanAircraft.Aircraft, service);
            //        break;
            //    case "一般改装":
            //        // 创建商业数据历史
            //        CreateAircraftBusiness(planDetail, planDetail.PlanAircraft.Aircraft, service);
            //        break;
            //    case "租转购":
            //        // 创建商业数据历史
            //        CreateAircraftBusiness(planDetail, planDetail.PlanAircraft.Aircraft, service);
            //        break;
            //}
            //// 更改计划飞机状态
            //planDetail.PlanAircraft.Status = (int)ManageStatus.Operation;
            //// 刷新计划完成状态
            //RaisePropertyChanged(() => planDetail.CompleteStatus);

            //return planDetail.PlanAircraft.Aircraft;
            return null;
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
