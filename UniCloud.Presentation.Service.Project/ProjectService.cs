#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/09，15:04
// 方案：FRP
// 项目：Service.Project
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using UniCloud.Presentation.Service.Project.Project;

#endregion

namespace UniCloud.Presentation.Service.Project
{
    [Export(typeof (IProjectService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectService : ServiceBase, IProjectService
    {
        public ProjectService()
        {
            context = new ProjectData(AgentHelper.ProjectServiceUri);
        }

        #region IProjectService 成员

        public ProjectData Context
        {
            get { return context as ProjectData; }
        }

        #endregion
    }
}