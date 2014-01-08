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
        #region 私有字段

        private HashSet<Task> _children;

        #endregion

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
        public TimeSpan Duration { get; internal set; }

        /// <summary>
        ///     期限
        /// </summary>
        public DateTime DeadLine { get; internal set; }

        /// <summary>
        ///     是否里程碑
        /// </summary>
        public bool IsMileStone { get; internal set; }

        /// <summary>
        ///     是否摘要任务
        /// </summary>
        public bool IsSummary { get; internal set; }

        /// <summary>
        ///     是否有风险
        /// </summary>
        public bool HasRisk { get;private set; }

        /// <summary>
        ///     时区ID
        /// </summary>
        public string TimeZoneId { get; internal set; }

        /// <summary>
        ///     任务跟踪记录
        /// </summary>
        public string Note { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     项目ID
        /// </summary>
        public int ProjectId { get; internal set; }

        /// <summary>
        ///     任务标准ID
        /// </summary>
        public int? TaskStandardId { get; internal set; }

        /// <summary>
        ///     相关具体任务ID
        ///     <remarks>
        ///         用于导航到具体任务，例如订单、批文申请等。
        ///     </remarks>
        /// </summary>
        public int? RelatedId { get; set; }

        /// <summary>
        ///     父项ID
        /// </summary>
        public int? ParentId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     父节点
        /// </summary>
        public virtual Task Parent { get; private set; }

        /// <summary>
        ///     子集
        /// </summary>
        public virtual ICollection<Task> Children
        {
            get { return _children ?? (_children = new HashSet<Task>()); }
            set { _children = new HashSet<Task>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置父项
        /// </summary>
        /// <param name="task">父项</param>
        private void SetParent(Task task)
        {
            if (task == null || task.IsTransient())
            {
                throw new ArgumentException("任务父项参数为空！");
            }

            Parent = task;
            ParentId = task.Id;
        }

        /// <summary>
        ///     添加子项
        /// </summary>
        /// <param name="task">子项</param>
        /// <returns>添加的子项</returns>
        public Task AddChild(Task task)
        {
            if (task == null || task.IsTransient())
            {
                throw new ArgumentException("任务子项参数为空！");
            }

            Children.Add(task);
            task.SetParent(this);
            return task;
        }

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