#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.BaseManagementBC.DTO
{
    /// <summary>
    ///     User
    /// </summary>
    [DataServiceKey("Id")]
    public class UserDTO
    {
        #region 属性

        /// <summary>
        ///     UserRole集合
        /// </summary>
        private List<UserRoleDTO> _userRoles;

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     用户名称
        /// </summary>
        /// <remarks>
        ///     员工号
        /// </remarks>
        public string UserName { get; set; }

        /// <summary>
        ///     组织机构编号
        /// </summary>
        public string OrganizationNo { get; set; }

        /// <summary>
        ///     显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     密码确认
        /// </summary>
        public string PasswordConfirm { get; set; }

        /// <summary>
        ///     密码问题
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        ///     密码问题答案
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        ///     邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     组织机构名称
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        ///     是否系统用户
        /// </summary>
        public bool IsSystemUser { get; set; }

        public List<UserRoleDTO> UserRoles
        {
            get { return _userRoles ?? new List<UserRoleDTO>(); }
            set { _userRoles = value; }
        }

        #endregion
    }
}