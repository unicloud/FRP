﻿#region 版本控制

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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserRoleAgg;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg
{
    public class User : EntityInt, IValidatableObject
    {
        private ICollection<UserRole> _userRoles;

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal User()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     用户名称
        /// </summary>
        /// <remarks>
        ///     员工工号
        /// </remarks>
        public string UserName { get; internal set; }

        /// <summary>
        ///     用户名称小写
        /// </summary>
        public string LoweredUserName { get; internal set; }

        /// <summary>
        ///     组织机构名称
        /// </summary>
        public string OrganizationNo { get; private set; }

        /// <summary>
        ///     名
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        ///     姓
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        ///     显示名称
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        ///     密码
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        ///     密码格式
        /// </summary>
        public int PasswordFormat { get; internal set; }

        /// <summary>
        ///     密码问题
        /// </summary>
        public string PasswordQuestion { get; private set; }

        /// <summary>
        ///     密码问题答案
        /// </summary>
        public string PasswordAnswer { get; private set; }

        /// <summary>
        ///     邮件
        /// </summary>
        public string Email { get; internal set; }

        /// <summary>
        ///     手机号码
        /// </summary>
        public string Mobile { get; private set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Comment { get; internal set; }

        /// <summary>
        ///     是否批准
        /// </summary>
        public bool IsApproved { get; private set; }

        /// <summary>
        ///     是否可用
        /// </summary>
        public bool IsValid { get; internal set; }

        /// <summary>
        ///     是否锁定
        /// </summary>
        /// <remarks>
        ///     密码重试超过限制后会锁定账户。
        /// </remarks>
        public bool IsLockedOut { get; internal set; }

        /// <summary>
        ///     是否系统用户
        /// </summary>
        public bool IsSystemUser { get; internal set; }

        /// <summary>
        ///     密码重试次数
        /// </summary>
        public int FailedPasswordAttemptCount { get; set; }

        /// <summary>
        ///     密码问题答案重试次数
        /// </summary>
        public int FailedPasswordAnswerAttemptCount { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     最近修改密码时间
        /// </summary>
        public DateTime LastPasswordChangedDate { get; private set; }

        /// <summary>
        ///     最近锁定账户时间
        /// </summary>
        public DateTime LastLockoutDate { get; set; }

        /// <summary>
        ///     密码重试开始时间
        /// </summary>
        public DateTime FailedPasswordAttemptWindowStart { get; set; }

        /// <summary>
        ///     密码答案重试开始时间
        /// </summary>
        public DateTime FailedPasswordAnswerAttemptWindowStart { get; set; }

        /// <summary>
        ///     最近活动时间
        /// </summary>
        public DateTime LastActivityDate { get; set; }

        /// <summary>
        ///     最近登录时间
        /// </summary>
        public DateTime LastLoginDate { get; set; }

        #endregion

        #region 外键

        #endregion

        #region 导航

        /// <summary>
        ///     用户角色
        /// </summary>
        public virtual ICollection<UserRole> UserRoles
        {
            get { return _userRoles ?? (_userRoles = new HashSet<UserRole>()); }
            set { _userRoles = new HashSet<UserRole>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置组织代码
        /// </summary>
        /// <param name="organizationNo">机构代码</param>
        public void SetOrganization(string organizationNo)
        {
            OrganizationNo = organizationNo;
        }

        /// <summary>
        ///     设置姓名
        /// </summary>
        /// <param name="firstName">名字</param>
        /// <param name="lastName">姓氏</param>
        /// <param name="displayName">显示名称</param>
        public void SetName(string firstName, string lastName, string displayName)
        {
            FirstName = firstName;
            LastName = lastName;
            DisplayName = displayName;
        }

        /// <summary>
        ///     设置联系方式
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="mobile">手机号</param>
        public void SetContact(string email, string mobile)
        {
            Email = email;
            Mobile = mobile;
        }

        /// <summary>
        ///     修改密码
        /// </summary>
        /// <param name="newPassword">新密码</param>
        public void SetPassword(string newPassword)
        {
            Password = newPassword;
            LastPasswordChangedDate = DateTime.Now;
        }

        /// <summary>
        ///     修改密码问题与答案。
        /// </summary>
        /// <param name="newPasswordQuestion">新的密码问题</param>
        /// <param name="newPasswordAnswer">新的密码问题答案</param>
        public void SetPasswordQuestionAndAnswer(string newPasswordQuestion, string newPasswordAnswer)
        {
            PasswordQuestion = newPasswordQuestion;
            PasswordAnswer = newPasswordAnswer;
        }

        /// <summary>
        ///     设置批准状态
        /// </summary>
        /// <param name="isApproved">是否批准</param>
        public void SetApproved(bool isApproved)
        {
            IsApproved = isApproved;
        }

        /// <summary>
        ///     添加备注
        /// </summary>
        /// <param name="comment">备注</param>
        public void SetComment(string comment)
        {
            Comment = comment;
        }

        /// <summary>
        ///     锁定账户
        /// </summary>
        public void Lockout()
        {
            IsLockedOut = true;
            LastLockoutDate = DateTime.Now;
        }

        /// <summary>
        ///     解锁账户
        /// </summary>
        public void Unlock()
        {
            IsLockedOut = false;
            LastLockoutDate = DateTime.Now;
        }

        /// <summary>
        ///     新增用户角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>用户角色</returns>
        public UserRole AddNewUserRole(int roleId)
        {
            var userRole = new UserRole
            {
                UserId = Id,
                RoleId = roleId
            };
            userRole.GenerateNewIdentity();
            UserRoles.Add(userRole);

            return userRole;
        }

        #endregion

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}