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

using System.Linq;
using UniCloud.Application.BaseManagementBC.AircraftCabinTypeServices;
using UniCloud.Application.BaseManagementBC.BusinessLicenseServices;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.FunctionItemServices;
using UniCloud.Application.BaseManagementBC.OrganizationServices;
using UniCloud.Application.BaseManagementBC.RoleServices;
using UniCloud.Application.BaseManagementBC.UserServices;
using UniCloud.Application.BaseManagementBC.XmlSettingServices;
using UniCloud.DistributedServices.Data;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DistributedServices.BaseManagement
{
    /// <summary>
    ///     基础管理模块数据类
    /// </summary>
    public class BaseManagementData : ServiceData
    {
        private readonly IAircraftCabinTypeAppService _aircraftCabinTypeAppService;
        private readonly IBusinessLicenseAppService _businessLicenseAppService;
        private readonly IFunctionItemAppService _functionItemAppService;
        private readonly IOrganizationAppService _organizationAppService;
        private readonly IRoleAppService _roleAppService;
        private readonly IUserAppService _userAppService;
        private readonly IXmlSettingAppService _xmlSettingAppService;

        public BaseManagementData()
            : base("UniCloud.Application.BaseManagementBC.DTO", UniContainer.Resolve<IQueryableUnitOfWork>())
        {
            _userAppService = UniContainer.Resolve<IUserAppService>();
            _functionItemAppService = UniContainer.Resolve<IFunctionItemAppService>();
            _roleAppService = UniContainer.Resolve<IRoleAppService>();
            _organizationAppService = UniContainer.Resolve<IOrganizationAppService>();
            _businessLicenseAppService = UniContainer.Resolve<IBusinessLicenseAppService>();
            _aircraftCabinTypeAppService = UniContainer.Resolve<IAircraftCabinTypeAppService>();
            _xmlSettingAppService = UniContainer.Resolve<IXmlSettingAppService>();
        }

        #region User集合

        /// <summary>
        ///     User集合
        /// </summary>
        public IQueryable<UserDTO> Users
        {
            get { return _userAppService.GetUsers(); }
        }

        #endregion

        #region Organization集合

        /// <summary>
        ///     Organization集合
        /// </summary>
        public IQueryable<OrganizationDTO> Organizations
        {
            get { return _organizationAppService.GetOrganizations(); }
        }

        #endregion

        #region FunctionItem集合

        /// <summary>
        ///     FunctionItem集合
        /// </summary>
        public IQueryable<FunctionItemDTO> FunctionItems
        {
            get { return _functionItemAppService.GetFunctionItems(); }
        }

        #endregion

        #region Role集合

        public IQueryable<RoleDTO> Roles
        {
            get { return _roleAppService.GetRoles(); }
        }

        #endregion

        #region RoleBusinessLicense集合

        public IQueryable<BusinessLicenseDTO> BusinessLicenses
        {
            get { return _businessLicenseAppService.GetBusinessLicenses(); }
        }

        #endregion

        #region 飞机舱位类型集合

        /// <summary>
        ///     飞机舱位类型集合
        /// </summary>
        public IQueryable<AircraftCabinTypeDTO> AircraftCabinTypes
        {
            get { return _aircraftCabinTypeAppService.GetAircraftCabinTypes(); }
        }

        #endregion

        #region 配置相关的xml

        /// <summary>
        ///     配置相关的xml集合
        /// </summary>
        public IQueryable<XmlSettingDTO> XmlSettings
        {
            get { return _xmlSettingAppService.GetXmlSettings(); }
        }

        #endregion
    }
}