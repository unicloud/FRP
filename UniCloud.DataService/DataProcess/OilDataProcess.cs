#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：10:34
// 方案：FRP
// 项目：DataService
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg;
using UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.DataService.DataProcess
{
    public class OilDataProcess
    {
        private readonly PartBCUnitOfWork _unitOfWork;

        public OilDataProcess(PartBCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void ProcessEngine()
        {
            var engines = _unitOfWork.CreateSet<SnReg>().OfType<EngineReg>();
            foreach (var engine in engines)
            {
                var lastTSR = GetLastTSR(engine.Sn);
                var flights =
                    _unitOfWork.CreateSet<FlightLog>()
                        .Where(f => f.FlightDate > lastTSR.ActionDate)
                        .OrderBy(f => f.TakeOff)
                        .ToList();
                if (!flights.Any()) continue;
                var idx = flights.Count - 1;
                var count = flights[idx].ENG1OilArr + flights[idx].ENG2OilArr + flights[idx].ENG3OilArr +
                              flights[idx].ENG4OilArr > 0
                    ? idx + 1
                    : idx;
                

                var tsr =
                    _unitOfWork.CreateSet<FlightLog>()
                        .Where(f => f.FlightDate > lastTSR.ActionDate)
                        .Sum(f => f.FlightHours);
                var qsr = _unitOfWork.CreateSet<FlightLog>()
                    .Where(f => f.FlightDate > lastTSR.ActionDate)
                    .Sum(f => f.FlightHours);

            }
        }

        public void ProcessAPU()
        {
            var apus = _unitOfWork.CreateSet<SnReg>().OfType<APUReg>().ToList();
            apus.ForEach(apu => { var lastTSR = GetLastTSR(apu.Sn); });
        }

        /// <summary>
        ///     获取序号件最近一次维修的操作记录。
        /// </summary>
        /// <param name="sn">序列号</param>
        /// <returns>序号件最近一次维修的操作记录。</returns>
        private SnHistory GetLastTSR(string sn)
        {
            return
                _unitOfWork.CreateSet<SnHistory>()
                    .Where(s => s.Sn == sn && s.ActionType == ActionType.装上)
                    .OrderBy(s => s.ActionDate)
                    .LastOrDefault();
        }
    }
}