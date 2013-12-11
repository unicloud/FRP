#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/10，10:12
// 文件名：MaintainSupplierDTO.cs
// 程序集：UniCloud.Application.PurchaseBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System.Data.Services.Common;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     维修供应商DTO
    /// </summary>
        [DataServiceKey("SupplierRoleId")]
    public class MaintainSupplierDTO 
    {
        /// <summary>
        ///     主键。
        /// </summary>
        public int SupplierRoleId { get; set; }

        /// <summary>
        ///     是否有效。
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     供应商公司外键。
        /// </summary>
        public int SupplierCompanyId { get; set; }
    }
}