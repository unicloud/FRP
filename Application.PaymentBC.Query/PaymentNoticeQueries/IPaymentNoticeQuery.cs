#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/20 10:50:25
// 文件名：IPaymentNoticeQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/20 10:50:25
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Domain.PaymentBC.Aggregates.PaymentNoticeAgg;

#endregion

namespace UniCloud.Application.PaymentBC.Query.PaymentNoticeQueries
{
    public interface IPaymentNoticeQuery
    {
        /// <summary>
        ///  付款通知发票查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>付款通知DTO集合</returns>
        IQueryable<PaymentNoticeDTO> PaymentNoticeDTOQuery( QueryBuilder<PaymentNotice> query);
    }
}
