#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/27，14:14
// 方案：FRP
// 项目：Domain.Common
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel;

#endregion

namespace UniCloud.Presentation.Service.Part.Part.Enums
{
    /// <summary>
    ///     滑油监控状态
    /// </summary>
    public enum OilMonitorStatus
    {
        [Description("3-正常")] 正常 = 0,
        [Description("2-关注")] 关注 = 1,
        [Description("1-警告")] 警告 = 2
    }
}