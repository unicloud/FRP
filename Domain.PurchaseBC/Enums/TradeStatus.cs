﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/05，13:11
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
    ///     交易状态
    /// </summary>
    public enum TradeStatus
    {
        [Display(ResourceType = typeof (DisplayInfo), Name = "TradeStatus_Start")] Start = 0,
        [Display(ResourceType = typeof (DisplayInfo), Name = "TradeStatus_InProgress")] InProgress = 1,
        [Display(ResourceType = typeof (DisplayInfo), Name = "TradeStatus_Repeal")] Repeal = 2,
        [Display(ResourceType = typeof (DisplayInfo), Name = "TradeStatus_Complete")] Complete = 3,
    }
}