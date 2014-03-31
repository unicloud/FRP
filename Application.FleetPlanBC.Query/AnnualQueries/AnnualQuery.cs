#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：AnnualQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.AnnualQueries
{
    public class AnnualQuery : IAnnualQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public AnnualQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     计划年度查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>计划年度DTO集合。</returns>
        public IQueryable<AnnualDTO> AnnualDTOQuery(
            QueryBuilder<Annual> query)
        {
            var result = query.ApplyTo(_unitOfWork.CreateSet<Annual>()).Select(p => new AnnualDTO
            {
                Id = p.Id,
                IsOpen = p.IsOpen,
                ProgrammingId = p.ProgrammingId,
                Year = p.Year,
                ProgrammingName = p.Programming.Name,
            });
            var a = result.OrderByDescending(p => p.Year).ToList();
            return result.OrderByDescending(p => p.Year);
        }

        public IQueryable<PlanYearDTO> PlanYearDTOQuery(QueryBuilder<Annual> query)
        {
            var result = query.ApplyTo(_unitOfWork.CreateSet<Annual>()).Select(p => new PlanYearDTO
            {
                Id = p.Id,
                IsOpen = p.IsOpen,
                ProgrammingId = p.ProgrammingId,
                Year = p.Year,
                ProgrammingName = p.Programming.Name,
            });
            return result.OrderByDescending(p => p.Year);
        }
    }
}