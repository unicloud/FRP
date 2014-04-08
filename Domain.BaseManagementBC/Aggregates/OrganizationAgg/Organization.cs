#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationRoleAgg;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg
{
    public class Organization : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Organization()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     编号
        /// </summary>
        public  string Code { get; internal set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     更新时间
        /// </summary>
        public DateTime? LastUpdateTime { get; internal set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; internal set; }

        public int? ParentCode { get; internal set; }

        public int Sort { get; internal set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        /// 描述
        /// </summary>
        public String Description { get; set; }

        private ICollection<Organization> _subOrganizations;
        /// <summary>
        ///     子项集合
        /// </summary>
        public ICollection<Organization> SubOrganizations
        {
            get { return _subOrganizations ?? (_subOrganizations = new HashSet<Organization>()); }
            set { _subOrganizations = new HashSet<Organization>(value); }
        }

        /// <summary>
        /// 角色集合
        /// </summary>
        private ICollection<OrganizationRole> _organizationRoles;
        public ICollection<OrganizationRole> OrganizationRoles
        {
            get { return _organizationRoles ?? (_organizationRoles = new HashSet<OrganizationRole>()); }
            set { _organizationRoles = new HashSet<OrganizationRole>(value); }
        }
        #endregion

        #region 操作
        /// <summary>
        /// 新增OrganizationRole
        /// </summary>
        /// <returns></returns>
        public OrganizationRole AddNewOrganizationRole()
        {
            var organizationRole = new OrganizationRole
            {
                OrganizationId = Id,
            };

            organizationRole.GenerateNewIdentity();
            OrganizationRoles.Add(organizationRole);

            return organizationRole;
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