#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/28 14:18:41
// 文件名：AirStructureDamageQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/28 14:18:41
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.AirStructureDamageAgg;
using UniCloud.Infrastructure.Data;

namespace UniCloud.Application.PartBC.Query.AirStructureDamageQueries
{
    /// <summary>
    /// AirStructureDamage查询
    /// </summary>
    public class AirStructureDamageQuery : IAirStructureDamageQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public AirStructureDamageQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// AirStructureDamage查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>AirStructureDamageDTO集合</returns>
        public IQueryable<AirStructureDamageDTO> AirStructureDamageDTOQuery(QueryBuilder<AirStructureDamage> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AirStructureDamage>()).Select(p => new AirStructureDamageDTO
            {
                Id = p.Id,
                AircraftId = p.AircraftId,
                AircraftReg = p.Aircraft.RegNumber,
                AircraftSeries = p.AircraftSeries,
                AircraftType = p.AircraftType,
                CloseDate = p.CloseDate,
                Description = p.Description,
                DocumentName = p.DocumentName,
                IsDefer = p.IsDefer,
                Level = (int)p.Level,
                RepairDeadline = p.RepairDeadline,
                ReportDate = p.ReportDate,
                ReportNo = p.ReportNo,
                ReportType = (int)p.ReportType,
                Source = p.Source,
                Status = (int)p.Status,
                TecAssess = p.TecAssess,
                TotalCost = p.TotalCost,
                TreatResult = p.TreatResult,
                DocumentId = p.DocumentId
            });
        }
    }
}
