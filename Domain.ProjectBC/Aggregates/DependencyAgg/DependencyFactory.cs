#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/30，19:40
// 方案：FRP
// 项目：Domain.ProjectBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.ProjectBC.Aggregates.DependencyAgg
{
    /// <summary>
    ///     依赖任务工厂
    /// </summary>
    public static class DependencyFactory
    {
        /// <summary>
        ///     创建依赖任务
        /// </summary>
        /// <param name="scheduleId">任务ID</param>
        /// <param name="dependencyScheduleId">依赖任务ID</param>
        /// <param name="type">依赖类型</param>
        /// <returns></returns>
        public static Dependency CreateDependency(int scheduleId, int dependencyScheduleId, DependencyType type)
        {
            var dependency = new Dependency
            {
                ScheduleId = scheduleId,
            };
            dependency.SetDependencyType(type);
            dependency.SetDependency(dependencyScheduleId);

            return dependency;
        }
    }
}