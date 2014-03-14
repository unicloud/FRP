//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.FunctionItemServices;
using UniCloud.Application.BaseManagementBC.RoleServices;
using UniCloud.Application.BaseManagementBC.UserServices;
using UniCloud.Infrastructure.Utilities.Container;

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
        public BaseManagementData()
            : base("UniCloud.Application.BaseManagementBC.DTO")
        {
            _userAppService = DefaultContainer.Resolve<IUserAppService>();
            _functionItemAppService = DefaultContainer.Resolve<IFunctionItemAppService>();
            _roleAppService = DefaultContainer.Resolve<IRoleAppService>();
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