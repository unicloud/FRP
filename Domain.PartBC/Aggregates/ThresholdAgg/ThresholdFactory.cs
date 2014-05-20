#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/20 15:24:09
// 文件名：ThresholdFactory
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/20 15:24:09
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.ThresholdAgg
{
    /// <summary>
    /// Threshold工厂。
    /// </summary>
    public static class ThresholdFactory
    {
        /// <summary>
        /// 创建阀值
        /// </summary>
        /// <param name="pnReg"></param>
        /// <param name="totalThreshold">总滑油消耗率阀值</param>
        /// <param name="intervalThreshold">区间滑油消耗率阀值</param>
        /// <param name="deltaIntervalThreshold">区间滑油消耗率变化量阀值</param>
        /// <param name="average3Threshold">3天移动平均阀值</param>
        /// <param name="average7Threshold">7天移动平均阀值</param>
        /// <returns></returns>
        public static Threshold CreateThreshold(PnReg pnReg,
            decimal totalThreshold,
            decimal intervalThreshold,
            decimal deltaIntervalThreshold,
            decimal average3Threshold,
            decimal average7Threshold)
        {
            var threshold = new Threshold
            {
                TotalThreshold = totalThreshold,
                IntervalThreshold = intervalThreshold,
                DeltaIntervalThreshold = deltaIntervalThreshold,
                Average3Threshold = average3Threshold,
                Average7Threshold = average7Threshold,
            };
            threshold.GenerateNewIdentity();
            threshold.SetPnReg(pnReg);
            return threshold;
        }

        public static void UpdateThreshold(Threshold existThreshold, PnReg pnReg,
            decimal totalThreshold,
            decimal intervalThreshold,
            decimal deltaIntervalThreshold,
            decimal average3Threshold,
            decimal average7Threshold)
        {
            if (existThreshold == null) return;

            existThreshold.SetPnReg(pnReg);
            existThreshold.TotalThreshold = totalThreshold;
            existThreshold.IntervalThreshold = intervalThreshold;
            existThreshold.DeltaIntervalThreshold = deltaIntervalThreshold;
            existThreshold.Average3Threshold = average3Threshold;
            existThreshold.Average7Threshold = average7Threshold;
        }

    }
}
