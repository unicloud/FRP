#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：PlanEngineQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanEngineAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.PlanEngineQueries
{
    public class PlanEngineQuery : IPlanEngineQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public PlanEngineQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     计划发动机查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>计划发动机DTO集合。</returns>
        public IQueryable<PlanEngineDTO> PlanEngineDTOQuery(
            QueryBuilder<PlanEngine> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<PlanEngine>()).Select(p => new PlanEngineDTO
            {
                Id = p.Id,
                AirlinesId = p.AirlinesId,
                EngineTypeId = p.EngineTypeId,
                EngineId = p.EngineId,
            });
        }
    }
}