#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：PlanAircraftQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.PlanAircraftQueries
{
    public class PlanAircraftQuery : IPlanAircraftQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public PlanAircraftQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     计划飞机查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>计划飞机DTO集合。</returns>
        public IQueryable<PlanAircraftDTO> PlanAircraftDTOQuery(
            QueryBuilder<PlanAircraft> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<PlanAircraft>()).Select(p => new PlanAircraftDTO
            {
                Id = p.Id,
                AirlinesId = p.AirlinesId,
                AircraftId = p.AircraftId,
                AircraftTypeId = p.AircraftTypeId,
                IsLock = p.IsLock,
                IsOwn = p.IsOwn,
                Status = (int)p.Status,
                AirlinesName = p.Airlines.CnShortName,
                AircraftTypeName = p.AircraftType.Name,
                Regional = p.AircraftType.AircraftCategory.Regional,
            });
        }
    }
}