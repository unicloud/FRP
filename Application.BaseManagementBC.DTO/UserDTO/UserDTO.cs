#region NameSpace

using System;
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
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     员工号
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        ///     显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     密码
        /// </summary>
        public string Password { get; set; }

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

        #endregion
    }
}