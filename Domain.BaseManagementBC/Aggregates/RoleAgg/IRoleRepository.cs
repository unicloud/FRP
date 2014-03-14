using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleFunctionAgg;

namespace UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg
{
    /// <summary>
    ///     Role仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{Document}" />
    /// </summary>
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        /// 删除Role
        /// </summary>
        /// <param name="role"></param>
        void DeleteRole(Role role);

        /// <summary>
        /// 删除RoleFunction
        /// </summary>
        /// <param name="roleFunction"></param>
        void DeleteRoleFunction(RoleFunction roleFunction);
    }
}