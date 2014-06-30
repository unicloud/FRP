#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:54
// 方案：FRP
// 项目：Application.PurchaseBC.DTO
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    [DataServiceKey("Id")]
    public class PlanHistoryDTO
    {
        /// <summary>
        ///     主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     主键
        /// </summary>
        public Guid PlanAircraftId { get; set; }

        /// <summary>
        ///     计划年度
        /// </summary>
        public int PlanYear { get; set; }

        /// <summary>
        ///     版本号
        /// </summary>
        public int VersionNumber { get; set; }

        /// <summary>
        ///     执行年度
        /// </summary>
        public int PerformAnnual { get; set; }

        /// <summary>
        ///     执行月份
        /// </summary>
        public int PerformMonth { get; set; }

        /// <summary>
        ///     活动类型
        /// </summary>
        public string ActionType { get; set; }

        /// <summary>
        ///     活动类型名称
        /// </summary>
        public string ActionName { get; set; }
    }
}