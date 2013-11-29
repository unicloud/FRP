#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/18，10:11
// 文件名：IForwarderQuery.cs
// 程序集：UniCloud.Application.PurchaseBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.ForwarderAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.ForwarderQueries
{
    /// <summary>
    ///     承运人查询接口。
    /// </summary>
    public interface IForwarderQuery
    {
        /// <summary>
        ///     承运人查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>承运人DTO集合</returns>
        IQueryable<ForwarderDTO> ForwardersQuery(
            QueryBuilder<Forwarder> query);
    }
}