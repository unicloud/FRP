#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：DistributedServices.Project
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.ProjectBC.DTO;
using UniCloud.Application.ProjectBC.RelatedDocServices;
using UniCloud.Application.ProjectBC.TemplateServices;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.Project
{
    /// <summary>
    ///     项目管理模块数据类
    /// </summary>
    public class ProjectData : ExposeData.ExposeData
    {
        private readonly IRelatedDocAppService _relatedDocAppService;
        private readonly ITemplateAppService _templateAppService;

        public ProjectData()
            : base("UniCloud.Application.ProjectBC.DTO")
        {
            _templateAppService = DefaultContainer.Resolve<ITemplateAppService>();
            _relatedDocAppService = DefaultContainer.Resolve<IRelatedDocAppService>();
        }

        #region 关联文档

        /// <summary>
        ///     关联文档集合
        /// </summary>
        public IQueryable<RelatedDocDTO> RelatedDocs
        {
            get { return _relatedDocAppService.GetRelatedDocs(); }
        }

        #endregion

        #region 模板管理

        /// <summary>
        ///     任务模板集合
        /// </summary>
        public IQueryable<TaskStandardDTO> TaskStandards
        {
            get { return _templateAppService.GetTaskStandards(); }
        }

        /// <summary>
        ///     工作组集合
        /// </summary>
        public IQueryable<WorkGroupDTO> WorkGroups
        {
            get { return _templateAppService.GetWorkGroups(); }
        }

        /// <summary>
        ///     用户集合
        /// </summary>
        public IQueryable<UserDTO> Users
        {
            get { return _templateAppService.GetUsers(); }
        }

        #endregion
    }
}