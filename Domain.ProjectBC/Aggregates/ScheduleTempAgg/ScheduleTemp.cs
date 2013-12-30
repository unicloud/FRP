#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/30，17:55
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

#endregion

namespace UniCloud.Domain.ProjectBC.Aggregates.ScheduleTempAgg
{
    /// <summary>
    ///     任务模板聚合根
    /// </summary>
    public class ScheduleTemp : ScheduleBase, IValidatableObject
    {
        #region 私有字段

        private HashSet<ScheduleTemp> _children;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ScheduleTemp()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     期限
        /// </summary>
        public DateTime? DeadLine { get; set; }

        /// <summary>
        ///     工期
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        ///     是否里程碑
        /// </summary>
        public bool IsMileStone { get; set; }

        /// <summary>
        ///     时区ID
        /// </summary>
        public string TimeZoneId { get; set; }

        /// <summary>
        ///     成员
        /// </summary>
        public string Member { get; set; }

        /// <summary>
        ///     乐观时间
        /// </summary>
        public TimeSpan OptimisticTime { get; set; }

        /// <summary>
        ///     悲观时间
        /// </summary>
        public TimeSpan PessimisticTime { get; set; }

        /// <summary>
        ///     正常时间
        /// </summary>
        public TimeSpan NormalTime { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        /// </summary>
        public int? ParentId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     父节点
        /// </summary>
        public virtual ScheduleTemp Parent { get; set; }

        /// <summary>
        ///     子集
        /// </summary>
        public virtual ICollection<ScheduleTemp> Children
        {
            get { return _children ?? (_children = new HashSet<ScheduleTemp>()); }
            set { _children = new HashSet<ScheduleTemp>(value); }
        }

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