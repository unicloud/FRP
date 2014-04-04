//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

#region 命名空间

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.FunctionItemServices;
using UniCloud.Application.BaseManagementBC.OrganizationServices;
using UniCloud.Application.BaseManagementBC.RoleServices;
using UniCloud.Application.BaseManagementBC.UserServices;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.BaseManagement
{


    /// <summary>
    /// 基础管理模块数据类
    /// </summary>
    public class BaseManagementData : ExposeData.ExposeData
    {
        private readonly IUserAppService _userAppService;
        private readonly IFunctionItemAppService _functionItemAppService;
        private readonly IRoleAppService _roleAppService;
        private readonly IOrganizationAppService _organizationAppService;

        public BaseManagementData()
            : base("UniCloud.Application.BaseManagementBC.DTO")
        {
            _userAppService = DefaultContainer.Resolve<IUserAppService>();
            _functionItemAppService = DefaultContainer.Resolve<IFunctionItemAppService>();
            _roleAppService = DefaultContainer.Resolve<IRoleAppService>();
            _organizationAppService = DefaultContainer.Resolve<IOrganizationAppService>();
        }


        #region User集合
        /// <summary>
        /// User集合
        /// </summary>
        public IQueryable<UserDTO> Users
        {
            get { return _userAppService.GetUsers(); }
        }
        #endregion

        #region Organization集合
        /// <summary>
        /// Organization集合
        /// </summary>
        public IQueryable<OrganizationDTO> Organizations
        {
            get { return _organizationAppService.GetOrganizations(); }
        }
        #endregion


        #region FunctionItem集合
        /// <summary>
        /// FunctionItem集合
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
    }
}