#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationRoleAgg
{
    public class OrganizationRole : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal OrganizationRole()
        {
        }

        public OrganizationRole(int organazitionId, int roleId)
        {
            OrganizationId = organazitionId;
            RoleId = roleId;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 组织机构Id
        /// </summary>
        public  int OrganizationId
        {
            get;
           internal set;
        }

        /// <summary>
        /// 角色Id
        /// </summary>
        public  int RoleId
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