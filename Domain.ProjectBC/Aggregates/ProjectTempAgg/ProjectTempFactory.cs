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
    }
}