#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/02，22:23
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

namespace UniCloud.Domain.ProjectBC.Aggregates.ProjectTempAgg
{
    /// <summary>
    ///     项目模板工厂
    /// </summary>
    public static class ProjectTempFactory
    {
        /// <summary>
        ///     创建项目模板
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        /// <returns>项目模板</returns>
        public static ProjectTemp CreateProjectTemp(string name, string description)
        {
            var projectTemp = new ProjectTemp
            {
                Name = name,
                Description = description
            };
            projectTemp.GenerateNewIdentity();

            return projectTemp;
        }

        /// <summary>
        ///     创建任务模板
        ///     <remarks>
        ///         添加非标准任务，包括摘要任务、里程碑任务。
        ///     </remarks>
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="start">开始偏移量</param>
        /// <param name="end">结束偏移量</param>
        /// <param name="isSummary">是否摘要任务</param>
        /// <returns>任务模板</returns>
        public static TaskTemp CreateTaskTemp(string subject, TimeSpan start, TimeSpan end, bool isSummary)
        {
            var taskTemp = new TaskTemp
            {
                Subject = subject,
                Start = start,
                End = end,
                IsSummary = isSummary
            };
            taskTemp.GenerateNewIdentity();
            taskTemp.IsMileStone = start == end;

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
        public static TaskTemp CreateTaskTemp(string subject, TimeSpan start, TimeSpan end, bool isSummary,
            int taskStandardId)
        {
            var taskTemp = new TaskTemp
            {
                Subject = subject,
                Start = start,
                End = end,
                IsSummary = isSummary,
                TaskStandardId = taskStandardId
            };
            taskTemp.GenerateNewIdentity();
            taskTemp.IsMileStone = start == end;

            return taskTemp;
        }
    }
}