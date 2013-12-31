#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：ProgrammingQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.ProgrammingQueries
{
    public class ProgrammingQuery : IProgrammingQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public ProgrammingQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     规划期间查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>规划期间DTO集合。</returns>
        public IQueryable<ProgrammingDTO> ProgrammingDTOQuery(
            QueryBuilder<Programming> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Programming>()).Select(p => new ProgrammingDTO
            {
                Id = p.Id,
                EndDate = p.EndDate,
                Name = p.Name,
                StartDate = p.StartDate,
            });
        }
    }
}