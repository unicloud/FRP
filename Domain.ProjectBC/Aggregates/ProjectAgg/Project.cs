#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/02，22:28
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
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.ProjectBC.Aggregates.ProjectAgg
{
    /// <summary>
    ///     项目聚合根
    /// </summary>
    public class Project : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<Task> _tasks;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Project()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        ///     计划开始
        /// </summary>
        public DateTime PlannedStart { get; internal set; }

        /// <summary>
        ///     计划结束
        /// </summary>
        public DateTime PlannedEnd { get; internal set; }

        /// <summary>
        ///     项目状态
        /// </summary>
        public ProjectStatus Status { get; private set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        /// <summary>
        ///     任务模板集合
        /// </summary>
        public virtual ICollection<Task> Tasks
        {
            get { return _tasks ?? (_tasks = new HashSet<Task>()); }
            set { _tasks = new HashSet<Task>(value); }
        }

        #endregion

        #region 操作

        public void SetProjectStatus(ProjectStatus status)
        {
            switch (status)
            {
                case ProjectStatus.草稿:
                    Status = ProjectStatus.草稿;
                    break;
                case ProjectStatus.待审核:
                    Status = ProjectStatus.待审核;
                    break;
                case ProjectStatus.已审核:
                    Status = ProjectStatus.已审核;
                    break;
                case ProjectStatus.已发布:
                    Status = ProjectStatus.已发布;
                    break;
                case ProjectStatus.已完成:
                    Status = ProjectStatus.已完成;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

        /// <summary>
        ///     添加任务模板
        ///     <remarks>
        ///         添加非标准任务，包括摘要任务、里程碑任务。
        ///     </remarks>
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="importance">重要性级别</param>
        /// <param name="tempo">进度</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="isAllDay">是否全天事件</param>
        /// <param name="duration">工期</param>
        /// <param name="deadLine">期限</param>
        /// <param name="isSummary">是否摘要任务</param>
        /// <returns>任务模板</returns>
        public Task CreateTaskTemp(string subject, string body, string importance, string tempo, DateTime start,
            DateTime end, bool isAllDay, TimeSpan duration, DateTime deadLine, bool isSummary)
        {
            var task = ProjectFactory.CreateTask(subject, body, importance, tempo, start, end, isAllDay, duration,
                deadLine, isSummary);
            task.ProjectId = Id;
            Tasks.Add(task);

            return task;
        }

        /// <summary>
        ///     添加任务模板
        ///     <remarks>
        ///         通过任务标准创建，只能为叶子任务，非摘要任务或里程碑任务
        ///     </remarks>
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="importance">重要性级别</param>
        /// <param name="tempo">进度</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="isAllDay">是否全天事件</param>
        /// <param name="duration">工期</param>
        /// <param name="deadLine">期限</param>
        /// <param name="isSummary">是否摘要任务</param>
        /// <param name="taskStandardId">任务标准ID</param>
        /// <returns>任务模板</returns>
        public Task CreateTaskTemp(string subject, string body, string importance, string tempo, DateTime start,
            DateTime end, bool isAllDay, TimeSpan duration, DateTime deadLine, bool isSummary, int taskStandardId)
        {
            var task = ProjectFactory.CreateTask(subject, body, importance, tempo, start, end, isAllDay, duration,
                deadLine, isSummary, taskStandardId);
            task.ProjectId = Id;
            Tasks.Add(task);

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