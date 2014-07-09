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
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.FlightLogAgg;
using UniCloud.Domain.PartBC.Aggregates.OilMonitorAgg;
using UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.ThresholdAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DataService.DataProcess
{
    /// <summary>
    ///     滑油消耗数据处理，必须过零点之后执行。
    /// </summary>
    public class OilDataProcess
    {
        private readonly PartBCUnitOfWork _unitOfWork = UniContainer.Resolve<PartBCUnitOfWork>();

        public void ProcessEngine(List<FlightLog> flights = null)
        {
            var engines = _unitOfWork.CreateSet<EngineReg>().ToList();
            foreach (var engine in engines)
            {
                var lastTSR = GetLastTSR(engine.Sn);
                var ac =
                    _unitOfWork.CreateSet<Aircraft>()
                        .Where(a => a.Id == lastTSR.AircraftId)
                        .Select(f => f.RegNumber)
                        .FirstOrDefault();
                if (flights == null)
                {
                    flights =
                        _unitOfWork.CreateSet<FlightLog>()
                            .Where(f => f.AcReg == ac && f.FlightDate > lastTSR.ActionDate)
                            .OrderBy(f => new {f.FlightDate, f.TakeOff})
                            .ToList();
                }
                // 计算添加滑油监控记录
                var newOilMonitors = CreateEngineOils(lastTSR, engine, flights);
                if (newOilMonitors.Count == 0) continue;
                newOilMonitors.ForEach(eo => _unitOfWork.OilMonitors.Add(eo));
                // 计算3日、7日均值
                CalAverageRate3(lastTSR, ref newOilMonitors);
                CalAverageRate7(lastTSR, ref newOilMonitors);
                // 根据超限情况修改监控对象的滑油监控状态
                SetEngineOilStatus(engine);

                _unitOfWork.Commit();
            }
        }

        public void ProcessAPU(List<FlightLog> flights)
        {
            var apus = _unitOfWork.CreateSet<APUReg>().ToList();
            foreach (var apu in apus)
            {
                var lastTSR = GetLastTSR(apu.Sn);
                var ac =
                    _unitOfWork.CreateSet<Aircraft>()
                        .Where(a => a.Id == lastTSR.AircraftId)
                        .Select(f => f.RegNumber)
                        .FirstOrDefault();
                if (!flights.Any())
                {
                    flights =
                        _unitOfWork.CreateSet<FlightLog>()
                            .Where(f => f.AcReg == ac && f.FlightDate > lastTSR.ActionDate)
                            .OrderBy(f => new {f.FlightDate, f.TakeOff})
                            .ToList();
                }
                // 计算添加滑油监控记录
                var newOilMonitors = CreateAPUOils(lastTSR, apu, flights);
                if (newOilMonitors.Count == 0) continue;
                newOilMonitors.ForEach(ao => _unitOfWork.OilMonitors.Add(ao));
                // 计算3日、7日均值
                CalAverageRate3(lastTSR, ref newOilMonitors);
                CalAverageRate7(lastTSR, ref newOilMonitors);
                // 根据超限情况修改监控对象的滑油监控状态
                SetAPUOilStatus(apu);

                _unitOfWork.Commit();
            }
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
                    .OrderByDescending(s => s.ActionDate)
                    .FirstOrDefault();
        }

        /// <summary>
        ///     获取开始计算日期
        /// </summary>
        /// <param name="flights">最近一次装上以来的飞行日志集合</param>
        /// <returns>开始计算日期</returns>
        private DateTime GetStartDate(IReadOnlyList<FlightLog> flights)
        {
            var lastOilMonitor = _unitOfWork.CreateSet<OilMonitor>().OrderByDescending(o => o.Date).FirstOrDefault();
            // 最近滑油监控记录日期
            var lastDate = lastOilMonitor == null || DateTime.Today.AddDays(-30) < flights[0].FlightDate
                ? flights[0].FlightDate
                : DateTime.Today.AddDays(-30);
            var calDate = lastDate.AddDays(1);
            return calDate;
        }

        /// <summary>
        ///     创建发动机滑油监控记录。
        /// </summary>
        /// <remarks>
        ///     对每一台发动机，自滑油监控最后一条记录至当天，每天计算一条监控记录。
        ///     初始的时候，从最近一次装上开始计算。
        /// </remarks>
        /// <param name="lastTSR">最近一次装上记录</param>
        /// <param name="engineReg">发动机</param>
        /// <param name="flights">最近一次装上以来的飞行日志集合</param>
        private List<OilMonitor> CreateEngineOils(SnHistory lastTSR, EngineReg engineReg,
            IReadOnlyList<FlightLog> flights)
        {
            if (lastTSR == null) throw new ArgumentNullException("lastTSR");
            var oilMonitors = new List<OilMonitor>();
            // 开始计算日期
            var startDate = GetStartDate(flights);
            if (!flights.Any()) return oilMonitors;
            // 计算截止日期，飞行日志最后一天的次日，最后一天为当天的则为当天。
            var endDate = flights.Last().FlightDate.Date == DateTime.Today
                ? DateTime.Today
                : flights.Last().FlightDate.Date.AddDays(1);
            while (endDate.Subtract(startDate).TotalDays > 0)
            {
                decimal qsr, interval, deltaInterval, lastInterval;
                var tsr = flights.TakeWhile(f => f.FlightDate < startDate.AddDays(1)).Sum(f => f.FlightHours);
                switch (lastTSR.Position)
                {
                    case Position.发动机1:
                        qsr =
                            flights.TakeWhile(f => f.FlightDate < startDate.AddDays(1))
                                .Sum(f => f.ENG1OilDep + f.ENG1OilArr);
                        lastInterval =
                            _unitOfWork.CreateSet<OilMonitor>()
                                .Where(o => o.IntervalRate > 0)
                                .Select(o => o.IntervalRate)
                                .FirstOrDefault();
                        CalIntervalOil(
                            flights.Select(
                                f => Tuple.Create(f.FlightDate, f.TakeOff, f.ENG1OilDep, f.ENG1OilArr, f.FlightHours))
                                .ToList(), startDate, out interval);
                        deltaInterval = lastInterval > 0 ? interval - lastInterval : 0;
                        oilMonitors.Add(CreateEngineOil(lastTSR, engineReg, startDate, tsr, qsr, interval, deltaInterval));
                        break;
                    case Position.发动机2:
                        qsr =
                            flights.TakeWhile(f => f.FlightDate < startDate.AddDays(1))
                                .Sum(f => f.ENG2OilDep + f.ENG2OilArr);
                        lastInterval =
                            _unitOfWork.CreateSet<OilMonitor>()
                                .Where(o => o.IntervalRate > 0)
                                .Select(o => o.IntervalRate)
                                .FirstOrDefault();
                        CalIntervalOil(
                            flights.Select(
                                f => Tuple.Create(f.FlightDate, f.TakeOff, f.ENG2OilDep, f.ENG2OilArr, f.FlightHours))
                                .ToList(), startDate, out interval);
                        deltaInterval = lastInterval > 0 ? interval - lastInterval : 0;
                        oilMonitors.Add(CreateEngineOil(lastTSR, engineReg, startDate, tsr, qsr, interval, deltaInterval));
                        break;
                    case Position.发动机3:
                        qsr =
                            flights.TakeWhile(f => f.FlightDate < startDate.AddDays(1))
                                .Sum(f => f.ENG3OilDep + f.ENG3OilArr);
                        lastInterval =
                            _unitOfWork.CreateSet<OilMonitor>()
                                .Where(o => o.IntervalRate > 0)
                                .Select(o => o.IntervalRate)
                                .FirstOrDefault();
                        CalIntervalOil(
                            flights.Select(
                                f => Tuple.Create(f.FlightDate, f.TakeOff, f.ENG3OilDep, f.ENG3OilArr, f.FlightHours))
                                .ToList(), startDate, out interval);
                        deltaInterval = lastInterval > 0 ? interval - lastInterval : 0;
                        oilMonitors.Add(CreateEngineOil(lastTSR, engineReg, startDate, tsr, qsr, interval, deltaInterval));
                        break;
                    case Position.发动机4:
                        qsr =
                            flights.TakeWhile(f => f.FlightDate < startDate.AddDays(1))
                                .Sum(f => f.ENG4OilDep + f.ENG4OilArr);
                        lastInterval =
                            _unitOfWork.CreateSet<OilMonitor>()
                                .Where(o => o.IntervalRate > 0)
                                .Select(o => o.IntervalRate)
                                .FirstOrDefault();
                        CalIntervalOil(
                            flights.Select(
                                f => Tuple.Create(f.FlightDate, f.TakeOff, f.ENG4OilDep, f.ENG4OilArr, f.FlightHours))
                                .ToList(), startDate, out interval);
                        deltaInterval = lastInterval > 0 ? interval - lastInterval : 0;
                        oilMonitors.Add(CreateEngineOil(lastTSR, engineReg, startDate, tsr, qsr, interval, deltaInterval));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                startDate = startDate.AddDays(1);
            }
            return oilMonitors;
        }

        /// <summary>
        ///     创建APU滑油监控记录。
        /// </summary>
        /// <remarks>
        ///     对每一台APU，自滑油监控最后一条记录至当天，每天计算一条监控记录。
        ///     初始的时候，从最近一次装上开始计算。
        /// </remarks>
        /// <param name="lastTSR">最近一次装上记录</param>
        /// <param name="apuReg">APU</param>
        /// <param name="flights">最近一次装上以来的飞行日志集合</param>
        /// <returns>滑油监控记录集合。</returns>
        private List<OilMonitor> CreateAPUOils(SnHistory lastTSR, APUReg apuReg, IReadOnlyList<FlightLog> flights)
        {
            var oilMonitors = new List<OilMonitor>();
            // 开始计算日期
            var startDate = GetStartDate(flights);
            if (!flights.Any()) return oilMonitors;
            // 计算截止日期，飞行日志最后一天的次日，最后一天为当天的则为当天。
            var endDate = flights.Last().FlightDate.Date == DateTime.Today
                ? DateTime.Today
                : flights.Last().FlightDate.Date.AddDays(1);
            while (endDate.Subtract(startDate).TotalDays > 0)
            {
                decimal interval;
                var tsr = flights.TakeWhile(f => f.FlightDate < startDate.AddDays(1)).Sum(f => f.FlightHours);
                var qsr = flights.TakeWhile(f => f.FlightDate < startDate.AddDays(1))
                    .Sum(f => f.ApuOilDep + f.ApuOilArr);
                var lastInterval =
                    _unitOfWork.CreateSet<OilMonitor>()
                        .Where(o => o.IntervalRate > 0)
                        .Select(o => o.IntervalRate)
                        .FirstOrDefault();
                CalIntervalOil(
                    flights.Select(f => Tuple.Create(f.FlightDate, f.TakeOff, f.ApuOilDep, f.ApuOilArr, f.FlightHours))
                        .ToList(), startDate, out interval);
                var deltaInterval = lastInterval > 0 ? interval - lastInterval : 0;
                _unitOfWork.CreateSet<OilMonitor>()
                    .Add(CreateAPUOil(lastTSR, apuReg, startDate, tsr, qsr, interval, deltaInterval));
                startDate = startDate.AddDays(1);
            }
            return oilMonitors;
        }

        /// <summary>
        ///     创建发动机滑油监控记录。
        /// </summary>
        /// <param name="lastTSR">最近一次装上记录</param>
        /// <param name="engineReg">发动机</param>
        /// <param name="calDate">计算日期</param>
        /// <param name="tsr">自上一次装上以来飞行时间</param>
        /// <param name="qsr">自上一次装上以来滑油添加总量</param>
        /// <param name="interval">区间滑油消耗率</param>
        /// <param name="deltaInterval">区间滑油消耗率变化量</param>
        /// <returns>滑油监控记录</returns>
        private static OilMonitor CreateEngineOil(SnHistory lastTSR, EngineReg engineReg, DateTime calDate, decimal tsr,
            decimal qsr, decimal interval, decimal deltaInterval)
        {
            var tsn = lastTSR.TSN + tsr;
            var toc = tsr == 0 ? 0 : Math.Round(qsr/tsr, 2);
            var oilMonitor = OilMonitorFactory.CreateEngineOil(engineReg, calDate, tsn, tsr, toc, interval,
                deltaInterval);
            return oilMonitor;
        }

        /// <summary>
        ///     创建APU滑油监控记录。
        /// </summary>
        /// <param name="lastTSR">最近一次装上记录</param>
        /// <param name="apuReg">APU</param>
        /// <param name="calDate">计算日期</param>
        /// <param name="tsr">自上一次装上以来飞行时间</param>
        /// <param name="qsr">自上一次装上以来滑油添加总量</param>
        /// <param name="interval">区间滑油消耗率</param>
        /// <param name="deltaInterval">区间滑油消耗率变化量</param>
        /// <returns>滑油监控记录</returns>
        private static OilMonitor CreateAPUOil(SnHistory lastTSR, APUReg apuReg, DateTime calDate, decimal tsr,
            decimal qsr, decimal interval, decimal deltaInterval)
        {
            var tsn = lastTSR.TSN + tsr;
            var toc = tsr == 0 ? 0 : Math.Round(qsr/tsr, 2);
            var oilMonitor = OilMonitorFactory.CreateAPUOil(apuReg, calDate, tsn, tsr, toc, interval, deltaInterval);
            return oilMonitor;
        }

        /// <summary>
        ///     计算区间耗率和区间耗率增量。
        /// </summary>
        /// <param name="flights">飞行任务</param>
        /// <param name="calDate">计算日期</param>
        /// <param name="interval">区间耗率</param>
        private static void CalIntervalOil(List<Tuple<DateTime, string, decimal, decimal, decimal>> flights,
            DateTime calDate, out decimal interval)
        {
            var lastDep = flights.LastOrDefault(f => f.Item1.Date == calDate && f.Item3 > 0);
            var lastArr = flights.LastOrDefault(f => f.Item1.Date == calDate && f.Item4 > 0);
            var preFlights = flights.TakeWhile(f => f.Item1.Date < calDate).ToList();
            var preDep = preFlights.LastOrDefault(f => f.Item3 > 0);
            var preArr = preFlights.LastOrDefault(f => f.Item4 > 0);
            if (lastDep != null)
            {
                var qsa = lastDep.Item3;
                var lastIdx = flights.LastIndexOf(lastDep);
                var depIdx = flights.LastIndexOf(preDep);
                var arrIdx = flights.LastIndexOf(preArr);
                var tsa = depIdx > arrIdx
                    ? flights.Skip(depIdx).Take(lastIdx - depIdx).Sum(f => f.Item5)
                    : flights.Skip(arrIdx + 1).Take(lastIdx - depIdx).Sum(f => f.Item5);
                interval = tsa == 0 ? 0 : Math.Round(qsa/tsa, 2);
                return;
            }
            if (lastArr != null)
            {
                var qsa = lastArr.Item4;
                var lastIdx = flights.LastIndexOf(lastArr);
                var depIdx = flights.LastIndexOf(preDep);
                var arrIdx = flights.LastIndexOf(preArr);
                var tsa = depIdx > arrIdx
                    ? flights.Skip(depIdx).Take(lastIdx - depIdx + 1).Sum(f => f.Item5)
                    : flights.Skip(arrIdx + 1).Take(lastIdx - depIdx + 1).Sum(f => f.Item5);
                interval = tsa == 0 ? 0 : Math.Round(qsa/tsa, 2);
                return;
            }
            interval = 0;
        }

        /// <summary>
        ///     计算滑油耗率3日均线
        /// </summary>
        /// <param name="lastTSR">最近一次装上记录</param>
        /// <param name="currents">新增的滑油监控记录</param>
        private void CalAverageRate3(SnHistory lastTSR, ref List<OilMonitor> currents)
        {
            if (currents.Count == 0) return;
            var startDate = currents.First().Date.AddDays(-3);
            var oilMonitors =
                _unitOfWork.CreateSet<OilMonitor>()
                    .Where(o => o.SnRegID == lastTSR.SnRegId && o.Date > startDate)
                    .OrderBy(o => o.Date)
                    .ToList();
            var count = oilMonitors.Count;
            oilMonitors.AddRange(currents);
            for (var i = count; i < oilMonitors.Count; i++)
            {
                if (i < 2) continue;
                var average = oilMonitors.Skip(i - 2).Take(3).Average(o => o.TotalRate);
                oilMonitors[i].SetAverageRate3(average);
            }
        }

        /// <summary>
        ///     计算滑油耗率7日均线
        /// </summary>
        /// <param name="lastTSR">最近一次装上记录</param>
        /// <param name="currents">新增的滑油监控记录</param>
        private void CalAverageRate7(SnHistory lastTSR, ref List<OilMonitor> currents)
        {
            if (currents.Count == 0) return;
            var startDate = currents.First().Date.AddDays(-7);
            var oilMonitors =
                _unitOfWork.CreateSet<OilMonitor>()
                    .Where(o => o.SnRegID == lastTSR.SnRegId && o.Date > startDate)
                    .OrderBy(o => o.Date)
                    .ToList();
            var count = oilMonitors.Count;
            oilMonitors.AddRange(currents);
            for (var i = count; i < oilMonitors.Count; i++)
            {
                if (i < 6) continue;
                var average = oilMonitors.Skip(i - 6).Take(7).Average(o => o.TotalRate);
                oilMonitors[i].SetAverageRate7(average);
            }
        }

        /// <summary>
        ///     设置发动机滑油监控状态
        /// </summary>
        /// <param name="engineReg">发动机序号件</param>
        private void SetEngineOilStatus(EngineReg engineReg)
        {
            var threshold = _unitOfWork.CreateSet<Threshold>().FirstOrDefault(t => t.PnRegId == engineReg.PnRegId);
            if (threshold == null) return;
            var weekOils =
                _unitOfWork.CreateSet<OilMonitor>()
                    .Where(o => o.SnRegID == engineReg.Id && o.Date > DateTime.Today.AddDays(-8));
            if (
                weekOils.Any(
                    o =>
                        o.TotalRate > threshold.TotalThreshold || o.IntervalRate > threshold.IntervalThreshold ||
                        o.DeltaIntervalRate > threshold.DeltaIntervalThreshold ||
                        o.AverageRate3 > threshold.Average3Threshold || o.AverageRate7 > threshold.Average7Threshold))
            {
                engineReg.SetMonitorStatus(OilMonitorStatus.警告);
                return;
            }
            var monthOils =
                _unitOfWork.CreateSet<OilMonitor>()
                    .Where(o => o.SnRegID == engineReg.Id && o.Date > DateTime.Today.AddDays(-31));
            if (
                monthOils.Any(
                    o =>
                        o.TotalRate > threshold.TotalThreshold || o.IntervalRate > threshold.IntervalThreshold ||
                        o.DeltaIntervalRate > threshold.DeltaIntervalThreshold ||
                        o.AverageRate3 > threshold.Average3Threshold || o.AverageRate7 > threshold.Average7Threshold))
            {
                engineReg.SetMonitorStatus(OilMonitorStatus.关注);
            }
        }

        /// <summary>
        ///     设置APU滑油监控状态
        /// </summary>
        /// <param name="apuReg">APU序号件</param>
        private void SetAPUOilStatus(APUReg apuReg)
        {
            var threshold = _unitOfWork.CreateSet<Threshold>().FirstOrDefault(t => t.PnRegId == apuReg.PnRegId);
            if (threshold == null) return;
            var weekOils =
                _unitOfWork.CreateSet<OilMonitor>()
                    .Where(o => o.SnRegID == apuReg.Id && o.Date > DateTime.Today.AddDays(-8));
            if (
                weekOils.Any(
                    o =>
                        o.TotalRate > threshold.TotalThreshold || o.IntervalRate > threshold.IntervalThreshold ||
                        o.DeltaIntervalRate > threshold.DeltaIntervalThreshold ||
                        o.AverageRate3 > threshold.Average3Threshold || o.AverageRate7 > threshold.Average7Threshold))
            {
                apuReg.SetMonitorStatus(OilMonitorStatus.警告);
                return;
            }
            var monthOils =
                _unitOfWork.CreateSet<OilMonitor>()
                    .Where(o => o.SnRegID == apuReg.Id && o.Date > DateTime.Today.AddDays(-31));
            if (
                monthOils.Any(
                    o =>
                        o.TotalRate > threshold.TotalThreshold || o.IntervalRate > threshold.IntervalThreshold ||
                        o.DeltaIntervalRate > threshold.DeltaIntervalThreshold ||
                        o.AverageRate3 > threshold.Average3Threshold || o.AverageRate7 > threshold.Average7Threshold))
            {
                apuReg.SetMonitorStatus(OilMonitorStatus.关注);
            }
        }
    }
}