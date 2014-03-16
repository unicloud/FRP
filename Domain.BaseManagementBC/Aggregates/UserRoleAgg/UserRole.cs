#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Aggregates.UserRoleAgg
{
    public class UserRole : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal UserRole()
        {
        }

        public UserRole(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
        #endregion

        #region 属性

        public  int UserId
        {
            get;
            internal set;
        }

        public int RoleId
        {
            get;
            internal set;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 设置角色Id
        /// </summary>
        /// <param name="roleId"></param>
        public void SetRoleId(int roleId)
        {
            RoleId = roleId;
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