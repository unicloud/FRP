#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 15:35:25
// 文件名：AircraftCabinTypeQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 15:35:25
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Domain.BaseManagementBC.Aggregates.AircraftCabinTypeAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.BaseManagementBC.Query.AircraftCabinTypeQueries
{
    public class AircraftCabinTypeQuery : IAircraftCabinTypeQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public AircraftCabinTypeQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     飞机舱位类型查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>飞机舱位类型DTO集合。</returns>
        public IQueryable<AircraftCabinTypeDTO> AircraftCabinTypeDTOQuery(
            QueryBuilder<AircraftCabinType> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AircraftCabinType>()).Select(p => new AircraftCabinTypeDTO
            {
                Id = p.Id,
                Name = p.Name,
                Note = p.Note
            });
        }
    }
}
