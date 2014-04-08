#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/16 10:39:27
// 文件名：IMaintainInvoiceQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/16 10:39:27
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg;

#endregion

namespace UniCloud.Application.PaymentBC.Query.MaintainInvoiceQueries
{
    public interface IMaintainInvoiceQuery
    {
        /// <summary>
        ///     维修发票查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>维修发票DTO集合</returns>
        IQueryable<BaseMaintainInvoiceDTO> MaintainInvoiceDTOQuery(
            QueryBuilder<MaintainInvoice> query);

        /// <summary>
        ///     发动机维修发票查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>发动机维修发票DTO集合</returns>
        IQueryable<EngineMaintainInvoiceDTO> EngineMaintainInvoiceDTOQuery(
            QueryBuilder<MaintainInvoice> query);

        /// <summary>
        ///     APU维修发票查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>APU维修发票DTO集合</returns>
        IQueryable<APUMaintainInvoiceDTO> APUMaintainInvoiceDTOQuery(
            QueryBuilder<MaintainInvoice> query);

        /// <summary>
        ///     机身维修发票查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>机身维修发票DTO集合</returns>
        IQueryable<AirframeMaintainInvoiceDTO> AirframeMaintainInvoiceDTOQuery(
            QueryBuilder<MaintainInvoice> query);

        /// <summary>
        ///     起落架维修发票查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>起落架维修发票DTO集合</returns>
        IQueryable<UndercartMaintainInvoiceDTO> UndercartMaintainInvoiceDTOQuery(
            QueryBuilder<MaintainInvoice> query);
    }
}
