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

        public IQueryable<EngineOilDTO> EngineOilDTOQuery(QueryBuilder<OilMonitor> query)
        {
            var result =
                query.ApplyTo(_unitOfWork.CreateSet<OilMonitor>().OfType<EngineOil>()).Select(o => new EngineOilDTO
                {
                    Id = o.Id,
                    Date = o.Date,
                    TSN = o.TSN,
                    TSR = o.TSR,
                    TotalRate = o.TotalRate,
                    IntervalRate = o.IntervalRate,
                    DeltaIntervalRate = o.DeltaIntervalRate,
                    AverageRate3 = o.AverageRate3,
                    AverageRate7 = o.AverageRate7
                });
            return result;
        }

        public IQueryable<APUOilDTO> APUOilDTOQuery(QueryBuilder<OilMonitor> query)
        {
            var result = query.ApplyTo(_unitOfWork.CreateSet<OilMonitor>().OfType<APUOil>()).Select(o => new APUOilDTO
            {
                Id = o.Id,
                Date = o.Date,
                TSN = o.TSN,
                TSR = o.TSR,
                TotalRate = o.TotalRate,
                IntervalRate = o.IntervalRate,
                DeltaIntervalRate = o.DeltaIntervalRate,
                AverageRate3 = o.AverageRate3,
                AverageRate7 = o.AverageRate7
            });
            return result;
        }

        #endregion
    }
}