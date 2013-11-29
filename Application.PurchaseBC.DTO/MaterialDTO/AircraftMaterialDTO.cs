#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，10:11
// 文件名：AircraftMaterialDTO.cs
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
    ///     飞机物料
    /// </summary>
    [DataServiceKey("AcMaterialId")]
    public class AircraftMaterialDTO
    {
        /// <summary>
        ///     主键
        /// </summary>
        public int AcMaterialId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     机型ID
        /// </summary>
        public Guid AircraftTypeId { get; set; }

        /// <summary>
        /// 合作公司Id
        /// </summary>
        public int SupplierCompanyId { get; set; }
    }
}