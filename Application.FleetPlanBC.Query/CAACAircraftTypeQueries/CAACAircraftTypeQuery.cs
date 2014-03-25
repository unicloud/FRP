#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/03/25，14:03
// 文件名：CAACAircraftTypeQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO.CAACAircraftTypeDTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.CAACAircraftTypeAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.CAACAircraftTypeQueries
{
    public class CAACAircraftTypeQuery : ICAACAircraftTypeQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public CAACAircraftTypeQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     民航机型查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>民航机型DTO集合。</returns>
        public IQueryable<CAACAircraftTypeDTO> CAACAircraftTypeDTOQuery(
            QueryBuilder<CAACAircraftType> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<CAACAircraftType>()).Select(p => new CAACAircraftTypeDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                AircraftCategoryId = p.AircraftCategoryId,
                AircraftSeriesId = p.AircraftSeriesId,
                ManufacturerId = p.ManufacturerId,
            });
        }
    }
}