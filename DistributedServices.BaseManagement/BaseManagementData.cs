//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.UserServices;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.DistributedServices.BaseManagement
{
    

    /// <summary>
    /// 基础管理模块数据类
    /// </summary>
    public class BaseManagementData: ExposeData.ExposeData
    {
        private readonly IUserAppService _userAppService;

        public BaseManagementData()
            : base("UniCloud.Application.BaseManagementBC.DTO")
        {
            _userAppService = DefaultContainer.Resolve<IUserAppService>();
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
    }
}