using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserRoleAgg;

namespace UniCloud.Domain.BaseManagementBC.Services
{
    /// <summary>
    ///     用户角色领域服务
    /// </summary>
    public interface IUserRoleService
    {
        /// <summary>
        /// 将指定的用户赋予特定的角色。
        /// </summary>
        /// <param name="user">用户实体。</param>
        /// <param name="role">角色实体。</param>
        /// <returns>用以表述用户及其角色之间关系的实体。</returns>
        UserRole AssignRole(User user, Role role);

        /// <summary>
        /// 将指定的用户从角色中移除。
        /// </summary>
        /// <param name="user">用户实体。</param>
        /// <param name="role">角色实体，若为NULL，则表示从所有角色中移除。</param>
        void UnassignRole(User user, Role role = null);
    }
}