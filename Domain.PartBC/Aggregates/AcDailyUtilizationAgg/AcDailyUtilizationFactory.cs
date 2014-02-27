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
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.AcDailyUtilizationAgg
{
    /// <summary>
    /// AcDailyUtilization工厂。
    /// </summary>
    public static class AcDailyUtilizationFactory
    {
        /// <summary>
        /// 创建AcDailyUtilization。
        /// </summary>
        ///  <returns>AcDailyUtilization</returns>
        public static AcDailyUtilization CreateAcDailyUtilization()
        {
            var acDailyUtilization = new AcDailyUtilization
            {
            };
            acDailyUtilization.GenerateNewIdentity();
            return acDailyUtilization;
        }

        /// <summary>
        /// 创建飞机日利用率
        /// </summary>
        /// <param name="aircraft">运营飞机</param>
        /// <param name="amendValue">修正日利用率</param>
        /// <param name="calculatedValue">计算日利用率</param>
        /// <param name="isCurrent">是否当前</param>
        /// <param name="month">月份</param>
        /// <param name="regNumber">飞机注册号</param>
        /// <param name="year">年度</param>
        /// <returns></returns>
        public static AcDailyUtilization CreateAcDailyUtilization(Aircraft aircraft, decimal amendValue, decimal calculatedValue,
            bool isCurrent,int month,string regNumber,int year)
        {
            var acDailyUtilization = new AcDailyUtilization
            {
            };
            acDailyUtilization.GenerateNewIdentity();
            acDailyUtilization.SetAircraft(aircraft);
            acDailyUtilization.SetAmendValue(amendValue);
            acDailyUtilization.SetCalculatedValue(calculatedValue);
            acDailyUtilization.SetIsCurrent(isCurrent);
            acDailyUtilization.SetMonth(month);
            acDailyUtilization.SetRegNumber(regNumber);
            acDailyUtilization.SetYear(year);
            return acDailyUtilization;
        }
    }
}
