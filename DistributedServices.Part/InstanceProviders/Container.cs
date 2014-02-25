#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/10，13:11
// 方案：FRP
// 项目：DistributedServices.Part
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using Microsoft.Practices.Unity;
using UniCloud.Application.PartBC.ContractAircraftServices;
using UniCloud.Application.PartBC.Query.ContractAircraftQueries;
using UniCloud.Application.PartBC.Query.ScnQueries;
using UniCloud.Application.PartBC.ScnServices;
using UniCloud.Domain.Events;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.ScnAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PartBC.Repositories;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.Part.InstanceProviders
{
    /// <summary>
    ///     DI 容器
    /// </summary>
    public static class Container
    {
        #region 方法

        public static void ConfigureContainer()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PartBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                //.RegisterType<IEventAggregator, EventAggregator>(new WcfPerRequestLifetimeManager())

            #region 领域事件相关配置

            //.RegisterType<IPartEvent, PartEvent>(new WcfPerRequestLifetimeManager())

            #endregion

            #region SCN相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IScnQuery, ScnQuery>()
                .RegisterType<IScnAppService, ScnAppService>()
                .RegisterType<IScnRepository, ScnRepository>()
            #endregion
            #region ContractAircraft相关配置，包括查询，应用服务，仓储注册

.RegisterType<IContractAircraftQuery, ContractAircraftQuery>()
                .RegisterType<IContractAircraftAppService, ContractAircraftAppService>()
                .RegisterType<IContractAircraftRepository, ContractAircraftRepository>()
            #endregion    
            ;
        }

        #endregion
    }
}