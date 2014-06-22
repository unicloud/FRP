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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.Query.UserQueries;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserRoleAgg;

#endregion

namespace UniCloud.Application.BaseManagementBC.UserServices
{
    /// <summary>
    ///     实现User的服务接口。
    ///     用于处理User相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class UserAppService : ContextBoundObject, IUserAppService
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
        ///     获取所有User。
        /// </summary>
        public IQueryable<UserDTO> GetUsers()
        {
            var queryBuilder = new QueryBuilder<User>();
            return _userQuery.UsersQuery(queryBuilder);
        }

        public void CreateUser(string userName, string password, string email, string question, string answer)
        {
            MembershipCreateStatus createStatus;
            Membership.CreateUser(userName, password, email, question, answer, true, null, out createStatus);
            _userRepository.UnitOfWork.Commit();
        }

        /// <summary>
        ///     新增User。
        /// </summary>
        /// <param name="user">UserDTO。</param>
        [Insert(typeof (UserDTO))]
        public void InsertUser(UserDTO user)
        {
            //User newUser = UserFactory.CreateUser(user.EmployeeCode, user.OrganizationNo, string.Empty, string.Empty,
            //    user.DisplayName, user.Password, user.Email,
            //    user.Mobile, user.Description, true);
            //_userRepository.Add(newUser);
        }


        /// <summary>
        ///     更新User。
        /// </summary>
        /// <param name="user">UserDTO。</param>
        [Update(typeof (UserDTO))]
        public void ModifyUser(UserDTO user)
        {
            //User updateUser = _userRepository.Get(user.Id); //获取需要更新的对象。
            //UserFactory.SetUser(updateUser, user.EmployeeCode, user.OrganizationNo, string.Empty, string.Empty,
            //    user.DisplayName, user.Password, user.Email,
            //    user.Mobile, user.Description, true);

            //List<UserRoleDTO> dtoUserRoles = user.UserRoles;
            //ICollection<UserRole> userRoles = updateUser.UserRoles;
            //DataHelper.DetailHandle(dtoUserRoles.ToArray(),
            //    userRoles.ToArray(),
            //    c => c.Id, p => p.Id,
            //    i => InsertUserRole(updateUser, i),
            //    UpdateUserRole,
            //    d => _userRepository.DeleteUserRole(d));
            //_userRepository.Modify(updateUser);
        }

        /// <summary>
        ///     删除User。
        /// </summary>
        /// <param name="user">UserDTO。</param>
        [Delete(typeof (UserDTO))]
        public void DeleteUser(UserDTO user)
        {
            var deleteUser = _userRepository.Get(user.Id); //获取需要删除的对象。
            _userRepository.Remove(deleteUser); //删除User。
        }

        /// <summary>
        ///     插入UserRole
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="userRoleDto">UserRoleDTO</param>
        private void InsertUserRole(User user, UserRoleDTO userRoleDto)
        {
            // 添加UserRole
            //UserRole userRole = user.AddNewUserRole();
            //UserFactory.SetUserRole(userRole, userRoleDto.UserId, userRoleDto.RoleId);
        }

        /// <summary>
        ///     更新UserRole
        /// </summary>
        /// <param name="userRoleDto">UserRoleDTO</param>
        /// <param name="userRole">UserRole</param>
        private void UpdateUserRole(UserRoleDTO userRoleDto, UserRole userRole)
        {
            // 更新UserRole
            //UserFactory.SetUserRole(userRole, userRoleDto.UserId, userRoleDto.RoleId);
        }

        public void SyncUserInfo(List<UserDTO> users)
        {
            //foreach (UserDTO user in users)
            //{
            //    Expression<Func<User, bool>> condition = p => p.EmployeeCode == user.EmployeeCode;

            //    User tempUser = _userRepository.GetUser(condition);
            //    if (tempUser != null)
            //    {
            //        UserFactory.SetUser(tempUser, user.EmployeeCode, user.OrganizationNo, string.Empty, string.Empty,
            //            user.DisplayName, user.Password, user.Email,
            //            user.Mobile, user.Description, true);
            //        _userRepository.Modify(tempUser);
            //    }
            //    else
            //    {
            //        InsertUser(user);
            //    }
            //}
            //_userRepository.UnitOfWork.CommitAndRefreshChanges();
        }

        #endregion
    }
}