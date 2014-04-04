#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/20 11:05:28
// 文件名：IPaymentNoticeAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/20 11:05:28
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;

#endregion

namespace UniCloud.Application.PaymentBC.PaymentNoticeServices
{
    /// <summary>
    ///     表示用于付款通知相关信息服务
    /// </summary>
    public interface IPaymentNoticeAppService
    {
        /// <summary>
        ///     获取所有付款通知
        /// </summary>
        /// <returns>所有付款通知</returns>
        IQueryable<PaymentNoticeDTO> GetPaymentNotices();
    }
}
