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

using UniCloud.Application.ProjectBC.Query.RelatedDocQueries;
using UniCloud.Application.ProjectBC.Query.TemplateQueries;
using UniCloud.Application.ProjectBC.RelatedDocServices;
using UniCloud.Application.ProjectBC.TemplateServices;
using UniCloud.Domain.Events;
using UniCloud.Domain.ProjectBC.Aggregates.ProjectTempAgg;
using UniCloud.Domain.ProjectBC.Aggregates.RelatedDocAgg;
using UniCloud.Domain.ProjectBC.Aggregates.TaskStandardAgg;
using UniCloud.Domain.ProjectBC.Aggregates.UserAgg;
using UniCloud.Domain.ProjectBC.Aggregates.WorkGroupAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.ProjectBC.Repositories;
using UniCloud.Infrastructure.Data.ProjectBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DistributedServices.Project.InstanceProviders
{
    /// <summary>
    ///     DI 容器
    /// </summary>
    public static class Container
    {
        public static void ConfigureContainer()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, ProjectBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")

                #region 模板管理相关配置注册

                .Register<IProjectTempRepository, ProjectTempRepository>()
                .Register<ITaskStandardRepository, TaskStandardRepository>()
                .Register<IWorkGroupRepository, WorkGroupRepository>()
                .Register<IUserRepository, UserRepository>()
                .Register<ITemplateQuery, TemplateQuery>()
                .Register<ITemplateAppService, TemplateAppService>()
                
                #endregion

                #region 项目管理相关配置注册

                
                #endregion

                #region 关联文档相关配置注册

                .Register<IRelatedDocRepository, RelatedDocRepository>()
                .Register<IRelatedDocQuery, RelatedDocQuery>()
                .Register<IRelatedDocAppService, RelatedDocAppService>()

                #endregion

                .Register<IEventAggregator, EventAggregator>(new WcfPerRequestLifetimeManager());
        }
    }
}