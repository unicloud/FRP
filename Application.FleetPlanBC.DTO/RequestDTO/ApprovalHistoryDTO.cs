#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:33:45
// 文件名：ApprovalHistoryDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO.RequestDTO
{
    /// <summary>
    /// 审批历史（计划明细）
    /// </summary>
    [DataServiceKey("Id")]
    public class ApprovalHistoryDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     是否批准
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        ///     座位数
        /// </summary>
        public int SeatingCapacity { get;set; }

        /// <summary>
        ///     商载量
        /// </summary>
        public decimal CarryingCapacity { get;set; }

        /// <summary>
        ///     申请交付月份
        /// </summary>
        public int RequestDeliverMonth { get;set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get;set; }


        #endregion

        #region 外键属性

        /// <summary>
        ///     申请外键
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        ///     计划飞机外键
        /// </summary>
        public Guid PlanAircraftId { get;set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public Guid ImportCategoryId { get;set; }

        /// <summary>
        ///     申请交付年度
        /// </summary>
        public Guid RequestDeliverAnnualId { get;set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get;set; }

        #endregion
    }
}
