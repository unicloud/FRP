﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/23，11:39
// 方案：FRP
// 项目：Application.PartBC.Query
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.OilMonitorAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PartBC.Query.OilMonitorQueries
{
    public class OilMonitorQuery : IOilMonitorQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public OilMonitorQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region IOilMonitorQuery 成员

        public IQueryable<EngineOilDTO> EngineOilDTOQuery(QueryBuilder<SnReg> query)
        {
            var result =
                query.ApplyTo(_unitOfWork.CreateSet<SnReg>())
                    .OfType<EngineReg>()
                    .Where(o => o.NeedMonitor)
                    .Select(o => new EngineOilDTO
                    {
                        Id = o.Id,
                        Sn = o.Sn,
                        Status = (int) o.MonitorStatus
                    });
            return result;
        }

        public IQueryable<APUOilDTO> APUOilDTOQuery(QueryBuilder<SnReg> query)
        {
            var result =
                query.ApplyTo(_unitOfWork.CreateSet<SnReg>())
                    .OfType<APUReg>()
                    .Where(o => o.NeedMonitor)
                    .Select(o => new APUOilDTO
                    {
                        Id = o.Id,
                        Sn = o.Sn,
                        Status = (int) o.MonitorStatus
                    });
            return result;
        }

        public IQueryable<OilMonitorDTO> OilMonitorDTOQuery(QueryBuilder<OilMonitor> query)
        {
            var result = query.ApplyTo(_unitOfWork.CreateSet<OilMonitor>()).Select(m => new OilMonitorDTO
            {
                Id = m.Id,
                OilUserID = m.SnRegID,
                Date = m.Date,
                TSN = m.TSN,
                TSR = m.TSR,
                TotalRate = m.TotalRate,
                IntervalRate = m.IntervalRate,
                DeltaIntervalRate = m.DeltaIntervalRate,
                AverageRate3 = m.AverageRate3,
                AverageRate7 = m.AverageRate7
            });
            return result;
        }

        #endregion
    }
}