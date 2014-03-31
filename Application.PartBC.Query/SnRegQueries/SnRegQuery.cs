#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SnRegQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.SnRegQueries
{
    /// <summary>
    /// SnReg查询
    /// </summary>
    public class SnRegQuery : ISnRegQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public SnRegQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// SnReg查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>SnRegDTO集合</returns>
        public IQueryable<SnRegDTO> SnRegDTOQuery(QueryBuilder<SnReg> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<SnReg>()).Select(p => new SnRegDTO
            {
                Id = p.Id,
                Pn = p.Pn,
                Sn = p.Sn,
                TSN = p.TSN,
                TSR = p.TSR,
                CSN = p.CSN,
                CSR = p.CSR,
                IsStop = p.IsStop,
                InstallDate = p.InstallDate,
                AircraftId = p.AircraftId,
                PnRegId = p.PnRegId,
                RegNumber = p.RegNumber,
                Status = (int)p.Status,
                LiftMonitors = p.LifeMonitors.Select(q => new LifeMonitorDTO
                {
                    Id = q.Id,
                    MointorEnd = q.MointorEnd,
                    MointorStart = q.MointorStart,
                    WorkCode = q.WorkCode,
                    MaintainWorkId = q.MaintainWorkId,
                    SnRegId = q.SnRegId,
                }).ToList(),
                SnHistories = p.SnHistories.Select(q => new SnHistoryDTO
                {
                    Id = q.Id,
                    FiNumber = q.FiNumber,
                    CSN = q.CSN,
                    CSR = q.CSR,
                    TSN = q.TSN,
                    TSR = q.TSR,
                    InstallDate = q.InstallDate,
                    RemoveDate = q.RemoveDate,
                    Sn = q.Sn,
                    SnRegId = q.SnRegId,
                    AircraftId = q.AircraftId,
                }).ToList(),
            });
        }

        /// <summary>
        /// ApuEngineSnReg查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>ApuEngineSnRegDTO集合</returns>
        public IQueryable<ApuEngineSnRegDTO> ApuEngineSnRegDTOQuery(QueryBuilder<SnReg> query)
        {
            var dbAircraft = _unitOfWork.CreateSet<Aircraft>();
            var apuSnRegs = query.ApplyTo(_unitOfWork.CreateSet<SnReg>()).Select(p => new ApuEngineSnRegDTO
            {
                Id = p.Id,
                Pn = p.Pn,
                Sn = p.Sn,
                TSN = p.TSN,
                TSR = p.TSR,
                CSN = p.CSN,
                CSR = p.CSR,
                IsStop = p.IsStop,
                InstallDate = p.InstallDate,
                AircraftId = p.AircraftId,
                PnRegId = p.PnRegId,
                RegNumber = p.RegNumber,
                Status = (int)p.Status,
                SnHistories = p.SnHistories.Select(q => new SnHistoryDTO
                {
                    Id = q.Id,
                    FiNumber = q.FiNumber,
                    CSN = q.CSN,
                    CSR = q.CSR,
                    TSN = q.TSN,
                    TSR = q.TSR,
                    InstallDate = q.InstallDate,
                    RemoveDate = q.RemoveDate,
                    Sn = q.Sn,
                    SnRegId = q.SnRegId,
                    AircraftId = q.AircraftId,
                    AcReg = dbAircraft.FirstOrDefault(c=>c.Id==q.AircraftId).RegNumber,
                }).ToList(),
            }).ToList();
            var engineSnRegs = query.ApplyTo(_unitOfWork.CreateSet<EngineReg>()).Select(p => new ApuEngineSnRegDTO
            {
                Id = p.Id,
                Pn = p.Pn,
                Sn = p.Sn,
                TSN = p.TSN,
                TSR = p.TSR,
                CSN = p.CSN,
                CSR = p.CSR,
                IsStop = p.IsStop,
                InstallDate = p.InstallDate,
                AircraftId = p.AircraftId,
                PnRegId = p.PnRegId,
                RegNumber = p.RegNumber,
                Status = (int)p.Status,
                SnHistories = p.SnHistories.Select(q => new SnHistoryDTO
                {
                    Id = q.Id,
                    FiNumber = q.FiNumber,
                    CSN = q.CSN,
                    CSR = q.CSR,
                    TSN = q.TSN,
                    TSR = q.TSR,
                    InstallDate = q.InstallDate,
                    RemoveDate = q.RemoveDate,
                    Sn = q.Sn,
                    SnRegId = q.SnRegId,
                    AircraftId = q.AircraftId,
                    AcReg = dbAircraft.FirstOrDefault(c => c.Id == q.AircraftId).RegNumber,
                }).ToList(),
            }).ToList();

            return apuSnRegs.Union(engineSnRegs).AsQueryable();
        }
    }
}
