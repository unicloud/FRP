#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：AircraftTypeQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.AircraftTypeQueries
{
    /// <summary>
    /// AircraftType查询
    /// </summary>
    public class AircraftTypeQuery : IAircraftTypeQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public AircraftTypeQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// AircraftType查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>AircraftTypeDTO集合</returns>
        public IQueryable<AircraftTypeDTO> AircraftTypeDTOQuery(QueryBuilder<AircraftType> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AircraftType>()).Select(p => new AircraftTypeDTO
            {
                Id = p.Id,
                Name = p.Name,
            });
        }

        /// <summary>
        /// AircraftSeries查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>AircraftSeriesDTO集合</returns>
        public IQueryable<AircraftSeriesDTO> AircraftSeriesDTOQuery(QueryBuilder<AircraftSeries> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AircraftSeries>()).Select(p => new AircraftSeriesDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            });
        }
    }
}
