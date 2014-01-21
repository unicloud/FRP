#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/29，16:12
// 文件名：IRequestQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.RequestQueries
{
    public interface IRequestQuery
    {

        /// <summary>
        ///    所有申请查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>申请DTO集合。</returns>
        IQueryable<RequestDTO> RequestsQuery(
            QueryBuilder<Request> query);

        /// <summary>
        ///    查询带有申请的批文
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>申请的批文DTO集合。</returns>
        IQueryable<ApprovalRequestDTO> ApprovalRequestsQuery(
            QueryBuilder<Request> query);

    }
}
