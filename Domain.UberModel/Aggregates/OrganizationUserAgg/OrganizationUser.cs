#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.OrganizationUserAgg
{
    public class OrganizationUser : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal OrganizationUser()
        {
        }

        public OrganizationUser(int organizationId, int userId)
        {
            OrganizationId = organizationId;
            UserId = userId;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 机构外键
        /// </summary>
        public  int OrganizationId
        {
            get;
            private set;
        }

        /// <summary>
        /// 用户外键
        /// </summary>
        public  int UserId
        {
            get;
           private set;
        }

        #endregion

        #region 方法

        public void SetOrganizationId(int organazationId)
        {
            OrganizationId = organazationId;
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