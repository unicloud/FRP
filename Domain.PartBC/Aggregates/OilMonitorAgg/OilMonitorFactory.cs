#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/22，14:12
// 方案：FRP
// 项目：Domain.PartBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.OilMonitorAgg
{
    /// <summary>
    ///     滑油监控工厂
    /// </summary>
    public static class OilMonitorFactory
    {
        /// <summary>
        ///     创建发动机滑油消耗数据
        /// </summary>
        /// <param name="engineReg">发动机</param>
        /// <param name="date">日期</param>
        /// <param name="tsn">TSN</param>
        /// <param name="tsr">TSR</param>
        /// <param name="totalRate">总消耗率</param>
        /// <param name="intervalRate">区间消耗率</param>
        /// <param name="deltaIntervalRate">区间消耗率增量</param>
        /// <param name="averageRate3">总消耗率3天移动平均</param>
        /// <param name="averageRate7">总消耗率7天移动平均</param>
        /// <returns>滑油消耗数据</returns>
        public static OilMonitor CreateEngineOil(
            EngineReg engineReg,
            DateTime date,
            decimal tsn,
            decimal tsr,
            decimal totalRate,
            decimal intervalRate,
            decimal deltaIntervalRate,
            decimal averageRate3,
            decimal averageRate7)
        {
            var oilMonitor = new OilMonitor
            {
                Date = date,
                TSN = tsn,
                TSR = tsr,
                TotalRate = totalRate,
                IntervalRate = intervalRate,
                DeltaIntervalRate = deltaIntervalRate,
                AverageRate3 = averageRate3,
                AverageRate7 = averageRate7,
            };
            oilMonitor.GenerateNewIdentity();

            var user = oilMonitor.SetEngineOil(engineReg);
            if (!user.NeedMonitor)
                user.NeedMonitor = true;

            return oilMonitor;
        }

        /// <summary>
        ///     创建APU滑油消耗数据
        /// </summary>
        /// <param name="apuReg">APU</param>
        /// <param name="date">日期</param>
        /// <param name="tsn">TSN</param>
        /// <param name="tsr">TSR</param>
        /// <param name="totalRate">总消耗率</param>
        /// <param name="intervalRate">区间消耗率</param>
        /// <param name="deltaIntervalRate">区间消耗率增量</param>
        /// <param name="averageRate3">总消耗率3天移动平均</param>
        /// <param name="averageRate7">总消耗率7天移动平均</param>
        /// <returns>滑油消耗数据</returns>
        public static OilMonitor CreateAPUOil(
            APUReg apuReg,
            DateTime date,
            decimal tsn,
            decimal tsr,
            decimal totalRate,
            decimal intervalRate,
            decimal deltaIntervalRate,
            decimal averageRate3,
            decimal averageRate7)
        {
            var oilMonitor = new OilMonitor
            {
                Date = date,
                TSN = tsn,
                TSR = tsr,
                TotalRate = totalRate,
                IntervalRate = intervalRate,
                DeltaIntervalRate = deltaIntervalRate,
                AverageRate3 = averageRate3,
                AverageRate7 = averageRate7,
            };
            oilMonitor.GenerateNewIdentity();

            var user = oilMonitor.SetApuOil(apuReg);
            if (!user.NeedMonitor)
                user.NeedMonitor = true;

            return oilMonitor;
        }
    }
}