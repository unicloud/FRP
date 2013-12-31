#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：AirProgrammingQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.AirProgrammingQueries
{
    public class AirProgrammingQuery : IAirProgrammingQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public AirProgrammingQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     航空公司五年规划查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>航空公司五年规划DTO集合。</returns>
        public IQueryable<AirProgrammingDTO> AirProgrammingDTOQuery(
            QueryBuilder<AirProgramming> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AirProgramming>()).Select(p => new AirProgrammingDTO
            {
                Id = p.Id,
                CreateDate = p.CreateDate,
                DocName = p.DocName,
                DocumentId = p.DocumentId,
                IssuedDate = p.IssuedDate,
                IssuedUnitId = p.IssuedUnitId,
                Name = p.Name,
                Note = p.Note,
                ProgrammingId = p.ProgrammingId,
                AirProgrammingLines = p.AirProgrammingLines.Select(q=>new AirProgrammingLineDTO
                {
                    Id = q.Id,
                    AcTypeId = q.AcTypeId,
                    AircraftCategoryId = q.AircraftCategoryId,
                    AirProgrammingId = q.AirProgrammingId,
                    BuyNum = q.BuyNum,
                    ExportNum = q.ExportNum,
                    LeaseNum = q.LeaseNum,
                    Year = q.Year,
                }).ToList(),
            });
        }
    }
}