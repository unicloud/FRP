#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/25，16:12
// 文件名：IMaintainInvoiceQuery.cs
// 程序集：UniCloud.Application.PaymentBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion


#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Domain.PaymentBC.Aggregates.GuaranteeAgg;

#endregion

namespace UniCloud.Application.PaymentBC.Query.GuaranteeQueries
{
    public interface IGuaranteeQuery
    {
        /// <summary>
        ///    租赁保证金查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>租赁保证金DTO集合</returns>
        IQueryable<LeaseGuaranteeDTO> LeaseGuaranteeQuery(
            QueryBuilder<Guarantee> query);

        /// <summary>
        ///     维修保证金查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>大修保证金DTO集合</returns>
        IQueryable<MaintainGuaranteeDTO> MaintainGuaranteeQuery(
            QueryBuilder<Guarantee> query);
     
    }
}
