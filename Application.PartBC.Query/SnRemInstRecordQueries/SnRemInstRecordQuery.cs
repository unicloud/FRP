#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/15，23:04
// 文件名：SnRemInstRecordQuery.cs
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
using UniCloud.Domain.PartBC.Aggregates.SnRemInstRecordAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PartBC.Query.SnRemInstRecordQueries
{
    public class SnRemInstRecordQuery : ISnRemInstRecordQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public SnRemInstRecordQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     拆装记录查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>拆装记录DTO集合。</returns>
        public IQueryable<SnRemInstRecordDTO> SnRemInstRecordDTOQuery(
            QueryBuilder<SnRemInstRecord> query)
        {
            var aircrafts = _unitOfWork.CreateSet<Aircraft>();

            return query.ApplyTo(_unitOfWork.CreateSet<SnRemInstRecord>()).Select(p => new SnRemInstRecordDTO
            {
                Id = p.Id,
                ActionNo = p.ActionNo,
                ActionDate = p.ActionDate,
                ActionType = (int)p.ActionType,
                Position = p.Position,
                AircraftId = p.AircraftId,
                Reason = p.Reason,
                RegNumber = aircrafts.FirstOrDefault(l=>l.Id==p.AircraftId).RegNumber,
            });

        }
    }
}