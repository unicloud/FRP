using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationUserAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;

namespace UniCloud.Domain.BaseManagementBC.Services
{
    /// <summary>
    ///     给指定的用户赋予某种组织机构
    /// </summary>
    public interface IOrganizationUserService
    {
        /// <summary>
        /// 将指定的用户赋予特定的组织机构。
        /// </summary>
        /// <param name="user">用户实体。</param>
        /// <param name="organization">组织结构实体。</param>
        /// <returns>用以表述用户及其角色之间关系的实体。</returns>
        OrganizationUser AssignOrganization(User user, Organization organization);

        /// <summary>
        /// 将指定的用户从角色中移除。
        /// </summary>
        /// <param name="user">用户实体。</param>
        /// <param name="organization">组织机构实体，若为NULL，则表示从所有组织机构中移除。</param>
        void UnassignOrganization(User user, Organization organization = null);
    }
}