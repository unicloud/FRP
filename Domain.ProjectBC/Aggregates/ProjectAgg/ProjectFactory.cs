#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/02，22:41
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

#endregion

namespace UniCloud.Domain.ProjectBC.Aggregates.ProjectAgg
{
    /// <summary>
    ///     项目工厂
    /// </summary>
    public static class ProjectFactory
    {
        /// <summary>
        ///     创建项目
        /// </summary>
        /// <param name="name">项目名称</param>
        /// <param name="description">项目描述</param>
        /// <param name="start">计划开始时间</param>
        /// <param name="end">计划结束时间</param>
        /// <returns>项目</returns>
        public static Project CreateProject(string name, string description, DateTime start, DateTime end)
        {
            var project = new Project
            {
                Name = name,
                Description = description,
                PlannedStart = start,
                PlannedEnd = end
            };
            project.GenerateNewIdentity();

            return project;
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
        public static Task CreateTask(string subject, string body, string importance, string tempo, DateTime start,
            DateTime end, bool isAllDay, TimeSpan duration, DateTime deadLine, bool isSummary)
        {
            var task = new Task
            {
                Duration = duration,
                DeadLine = deadLine,
                IsSummary = isSummary
            };
            task.GenerateNewIdentity();
            task.IsMileStone = start == end;
            task.SetSchedule(subject, body, importance, tempo, start, end, isAllDay);

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
        public static Task CreateTask(string subject, string body, string importance, string tempo, DateTime start,
            DateTime end, bool isAllDay, TimeSpan duration, DateTime deadLine, bool isSummary, int taskStandardId)
        {
            var task = new Task
            {
                Duration = duration,
                DeadLine = deadLine,
                IsSummary = isSummary,
                TaskStandardId = taskStandardId
            };
            task.GenerateNewIdentity();
            task.IsMileStone = start == end;
            task.SetSchedule(subject, body, importance, tempo, start, end, isAllDay);

            return task;
        }

    }
}