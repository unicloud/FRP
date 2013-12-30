﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：ApprovalDocQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.ApprovalDocQueries
{
    public class ApprovalDocQuery : IApprovalDocQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public ApprovalDocQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     批文查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>批文DTO集合。</returns>
        public IQueryable<ApprovalDocDTO> ApprovalDocDTOQuery(
            QueryBuilder<ApprovalDoc> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<ApprovalDoc>()).Select(p => new ApprovalDocDTO
            {
                Id = p.Id,
                
            });
        }
    }
}