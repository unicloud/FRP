#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:32:39
// 文件名：PlanAircraftDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    /// 计划飞机
    /// </summary>
    [DataServiceKey("Id")]
    public class PlanAircraftDTO
    {
        #region 私有字段

        //private List<PlanHistoryDTO> _planHistories;
        private List<ApprovalHistoryDTO> _approvalHistories;

        #endregion

        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     是否锁定，确定计划时锁定相关飞机。一旦锁定，对应的计划明细不能修改机型。
        /// </summary>
        public bool IsLock { get; set; }

        /// <summary>
        ///     是否自有，用以区分PlanAircraft，民航局均为False。
        /// </summary>
        public bool IsOwn { get; set; }

        /// <summary>
        ///     管理状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 航空公司名称
        /// </summary>
        public string AirlinesName { get; set; }

        /// <summary>
        /// 机型
        /// </summary>
        public string AircraftTypeName { get; set; }

        /// <summary>
        /// 座级
        /// </summary>
        public string Regional { get; set; }
        #endregion

        #region 外键属性

        /// <summary>
        ///     合同飞机外键
        /// </summary>
        public int? ContractAircraftId { get; set; }

        /// <summary>
        ///     实际飞机外键
        /// </summary>
        public Guid? AircraftId { get; set; }

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeId { get; set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; set; }

        #endregion

        #region 导航属性

        ///// <summary>
        /////     飞机计划明细
        ///// </summary>
        //public virtual List<PlanHistoryDTO> PlanHistories
        //{
        //    get { return _planHistories ?? (_planHistories = new List<PlanHistoryDTO>()); }
        //    set { _planHistories = value; }
        //}

        /// <summary>
        ///     审批历史集合
        /// </summary>
        public virtual List<ApprovalHistoryDTO> ApprovalHistories
        {
            get { return _approvalHistories ?? (_approvalHistories = new List<ApprovalHistoryDTO>()); }
            set { _approvalHistories = value; }
        }
        #endregion
    }
}