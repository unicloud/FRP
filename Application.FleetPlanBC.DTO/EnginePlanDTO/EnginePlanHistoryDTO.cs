#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:29:38
// 文件名：EnginePlanHistoryDTO
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
    /// 备发计划明细
    /// </summary>
    [DataServiceKey("Id")]
    public class EnginePlanHistoryDTO
    {
        #region 私有字段

        #endregion

        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     执行月份
        /// </summary>
        public int PerformMonth { get; set; }

        /// <summary>
        ///     执行情况
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        ///     最大推力
        /// </summary>
        public decimal MaxThrust { get; set; }

        /// <summary>
        ///     发动机实际引进日期
        /// </summary>
        public DateTime? ImportDate { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }
        #endregion

        #region 外键属性

        /// <summary>
        ///     发动机型号
        /// </summary>
        public Guid EngineTypeId { get; set; }

        /// <summary>
        ///     发动机计划外键
        /// </summary>
        public Guid EnginePlanId { get; set; }

        /// <summary>
        ///     执行年度
        /// </summary>
        public Guid PerformAnnualId { get; set; }

        /// <summary>
        ///     计划发动机ID
        /// </summary>
        public Guid? PlanEngineId { get; set; }

        /// <summary>
        ///     活动类型
        /// </summary>
        public Guid ActionCategoryId { get; set; }

        #endregion

        #region 导航属性

        #endregion
    }
}
