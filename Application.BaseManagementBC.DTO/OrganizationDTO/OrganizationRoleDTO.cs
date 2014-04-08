#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 10:14:58
// 文件名：OrganizationRoleDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 10:14:58
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.BaseManagementBC.DTO
{
    /// <summary>
    ///     OrganizationRole
    /// </summary>
    [DataServiceKey("Id")]
    public class OrganizationRoleDTO
    {
        #region 属性
        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 组织机构Id
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }

        #endregion
    }
}
