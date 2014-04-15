#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/03，09:04
// 文件名：SnInstallHistoryQuery.cs
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
using UniCloud.Domain.PartBC.Aggregates.SnInstallHistoryAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PartBC.Query.SnInstallHistoryQueries
{
    public class SnInstallHistoryQuery : ISnInstallHistoryQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public SnInstallHistoryQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     序号件装机历史查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>序号件装机历史DTO集合。</returns>
        public IQueryable<SnInstallHistoryDTO> SnInstallHistoryDTOQuery(
            QueryBuilder<SnInstallHistory> query)
        {
            var dbAircrafts = _unitOfWork.CreateSet<Aircraft>();
            return query.ApplyTo(_unitOfWork.CreateSet<SnInstallHistory>()).Select(p => new SnInstallHistoryDTO
            {
                Id = p.Id,
                Sn = p.Sn,
                SnRegId = p.SnRegId,
                Pn = p.Pn,
                PnRegId = p.PnRegId,
                CSN = p.CSN,
                CSR = p.CSR,
                TSN = p.TSN,
                TSR = p.TSR,
                AircraftId = p.AircraftId,
                InstallDate = p.InstallDate,
                RemoveDate = p.RemoveDate,
                InstallReason = p.InstallReason,
                RemoveReason = p.RemoveReason,
                RegNumber = dbAircrafts.FirstOrDefault(c => c.Id == p.AircraftId).RegNumber,
            });
        }
    }
}