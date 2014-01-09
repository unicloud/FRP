#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/09，11:01
// 方案：FRP
// 项目：Application.ProjectBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.ProjectBC.DTO;

#endregion

namespace UniCloud.Application.ProjectBC.TemplateServices
{
    /// <summary>
    ///     项目管理模板接口
    /// </summary>
    public interface ITemplateAppService
    {
        /// <summary>
        ///     获取任务模板
        /// </summary>
        /// <returns>任务模板集合</returns>
        IQueryable<TaskStandardDTO> GetTaskStandards();

        /// <summary>
        ///     获取工作组
        /// </summary>
        /// <returns>工作组集合</returns>
        IQueryable<WorkGroupDTO> GetWorkGroups();
    }
}