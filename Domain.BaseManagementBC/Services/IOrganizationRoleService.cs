#region 命名空间

using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationRoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Services
{
    /// <summary>
    ///     组织机构领域服务
    /// </summary>
    public interface IOrganizationRoleService
    {
        /// <summary>
        ///     将指定的组织机构赋予特定的角色。
        /// </summary>
        /// <param name="organization">组织机构实体。</param>
        /// <param name="role">角色实体。</param>
        /// <returns>用以表述组织机构及其角色之间关系的实体。</returns>
        OrganizationRole AssignRole(Organization organization, Role role);

        /// <summary>
        ///     将指定的组织机构从角色中移除。
        /// </summary>
        /// <param name="organization">组织机构实体。</param>
        /// <param name="role">角色实体，若为NULL，则表示从所有角色中移除。</param>
        void UnassignRole(User organization, Role role = null);
    }
}