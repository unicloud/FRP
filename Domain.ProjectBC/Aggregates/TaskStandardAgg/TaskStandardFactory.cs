#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/02，21:00
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
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.ProjectBC.Aggregates.TaskStandardAgg
{
    /// <summary>
    ///     任务标准工厂
    /// </summary>
    public static class TaskStandardFactory
    {
        /// <summary>
        ///     创建任务标准
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        /// <param name="optimistic">乐观时间</param>
        /// <param name="pessimistic">悲观时间</param>
        /// <param name="nomal">正常时间</param>
        /// <param name="isCustom">是否自定义</param>
        /// <param name="taskType">任务类型</param>
        /// <returns>新的标准任务</returns>
        public static TaskStandard CreateTaskStandard(string name, string description, TimeSpan optimistic,
            TimeSpan pessimistic, TimeSpan nomal, bool isCustom, TaskType taskType)
        {
            var task = new TaskStandard
            {
                Name = name,
                Description = description,
                OptimisticTime = optimistic,
                PessimisticTime = pessimistic,
                NormalTime = nomal,
                IsCustom = isCustom,
                TaskType = taskType
            };
            task.GenerateNewIdentity();

            return task;
        }

        /// <summary>
        ///     创建任务案例
        /// </summary>
        /// <param name="description">案例简述</param>
        /// <returns>任务案例</returns>
        public static TaskCase CreateTaskCase(string description)
        {
            var taskCase = new TaskCase
            {
                Description = description
            };
            taskCase.GenerateNewIdentity();
            return taskCase;
        }
    }
}