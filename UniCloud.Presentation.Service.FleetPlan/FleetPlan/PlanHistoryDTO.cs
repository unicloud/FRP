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
using System.ComponentModel.Composition;
using System.Data.Services.Common;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Data.OData;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.Service.FleetPlan.FleetPlan
{
    public partial class PlanHistoryDTO
    {
        static readonly FleetPlanService FleetPlanService = new FleetPlanService();

      

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

        public bool IsPlanChecked
        {
            get { return this.PlanCheckedCondition; }
        }

        public bool IsPlanCheckedOrLock
        {
            get { return this.PlanCheckedCondition || this.LockCondition; }
        }

        public bool IsPlanCheckedOrOnlyAirlines
        {
            get { return this.PlanCheckedCondition || this.OnlyAirlinesCondition; }
        }

        public bool IsPlanCheckedOrOperation
        {
            get { return this.PlanCheckedCondition || this.OperationCondition; }
        }

        public bool IsManageRequestOrPlanSubmitted
        {
            get { return this.ManageRequestCondition; }
        }

        public bool IsOperationAndExportPlan
        {
            get { return this.OperationCondition && this.ExportPlanCondition; }
        }

        #endregion

        #region 可用逻辑

        public bool IsAirlineEnabled
        {
            get { return !this.IsPlanCheckedOrOperation; }
        }

        public bool IsNotOperationOrChangePlan
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

        public PlanHistoryCompareStatus PlanHistoryCompareStatus
        { get; set; }

        #endregion

        #region 属性绑定
        /// <summary>
        /// 座级集合，用于属性绑定
        /// </summary>
        public IEnumerable<AircraftCategoryDTO> AircraftCategories
        {
            get { return FleetPlanService.GetAircraftCategories(null); }

        }

        ///// <summary>
        ///// 机型集合，用于属性绑定
        ///// </summary>
        //public IEnumerable<AircraftTypeDTO> AircraftTypes
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 操作集合，用于属性绑定
        /// </summary>
        public IEnumerable<AircraftTypeDTO> AircraftTypes
        {
            get;
            set;
        }
        ///// <summary>
        ///// 计划历史比较状态
        ///// </summary>
        //public IEnumerable<ActionCategoryDTO> ActionCategories
        //{
        //    get;
        //    set;
        //}
      

        //#endregion

        #endregion

        #region 方法

        partial void OnPlanTypeChanged()
        {
            //if (PlanType == 1)
            //    ActionCategories = FleetPlanService.GetActionCategories(null).Where(p => p.ActionType != "变更");
            //else if (PlanType == 2)
            //    ActionCategories = FleetPlanService.GetActionCategories(null).Where(p => p.ActionType == "变更");
            //else ActionCategories = FleetPlanService.GetActionCategories(null);
        }

        partial void OnActionCategoryIdChanged()
        {
            TargetCategoryId = ActionCategoryId;
        }

        partial void OnRegionalChanged()
        {
            //if (Regional != null)
            //    AircraftTypes = FleetPlanService.GetAircraftTypes(null).Where(p => p.Regional == Regional);
            //else AircraftTypes = FleetPlanService.GetAircraftTypes(null);
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
        public void  RefrashCanRequest(PlanDTO plan)
        {
            if (ActionCategoryId != Guid.Empty && NeedRequest)
            {
                if (ManageStatus > (int)Enums.ManageStatus.计划) _canRequest= CanRequest.已申请;
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
