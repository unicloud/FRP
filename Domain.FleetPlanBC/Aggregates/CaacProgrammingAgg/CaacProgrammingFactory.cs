#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 10:15:52
// 文件名：CaacProgrammingFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;

namespace UniCloud.Domain.FleetPlanBC.Aggregates.CaacProgrammingAgg
{
    /// <summary>
    ///     民航局五年规划工厂
    /// </summary>
    public static class CaacProgrammingFactory
    {
        /// <summary>
        ///     创建民航局五年规划
        /// </summary>
        /// <returns>民航局五年规划</returns>
        public static CaacProgramming CreateCaacProgramming()
        {
            var caacProgramming = new CaacProgramming
            {
                CreateDate = DateTime.Now,
            };

            return caacProgramming;
        }
    }
}
