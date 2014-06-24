#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：DistributedServices.AircraftConfig
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

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
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DistributedServices.AircraftConfig.InstanceProviders
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
                .Register<IQueryableUnitOfWork, AircraftConfigBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IEventAggregator, EventAggregator>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")

                #region 活动类型相关配置，包括查询，应用服务，仓储注册

                .Register<IActionCategoryQuery, ActionCategoryQuery>()
                .Register<IActionCategoryAppService, ActionCategoryAppService>()
                .Register<IActionCategoryRepository, ActionCategoryRepository>()
                #endregion

                #region 实际飞机相关配置，包括查询，应用服务，仓储注册

                .Register<IAircraftQuery, AircraftQuery>()
                .Register<IAircraftAppService, AircraftAppService>()
                .Register<IAircraftRepository, AircraftRepository>()
                #endregion

                #region 飞机系列相关配置，包括查询，应用服务，仓储注册

                .Register<IAircraftSeriesQuery, AircraftSeriesQuery>()
                .Register<IAircraftSeriesAppService, AircraftSeriesAppService>()
                .Register<IAircraftSeriesRepository, AircraftSeriesRepository>()
                #endregion

                #region 座级相关配置，包括查询，应用服务，仓储注册

                .Register<IAircraftCategoryQuery, AircraftCategoryQuery>()
                .Register<IAircraftCategoryAppService, AircraftCategoryAppService>()
                .Register<IAircraftCategoryRepository, AircraftCategoryRepository>()
                #endregion

                #region 机型相关配置，包括查询，应用服务，仓储注册

                .Register<IAircraftTypeQuery, AircraftTypeQuery>()
                .Register<IAircraftTypeAppService, AircraftTypeAppService>()
                .Register<IAircraftTypeRepository, AircraftTypeRepository>()
                .Register<ICAACAircraftTypeRepository, CAACAircraftTypeRepository>()
                #endregion

                #region 制造商相关配置，包括查询，应用服务，仓储注册

                .Register<IManufacturerQuery, ManufacturerQuery>()
                .Register<IManufacturerAppService, ManufacturerAppService>()
                .Register<IManufacturerRepository, ManufacturerRepository>()
                #endregion

                #region 飞机证照相关配置，包括查询，应用服务，仓储注册

                .Register<IAircraftLicenseQuery, AircraftLicenseQuery>()
                .Register<IAircraftLicenseAppService, AircraftLicenseAppService>()
                .Register<ILicenseTypeRepository, LicenseTypeRepository>()
                #endregion

                #region 飞机配置相关配置，包括查询，应用服务，仓储注册

                .Register<IAircraftConfigurationQuery, AircraftConfigurationQuery>()
                .Register<IAircraftConfigurationAppService, AircraftConfigurationAppService>()
                .Register<IAircraftConfigurationRepository, AircraftConfigurationRepository>()
                #endregion

                ;
        }

        #endregion
    }
}