#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/02，20:50
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
using UniCloud.Domain.ProjectBC.Aggregates.WorkGroupAgg;

#endregion

namespace UniCloud.Domain.ProjectBC.Aggregates.TaskStandardAgg
{
    /// <summary>
    ///     任务标准聚合根
    /// </summary>
    public class TaskStandard : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<TaskCase> _taskCases;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal TaskStandard()
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
        ///     乐观时间
        /// </summary>
        public TimeSpan OptimisticTime { get; internal set; }

        /// <summary>
        ///     悲观时间
        /// </summary>
        public TimeSpan PessimisticTime { get; internal set; }

        /// <summary>
        ///     正常时间
        /// </summary>
        public TimeSpan NormalTime { get; internal set; }

        /// <summary>
        ///     源GUID
        /// </summary>
        public Guid SourceGuid { get; private set; }

        /// <summary>
        ///     是否自定义
        /// </summary>
        public bool IsCustom { get; internal set; }

        /// <summary>
        ///     任务类型
        /// </summary>
        public TaskType TaskType { get; internal set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     工作组ID
        /// </summary>
        public int WorkGroupId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     工作组
        /// </summary>
        public virtual WorkGroup WorkGroup { get; private set; }

        public virtual ICollection<TaskCase> TaskCases
        {
            get { return _taskCases ?? (_taskCases = new HashSet<TaskCase>()); }
            set { _taskCases = new HashSet<TaskCase>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置工作组
        /// </summary>
        /// <param name="workGroup">工作组</param>
        public void SetWorkGroup(WorkGroup workGroup)
        {
            if (workGroup == null || workGroup.IsTransient())
            {
                throw new ArgumentException("工作组参数为空！");
            }

            WorkGroup = workGroup;
            WorkGroupId = workGroup.Id;
        }

        /// <summary>
        ///     设置工作组
        /// </summary>
        /// <param name="workGroupId">工作组ID</param>
        public void SetWorkGroup(int workGroupId)
        {
            if (workGroupId == 0)
            {
                throw new ArgumentException("工作组ID参数为空！");
            }

            WorkGroupId = workGroupId;
        }

        /// <summary>
        ///     设置源GUID
        /// </summary>
        /// <param name="id">源GUID</param>
        public void SetSourceGuid(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new ArgumentException("源GUID参数为空！");
            }

            SourceGuid = id;
        }

        /// <summary>
        ///     添加任务案例
        /// </summary>
        /// <param name="description">案例简述</param>
        /// <returns></returns>
        public TaskCase AddTaskCase(string description)
        {
            var taskCase = TaskStandardFactory.CreateTaskCase(description);
            taskCase.TaskStandardId = Id;
            TaskCases.Add(taskCase);
            return taskCase;
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