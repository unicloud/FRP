#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/18，10:11
// 文件名：ISupplierQuery.cs
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
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Application.PaymentBC.Query.SupplierQueries
{
    /// <summary>
    ///     供应商查询接口。
    /// </summary>
    public interface ISupplierQuery
    {
        /// <summary>
        ///     查询供应商。
        /// </summary>
        /// <param name="query">查询条件。</param>
        /// <returns>供应商。</returns>
        IQueryable<SupplierDTO> SuppliersQuery(QueryBuilder<Supplier> query);
    }
}