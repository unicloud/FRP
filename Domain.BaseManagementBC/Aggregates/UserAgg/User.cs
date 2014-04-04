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
        ///     员工号
        /// </summary>
        public string EmployeeCode { get; internal set; }

        /// <summary>
        ///     组织机构名称
        /// </summary>
        public string OrganizationNo { get; internal set; }

        /// <summary>
        ///     名
        /// </summary>
        public string FirstName { get; internal set; }

        /// <summary>
        ///     姓
        /// </summary>
        public string LastName { get; internal set; }

        /// <summary>
        ///     显示名称
        /// </summary>
        public string DisplayName { get; internal set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get;
            internal set;
        }

        /// <summary>
        /// 邮件
        /// </summary>
        public string Email
        {
            get;
            internal set;
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile
        {
            get;
            internal set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description
        {
            get;
            internal set;
        }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsValid
        {
            get;
            internal set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get;
            internal set;
        }

        /// <summary>
        /// 角色集合
        /// </summary>
        private ICollection<UserRole> _userRoles;
        public ICollection<UserRole> UserRoles
        {
            get { return _userRoles ?? (_userRoles = new HashSet<UserRole>()); }
            set { _userRoles = new HashSet<UserRole>(value); }
        }
        #endregion

        #region 操作
        /// <summary>
        /// 新增UserRole
        /// </summary>
        /// <returns></returns>
        public UserRole AddNewUserRole()
        {
            var userRole = new UserRole
            {
                UserId = Id,
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