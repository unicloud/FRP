﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：ManagerQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManagerAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.ManagerQueries
{
    public class ManagerQuery : IManagerQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public ManagerQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     管理者查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>管理者DTO集合。</returns>
        public IQueryable<ManagerDTO> ManagerDTOQuery(
            QueryBuilder<Manager> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Manager>()).Select(p => new ManagerDTO
            {
                Id = p.Id,
                CnShortName = p.CnShortName,
                CnName = p.CnName,
                EnName = p.EnName,
                EnShortName = p.EnShortName,
            });
        }
    }
}