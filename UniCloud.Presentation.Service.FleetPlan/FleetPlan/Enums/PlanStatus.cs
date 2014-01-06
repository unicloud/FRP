#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/03，09:01
// 方案：FRP
// 项目：Domain.Common
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums
{
    /// <summary>
    ///     计划的处理状态
    /// </summary>
    public enum PlanStatus
    {
        草稿 = 0,
        待审核 = 1,
        已审核 = 2,
        已提交 = 3,
        退回 = 4,
    }
}