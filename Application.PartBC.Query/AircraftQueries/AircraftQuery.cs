#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：AircraftQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.AircraftQueries
{
    /// <summary>
    /// Aircraft查询
    /// </summary>
    public class AircraftQuery : IAircraftQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public AircraftQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Aircraft查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>AircraftDTO集合</returns>
        public IQueryable<AircraftDTO> AircraftDTOQuery(QueryBuilder<Aircraft> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Aircraft>()).Select(p => new AircraftDTO
            {
                Id = p.Id,
                RegNumber = p.RegNumber,
                SerialNumber = p.SerialNumber,
                IsOperation = p.IsOperation,
                AircraftType = p.AircraftType.Name,
                AircraftSeries = p.AircraftType.AircraftSeries.Name
            });
        }
    }
}
