#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/03/27，09:03
// 方案：FRP
// 项目：Domain.BaseManagementBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg
{
    /// <summary>
    ///     User工厂
    /// </summary>
    public static class UserFactory
    {
        /// <summary>
        ///     创建用户。
        /// </summary>
        /// <param name="username">登录名</param>
        /// <param name="password">加密后的密码</param>
        /// <param name="passwordQuestion">密码问题</param>
        /// <param name="passwordAnswer">密码问题答案</param>
        /// <param name="createDate">创建日期</param>
        /// <param name="passwordFormat">密码格式：0-Clear；1-Hashed；2-Encrypted</param>
        /// <returns>创建的用户</returns>
        public static User CreateUser(string username, string password, string passwordQuestion, string passwordAnswer,
            DateTime createDate, int passwordFormat = 1)
        {
            var user = new User
            {
                UserName = username,
                LoweredUserName = username.ToLower(),
                PasswordFormat = passwordFormat,
                CreateDate = createDate,
                IsLockedOut = false,
                IsValid = true,
                LastLockoutDate = createDate,
                FailedPasswordAttemptCount = 0,
                FailedPasswordAttemptWindowStart = createDate,
                FailedPasswordAnswerAttemptCount = 0,
                FailedPasswordAnswerAttemptWindowStart = createDate,
                LastActivityDate = DateTime.Now,
                LastLoginDate = DateTime.Now,
            };
            user.GenerateNewIdentity();
            user.SetPassword(password);
            user.SetPasswordQuestionAndAnswer(passwordQuestion, passwordAnswer);
            return user;
        }
    }
}