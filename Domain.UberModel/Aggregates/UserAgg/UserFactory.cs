#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/03/27，09:03
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.UserAgg
{
    /// <summary>
    ///     User工厂
    /// </summary>
    public static class UserFactory
    {
        public static User CreateUser(string username, string password, string email, int passwordFormat,
            string passwordQuestion, string passwordAnswer, DateTime createDate)
        {
            var user = new User
            {
                UserName = username,
                LoweredUserName = username.ToLower(),
                Email = email,
                PasswordFormat = passwordFormat,
                CreateDate = DateTime.Now,
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