#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：DistributedServices.FlightLog
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Application.FlightLogBC.FlightLogServices;
using UniCloud.Application.FlightLogBC.Query.FlightLogQueries;
using UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FlightLogBC.Repositories;
using UniCloud.Infrastructure.Data.FlightLogBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DistributedServices.FlightLog.InstanceProviders
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
                .Register<IQueryableUnitOfWork, FlightLogBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")

                #region 飞行日志相关配置，包括查询，应用服务，仓储注册

                .Register<IFlightLogAppService, FlightLogAppService>()
                .Register<IFlightLogRepository, FlightLogRepository>()
                .Register<IFlightLogQuery, FlightLogQuery>()
                #endregion

                ;
        }

        #endregion
    }
}