#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 10:40:36
// 文件名：IPrepaymentInvoiceQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;

#endregion

namespace UniCloud.Application.PaymentBC.Query.InvoiceQueries
{
    /// <summary>
    /// 预付款发票查询接口
    /// </summary>
    public interface IPrepaymentInvoiceQuery
    {
        /// <summary>
        ///    预付款发票查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>预付款发票DTO集合。</returns>
        IQueryable<PurchasePrepaymentInvoiceDTO> PurchasePrepaymentInvoiceDTOQuery(
            QueryBuilder<PurchasePrepaymentInvoice> query);

        /// <summary>
        ///    预付款发票查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>预付款发票DTO集合。</returns>
        IQueryable<MaintainPrepaymentInvoiceDTO> MaintainPrepaymentInvoiceDTOQuery(
            QueryBuilder<MaintainPrepaymentInvoice> query);
    }
}
