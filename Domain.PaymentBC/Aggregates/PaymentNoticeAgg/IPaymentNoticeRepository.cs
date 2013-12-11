#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/08，11:56
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.PaymentNoticeAgg
{
    /// <summary>
    ///     付款通知仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{PaymentNotice}" />
    /// </summary>
    public interface IPaymentNoticeRepository : IRepository<PaymentNotice>
    {
    }
}