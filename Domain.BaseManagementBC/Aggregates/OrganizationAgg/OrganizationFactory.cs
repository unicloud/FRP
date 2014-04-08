#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/4 15:56:40
// 文件名：OrganizationFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/4 15:56:40
// 修改说明：
// ========================================================================*/
#endregion
using System;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationRoleAgg;

namespace UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg
{
    /// <summary>
    ///     组织机构工厂
    /// </summary>
    public static class OrganizationFactory
    {
        /// <summary>
        /// 创建组织机构
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="name">名称</param>
        /// <param name="sort">序号</param>
        /// <param name="description">描述</param>
        /// <param name="isValid">是否有效</param>
        /// <returns></returns>
        public static Organization CreateOrganization(string code, string name, int sort, string description, bool isValid)
        {
            var organization = new Organization
            {
                Code = code,
                Name = name,
                CreateDate = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                Description = description,
                Sort = sort,
                IsValid = isValid,
            };
            organization.GenerateNewIdentity();
            return organization;
        }

        /// <summary>
        /// 设置组织机构
        /// </summary>
        /// <param name="organization">组织机构</param>
        /// <param name="code">编号</param>
        /// <param name="name">名称</param>
        /// <param name="sort">序号</param>
        /// <param name="description">描述</param>
        /// <param name="isValid">是否有效</param>
        public static void SetOrganization(Organization organization, string code, string name, int sort, string description, bool isValid)
        {
            organization.Code = code;
            organization.Name = name;
            organization.Sort = sort;
            organization.Description = description;
            organization.IsValid = isValid;
            organization.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 设置OrganizationRole
        /// </summary>
        /// <param name="organizationRole">组织机构角色</param>
        /// <param name="organizationId">组织机构</param>
        /// <param name="roleId">角色</param>
        public static void SetOrganizationRole(OrganizationRole organizationRole, int organizationId, int roleId)
        {
            organizationRole.OrganizationId = organizationId;
            organizationRole.RoleId = roleId;
        }
    }
}
