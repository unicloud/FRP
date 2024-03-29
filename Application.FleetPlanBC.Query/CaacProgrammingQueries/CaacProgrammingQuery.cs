﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：CaacProgrammingQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.CaacProgrammingAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.CaacProgrammingQueries
{
    public class CaacProgrammingQuery : ICaacProgrammingQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public CaacProgrammingQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     民航局五年规划查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>民航局五年规划DTO集合。</returns>
        public IQueryable<CaacProgrammingDTO> CaacProgrammingDTOQuery(
            QueryBuilder<CaacProgramming> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<CaacProgramming>()).Select(p => new CaacProgrammingDTO
            {
                Id = p.Id,
                CreateDate = p.CreateDate,
                DocName = p.DocName,
                DocNumber = p.DocNumber,
                DocumentId = p.DocumentId,
                IssuedDate = p.IssuedDate,
                IssuedUnitId = p.IssuedUnitId,
                Name = p.Name,
                Note = p.Note,
                ProgrammingId = p.ProgrammingId,
                ProgrammingName = p.Programming.Name,
                CaacProgrammingLines = p.CaacProgrammingLines.Select(q => new CaacProgrammingLineDTO
                {
                    Id = q.Id,
                    AircraftCategoryId = q.AircraftCategoryId,
                    CaacProgrammingId = q.CaacProgrammingId,
                    Number = q.Number,
                    Year = q.Year,
                }).ToList(),
            });
        }
    }
}