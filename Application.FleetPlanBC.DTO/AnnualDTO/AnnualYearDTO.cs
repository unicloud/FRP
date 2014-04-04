#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2014/01/04，20:01
// 文件名：AnnualYearDTO.cs
// 程序集：UniCloud.Application.FleetPlanBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    ///     计划年度
    /// </summary>
    [DataServiceKey("Id")]
    public class PlanYearDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     年度
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        ///     是否打开年度
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 规划期间
        /// </summary>
        public string ProgrammingName { get; set; }

        /// <summary>
        ///     五年规划期间ID
        /// </summary>
        public Guid ProgrammingId { get; set; }
    }
}