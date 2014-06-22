#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：DistributedServices.BaseManagement
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

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
using UniCloud.Application.BaseManagementBC.Query.XmlSettingQueries;
using UniCloud.Application.BaseManagementBC.RoleServices;
using UniCloud.Application.BaseManagementBC.UserServices;
using UniCloud.Application.BaseManagementBC.XmlSettingServices;
using UniCloud.Domain.BaseManagementBC.Aggregates.AircraftCabinTypeAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.BusinessLicenseAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleFunctionAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserRoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.XmlSettingAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.BaseManagementBC.Repositories;
using UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.BaseManagement.InstanceProviders
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
                .Register<IQueryableUnitOfWork, BaseManagementBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")

                #region User相关配置，包括查询，应用服务，仓储注册

                .Register<IUserAppService, UserAppService>()
                .Register<IUserQuery, UserQuery>()
                .Register<IUserRepository, UserRepository>()
                .Register<IUserRoleRepository, UserRoleRepository>()

                #endregion

                #region Organization相关配置，包括查询，应用服务，仓储注册

                .Register<IOrganizationAppService, OrganizationAppService>()
                .Register<IOrganizationQuery, OrganizationQuery>()
                .Register<IOrganizationRepository, OrganizationRepository>()

                #endregion

                #region FunctionItem相关配置，包括查询，应用服务，仓储注册

                .Register<IFunctionItemAppService, FunctionItemAppService>()
                .Register<IFunctionItemQuery, FunctionItemQuery>()
                .Register<IFunctionItemRepository, FunctionItemRepository>()
                .Register<IRoleFunctionRepository, RoleFunctionRepository>()

                #endregion

                #region Role相关配置，包括查询，应用服务，仓储注册

                .Register<IRoleAppService, RoleAppService>()
                .Register<IRoleQuery, RoleQuery>()
                .Register<IRoleRepository, RoleRepository>()

                #endregion

                #region BusinessLicense相关配置，包括查询，应用服务，仓储注册

                .Register<IBusinessLicenseAppService, BusinessLicenseAppService>()
                .Register<IBusinessLicenseQuery, BusinessLicenseQuery>()
                .Register<IBusinessLicenseRepository, BusinessLicenseRepository>()

                #endregion

                #region AircraftCabinType相关配置，包括查询，应用服务，仓储注册

                .Register<IAircraftCabinTypeAppService, AircraftCabinTypeAppService>()
                .Register<IAircraftCabinTypeQuery, AircraftCabinTypeQuery>()
                .Register<IAircraftCabinTypeRepository, AircraftCabinTypeRepository>()

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