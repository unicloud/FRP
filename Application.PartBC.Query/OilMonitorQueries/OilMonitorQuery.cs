#region 版本信息

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
using UniCloud.Domain.PartBC.Aggregates.OilUserAgg;
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

        public IQueryable<EngineOilDTO> EngineOilDTOQuery(QueryBuilder<OilUser> query)
        {
            var result =
                query.ApplyTo(_unitOfWork.CreateSet<OilUser>().OfType<EngineOil>()).Select(o => new EngineOilDTO
                {
                    Id = o.Id,
                    Sn = o.Sn,
                    TSN = o.TSN,
                    TSR = o.TSR,
                    CSN = o.CSN,
                    CSR = o.CSR,
                    Status = (int) o.MonitorStatus,
                    OilMonitors = o.OilMonitors.Select(m => new OilMonitorDTO
                    {
                        Id = m.Id,
                        Date = m.Date,
                        TSN = m.TSN,
                        TSR = m.TSR,
                        TotalRate = m.TotalRate,
                        IntervalRate = m.IntervalRate,
                        DeltaIntervalRate = m.DeltaIntervalRate,
                        AverageRate3 = m.AverageRate3,
                        AverageRate7 = m.AverageRate7
                    }).ToList()
                });
            return result;
        }

        public IQueryable<APUOilDTO> APUOilDTOQuery(QueryBuilder<OilUser> query)
        {
            var result = query.ApplyTo(_unitOfWork.CreateSet<OilUser>().OfType<APUOil>()).Select(o => new APUOilDTO
            {
                Id = o.Id,
                Sn = o.Sn,
                TSN = o.TSN,
                TSR = o.TSR,
                CSN = o.CSN,
                CSR = o.CSR,
                Status = (int) o.MonitorStatus,
                OilMonitors = o.OilMonitors.Select(m => new OilMonitorDTO
                {
                    Id = m.Id,
                    Date = m.Date,
                    TSN = m.TSN,
                    TSR = m.TSR,
                    TotalRate = m.TotalRate,
                    IntervalRate = m.IntervalRate,
                    DeltaIntervalRate = m.DeltaIntervalRate,
                    AverageRate3 = m.AverageRate3,
                    AverageRate7 = m.AverageRate7
                }).ToList()
            });
            return result;
        }

        #endregion
    }
}