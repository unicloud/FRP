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

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg;
using UniCloud.Domain.PartBC.Aggregates.OilMonitorAgg;
using UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.DataService.DataProcess
{
    /// <summary>
    ///     滑油消耗数据处理，必须过零点之后执行。
    /// </summary>
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
                var oilMonitors = CreateOilMonitors(lastTSR);

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

        /// <summary>
        ///     对每一个监控对象，自滑油监控最后一条记录至当天，每天计算一条监控记录。
        /// </summary>
        /// <remarks>
        ///     初始的时候，从最近一次装上开始计算。
        /// </remarks>
        /// <param name="lastTSR">最近一次装上记录</param>
        /// <returns>滑油监控记录集合。</returns>
        private List<OilMonitor> CreateOilMonitors(SnHistory lastTSR)
        {
            var oilMonitors = new List<OilMonitor>();
            var positon = lastTSR.Position;
            var lastOilMonitor = _unitOfWork.CreateSet<OilMonitor>().OrderBy(o => o.Date).LastOrDefault();
            var lastDate = lastOilMonitor == null ? lastTSR.ActionDate.Date : lastOilMonitor.Date.Date;
            lastDate = lastDate.AddDays(1);
            var flights =
                _unitOfWork.CreateSet<FlightLog>()
                    .Where(f => f.FlightDate > lastTSR.ActionDate)
                    .OrderBy(f => f.TakeOff);
            while (DateTime.Today > lastDate)
            {
                decimal qsr;
                var tsr = flights.TakeWhile(f => f.TakeOff < lastDate.AddDays(1)).Sum(f => f.FlightHours);
                switch (positon)
                {
                    case Position.发动机1:
                        qsr =
                            flights.TakeWhile(f => f.TakeOff < lastDate.AddDays(1))
                                .Sum(f => f.ENG1OilDep + f.ENG1OilArr);
                        oilMonitors.Add(CreateOilMonitor(lastDate, tsr, qsr));
                        break;
                    case Position.发动机2:
                        qsr =
                            flights.TakeWhile(f => f.TakeOff < lastDate.AddDays(1))
                                .Sum(f => f.ENG2OilDep + f.ENG2OilArr);
                        oilMonitors.Add(CreateOilMonitor(lastDate, tsr, qsr));
                        break;
                    case Position.发动机3:
                        qsr =
                            flights.TakeWhile(f => f.TakeOff < lastDate.AddDays(1))
                                .Sum(f => f.ENG3OilDep + f.ENG3OilArr);
                        oilMonitors.Add(CreateOilMonitor(lastDate, tsr, qsr));
                        break;
                    case Position.发动机4:
                        qsr =
                            flights.TakeWhile(f => f.TakeOff < lastDate.AddDays(1))
                                .Sum(f => f.ENG4OilDep + f.ENG4OilArr);
                        oilMonitors.Add(CreateOilMonitor(lastDate, tsr, qsr));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                lastDate = lastDate.AddDays(1);
            }
            return oilMonitors;
        }

        /// <summary>
        ///     创建滑油监控记录。
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="tsr">自上一次装上以来飞行时间</param>
        /// <param name="qsr">自上一次装上以来滑油添加总量</param>
        /// <returns>滑油监控记录</returns>
        private OilMonitor CreateOilMonitor(DateTime date, decimal tsr, decimal qsr)
        {

        }
    }
}