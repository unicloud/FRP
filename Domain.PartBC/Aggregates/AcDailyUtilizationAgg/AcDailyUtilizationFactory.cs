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
    }
}
