#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:16:57

// 文件名：AcDailyUtilizationFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.AcDailyUtilizationAgg
{
    /// <summary>
    ///     AcDailyUtilization工厂。
    /// </summary>
    public static class AcDailyUtilizationFactory
    {
        #region 创建

        /// <summary>
        ///     创建飞机日利用率
        /// </summary>
        /// <param name="aircraftId">运营飞机</param>
        /// <param name="regNumber">飞机注册号</param>
        /// <param name="calculatedHour">计算日利用小时</param>
        /// <param name="calculatedCycle">计算日利用循环</param>
        /// <param name="year">年度</param>
        /// <param name="month">月份</param>
        /// <returns>飞机日利用率</returns>
        public static AcDailyUtilization CreateAcDailyUtilization(
            Guid aircraftId,
            string regNumber,
            decimal calculatedHour,
            decimal calculatedCycle,
            int year,
            int month)
        {
            var acDailyUtilization = new AcDailyUtilization
            {
                CalculatedHour = calculatedHour,
                CalculatedCycle = calculatedCycle,
                Year = year,
                Month = month
            };
            acDailyUtilization.GenerateNewIdentity();
            acDailyUtilization.SetAircraft(aircraftId, regNumber);
            acDailyUtilization.SetIsCurrent(true);
            return acDailyUtilization;
        }

        #endregion

        #region 更新

        /// <summary>
        ///     更新飞机日利用率
        /// </summary>
        /// <param name="acDailyUtilization">飞机日利用率</param>
        /// <param name="aircraftId">运营飞机</param>
        /// <param name="regNumber">飞机注册号</param>
        /// <param name="calculatedHour">计算日利用小时</param>
        /// <param name="calculatedCycle">计算日利用循环</param>
        /// <param name="amendHour">修正日利用小时</param>
        /// <param name="amendCycle">修正日利用循环</param>
        /// <param name="year">年度</param>
        /// <param name="month">月份</param>
        /// <returns>飞机日利用率</returns>
        public static AcDailyUtilization UpdateAcDailyUtilization(
            AcDailyUtilization acDailyUtilization,
            Guid aircraftId,
            string regNumber,
            decimal calculatedHour,
            decimal calculatedCycle,
            decimal amendHour,
            decimal amendCycle,
            int year,
            int month)
        {
            acDailyUtilization.CalculatedHour = calculatedHour;
            acDailyUtilization.CalculatedCycle = calculatedCycle;
            acDailyUtilization.Year = year;
            acDailyUtilization.Month = month;
            acDailyUtilization.GenerateNewIdentity();
            acDailyUtilization.SetAircraft(aircraftId, regNumber);
            acDailyUtilization.SetIsCurrent(true);
            acDailyUtilization.SetAmendHour(amendHour);
            acDailyUtilization.SetAmendCycle(amendCycle);
            return acDailyUtilization;
        }

        #endregion
    }
}