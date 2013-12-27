#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 10:39:32
// 文件名：ILeaseInvoiceQuery
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
    /// 租赁发票查询接口
    /// </summary>
    public interface ILeaseInvoiceQuery
    {
        /// <summary>
        ///    租赁发票查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>租赁发票DTO集合。</returns>
        IQueryable<LeaseInvoiceDTO> LeaseInvoiceDTOQuery(
            QueryBuilder<LeaseInvoice> query);
    }
}
