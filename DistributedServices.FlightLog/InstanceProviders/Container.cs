//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using Microsoft.Practices.Unity;
using UniCloud.Application.FlightLogBC.FlightLogServices;
using UniCloud.Application.FlightLogBC.Query.FlightLogQueries;
using UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FlightLogBC.Repositories;
using UniCloud.Infrastructure.Data.FlightLogBC.UnitOfWork.Mapping;
using UniCloud.Infrastructure.Utilities.Container;

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
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, FlightLogBCUnitOfWork>(new WcfPerRequestLifetimeManager())

                #region 飞行日志相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IFlightLogAppService, FlightLogAppService>()
                .RegisterType<IFlightLogRepository, FlightLogRepository>()
                .RegisterType<IFlightLogQuery, FlightLogQuery>()
                #endregion

                ;
        }

        #endregion
    }
}