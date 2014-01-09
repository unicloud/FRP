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

using Microsoft.Practices.Unity;
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
using UniCloud.Infrastructure.Crosscutting.Logging;
using UniCloud.Infrastructure.Crosscutting.NetFramework.Logging;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.ProjectBC.Repositories;
using UniCloud.Infrastructure.Data.ProjectBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

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
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, ProjectBCUnitOfWork>(new WcfPerRequestLifetimeManager())

                #region 模板管理相关配置注册

                .RegisterType<IProjectTempRepository, ProjectTempRepository>()
                .RegisterType<ITaskStandardRepository, TaskStandardRepository>()
                .RegisterType<IWorkGroupRepository, WorkGroupRepository>()
                .RegisterType<IUserRepository, UserRepository>()
                .RegisterType<ITemplateQuery, TemplateQuery>()
                .RegisterType<ITemplateAppService, TemplateAppService>()
                
                #endregion

                #region 项目管理相关配置注册

                
                #endregion

                #region 关联文档相关配置注册

                .RegisterType<IRelatedDocRepository, RelatedDocRepository>()
                .RegisterType<IRelatedDocQuery, RelatedDocQuery>()
                .RegisterType<IRelatedDocAppService, RelatedDocAppService>()

                #endregion

                .RegisterType<IEventAggregator, EventAggregator>(new WcfPerRequestLifetimeManager());
        }


        private static void ConfigureFactories()
        {
            LoggerFactory.SetCurrent(new UniCloudLogFactory());
        }
    }
}