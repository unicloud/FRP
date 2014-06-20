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
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, BaseManagementBCUnitOfWork>(new WcfPerRequestLifetimeManager())

                #region User相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IUserAppService, UserAppService>()
                .RegisterType<IUserQuery, UserQuery>()
                .RegisterType<IUserRepository, UserRepository>()
                .RegisterType<IUserRoleRepository, UserRoleRepository>()

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
                .RegisterType<IRoleFunctionRepository, RoleFunctionRepository>()

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

                #region 配置相关的xml相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IXmlSettingQuery, XmlSettingQuery>()
                .RegisterType<IXmlSettingAppService, XmlSettingAppService>()
                .RegisterType<IXmlSettingRepository, XmlSettingRepository>()

                #endregion

                ;
        }

        #endregion
    }
}