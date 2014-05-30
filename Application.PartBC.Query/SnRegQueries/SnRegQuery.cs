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

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PartBC.Query.SnRegQueries
{
    /// <summary>
    ///     SnReg查询
    /// </summary>
    public class SnRegQuery : ISnRegQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public SnRegQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     SnReg查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>SnRegDTO集合</returns>
        public IQueryable<SnRegDTO> SnRegDTOQuery(QueryBuilder<SnReg> query)
        {
            DbSet<SnHistory> snHistories = _unitOfWork.CreateSet<SnHistory>();
            DbSet<Aircraft> dbAircrafts = _unitOfWork.CreateSet<Aircraft>();

            return query.ApplyTo(_unitOfWork.CreateSet<SnReg>()).Select(p => new SnRegDTO
            {
                Id = p.Id,
                Pn = p.Pn,
                Sn = p.Sn,
                IsStop = p.IsStop,
                InstallDate = p.InstallDate,
                AircraftId = p.AircraftId,
                PnRegId = p.PnRegId,
                RegNumber = p.RegNumber,
                Status = (int) p.Status,
                IsLife = p.IsLife,
                IsLifeCst = p.IsLifeCst,
                TimeRate = p.TimeRate,
                CycleRate = p.CycleRate,
                LiftMonitors = p.LifeMonitors.Select(q => new LifeMonitorDTO
                {
                    Id = q.Id,
                    MointorEnd = q.MointorEnd,
                    MointorStart = q.MointorStart,
                    WorkDescription = q.WorkDescription,
                    SnRegId = q.SnRegId,
                    Sn = p.Sn,
                }).ToList(),
                SnHistories =
                    snHistories.Where(q => q.SnRegId == p.Id).Select(r => new SnHistoryDTO
                    {
                        Id = r.Id,
                        Sn = r.Sn,
                        SnRegId = r.SnRegId,
                        Pn = r.Pn,
                        PnRegId = r.PnRegId,
                        CSN = r.CSN,
                        TSN = r.TSN,
                        AircraftId = r.AircraftId,
                        ActionDate = r.ActionDate,
                        ActionType = (int)r.ActionType,
                        ActionNo = r.RemInstRecord.ActionNo,
                        ActionReason = r.RemInstRecord.Reason,
                        RegNumber = dbAircrafts.FirstOrDefault(c => c.Id == r.AircraftId).RegNumber,
                        RemInstRecordId=r.RemInstRecordId,
                    }).ToList(),
            });
        }

        /// <summary>
        ///     ApuEngineSnReg查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>ApuEngineSnRegDTO集合</returns>
        public IQueryable<ApuEngineSnRegDTO> ApuEngineSnRegDTOQuery(QueryBuilder<SnReg> query)
        {
            DbSet<SnHistory> snHistories = _unitOfWork.CreateSet<SnHistory>();
            DbSet<Aircraft> dbAircrafts = _unitOfWork.CreateSet<Aircraft>();

            List<ApuEngineSnRegDTO> apuSnRegs =
                query.ApplyTo(_unitOfWork.CreateSet<SnReg>()).Select(p => new ApuEngineSnRegDTO
                {
                    Id = p.Id,
                    Pn = p.Pn,
                    Sn = p.Sn,
                    IsStop = p.IsStop,
                    InstallDate = p.InstallDate,
                    AircraftId = p.AircraftId,
                    PnRegId = p.PnRegId,
                    RegNumber = p.RegNumber,
                    Status = (int) p.Status,
                    SnHistories =
                        snHistories.Where(q => q.SnRegId == p.Id).Select(r => new SnHistoryDTO
                        {
                            Id = r.Id,
                            Sn = r.Sn,
                            SnRegId = r.SnRegId,
                            Pn = r.Pn,
                            PnRegId = r.PnRegId,
                            CSN = r.CSN,
                            TSN = r.TSN,
                            AircraftId = r.AircraftId,
                            ActionDate = r.ActionDate,
                            ActionType = (int)r.ActionType,
                            ActionNo = r.RemInstRecord.ActionNo,
                            ActionReason = r.RemInstRecord.Reason,
                            RegNumber = dbAircrafts.FirstOrDefault(c => c.Id == r.AircraftId).RegNumber,
                            RemInstRecordId = r.RemInstRecordId,
                        }).ToList(),
                }).ToList();
            List<ApuEngineSnRegDTO> engineSnRegs =
                query.ApplyTo(_unitOfWork.CreateSet<EngineReg>()).Select(p => new ApuEngineSnRegDTO
                {
                    Id = p.Id,
                    Pn = p.Pn,
                    Sn = p.Sn,
                    IsStop = p.IsStop,
                    InstallDate = p.InstallDate,
                    AircraftId = p.AircraftId,
                    PnRegId = p.PnRegId,
                    RegNumber = p.RegNumber,
                    Status = (int) p.Status,
                    SnHistories =
                        snHistories.Where(q => q.SnRegId == p.Id).Select(r => new SnHistoryDTO
                        {
                            Id = r.Id,
                            Sn = r.Sn,
                            SnRegId = r.SnRegId,
                            Pn = r.Pn,
                            PnRegId = r.PnRegId,
                            CSN = r.CSN,
                            TSN = r.TSN,
                            ActionDate = r.ActionDate,
                            ActionType = (int)r.ActionType,
                            ActionNo = r.RemInstRecord.ActionNo,
                            ActionReason = r.RemInstRecord.Reason,
                            AircraftId = r.AircraftId,
                            RegNumber = dbAircrafts.FirstOrDefault(c => c.Id == r.AircraftId).RegNumber,
                            RemInstRecordId = r.RemInstRecordId,
                        }).ToList(),
                }).ToList();

            return apuSnRegs.Union(engineSnRegs).AsQueryable();
        }
    }
}