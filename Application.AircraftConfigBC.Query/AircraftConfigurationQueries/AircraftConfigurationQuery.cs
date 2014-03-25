#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 14:01:06
// 文件名：AircraftConfigurationQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 14:01:06
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftCabinTypeAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftConfigurationAgg;
using UniCloud.Infrastructure.Data;

namespace UniCloud.Application.AircraftConfigBC.Query.AircraftConfigurationQueries
{
   public class AircraftConfigurationQuery:IAircraftConfigurationQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public AircraftConfigurationQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     飞机配置查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>飞机配置DTO集合。</returns>
        public IQueryable<AircraftConfigurationDTO> AircraftConfigurationDTOQuery(
            QueryBuilder<AircraftConfiguration> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AircraftConfiguration>()).Select(p => new AircraftConfigurationDTO
            {
                Id = p.Id,
                AircraftSeriesId = p.AircraftSeriesId,
                AircraftTypeId = p.AircraftTypeId,
                BEW = p.BEW,
                BW = p.BW,
                BWF = p.BWF,
                BWI = p.BWI,
                ConfigCode = p.ConfigCode,
                Description = p.Description,
                FileContent = p.FileContent,
                FileName = p.FileName,
                MCC = p.MCC,
                MLW = p.MLW,
                MMFW = p.MMFW,
                MTOW = p.MTOW,
                MTW = p.MTW,
                MZFW = p.MZFW,
                AircraftCabins = p.AircraftCabins.Select(q=>new AircraftCabinDTO
                                                         {
                                                             Id = q.Id,
                                                             AircraftCabinTypeId = q.AircraftCabinTypeId,
                                                             SeatNumber = q.SeatNumber,
                                                             Note = q.Note
                                                         }).ToList(),
            });
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
