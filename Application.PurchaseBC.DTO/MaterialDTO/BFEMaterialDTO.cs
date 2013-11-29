#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，10:11
// 文件名：BFEMaterialDTO.cs
// 程序集：UniCloud.Application.PurchaseBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间



#endregion

using System.Data.Services.Common;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     BFE物料
    /// </summary>
    [DataServiceKey("BFEMaterialId")]
    public class BFEMaterialDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int BFEMaterialId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     附件ID
        /// </summary>
        public int PartId { get; set; }

        /// <summary>
        /// 合作公司Id
        /// </summary>
        public int SupplierCompanyId { get; set; }

    }
}