#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/02，22:13
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

namespace UniCloud.Domain.ProjectBC.Aggregates.ProjectTempAgg
{
    /// <summary>
    ///     项目模板聚合根
    /// </summary>
    public class ProjectTemp : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<TaskTemp> _taskTemps;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ProjectTemp()
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
        ///     项目模板状态
        /// </summary>
        public ProjectTempStatus Status { get; private set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        /// <summary>
        ///     任务模板集合
        /// </summary>
        public virtual ICollection<TaskTemp> TaskTemps
        {
            get { return _taskTemps ?? (_taskTemps = new HashSet<TaskTemp>()); }
            set { _taskTemps = new HashSet<TaskTemp>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置项目模板状态
        /// </summary>
        /// <param name="status">项目模板状态</param>
        public void SetProjectStatus(ProjectTempStatus status)
        {
            switch (status)
            {
                case ProjectTempStatus.草稿:
                    Status = ProjectTempStatus.草稿;
                    break;
                case ProjectTempStatus.待审核:
                    Status = ProjectTempStatus.待审核;
                    break;
                case ProjectTempStatus.已审核:
                    Status = ProjectTempStatus.已审核;
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
        /// <param name="start">开始偏移量</param>
        /// <param name="end">结束偏移量</param>
        /// <param name="isSummary">是否摘要任务</param>
        /// <returns>任务模板</returns>
        public TaskTemp CreateTaskTemp(string subject, TimeSpan start, TimeSpan end, bool isSummary)
        {
            var taskTemp = ProjectTempFactory.CreateTaskTemp(subject, start, end, isSummary);
            taskTemp.ProjectTempId = Id;
            TaskTemps.Add(taskTemp);

            return taskTemp;
        }

        /// <summary>
        ///     添加任务模板
        ///     <remarks>
        ///         通过任务标准创建，只能为叶子任务，非摘要任务或里程碑任务
        ///     </remarks>
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="start">开始偏移量</param>
        /// <param name="end">结束偏移量</param>
        /// <param name="isSummary">是否摘要任务</param>
        /// <param name="taskStandardId">任务标准ID</param>
        /// <returns>任务模板</returns>
        public TaskTemp CreateTaskTemp(string subject, TimeSpan start, TimeSpan end, bool isSummary, int taskStandardId)
        {
            var taskTemp = ProjectTempFactory.CreateTaskTemp(subject, start, end, isSummary, taskStandardId);
            taskTemp.ProjectTempId = Id;
            TaskTemps.Add(taskTemp);

            return taskTemp;
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