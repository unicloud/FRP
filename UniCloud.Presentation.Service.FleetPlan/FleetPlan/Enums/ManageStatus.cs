#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2014/01/08，14:01
// 文件名：ManageStatus.cs
// 程序集：UniCloud.Presentation.Service.FleetPlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums
{
    /// <summary>
    ///     管理状态
    /// </summary>
    public enum ManageStatus
    {
        草稿 = 0,
        计划 = 1,
        申请 = 2,
        批文 = 3,
        签约 = 4,
        技术接收 = 5,
        接收 = 6,
        运营 = 7,
        停场待退 = 8,
        技术交付 = 9,
        退役 = 10,
    }
}