#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/17 16:30:27
// 文件名：UserFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/17 16:30:27
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Domain.UberModel.Aggregates.UserAgg
{
   public static class UserFactory
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="employeeCode">员工号</param>
        /// <param name="firstName">名</param>
        /// <param name="lastName">姓</param>
        /// <param name="displayName">全名</param>
        /// <param name="password">密码</param>
        /// <param name="email">邮箱</param>
        /// <param name="mobile">手机号</param>
        /// <param name="description">描述</param>
        /// <param name="isValid">是否有效</param>
        /// <returns></returns>
        public static User CreateUser(string employeeCode, string firstName, string lastName, string displayName, string password,
            string email, string mobile, string description, bool isValid)
        {
            var user = new User
            {
                EmployeeCode = employeeCode,
                FirstName = firstName,
                LastName = lastName,
                CreateDate = DateTime.Now,
                Description = description,
                DisplayName = displayName,
                Email = email,
                IsValid = isValid,
                Mobile = mobile,
            };
            user.GenerateNewIdentity();
            return user;
        }
    }
}
