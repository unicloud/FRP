//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

#region 命名空间

using Microsoft.Practices.Unity;
using UniCloud.Application.BaseManagementBC.AircraftCabinTypeServices;
using UniCloud.Application.BaseManagementBC.BusinessLicenseServices;
using UniCloud.Application.BaseManagementBC.FunctionItemServices;
using UniCloud.Application.BaseManagementBC.OrganizationServices;
using UniCloud.Application.BaseManagementBC.Query.AircraftCabinTypeQueries;
using UniCloud.Application.BaseManagementBC.Query.BusinessLicenseQueries;
using UniCloud.Application.BaseManagementBC.Query.FunctionItemQueries;
using UniCloud.Application.BaseManagementBC.Query.OrganizationQueries;
using UniCloud.Application.BaseManagementBC.Query.RoleQueries;
using UniCloud.Application.BaseManagementBC.Query.UserQueries;
using UniCloud.Application.BaseManagementBC.RoleServices;
using UniCloud.Application.BaseManagementBC.UserServices;
using UniCloud.Domain.BaseManagementBC.Aggregates.AircraftCabinTypeAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.BusinessLicenseAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.BaseManagementBC.Repositories;
using UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.BaseManagement.InstanceProviders
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
                .RegisterType<IQueryableUnitOfWork, BaseManagementBCUnitOfWork>(new WcfPerRequestLifetimeManager())
            #region User相关配置，包括查询，应用服务，仓储注册
.RegisterType<IUserAppService, UserAppService>()
                         .RegisterType<IUserQuery, UserQuery>()
                         .RegisterType<IUserRepository, UserRepository>()
            #endregion

            #region Organization相关配置，包括查询，应用服务，仓储注册
.RegisterType<IOrganizationAppService, OrganizationAppService>()
                         .RegisterType<IOrganizationQuery, OrganizationQuery>()
                         .RegisterType<IOrganizationRepository, OrganizationRepository>()
            #endregion
            #region FunctionItem相关配置，包括查询，应用服务，仓储注册
.RegisterType<IFunctionItemAppService, FunctionItemAppService>()
                         .RegisterType<IFunctionItemQuery, FunctionItemQuery>()
                         .RegisterType<IFunctionItemRepository, FunctionItemRepository>()
            #endregion

            #region Role相关配置，包括查询，应用服务，仓储注册
.RegisterType<IRoleAppService, RoleAppService>()
                         .RegisterType<IRoleQuery, RoleQuery>()
                         .RegisterType<IRoleRepository, RoleRepository>()
            #endregion

            #region BusinessLicense相关配置，包括查询，应用服务，仓储注册
.RegisterType<IBusinessLicenseAppService, BusinessLicenseAppService>()
                         .RegisterType<IBusinessLicenseQuery, BusinessLicenseQuery>()
                         .RegisterType<IBusinessLicenseRepository, BusinessLicenseRepository>()
            #endregion

            #region AircraftCabinType相关配置，包括查询，应用服务，仓储注册
.RegisterType<IAircraftCabinTypeAppService, AircraftCabinTypeAppService>()
                         .RegisterType<IAircraftCabinTypeQuery, AircraftCabinTypeQuery>()
                         .RegisterType<IAircraftCabinTypeRepository, AircraftCabinTypeRepository>()
            #endregion
;
        }

        #endregion

    }
}