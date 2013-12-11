#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/17，19:11
// 文件名：AircraftLeaseSupplierDTO.cs
// 程序集：UniCloud.Application.PurchaseBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     飞机租赁供应商。
    /// </summary>
    [DataServiceKey("SupplierRoleId")]
    public class AircraftLeaseSupplierDTO
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