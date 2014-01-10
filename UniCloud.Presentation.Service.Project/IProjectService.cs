#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/09，15:01
// 方案：FRP
// 项目：Service.Project
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Presentation.Service.Project.Project;

#endregion

namespace UniCloud.Presentation.Service.Project
{
    public interface IProjectService : IService
    {
        /// <summary>
        ///     数据服务上下文
        /// </summary>
        ProjectData Context { get; }
    }
}