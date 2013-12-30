//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using UniCloud.Application.FleetPlanBC.AircraftServices;
using UniCloud.Application.FleetPlanBC.Query.AircraftQueries;
using UniCloud.Application.FleetPlanBC.Query.XmlConfigQueries;
using UniCloud.Application.FleetPlanBC.Query.XmlSettingQueries;
using UniCloud.Application.FleetPlanBC.XmlConfigServices;
using UniCloud.Application.FleetPlanBC.XmlSettingServices;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlSettingAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.DistributedServices.FleetPlan.InstanceProviders
{
    /// <summary>
    /// DI 容器
    /// </summary>
    public static class Container
    {
        #region 方法

        public static void ConfigureContainer()
        {
            Configuration.Create()
                .UseAutofac()
                //.UserCaching()
                .CreateLog()
                .Register<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())

                #region 实际飞机相关配置，包括查询，应用服务，仓储注册

                .Register<IAircraftQuery, AircraftQuery>()
                .Register<IAircraftAppService, AircraftAppService>()
                .Register<IAircraftRepository, AircraftRepository>()
                #endregion

                #region 分析数据相关的xml相关配置，包括查询，应用服务，仓储注册

                .Register<IXmlConfigQuery, XmlConfigQuery>()
                .Register<IXmlConfigAppService, XmlConfigAppService>()
                .Register<IXmlConfigRepository, XmlConfigRepository>()
                #endregion

                #region 配置相关的xml相关配置，包括查询，应用服务，仓储注册

                .Register<IXmlSettingQuery, XmlSettingQuery>()
                .Register<IXmlSettingAppService, XmlSettingAppService>()
                .Register<IXmlSettingRepository, XmlSettingRepository>()
                #endregion

                ;
        }

        #endregion

    }
}