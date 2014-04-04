#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/13 16:11:22
// 文件名：UserFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/13 16:11:22
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserRoleAgg;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg
{
    /// <summary>
    ///     文档工厂
    /// </summary>
    public static class UserFactory
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="employeeCode">员工号</param>
        /// <param name="organizationNo">组织机构</param>
        /// <param name="firstName">名</param>
        /// <param name="lastName">姓</param>
        /// <param name="displayName">全名</param>
        /// <param name="password">密码</param>
        /// <param name="email">邮箱</param>
        /// <param name="mobile">手机号</param>
        /// <param name="description">描述</param>
        /// <param name="isValid">是否有效</param>
        /// <returns></returns>
        public static User CreateUser(string employeeCode, string organizationNo, string firstName, string lastName, string displayName, string password,
            string email, string mobile, string description, bool isValid)
        {
            var user = new User
                       {
                           EmployeeCode = employeeCode,
                           OrganizationNo = organizationNo,
                           FirstName = firstName,
                           LastName = lastName,
                           CreateDate = DateTime.Now,
                           Description = description,
                           DisplayName = displayName,
                           Password = password,
                           Email = email,
                           IsValid = isValid,
                           Mobile = mobile,
                       };
            user.GenerateNewIdentity();
            return user;
        }

        /// <summary>
        /// 设置用户属性
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="employeeCode">员工号</param>
        /// <param name="organizationNo">组织机构</param>
        /// <param name="firstName">名</param>
        /// <param name="lastName">姓</param>
        /// <param name="displayName">全名</param>
        /// <param name="password">密码</param>
        /// <param name="email">邮箱</param>
        /// <param name="mobile">手机号</param>
        /// <param name="description">描述</param>
        /// <param name="isValid">是否有效</param>
        public static void SetUser(User user, string employeeCode, string organizationNo, string firstName, string lastName, string displayName, string password,
            string email, string mobile, string description, bool isValid)
        {
            user.EmployeeCode = employeeCode;
            user.OrganizationNo = organizationNo;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.DisplayName = displayName;
            user.Password = password;
            user.Mobile = mobile;
            user.Email = email;
            user.Description = description;
            user.IsValid = isValid;
        }

       /// <summary>
       /// 设置UserRole
       /// </summary>
       /// <param name="userRole">用户角色</param>
       /// <param name="userId">用户</param>
       /// <param name="roleId">角色</param>
        public static void SetUserRole(UserRole userRole, int userId, int roleId)
        {
            userRole.UserId = userId;
            userRole.RoleId = roleId;
        }
    }
}
