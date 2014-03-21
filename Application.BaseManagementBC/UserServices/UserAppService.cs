#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：chency 时间：2014/2/21 14:58:58

// 文件名：UserAppService
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.Query.UserQueries;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;

#endregion

namespace UniCloud.Application.BaseManagementBC.UserServices
{
    /// <summary>
    /// 实现User的服务接口。
    ///  用于处理User相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class UserAppService : IUserAppService
    {
        private readonly IUserQuery _userQuery;
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserQuery userQuery, IUserRepository userRepository)
        {
            _userQuery = userQuery;
            _userRepository = userRepository;
        }

        #region UserDTO

        /// <summary>
        /// 获取所有User。
        /// </summary>
        public IQueryable<UserDTO> GetUsers()
        {
            var queryBuilder =
               new QueryBuilder<User>();
            return _userQuery.UsersQuery(queryBuilder);
        }

        /// <summary>
        ///     新增User。
        /// </summary>
        /// <param name="user">UserDTO。</param>
        [Insert(typeof(UserDTO))]
        public void InsertUser(UserDTO user)
        {
            var newUser = UserFactory.CreateUser(user.EmployeeCode, string.Empty, string.Empty, user.DisplayName, user.Password, user.Email,
                user.Mobile, user.Description, true, user.CreateDate);
            _userRepository.Add(newUser);
        }


        /// <summary>
        ///     更新User。
        /// </summary>
        /// <param name="user">UserDTO。</param>
        [Update(typeof(UserDTO))]
        public void ModifyUser(UserDTO user)
        {
            var updateUser = _userRepository.Get(user.Id); //获取需要更新的对象。
            UserFactory.SetUser(updateUser, user.Mobile, user.Email);
            _userRepository.Modify(updateUser);
        }

        /// <summary>
        ///     删除User。
        /// </summary>
        /// <param name="user">UserDTO。</param>
        [Delete(typeof(UserDTO))]
        public void DeleteUser(UserDTO user)
        {
            var deleteUser = _userRepository.Get(user.Id); //获取需要删除的对象。
            _userRepository.Remove(deleteUser); //删除User。
        }
        #endregion

    }
}
