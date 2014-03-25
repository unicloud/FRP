#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/1 8:50:48
// 文件名：PlanHistoryDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.Service.FleetPlan.FleetPlan
{
    public partial class PlanHistoryDTO
    {
        static readonly FleetPlanService FleetPlanService = new FleetPlanService();

        #region 属性

        #region 控制只读属性

        #region 只读条件

        /// <summary>
        /// 已处于审核及之后的计划状态
        /// </summary>
        private bool PlanCheckedCondition
        {
            get { return true; } //TODO:需要重写
        }

        /// <summary>
        /// 已处于已提交及之后的计划状态
        /// </summary>
        private bool PlanSubmittedCondition
        {
            get { return true; }//TODO:需要重写
        }

        /// <summary>
        /// 已处于申请及之后的管理状态
        /// </summary>
        private bool ManageRequestCondition
        {
            get { return ManageStatus > 1; }
        }

        /// <summary>
        /// 计划飞机处于锁定状态
        /// </summary>
        private bool LockCondition
        {
            get { return PaIsLock; }
        }

        /// <summary>
        /// 属于现役飞机
        /// </summary>
        private bool OperationCondition
        {
            get { return AircraftId != null; }
        }

        /// <summary>
        /// 没有下属航空公司
        /// </summary>
        private bool OnlyAirlinesCondition
        {
            get { return true; }
        }

        /// <summary>
        /// 是退出计划
        /// </summary>
        private bool ExportPlanCondition
        {
            get { return this.ActionType == "退出"; }
        }

        /// <summary>
        /// 是变更计划
        /// </summary>
        private bool ChangePlanCondition
        {
            get { return PlanType == 2; }
        }

        #endregion

        #region 只读逻辑

        internal bool IsPlanChecked
        {
            get { return this.PlanCheckedCondition; }
        }

        internal bool IsPlanCheckedOrLock
        {
            get { return this.PlanCheckedCondition || this.LockCondition; }
        }

        internal bool IsPlanCheckedOrOnlyAirlines
        {
            get { return this.PlanCheckedCondition || this.OnlyAirlinesCondition; }
        }

        internal bool IsPlanCheckedOrOperation
        {
            get { return this.PlanCheckedCondition || this.OperationCondition; }
        }

        internal bool IsManageRequestOrPlanSubmitted
        {
            get { return this.ManageRequestCondition; }
        }

        internal bool IsOperationAndExportPlan
        {
            get { return this.OperationCondition && this.ExportPlanCondition; }
        }

        #endregion

        #region 可用逻辑

        internal bool IsAirlineEnabled
        {
            get { return !this.IsPlanCheckedOrOperation; }
        }

        internal bool IsNotOperationOrChangePlan
        {
            get { return !this.OperationCondition || ChangePlanCondition; }
        }

        #endregion

        #endregion

        #region 客户端计算属性

        /// <summary>
        ///     管理状态
        /// </summary>
        public ManageStatus ManaStatus
        {
            get { return (ManageStatus)ManageStatus; }
        }

        private CanRequest _canRequest;

        /// <summary>
        /// 能否提出申请
        /// 1、可申请
        /// 2、未报计划
        /// 3、已申请
        /// 4、无需申请
        /// </summary>
        public CanRequest CanRequest
        {
            get
            {
                return _canRequest;
            }
        }


        /// <summary>
        /// 能否执行交付操作
        /// 可交付存在两种情形，一种是无需申请的，一种是申请已批复且批准的
        /// 1、可交付
        /// 2、交付中
        /// 3、已交付
        /// 4、未申请
        /// 5、未批复
        /// 6、未批准
        /// </summary>
        public CanDeliver CanDeliver
        {
            get
            {
                // 1、活动是需要申报的类型
                if (ActionCategoryId != Guid.Empty && NeedRequest)
                {
                    //if (this.ApprovalHistory == null) return CanDeliver.未申请; TODO
                    if (ManageStatus == (int)Enums.ManageStatus.申请) return CanDeliver.未批复;
                    //if (!this.ApprovalHistory.IsApproved) return CanDeliver.未批准; TODO
                    var planDetail = this;
                    if (planDetail.Id == Guid.Empty) return CanDeliver.可交付;
                    if (planDetail.RelatedGuid == null) return CanDeliver.可交付;
                }
                // 2、活动是无需申报的类型
                else
                {
                    var planDetail = this;
                    if (planDetail.Id == Guid.Empty) return CanDeliver.可交付;
                    if (planDetail.RelatedGuid == null) return CanDeliver.可交付;

                }
                return CanDeliver.可交付;
            }
        }

        /// <summary>
        /// 计划完成状态
        /// 0：草稿
        /// 1：审核
        /// 2：已审核
        /// 3：已提交
        /// -1：无状态
        /// </summary>
        public CompleteStatus CompleteStatus
        {
            get
            {
                if (RelatedGuid == null)
                {
                    return CompleteStatus.无状态;
                }
                return (CompleteStatus)RelatedStatus;
            }
        }

        /// <summary>
        /// 净增客机
        /// </summary>
        internal int DeltaPnr
        {
            get
            {
                if (this.AircraftTypeId == Guid.Empty || this.ActionCategoryId == Guid.Empty)
                    return 0;
                if (Regional != null && Category == "客机")
                {
                    switch (ActionName)
                    {
                        case "购买": return 1;
                        case "融资租赁": return 1;
                        case "经营租赁": return 1;
                        case "湿租": return 1;
                        case "经营租赁续租": return 0;
                        case "湿租续租": return 0;
                        case "退役": return -1;
                        case "出售": return -1;
                        case "退租": return -1;
                        case "出租": return -1;
                        case "一般改装": return 0;
                        case "货改客": return 1;
                        case "售后回租": return 0;
                        case "租转购": return 0;
                        default: return 0;
                    }
                }
                if (ActionName == "客改货")
                {
                    return -1;
                }
                return 0;
            }
        }

        /// <summary>
        /// 净增货机
        /// </summary>
        internal int DeltaCargo
        {
            get
            {
                if (this.AircraftTypeId == Guid.Empty || this.ActionCategoryId == Guid.Empty)
                    return 0;
                if (Regional != null && Category == "货机")
                {
                    switch (ActionName)
                    {
                        case "购买": return 1;
                        case "融资租赁": return 1;
                        case "经营租赁": return 1;
                        case "湿租": return 1;
                        case "经营租赁续租": return 0;
                        case "湿租续租": return 0;
                        case "退役": return -1;
                        case "出售": return -1;
                        case "出租": return -1;
                        case "退租": return -1;
                        case "一般改装": return 0;
                        case "客改货": return 1;
                        case "售后回租": return 0;
                        case "租转购": return 0;
                        default: return 0;
                    }
                }
                if (ActionName == "货改客")
                {
                    return -1;
                }
                return 0;
            }
        }
        #endregion

        #region 属性绑定
        /// <summary>
        /// 座级集合，用于属性绑定
        /// </summary>
        //internal IEnumerable<AircraftCategoryDTO> AircraftCategories
        //{
        //    get
        //    {
        //        if (ActionName == "客改货")
        //            return FleetPlanService.GetAircraftCategories(null).SourceCollection.Cast<AircraftCategoryDTO>().Where(p => p.Category == "货机").ToList();
        //        else if (ActionName == "货改客")
        //            return FleetPlanService.GetAircraftCategories(null).SourceCollection.Cast<AircraftCategoryDTO>().Where(p => p.Category == "客机").ToList();
        //        else
        //            return FleetPlanService.GetAircraftCategories(null).SourceCollection.Cast<AircraftCategoryDTO>().ToList();
        //    }
        //}

        /// <summary>
        /// 机型集合，用于属性绑定
        /// </summary>
        //internal IEnumerable<AircraftTypeDTO> AircraftTypes
        //{
        //    get
        //    {
        //        if (Regional != null)
        //            return FleetPlanService.GetAircraftTypes(null).SourceCollection.Cast<AircraftTypeDTO>().Where(p => p.Regional == Regional).ToList();
        //        else return FleetPlanService.GetAircraftTypes(null).SourceCollection.Cast<AircraftTypeDTO>().ToList();
        //    }
        //}

        /// <summary>
        /// 操作集合，用于属性绑定
        /// </summary>
        //internal IEnumerable<ActionCategoryDTO> ActionCategories
        //{
        //    get
        //    {
        //        if (PlanType == 1 && ActionType == "引进")
        //            return FleetPlanService.GetActionCategories(null).SourceCollection.Cast<ActionCategoryDTO>().Where(p => p.ActionType == "引进").ToList();
        //        else if (PlanType == 1 && ActionType == "退出")
        //            return FleetPlanService.GetActionCategories(null).SourceCollection.Cast<ActionCategoryDTO>().Where(p => p.ActionType == "退出").ToList();
        //        else if (PlanType == 2 && ActionType == "变更")
        //            return FleetPlanService.GetActionCategories(null).SourceCollection.Cast<ActionCategoryDTO>().Where(p => p.ActionType == "变更").ToList();
        //        else return FleetPlanService.GetActionCategories(null).SourceCollection.Cast<ActionCategoryDTO>().ToList();
        //    }
        //}

        /// <summary>
        /// 计划历史比较状态
        /// </summary>
        internal PlanHistoryCompareStatus PlanHistoryCompareStatus
        { get; set; }
        #endregion

        #endregion

        #region 方法

        partial void OnPlanTypeChanged()
        {
            //List<ActionCategoryDTO> actionCategories;
            //if (PlanType == 1 && ActionType == "引进")
            //    actionCategories = FleetPlanService.GetActionCategories(null).SourceCollection.Cast<ActionCategoryDTO>().Where(p => p.ActionType == "引进").ToList();
            //else if (PlanType == 1 && ActionType == "退出")
            //    actionCategories = FleetPlanService.GetActionCategories(null).SourceCollection.Cast<ActionCategoryDTO>().Where(p => p.ActionType == "退出").ToList();
            //else if (PlanType == 2 && ActionType == "变更")
            //    actionCategories = FleetPlanService.GetActionCategories(null).SourceCollection.Cast<ActionCategoryDTO>().Where(p => p.ActionType == "变更").ToList();
            //else actionCategories = FleetPlanService.GetActionCategories(null).SourceCollection.Cast<ActionCategoryDTO>().ToList();

            //ActionCategories.Clear();
            //foreach (var actionCategory in actionCategories)
            //{
            //    var actioncate = new ActionCateDTO
            //    {
            //        Id = actionCategory.Id,
            //        ActionType = actionCategory.ActionType,
            //        ActionName = actionCategory.ActionName,
            //        NeedRequest = actionCategory.NeedRequest,
            //    };
            //    ActionCategories.Add(actioncate);
            //}
            //OnPropertyChanged("ActionCategories");

        }

        partial void OnActionTypeChanged()
        {
            //List<ActionCategoryDTO> actionCategories;
            //if (PlanType == 1 && ActionType == "引进")
            //    actionCategories = FleetPlanService.GetActionCategories(null).SourceCollection.Cast<ActionCategoryDTO>().Where(p => p.ActionType == "引进").ToList();
            //else if (PlanType == 1 && ActionType == "退出")
            //    actionCategories = FleetPlanService.GetActionCategories(null).SourceCollection.Cast<ActionCategoryDTO>().Where(p => p.ActionType == "退出").ToList();
            //else if (PlanType == 2 && ActionType == "变更")
            //    actionCategories = FleetPlanService.GetActionCategories(null).SourceCollection.Cast<ActionCategoryDTO>().Where(p => p.ActionType == "变更").ToList();
            //else actionCategories = FleetPlanService.GetActionCategories(null).SourceCollection.Cast<ActionCategoryDTO>().ToList();

            //ActionCategories.Clear();
            //foreach (var actionCategory in actionCategories)
            //{
            //    var actioncate = new ActionCateDTO
            //    {
            //        Id = actionCategory.Id,
            //        ActionType = actionCategory.ActionType,
            //        ActionName = actionCategory.ActionName,
            //        NeedRequest = actionCategory.NeedRequest,
            //    };
            //    ActionCategories.Add(actioncate);
            //}
            //OnPropertyChanged("ActionCategories");
        }

        partial void OnActionNameChanged()
        {
            //List<AircraftCategoryDTO> aircraftCategories;
            //if (ActionName == "客改货")
            //    aircraftCategories = FleetPlanService.GetAircraftCategories(null).SourceCollection.Cast<AircraftCategoryDTO>().Where(p => p.Category == "货机").ToList();
            //else if (ActionName == "货改客")
            //    aircraftCategories = FleetPlanService.GetAircraftCategories(null).SourceCollection.Cast<AircraftCategoryDTO>().Where(p => p.Category == "客机").ToList();
            //else
            //    aircraftCategories = FleetPlanService.GetAircraftCategories(null).SourceCollection.Cast<AircraftCategoryDTO>().ToList();

            //AircraftCategories.Clear();
            //foreach (var aircraftCategory in aircraftCategories)
            //{
            //    AircraftCategories.Add(aircraftCategory);
            //}
            //OnPropertyChanged("AircraftCategories");
        }


        partial void OnRegionalChanged()
        {
            //List<AircraftTypeDTO> aircraftTypes;
            //if (Regional != null)
            //    aircraftTypes = FleetPlanService.GetAircraftTypes(null).SourceCollection.Cast<AircraftTypeDTO>().Where(p => p.Regional == Regional).ToList();
            //else aircraftTypes = FleetPlanService.GetAircraftTypes(null).SourceCollection.Cast<AircraftTypeDTO>().ToList();

            //AircraftTypes.Clear();
            //foreach (var aircraftType in aircraftTypes)
            //{
            //    AircraftTypes.Add(aircraftType);
            //}
            //OnPropertyChanged("AircraftTypes");
        }

        /// <summary>
        /// 机型发生变化时触发相关变化
        /// </summary>
        partial void OnAircraftTypeIdChanged()
        {
            this.OnPropertyChanged("DeltaPnr");

            this.OnPropertyChanged("DeltaCargo");
        }



        partial void OnActionCategoryIdChanged()
        {
            var actionCategory =
                FleetPlanService.GetActionCategories(null).FirstOrDefault(ac => ac.Id == this.ActionCategoryId);
            if (actionCategory != null)
            {
                if (actionCategory.ActionType == "退出")
                {
                    if (this.PlanAircraftId != null && AircraftId != null)
                    {
                        if (SeatingCapacity > 0)
                            this.SeatingCapacity = -SeatingCapacity;
                        if (CarryingCapacity > 0)
                            this.CarryingCapacity = -CarryingCapacity;
                    }
                }

                if (actionCategory.ActionType == "引进" || actionCategory.ActionType == "退出")
                {
                    this.TargetCategoryId = ActionCategoryId;
                }
                else
                {
                    OnPropertyChanged("AircraftCategores");
                    OnPropertyChanged("AircraftTypes");

                    ActionCategoryDTO actionCategoryDTO;
                    // 改变目标引进方式
                    switch (actionCategory.ActionName)
                    {
                        case "一般改装":
                            if (this.PlanAircraftId != null && this.AircraftId != null)
                            {
                                var aircraftImport = this.AircraftImportCategoryId == null ? Guid.Empty : Guid.Parse(AircraftImportCategoryId.ToString());
                                this.TargetCategoryId = aircraftImport;
                            }
                            break;
                        case "客改货":
                            if (this.PlanAircraftId != null && this.AircraftId != null)
                            {
                                var aircraftImport = this.AircraftImportCategoryId == null
                                    ? Guid.Empty
                                    : Guid.Parse(AircraftImportCategoryId.ToString());
                                this.TargetCategoryId = aircraftImport;
                                this.AircraftTypeId = Guid.Empty;
                            }
                            break;
                        case "货改客":
                            if (this.PlanAircraftId != null && this.AircraftId != null)
                            {
                                var aircraftImport = this.AircraftImportCategoryId == null
                                    ? Guid.Empty
                                    : Guid.Parse(AircraftImportCategoryId.ToString());
                                this.TargetCategoryId = aircraftImport;
                                this.AircraftTypeId = Guid.Empty;
                            }
                            break;
                        case "售后融资租赁":
                            actionCategoryDTO = FleetPlanService.GetActionCategories(null).FirstOrDefault(a => a.ActionName == "融资租赁");
                            if (actionCategoryDTO != null)
                                this.TargetCategoryId = actionCategoryDTO.Id;
                            break;
                        case "售后经营租赁":
                            actionCategoryDTO = FleetPlanService.GetActionCategories(null).FirstOrDefault(a => a.ActionName == "经营租赁");
                            if (actionCategoryDTO != null)
                                this.TargetCategoryId = actionCategoryDTO.Id;
                            break;
                        case "租转购":
                            actionCategoryDTO = FleetPlanService.GetActionCategories(null).FirstOrDefault(a => a.ActionName == "购买");
                            if (actionCategoryDTO != null)
                                this.TargetCategoryId = actionCategoryDTO.Id;
                            break;
                    }
                }
            }
        }



        partial void OnRelatedGuidChanged()
        {
            OnPropertyChanged("CompleteStatus");
        }

        /// <summary>
        /// 复制一份新的计划历史
        /// </summary>
        /// <returns></returns>
        public PlanHistoryDTO Clone()
        {
            return MemberwiseClone() as PlanHistoryDTO;
        }

        /// <summary>
        /// 刷新是否申请状态
        /// </summary>
        /// <param name="plan"></param>
        public void RefrashCanRequest(PlanDTO plan)
        {
            if (ActionCategoryId != Guid.Empty && NeedRequest)
            {
                if (ManageStatus > (int)Enums.ManageStatus.计划) _canRequest = CanRequest.已申请;
                else
                    _canRequest = (this.IsSubmit && plan.Status == (int)OperationStatus.已提交)
                    ? CanRequest.可申请
                    : CanRequest.未报计划;
            }
            else
            {
                _canRequest = CanRequest.无需申请;
            }
            OnPropertyChanged("CanRequest");

        }
        #endregion
    }
}
