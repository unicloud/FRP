#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.UberModel.Resources;

#endregion

namespace UniCloud.Domain.UberModel.Enums
{
    /// <summary>
    ///     供应商类型
    /// </summary>
    public enum SupplierType
    {
        [Display(ResourceType = typeof (DisplayInfo), Name = "SupplierType_Foreign")] Foreign = 0,
        [Display(ResourceType = typeof (DisplayInfo), Name = "SupplierType_Inland")] Inland = 1,
    }
}