#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：AircraftTypeQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftTypeAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.AircraftConfigBC.Query.AircraftTypeQueries
{
    public class AircraftTypeQuery : IAircraftTypeQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public AircraftTypeQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     机型查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>机型DTO集合。</returns>
        public IQueryable<AircraftTypeDTO> AircraftTypeDTOQuery(
            QueryBuilder<AircraftType> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AircraftType>()).Select(p => new AircraftTypeDTO
            {
                Id = p.Id,
                Name = p.Name,
                AircraftCategoryId = p.AircraftCategoryId,
                Regional = p.AircraftCategory.Regional,
            });
        }
    }
}