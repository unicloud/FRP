#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/02，18:12
// 文件名：Container.cs
// 程序集：UniCloud.DistributedServices.CommonService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using UniCloud.Application.CommonServiceBC.DocumentServices;
using UniCloud.Application.CommonServiceBC.DocumnetSearch;
using UniCloud.Application.CommonServiceBC.Query.DocumentQueries;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentPathAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentTypeAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.CommonServiceBC.Repositories;
using UniCloud.Infrastructure.Data.CommonServiceBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DistributedServices.CommonService.InstanceProviders
{
    /// <summary>
    ///     DI 容器
    /// </summary>
    public static class Container
    {
        #region 方法

        public static void ConfigureContainer()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, CommonServiceBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")

                #region 文档相关配置，包括查询，应用服务，仓储注册

                .Register<IDocumentAppService, DocumentAppService>()
                .Register<IDocumentQuery, DocumentQuery>()
                .Register<IDocumentPathRepository, DocumentPathRepository>()
                .Register<IDocumentRepository, DocumentRepository>()
                .Register<IDocumentTypeRepository, DocumentTypeRepository>()
                .Register<IDocumentSearchAppService, DocumentSearchAppService>()
                #endregion

                ;
        }

        #endregion
    }
}