#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/11，17:13
// 方案：FRP
// 项目：Application.PurchaseBC.Query
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.CurrencyAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.CurrencyQueries
{
    public interface ICurrencyQuery
    {
        /// <summary>
        ///     查询币种
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>币种DTO集合</returns>
        IQueryable<CurrencyDTO> CurrenciesQuery(QueryBuilder<Currency> query);
    }
}