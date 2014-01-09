#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：AircraftCategoryQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.AircraftCategoryQueries
{
    public class AircraftCategoryQuery : IAircraftCategoryQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public AircraftCategoryQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     座级查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>座级DTO集合。</returns>
        public IQueryable<AircraftCategoryDTO> AircraftCategoryDTOQuery(
            QueryBuilder<AircraftCategory> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AircraftCategory>()).Select(p => new AircraftCategoryDTO
            {
                Id = p.Id,
                Category = p.Category,
                Regional = p.Regional,
            });
        }
    }
}