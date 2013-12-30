#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/29，17:12
// 文件名：IApprovalDocQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO.ApporvalDocDTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.ApprovalDocQueries
{
    public interface IApprovalDocQuery
    {

        /// <summary>
        ///    所有申请查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>申请DTO集合。</returns>
        IQueryable<ApprovalDocDTO> RequestsQuery(
            QueryBuilder<ApprovalDoc> query);

    }
}
