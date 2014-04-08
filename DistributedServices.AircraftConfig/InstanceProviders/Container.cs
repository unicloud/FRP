//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using UniCloud.Application.AircraftConfigBC.ActionCategoryServices;
using UniCloud.Application.AircraftConfigBC.AircraftCategoryServices;
using UniCloud.Application.AircraftConfigBC.AircraftConfigurationServices;
using UniCloud.Application.AircraftConfigBC.AircraftLicenseServices;
using UniCloud.Application.AircraftConfigBC.AircraftSeriesServices;
using UniCloud.Application.AircraftConfigBC.AircraftServices;
using UniCloud.Application.AircraftConfigBC.AircraftTypeServices;
using UniCloud.Application.AircraftConfigBC.ManufacturerServices;
using UniCloud.Application.AircraftConfigBC.Query.ActionCategoryQueries;
using UniCloud.Application.AircraftConfigBC.Query.AircraftCategoryQueries;
using UniCloud.Application.AircraftConfigBC.Query.AircraftConfigurationQueries;
using UniCloud.Application.AircraftConfigBC.Query.AircraftLicenseQueries;
using UniCloud.Application.AircraftConfigBC.Query.AircraftQueries;
using UniCloud.Application.AircraftConfigBC.Query.AircraftSeriesQueries;
using UniCloud.Application.AircraftConfigBC.Query.AircraftTypeQueries;
using UniCloud.Application.AircraftConfigBC.Query.ManufacturerQueries;
using UniCloud.Domain.AircraftConfigBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftCabinTypeAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftConfigurationAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.CAACAircraftTypeAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.LicenseTypeAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.ManufacturerAgg;
using UniCloud.Domain.Events;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.AircraftConfigBC.Repositories;
using UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;
namespace UniCloud.DistributedServices.AircraftConfig.InstanceProviders
{

    /// <summary>
    /// DI 容器
    /// </summary>
    public static class Container
    {
        #region 方法

        public static void ConfigureContainer()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, AircraftConfigBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<IEventAggregator, EventAggregator>(new WcfPerRequestLifetimeManager())

            #region 活动类型相关配置，包括查询，应用服务，仓储注册

.RegisterType<IActionCategoryQuery, ActionCategoryQuery>()
                .RegisterType<IActionCategoryAppService, ActionCategoryAppService>()
                .RegisterType<IActionCategoryRepository, ActionCategoryRepository>()
            #endregion

            #region 实际飞机相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAircraftQuery, AircraftQuery>()
                .RegisterType<IAircraftAppService, AircraftAppService>()
                .RegisterType<IAircraftRepository, AircraftRepository>()
            #endregion

            #region 飞机系列相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAircraftSeriesQuery, AircraftSeriesQuery>()
                .RegisterType<IAircraftSeriesAppService, AircraftSeriesAppService>()
                .RegisterType<IAircraftSeriesRepository, AircraftSeriesRepository>()
            #endregion

            #region 座级相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAircraftCategoryQuery, AircraftCategoryQuery>()
                .RegisterType<IAircraftCategoryAppService, AircraftCategoryAppService>()
                .RegisterType<IAircraftCategoryRepository, AircraftCategoryRepository>()
            #endregion

            #region 机型相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAircraftTypeQuery, AircraftTypeQuery>()
                .RegisterType<IAircraftTypeAppService, AircraftTypeAppService>()
                .RegisterType<IAircraftTypeRepository, AircraftTypeRepository>()
                .RegisterType<ICAACAircraftTypeRepository, CAACAircraftTypeRepository>()
            #endregion

            #region 制造商相关配置，包括查询，应用服务，仓储注册

.RegisterType<IManufacturerQuery, ManufacturerQuery>()
                .RegisterType<IManufacturerAppService, ManufacturerAppService>()
                .RegisterType<IManufacturerRepository, ManufacturerRepository>()
            #endregion

            #region 飞机证照相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAircraftLicenseQuery, AircraftLicenseQuery>()
                .RegisterType<IAircraftLicenseAppService, AircraftLicenseAppService>()
                .RegisterType<ILicenseTypeRepository, LicenseTypeRepository>()
            #endregion

            #region 飞机配置相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAircraftConfigurationQuery, AircraftConfigurationQuery>()
                .RegisterType<IAircraftConfigurationAppService, AircraftConfigurationAppService>()
                .RegisterType<IAircraftConfigurationRepository, AircraftConfigurationRepository>()
            #endregion

;
        }

        #endregion

    }
}