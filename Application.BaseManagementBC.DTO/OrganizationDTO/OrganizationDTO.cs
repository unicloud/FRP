#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/4 16:04:56
// 文件名：OrganizationDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/4 16:04:56
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.BaseManagementBC.DTO
{
    /// <summary>
    ///     Organization
    /// </summary>
    [DataServiceKey("Id")]
    public class OrganizationDTO
    {
        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     更新时间
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }

        public int? ParentCode { get; set; }

        public int Sort { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// OrganizationRole集合
        /// </summary>
        private List<OrganizationRoleDTO> _organizationRoles;
        public List<OrganizationRoleDTO> OrganizationRoles
        {
            get { return _organizationRoles ?? new List<OrganizationRoleDTO>(); }
            set { _organizationRoles = value; }
        }
        #endregion
    }
}
