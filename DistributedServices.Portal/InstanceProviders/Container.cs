#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：DistributedServices.Portal
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Application.PortalBC.Query;
using UniCloud.Application.PortalBC.Services;
using UniCloud.Domain.Events;
using UniCloud.Domain.PortalBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PortalBC.Repositories;
using UniCloud.Infrastructure.Data.PortalBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DistributedServices.Portal.InstanceProviders
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
                .Register<IQueryableUnitOfWork, PortalBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")

                #region 飞机系列相关配置，包括查询，应用服务，仓储注册

                .Register<IPortalAppService, PortalAppService>()
                .Register<IPortalQuery, PortalQuery>()
                .Register<IAircraftSeriesRepository, AircraftSeriesRepository>()

                #endregion

                .Register<IEventAggregator, EventAggregator>(new WcfPerRequestLifetimeManager());
        }

        #endregion
    }
}