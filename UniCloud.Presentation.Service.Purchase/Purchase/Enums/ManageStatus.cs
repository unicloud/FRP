#region 版本控制
// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：20:01
// 方案：FRP
// 项目：Service.Purchase
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================
#endregion

namespace UniCloud.Presentation.Service.Purchase.Purchase.Enums
{
    /// <summary>
    ///     管理状态
    /// </summary>
    public enum ManageStatus
    {
        预备 = 0,
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