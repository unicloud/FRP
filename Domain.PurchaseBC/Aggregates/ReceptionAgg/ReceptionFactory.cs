#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，13:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg
{
    /// <summary>
    ///     接收工厂
    /// </summary>
    public static class ReceptionFactory
    {
        /// <summary>
        ///     创建租赁飞机接收
        /// </summary>
        /// <returns>租赁飞机接收</returns>
        public static AircraftLeaseReception CreateAircraftLeaseReception()
        {
            var aircraftLeaseReception = new AircraftLeaseReception
            {
                CreateDate = DateTime.Now, 
            };

            return aircraftLeaseReception;
        }

        /// <summary>
        ///     创建租赁飞机接收行
        /// </summary>
        /// <returns>租赁飞机接收行</returns>
        public static AircraftLeaseReceptionLine CreateAircraftLeaseReceptionLine()
        {
            var aircraftLeaseReceptionLine = new AircraftLeaseReceptionLine
            {
            };
            return aircraftLeaseReceptionLine;
        }

        /// <summary>
        ///     创建购买飞机接收
        /// </summary>
        /// <returns>购买飞机接收</returns>
        public static AircraftPurchaseReception CreateAircraftPurchaseReception()
        {
            var aircraftPurchaseReception = new AircraftPurchaseReception
            {
                CreateDate = DateTime.Now,
            };

            return aircraftPurchaseReception;
        }

        /// <summary>
        ///     创建采购飞机接收行
        /// </summary>
        /// <returns>采购飞机接收行</returns>
        public static AircraftPurchaseReceptionLine CreateAircraftPurchaseReceptionLine()
        {
            var aircraftPurchaseReceptionLine = new AircraftPurchaseReceptionLine
            {
            };
            return aircraftPurchaseReceptionLine;
        }

        /// <summary>
        ///     创建租赁发动机接收
        /// </summary>
        /// <returns>租赁发动机接收</returns>
        public static EngineLeaseReception CreateEngineLeaseReception()
        {
            var engineLeaseReception = new EngineLeaseReception
            {
                CreateDate = DateTime.Now, 
            };

            return engineLeaseReception;
        }

        /// <summary>
        ///     创建租赁发动机接收行
        /// </summary>
        /// <returns>租赁发动机接收行</returns>
        public static EngineLeaseReceptionLine CreateEngineLeaseReceptionLine()
        {
            var engineLeaseReceptionLine = new EngineLeaseReceptionLine
            {
            };
            return engineLeaseReceptionLine;
        }

        /// <summary>
        ///     创建购买发动机接收
        /// </summary>
        /// <returns>购买发动机接收</returns>
        public static EnginePurchaseReception CreateEnginePurchaseReception()
        {
            var enginePurchaseReception = new EnginePurchaseReception
            {
                CreateDate = DateTime.Now, 
            };

            return enginePurchaseReception;
        }


        /// <summary>
        ///     创建采购发动机接收行
        /// </summary>
        /// <returns>采购发动机接收行</returns>
        public static EnginePurchaseReceptionLine CreateEnginePurchaseReceptionLine()
        {
            var enginePurchaseReceptionLine = new EnginePurchaseReceptionLine
            {
            };
            return enginePurchaseReceptionLine;
        }

    }
}