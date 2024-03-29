﻿#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 11:29:36
// 文件名：PlanAircraftFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg
{
    /// <summary>
    ///     计划飞机工厂
    /// </summary>
    public static class PlanAircraftFactory
    {
        /// <summary>
        ///     创建计划飞机
        /// </summary>
        /// <returns>计划飞机</returns>
        public static PlanAircraft CreatePlanAircraft()
        {
            var planAircraft = new PlanAircraft();

            planAircraft.GenerateNewIdentity();
            return planAircraft;
        }
    }
}