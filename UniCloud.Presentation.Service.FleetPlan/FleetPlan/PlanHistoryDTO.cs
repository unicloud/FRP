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
using System.ComponentModel.Composition;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.Service.FleetPlan.FleetPlan
{
    public partial class PlanHistoryDTO
    {
        [Import]
        public IFleetPlanService Service { get; set; }

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

        private string _regional;
        /// <summary>
        /// 机型所对应的座级别（用来控制计划历史所对应的机型）
        /// </summary>
        public string Regional
        {
            get
            {

                return null;
            }
            set
            {
                _regional = value;
            }
        }

        /// <summary>
        ///     管理状态
        /// </summary>
        public ManageStatus ManaStatus
        {
            get { return (ManageStatus)ManageStatus; }
        }

        public string PerformTime
        {
            get
            {
                return (Year + "/" + PerformMonth);
            }
        }

        public int DeltaPnr
        {
            get
            {
                return 0;
            }
        }

        public int DeltaCargo
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// 能否提出申请
        /// 1、可申请
        /// 2、未报计划
        /// 3、已申请
        /// 4、无需申请
        /// </summary>
        public string CanRequest
        {
            get
            {
                if (NeedRequest)
                {
                    if (ManageStatus > 1) return "3：已申请";
                    //return (this.IsSubmit && this.Plan.Status == (int)OpStatus.Submited) ? "1：可申请" : "2：未报计划";TODO:需要重写
                }
                return "4：无需申请";
            }
        }

        public string ActionCategoryOperation
        {
            get
            {
                if (ActionCategoryId != Guid.Empty)
                {
                    return ActionType + "：" + ActionName;
                }
                return null;
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
        public string CanDeliver
        {
            get
            {
                //    // 1、活动是需要申报的类型
                //    if (this.ActionCategory != null && this.ActionCategory.NeedRequest)
                //    {
                //        if (this.ApprovalHistory == null) return "4：未申请";
                //        if (this.PlanAircraft.Status == (int)ManageStatus.Request) return "5：未批复";
                //        if (!this.ApprovalHistory.IsApproved) return "6：未批准";
                //        if (this is OperationPlan)
                //        {
                //            var planDetail = this as OperationPlan;
                //            if (planDetail == null) return "1：可交付";
                //            if (planDetail.OperationHistory == null) return "1：可交付";
                //            return planDetail.OperationHistory.Status == (int)OpStatus.Submited ? "3：已交付" : "2：交付中";
                //        }
                //        if (this is ChangePlan)
                //        {
                //            var planDetail = this as ChangePlan;
                //            if (planDetail == null) return "1：可交付";
                //            if (planDetail.AircraftBusiness == null) return "1：可交付";
                //            return planDetail.AircraftBusiness.Status == (int)OpStatus.Submited ? "3：已交付" : "2：交付中";
                //        }
                //    }
                //    // 2、活动是无需申报的类型
                //    else
                //    {
                //        if (this is OperationPlan)
                //        {
                //            var planDetail = this as OperationPlan;
                //            if (planDetail == null) return "1：可交付";
                //            if (planDetail.OperationHistory == null) return "1：可交付";
                //            return planDetail.OperationHistory.Status == (int)OpStatus.Submited ? "3：已交付" : "2：交付中";
                //        }
                //        if (this is ChangePlan)
                //        {
                //            var planDetail = this as ChangePlan;
                //            if (planDetail == null) return "1：可交付";
                //            if (planDetail.AircraftBusiness == null) return "1：可交付";
                //            return planDetail.AircraftBusiness.Status == (int)OpStatus.Submited ? "3：已交付" : "2：交付中";
                //        }
                //    }
                return "1：可交付";
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
        public int CompleteStatus
        {
            get
            {
                if (RelatedGuid == null)
                {
                    return -1;
                }
                return RelatedStatus;
            }
        }

        #endregion

        #region 属性绑定



        #endregion

        #endregion

        #region 方法

        partial void OnPerformMonthChanged()
        {
            OnPropertyChanged("PerformTime");
        }

        /// <summary>
        /// 机型发生变化时触发相关变化
        /// </summary>
        partial void OnAircraftTypeIdChanged()
        {
            //if (this.PlanAircraft != null)
            //{
            //    this.PlanAircraft.AircraftTypeID = this.AircraftTypeID;
            //}
            //this.RaisePropertyChanged("DeltaPnr");
            //this.RaisePropertyChanged("DeltaCargo");
        }

        ///// <summary>
        ///// 申请明细发生变化时触发相关变化
        ///// </summary>
        //partial void OnApprovalHistoryIdChanged()
        //{
        //    RaisePropertyChanged("CanRequest");
        //}

        #endregion
    }
}
