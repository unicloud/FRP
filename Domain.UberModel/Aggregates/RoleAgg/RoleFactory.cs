#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：10:55
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.RoleAgg
{
    /// <summary>
    ///     角色工厂
    /// </summary>
    public static class RoleFactory
    {
        /// <summary>
        ///     创建角色
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">代码</param>
        /// <param name="levelCode">层级代码</param>
        /// <param name="decription">描述</param>
        public static Role CreateRole(string name, string code = null, string levelCode = null, string decription = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            var role = new Role
            {
                Name = name,
                Code = code,
                LevelCode = levelCode,
                Description = decription,
                CreateDate = DateTime.Now,
            };
            role.GenerateNewIdentity();
            return role;
        }
    }
}