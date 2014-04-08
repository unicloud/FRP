#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/14 14:54:36
// 文件名：RoleFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/14 14:54:36
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleFunctionAgg;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg
{
    /// <summary>
    ///     角色工厂
    /// </summary>
    public static class RoleFactory
    {
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <returns></returns>
        public static Role CreateRole()
        {
            var role = new Role();
            role.GenerateNewIdentity();
            return role;
        }

        /// <summary>
        /// 设置role属性
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="name">名字</param>
        /// <param name="description">描述</param>
        public static void SetRole(Role role, string name, string description)
        {
            role.SerRole(name, description);
        }

        /// <summary>
        /// 设置RoleFunction
        /// </summary>
        /// <param name="roleFunction">角色功能</param>
        /// <param name="roleId">角色</param>
        /// <param name="functionItemId">功能</param>
        public static void SetRoleFunction(RoleFunction roleFunction, int roleId, int functionItemId)
        {
            roleFunction.RoleId = roleId;
            roleFunction.FunctionItemId = functionItemId;
        }
    }
}
