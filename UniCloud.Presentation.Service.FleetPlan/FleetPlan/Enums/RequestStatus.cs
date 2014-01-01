#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/31，11:12
// 文件名：RequestStatus.cs
// 程序集：UniCloud.Presentation.Service.FleetPlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间



#endregion

namespace UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums
{
    /// <summary>
    ///申请的处理状态
    /// </summary>
    public enum RequestStatus
    {
        草稿 = 0,
        待审核 = 1,
        已审核 = 2,
        已提交 = 3,
        已审批 = 4,
    }
}
