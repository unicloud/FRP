#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，10:32
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

namespace UniCloud.Domain.PaymentBC.Enums
{
    /// <summary>
    ///     付款通知状态
    /// </summary>
    public enum PaymentNoticeStatus
    {
        草稿 = 0,
        待审核 = 1,
        已审核 = 2,
    }
}