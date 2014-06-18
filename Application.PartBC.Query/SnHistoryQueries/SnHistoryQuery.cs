#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/03，09:04
// 文件名：SnHistoryQuery.cs
// 程序集：UniCloud.Application.PartBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PartBC.Query.SnHistoryQueries
{
    public class SnHistoryQuery : ISnHistoryQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public SnHistoryQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     序号件装机历史查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>序号件装机历史DTO集合。</returns>
        public IQueryable<SnHistoryDTO> SnHistoryDTOQuery(
            QueryBuilder<SnHistory> query)
        {
            var dbAircrafts = _unitOfWork.CreateSet<Aircraft>();

            return query.ApplyTo(_unitOfWork.CreateSet<SnHistory>()).Select(p => new SnHistoryDTO
            {
                Id = p.Id,
                Sn = p.Sn,
                SnRegId = p.SnRegId,
                Pn = p.Pn,
                PnRegId = p.PnRegId,
                CSN = p.CSN,
                TSN = p.TSN,
                CSN2 = p.CSN2,
                TSN2 = p.TSN2,
                Status = (int)p.Status,
                Position = (int)p.Position,
                AircraftId = p.AircraftId,
                ActionDate = p.ActionDate,
                ActionType = (int)p.ActionType,
                ActionNo = p.RemInstRecord.ActionNo,
                ActionReason = p.RemInstRecord.Reason,
                RemInstRecordId = p.RemInstRecordId,
                RegNumber = dbAircrafts.FirstOrDefault(c => c.Id == p.AircraftId).RegNumber,
            });
        }
    }
}