#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/22 16:40:27
// 文件名：OtherSupplierDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/22 16:40:27
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     其他供应商。
    /// </summary>
    [DataServiceKey("SupplierRoleId")]
    public class OtherSupplierDTO
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
