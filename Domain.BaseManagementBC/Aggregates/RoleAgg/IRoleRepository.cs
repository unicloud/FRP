#region 命名空间

using UniCloud.Domain.BaseManagementBC.Aggregates.RoleFunctionAgg;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg
{
    /// <summary>
    ///     Role仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{Role}" />
    /// </summary>
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        ///     删除角色
        /// </summary>
        /// <param name="role">角色</param>
        void DeleteRole(Role role);

        /// <summary>
        ///     删除角色功能
        /// </summary>
        /// <param name="roleFunction">角色功能</param>
        void DeleteRoleFunction(RoleFunction roleFunction);
    }
}