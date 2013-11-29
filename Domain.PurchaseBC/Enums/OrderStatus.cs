#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/05，11:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.PurchaseBC.Resources;

#endregion

namespace UniCloud.Domain.PurchaseBC.Enums
{
    /// <summary>
    ///     订单状态
    /// </summary>
    public enum OrderStatus
    {
        [Display(ResourceType = typeof (DisplayInfo), Name = "OrderStatus_Draft")] Draft = 0,
        [Display(ResourceType = typeof (DisplayInfo), Name = "OrderStatus_Checking")] Checking = 1,
        [Display(ResourceType = typeof (DisplayInfo), Name = "OrderStatus_Checked")] Checked = 2,
    }
}