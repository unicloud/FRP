#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/21，13:11
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.ReceptionAgg
{
    /// <summary>
    ///     接收工厂
    /// </summary>
    public static class ReceptionFactory
    {
        /// <summary>
        ///     创建租赁飞机接收
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="seq">流水号</param>
        /// <returns>租赁飞机接收</returns>
        public static AircraftLeaseReception CreateAircraftLeaseReception(DateTime startDate, int seq)
        {
            var aircraftLeaseReception = new AircraftLeaseReception();
            aircraftLeaseReception.GenerateNewIdentity();

            aircraftLeaseReception.CreateDate = DateTime.Now;
            aircraftLeaseReception.StartDate = startDate;

            aircraftLeaseReception.SetReceptionNumber(seq);

            return aircraftLeaseReception;
        }

        /// <summary>
        ///     创建购买飞机接收
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="seq">流水号</param>
        /// <returns>购买飞机接收</returns>
        public static AircraftPurchaseReception CreateAircraftPurchaseReception(DateTime startDate, int seq)
        {
            var aircraftPurchaseReception = new AircraftPurchaseReception();
            aircraftPurchaseReception.GenerateNewIdentity();

            aircraftPurchaseReception.CreateDate = DateTime.Now;
            aircraftPurchaseReception.StartDate = startDate;

            aircraftPurchaseReception.SetReceptionNumber(seq);

            return aircraftPurchaseReception;
        }
    }
}