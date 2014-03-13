#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public User(string employeeCode, string displayName, string password,
            string email, string mobile,string description=null,bool isvalid=true)
        {
            if (string.IsNullOrEmpty(displayName))
            {
                throw new ArgumentNullException("displayName");
            }
            EmployeeCode = employeeCode;
            DisplayName = displayName;
            Password = password;
            Email = email;
            Mobile = mobile;
            Description = description;
            IsValid = isvalid;
            CreateDate = DateTime.Now;
        }

        #endregion

        #region 属性

        /// <summary>
        ///     员工号
        /// </summary>
        public string EmployeeCode { get; internal set; }

        /// <summary>
        ///     名
        /// </summary>
        public string FirstName { get;internal set; }

        /// <summary>
        ///     姓
        /// </summary>
        public string LaseName { get;internal set; }

        /// <summary>
        ///     显示名称
        /// </summary>
        public string DisplayName { get;internal set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get;
          internal  set;
        }

        /// <summary>
        /// 邮件
        /// </summary>
        public string Email
        {
            get;
          internal  set;
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
          internal  set;
        }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsValid
        {
            get;
          internal  set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get;
         internal set;
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