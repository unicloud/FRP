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
        /// 创建新年度计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <param name="newAnnual"></param>
        /// <param name="newYear"></param>
        /// <param name="curAirline"></param>
        /// <returns></returns>
        internal PlanDTO CreateNewYearPlan(PlanDTO lastPlan,Guid newAnnual,int newYear,AirlinesDTO curAirline)
        {
            var title = newYear + "年度运力规划";
            // 从当前计划复制生成新年度计划
            var planNew = new PlanDTO
            {
                Id = Guid.NewGuid(),
                Title = title,
                CreateDate = DateTime.Now,
                AnnualId = newAnnual,
                Year = newYear,
                AirlinesId = curAirline.Id,
                AirlinesName = curAirline.CnName,
                VersionNumber = 1,
                Status = 0,
                IsValid = false,
                IsCurrentVersion = false,
                IsFinished = false,
                PublishStatus = 0,
            };
            // 获取需要滚动到下一年度的计划明细项
            var planHistories = (lastPlan == null || lastPlan.PlanHistories == null) ? null
                                        : lastPlan.PlanHistories.Where(o => o.PlanAircraftId == null ||
                                           (o.PlanAircraftId != null && (o.ManageStatus != 1
                                                                       && o.ManageStatus != 7
                                                                       && o.ManageStatus != 10)));
            if(planHistories!=null)
            // 从当前计划往新版本计划复制运营计划
            planHistories.Where(ph => ph.RelatedGuid == Guid.Empty && ph.RelatedEndDate == null)
                                        .Select(q => new PlanHistoryDTO
                                        {
                                            PlanId = planNew.Id,
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
                                            PlanType = 1,
                                            TargetCategoryId = q.TargetCategoryId,
                                            AirlinesName = q.AirlinesName,
                                            Regional = q.Regional,
                                            AircraftTypeName = q.AircraftTypeName,
                                            ActionType = q.ActionType,
                                            TargetType = q.TargetType,
                                            Year = q.Year,
                                            ManageStatus = q.ManageStatus,
                                        }).ToList().ForEach(ph => planNew.PlanHistories.Add(ph));
            return planNew;
        }

        /// <summary>
        /// 创建当前新版本计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <returns></returns>
        internal PlanDTO CreateNewVersionPlan(PlanDTO lastPlan)
        {

            //// 从当前计划复制生成新版本计划
            //var planNew = new PlanDTO
            //{
            //    Id = Guid.NewGuid(),
            //    Title = title,
            //    CreateDate = DateTime.Now,
            //    Annual = curPlan.Annual,
            //    Airlines = curPlan.Airlines,
            //    VersionNumber = curPlan.VersionNumber + 1,
            //    Status = (int)PlanStatus.Draft,
            //    PublishStatus = (int)PlanPublishStatus.Draft,
            //};
            //// 从当前计划往新版本计划复制运营计划
            //curPlan.PlanHistories.OfType<OperationPlan>().Select(op => new OperationPlan
            //{
            //    Plan = planNew,
            //    PlanHistoryID = Guid.NewGuid(),
            //    PlanAircraft = op.PlanAircraft,
            //    Airlines = op.Airlines,
            //    AircraftType = op.AircraftType,
            //    ApprovalHistory = op.ApprovalHistory,
            //    IsSubmit = op.IsSubmit,
            //    IsValid = op.IsValid,
            //    ActionCategory = op.ActionCategory,
            //    TargetCategory = op.TargetCategory,
            //    Annual = op.Annual,
            //    PerformMonth = op.PerformMonth,
            //    SeatingCapacity = op.SeatingCapacity,
            //    CarryingCapacity = op.CarryingCapacity,
            //    OperationHistory = op.OperationHistory,
            //}).ToList().ForEach(op => planNew.PlanHistories.Add(op));
            //// 从当前计划往新版本计划复制变更计划
            //curPlan.PlanHistories.OfType<ChangePlan>().Select(cp => new ChangePlan
            //{
            //    PlanID = planNew.PlanID,
            //    PlanHistoryID = Guid.NewGuid(),
            //    PlanAircraft = cp.PlanAircraft,
            //    Airlines = cp.Airlines,
            //    AircraftType = cp.AircraftType,
            //    ApprovalHistory = cp.ApprovalHistory,
            //    IsSubmit = cp.IsSubmit,
            //    IsValid = cp.IsValid,
            //    ActionCategory = cp.ActionCategory,
            //    TargetCategory = cp.TargetCategory,
            //    Annual = cp.Annual,
            //    PerformMonth = cp.PerformMonth,
            //    SeatingCapacity = cp.SeatingCapacity,
            //    CarryingCapacity = cp.CarryingCapacity,
            //    AircraftBusiness = cp.AircraftBusiness,
            //}).ToList().ForEach(cp => planNew.PlanHistories.Add(cp));

            //service.EntityContainer.GetEntitySet<Plan>().Add(planNew);
            //service.SetCurrentPlan();
            //return planNew;
            return null;
        }

        /// <summary>
        /// 创建运营计划明细
        /// 同一架计划飞机在一份计划中的明细项不得超过两条，且两条不得为同种操作类型（含运营计划与变更计划）
        /// </summary>
        /// <param name="plan"></param>
        /// <param name="planAircraft"></param>
        /// <param name="actionType"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        internal PlanHistoryDTO CreateOperationPlan(PlanDTO plan, PlanAircraftDTO planAircraft, string actionType, IFleetPlanService service)
        {
            //if (plan == null) return null;
            //// 创建新的计划历史
            //var planDetail = new OperationPlan
            //{
            //    PlanHistoryID = Guid.NewGuid(),
            //    Plan = plan,
            //    ActionType = actionType,
            //    Airlines = service.CurrentAirlines,
            //    Annual = service.CurrentAnnual,
            //    PerformMonth = 1,
            //};
            //// 1、计划飞机为空
            //if (planAircraft == null)
            //{
            //    // 创建新的计划飞机
            //    var pa = new PlanAircraft
            //    {
            //        PlanAircraftID = Guid.NewGuid(),
            //        Airlines = service.CurrentAirlines,
            //        Status = (int)ManageStatus.Plan,
            //        IsOwn = true
            //    };
            //    planDetail.PlanAircraft = pa;
            //}
            //// 2、计划飞机非空
            //else
            //{
            //    // 获取计划飞机的所有计划明细集合
            //    var phs = planAircraft.PlanHistories;
            //    // 获取计划飞机在当前计划中的计划明细集合
            //    var planDetails = phs.Where(ph => ph.Plan == plan).ToList();
            //    // 2.1、不是针对现有飞机的计划明细
            //    if (planAircraft.Aircraft == null)
            //    {
            //        if (phs.Any())
            //        {
            //            // 获取计划飞机的最后一条计划明细，用于复制数据
            //            var planHistory =
            //                phs.OrderBy(ph => ph.Annual.Year)
            //                   .ThenBy(ph => ph.Plan.VersionNumber)
            //                   .LastOrDefault();
            //            if (planHistory != null)
            //            {
            //                // 1、计划飞机在当前计划中没有明细项
            //                if (!planDetails.Any())
            //                {
            //                    planDetail.AircraftType = planAircraft.AircraftType;
            //                    planDetail.ActionCategory = planHistory.ActionCategory;
            //                    planDetail.TargetCategory = planHistory.TargetCategory;
            //                    planDetail.SeatingCapacity = planHistory.SeatingCapacity;
            //                    planDetail.CarryingCapacity = planHistory.CarryingCapacity;
            //                    planAircraft.Status = (int)ManageStatus.Plan;
            //                }
            //                // 2、计划飞机在当前计划中已有明细项
            //                else
            //                {
            //                    planDetail.AircraftType = planAircraft.AircraftType;
            //                    planDetail.SeatingCapacity = -planHistory.SeatingCapacity;
            //                    planDetail.CarryingCapacity = -planHistory.CarryingCapacity;
            //                }
            //            }
            //        }
            //    }
            //    // 2.2、是针对现有飞机的计划明细，肯定是退出计划，无需改变计划飞机管理状态
            //    else
            //    {
            //        planDetail.AircraftType = planAircraft.AircraftType;
            //        planDetail.SeatingCapacity = planAircraft.Aircraft.SeatingCapacity;
            //        planDetail.CarryingCapacity = planAircraft.Aircraft.CarryingCapacity;
            //    }
            //    planDetail.PlanAircraft = planAircraft;

            //return planDetail;
            return null;
        }

        /// <summary>
        /// 移除计划明细项
        /// </summary>
        /// <param name="planDetail"></param>
        /// <param name="service"></param>
        internal void RemovePlanDetail(PlanHistoryDTO planDetail, IFleetPlanService service)
        {
            //if (planDetail != null)
            //{
            //    // 获取计划飞机
            //    var planAircraft = planDetail.PlanAircraft;
            //    // 获取计划飞机的明细项集合
            //    var planAircraftHistories = planAircraft.PlanHistories;
            //    // 获取计划飞机在当前计划中的明细项集合
            //    var planDetails = planAircraft.PlanHistories.Where(ph => ph.Plan == service.CurrentPlan).ToList();

            //    // 1、已有飞机（只有变更与退出计划）
            //    if (planAircraft.Aircraft != null)
            //    {
            //        // 1.1、计划飞机在当前计划中只有一条明细项
            //        if (planDetails.Count == 1)
            //            planAircraft.Status = (int)ManageStatus.Operation;
            //        // 1.2、计划飞机在当前计划中超过一条明细项，即一条变更、一条退出
            //        else
            //        {
            //            // 移除的是变更计划，计划飞机改为运营状态（可能之前也是运营状态）
            //            if (planDetail.ActionType == "变更")
            //                planAircraft.Status = (int)ManageStatus.Operation;
            //            // 移除的是退出计划，不做任何改变
            //        }
            //    }
            //    // 2、没有飞机（只有引进与退出计划）
            //    // 2.1、计划飞机相关的明细项数量为1
            //    // 删除相关计划飞机。
            //    else if (planAircraftHistories.Count == 1)
            //    {
            //        service.EntityContainer.GetEntitySet<PlanAircraft>().Remove(planAircraft);
            //    }
            //    // 2.2、计划飞机相关的计划历史数量不为1（即超过1）
            //    // 2.2.1、计划飞机在当前计划中只有一条明细项
            //    // 计划飞机的管理状态改为预备
            //    else if (planDetails.Count == 1)
            //    {
            //        planAircraft.Status = (int)ManageStatus.Prepare;
            //    }
            //    // 2.2.2、计划飞机在当前计划中超过一条明细项，即一条引进、一条退出
            //    // 不改变计划飞机状态

            //    service.EntityContainer.GetEntitySet<PlanHistory>().Remove(planDetail);
            //}
        }

        /// <summary>
        /// 完成计划项
        /// </summary>
        /// <param name="planDetail"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        internal AircraftDTO CompletePlan(PlanHistoryDTO planDetail, IFleetPlanService service)
        {
            //Aircraft aircraft;
            //OperationHistory operationHistory;
            //if (planDetail == null)
            //{
            //    throw new ArgumentNullException("planDetail");
            //}
            //var actionName = planDetail.ActionCategory.ActionName;
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
            //        CreateOperationHistory(planDetail, planDetail.PlanAircraft.Aircraft, service);
            //        break;
            //    case "湿租续租":
            //        // 创建新运营历史
            //        CreateOperationHistory(planDetail, planDetail.PlanAircraft.Aircraft, service);
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
