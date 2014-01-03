#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/03，9:09
// 方案：FRP
// 项目：Domain.ProjectBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.Common.Entities;

#endregion

namespace UniCloud.Domain.ProjectBC.Aggregates.ProjectAgg
{
    /// <summary>
    ///     项目聚合根
    ///     任务
    /// </summary>
    public class Task : ScheduleBase, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Task()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     工期
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        ///     期限
        /// </summary>
        public DateTime DeadLine { get; set; }

        /// <summary>
        ///     是否里程碑
        /// </summary>
        public bool IsMileStone { get; set; }

        /// <summary>
        ///     是否摘要任务
        /// </summary>
        public bool IsSummary { get; set; }

        /// <summary>
        ///     是否有风险
        /// </summary>
        public bool HasRisk { get; set; }

        /// <summary>
        /// </summary>
        public string TimeZoneId { get; set; }

        /// <summary>
        ///     任务跟踪记录
        /// </summary>
        public string Note { get; set; }

        #endregion

        #region 外键属性

        public int ProjectId { get; set; }

        public int TaskStandardId { get; set; }

        public int RelatedId { get; set; }

        public int? ParentId { get; set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}