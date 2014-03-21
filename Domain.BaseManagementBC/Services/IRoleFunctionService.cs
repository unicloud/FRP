#region NameSpace

using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleFunctionAgg;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Services
{
    public interface IRoleFunctionService
    {
        /// <summary>
        ///     将指定的功能项赋予特定的角色。
        /// </summary>
        /// <param name="function">功能项实体。</param>
        /// <param name="role">角色实体。</param>
        /// <returns>用以表述功能项及其角色之间关系的实体。</returns>
        RoleFunction AssignRole(FunctionItem function, Role role);

        /// <summary>
        ///     将指定的功能项从角色中移除。
        /// </summary>
        /// <param name="function">功能项实体。</param>
        /// <param name="role">角色实体，若为NULL，则表示从所有角色中移除。</param>
        void UnassignRole(FunctionItem function, Role role = null);
    }
}