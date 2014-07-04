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
using System.Security.Cryptography;
using System.Text;
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

        /// <summary>
        ///     新增User。
        /// </summary>
        /// <param name="dto">UserDTO。</param>
        [Insert(typeof (UserDTO))]
        public void InsertUser(UserDTO dto)
        {
            var newUser = UserFactory.CreateUser(dto.UserName, EncodePassword(dto.Password), dto.Question,
                EncodePassword(dto.Answer), DateTime.Now);
            newUser.SetName(string.Empty, string.Empty, dto.DisplayName);
            newUser.SetContact(dto.Email, dto.Mobile);
            newUser.SetComment(dto.Description);
            _userRepository.Add(newUser);
        }


        /// <summary>
        ///     更新User。
        /// </summary>
        /// <param name="user">UserDTO。</param>
        [Update(typeof (UserDTO))]
        public void ModifyUser(UserDTO user)
        {
            var updateUser = _userRepository.Get(user.Id);
            updateUser.SetOrganization(user.OrganizationNo);
            updateUser.SetContact(user.Email, user.Mobile);
            updateUser.SetComment(user.Description);

            var dtoUserRoles = user.UserRoles;
            var userRoles = updateUser.UserRoles;
            DataHelper.DetailHandle(dtoUserRoles.ToArray(),
                userRoles.ToArray(),
                c => c.Id, p => p.Id,
                i => InsertUserRole(updateUser, i),
                UpdateUserRole,
                d => _userRepository.DeleteUserRole(d));
            _userRepository.Modify(updateUser);
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
        private static void InsertUserRole(User user, UserRoleDTO userRoleDto)
        {
            user.AddNewUserRole(userRoleDto.RoleId);
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

        #endregion

        /// <summary>
        ///     创建加密的密码。
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>加密的密码。</returns>
        private static string EncodePassword(string password)
        {
            if (password == null) return null;
            const string validationKey = "C50B3C89CB21F4F1422FF158A5B42D0E8DB8CB5CDA1742572A487";
            var hash = new HMACSHA1 {Key = HexToByte(validationKey + "C50B3C89")};
            var encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
            return encodedPassword;
        }

        /// <summary>
        ///     十六进制转换为字节数组。
        ///     用于转换加密的key。
        /// </summary>
        /// <param name="hexString">十六进制字符串。</param>
        /// <returns>字节数组</returns>
        private static byte[] HexToByte(string hexString)
        {
            var returnBytes = new byte[hexString.Length/2];
            for (var i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i*2, 2), 16);
            return returnBytes;
        }
    }
}