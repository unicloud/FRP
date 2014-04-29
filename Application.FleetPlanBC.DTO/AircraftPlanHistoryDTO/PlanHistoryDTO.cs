#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:23:48
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
using System.Configuration;
using System.Data.Services.Common;
using System.Diagnostics;
using System.Security.AccessControl;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    /// 运力增减计划明细
    /// </summary>
    
    [DataServiceKey("Id")]
    public class PlanHistoryDTO
    {
        private List<AircraftCateDTO> _aircraftCateDtos;
        private List<AircraftTyDTO> _aircraftTyDtos;
        private List<ActionCateDTO> _actionCateDtos;
        public PlanHistoryDTO()
        {

        }

        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     座位数
        /// </summary>
        public int SeatingCapacity { get; set; }

        /// <summary>
        ///     商载量
        /// </summary>
        public decimal CarryingCapacity { get; set; }

        /// <summary>
        ///     执行月份
        /// </summary>
        public int PerformMonth { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     是否提交
        /// </summary>
        public bool IsSubmit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 航空公司
        /// </summary>
        public string AirlinesName { get; set; }

        /// <summary>
        ///     民航机型
        /// </summary>
        public string CaacAircraftTypeName { get; set; }

        /// <summary>
        /// 机型
        /// </summary>
        public string AircraftTypeName { get; set; }

        /// <summary>
        /// 座级
        /// </summary>
        public string Regional { get; set; }

        /// <summary>
        /// 座级类型
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        public string ActionType { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 引进/退出方式
        /// </summary>
        public string TargetType { get; set; }

        /// <summary>
        /// 是否需要申请
        /// </summary>
        public bool NeedRequest { get; set; }

        /// <summary>
        /// 执行年度
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 净增货机
        /// </summary>
        public int DeltaCargo { get; set; }

        /// <summary>
        /// 净增客机
        /// </summary>
        public int DeltaPnr { get; set; }

        /// <summary>
        /// 能否执行申请操作
        /// </summary>
        public int CanRequest { get; set; }

        /// <summary>
        /// 能否执行交付操作
        /// </summary>
        public int CanDeliver { get; set; }
        #endregion

        #region 外键属性

        /// <summary>
        ///     计划飞机外键
        /// </summary>
        public Guid? PlanAircraftId { get; set; }

        /// <summary>
        ///     计划外键
        /// </summary>
        public Guid PlanId { get; set; }

        /// <summary>
        ///     活动类别：包括引进、退出、变更
        /// </summary>
        public Guid ActionCategoryId { get; set; }

        /// <summary>
        /// 目标类别：具体的引进、退出方式
        /// </summary>
        public Guid TargetCategoryId { get; set; }

        /// <summary>
        ///     川航机型外键
        /// </summary>
        public Guid AircraftTypeId { get; set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; set; }

        /// <summary>
        ///     执行年度
        /// </summary>
        public Guid PerformAnnualId { get; set; }

        /// <summary>
        /// 申请明细Id
        /// </summary>
        public Guid? ApprovalHistoryId { get; set; }

        ///// <summary>
        ///// 申请是否审批
        ///// </summary>
        //public bool IsApproved { get; set; }
        #endregion

        #region 外加属性,用于处理运营计划\变更计划
        /// <summary>
        /// 计划明细类型 1-表示为运营计划，2-表示为变更计划
        /// </summary>
        public int PlanType { get; set; }

        /// <summary>
        /// 关联的Guid，运营计划时，记录OperationHistoryID，变更计划时记录为AircraftBusinessID
        /// </summary>
        public Guid? RelatedGuid { get; set; }

        /// <summary>
        ///  关联的运营计划历史或商业数据历史的开始日期
        ///  用于分析当前年度计划执行情况
        /// </summary>
        public DateTime? RelatedStartDate { get; set; }

        /// <summary>
        ///  关联的运营计划历史或商业数据历史的结束日期
        ///  在创建新年度计划时，用于判断是否需要复制此计划明细
        /// </summary>
        public DateTime? RelatedEndDate { get; set; }

        /// <summary>
        /// 关联的运营权历史或商业数据历史的状态
        /// </summary>
        public int RelatedStatus { get; set; }

        /// <summary>
        /// 计划明细的状态（取自关联的计划飞机的管理状态）
        /// </summary>
        public int ManageStatus { get; set; }

        /// <summary>
        /// 计划飞机锁定状态（取自关联的计划飞机的锁定状态）
        /// </summary>
        public bool PaIsLock { get; set; }


        /// <summary>
        /// 机号（取自关联的计划飞机带的飞机的机号）
        /// </summary>
        public string RegNumber { get; set; }

        /// <summary>
        /// 飞机id（取自关联的计划飞机带的飞机外键）
        /// </summary>
        public Guid? AircraftId { get; set; }

        /// <summary>
        /// 飞机的引进方式（取自关联的计划飞机带的飞机实际的引进方式）
        /// </summary>
        public Guid? AircraftImportCategoryId { get; set; }
        #endregion

        public List<ActionCateDTO> ActionCategories
        {
            get { return _actionCateDtos ?? (_actionCateDtos = new List<ActionCateDTO>()); }
            set { _actionCateDtos = value; }
        }


        public List<AircraftCateDTO> AircraftCategories
        {
            get { return _aircraftCateDtos ?? (_aircraftCateDtos = new List<AircraftCateDTO>()); }
            set { _aircraftCateDtos = value; }
        }

        public List<AircraftTyDTO> AircraftTypes
        {
            get { return _aircraftTyDtos ?? (_aircraftTyDtos = new List<AircraftTyDTO>()); }
            set { _aircraftTyDtos = value; }
        }
    }
}
