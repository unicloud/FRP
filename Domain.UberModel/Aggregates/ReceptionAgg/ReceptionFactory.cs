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
        /// <returns>租赁飞机接收</returns>
        public static AircraftLeaseReception CreateAircraftLeaseReception(DateTime startDate)
        {
            var aircraftLeaseReception = new AircraftLeaseReception
            {
                CreateDate = DateTime.Now,
                StartDate = startDate
            };

            return aircraftLeaseReception;
        }

        /// <summary>
        ///     创建购买飞机接收
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <returns>购买飞机接收</returns>
        public static AircraftPurchaseReception CreateAircraftPurchaseReception(DateTime startDate)
        {
            var aircraftPurchaseReception = new AircraftPurchaseReception
            {
                CreateDate = DateTime.Now,
                StartDate = startDate
            };

            return aircraftPurchaseReception;
        }

        /// <summary>
        ///     创建租赁发动机接收
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <returns>租赁发动机接收</returns>
        public static EngineLeaseReception CreateEngineLeaseReception(DateTime startDate)
        {
            var engineLeaseReception = new EngineLeaseReception
            {
                CreateDate = DateTime.Now,
                StartDate = startDate
            };

            return engineLeaseReception;
        }

        /// <summary>
        ///     创建购买发动机接收
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <returns>购买发动机接收</returns>
        public static EnginePurchaseReception CreateEnginePurchaseReception(DateTime startDate)
        {
            var enginePurchaseReception = new EnginePurchaseReception
            {
                CreateDate = DateTime.Now,
                StartDate = startDate
            };

            return enginePurchaseReception;
        }
    }
}