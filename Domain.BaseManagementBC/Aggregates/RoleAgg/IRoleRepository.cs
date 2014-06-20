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
    }
}