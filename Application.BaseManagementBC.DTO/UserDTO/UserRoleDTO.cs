#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/16 16:24:38
// 文件名：UserRoleDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/16 16:24:38
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.BaseManagementBC.DTO
{
    /// <summary>
    ///     UserRole
    /// </summary>
    [DataServiceKey("Id")]
    public class UserRoleDTO
    {
        #region 属性
        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public int RoleId { get; set; }

        #endregion
    }
}
