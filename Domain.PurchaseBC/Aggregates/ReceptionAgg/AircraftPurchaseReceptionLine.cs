#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，14:11
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
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg
{
    /// <summary>
    ///     接收聚合根
    ///     购买飞机接收行
    /// </summary>
    public class AircraftPurchaseReceptionLine : ReceptionLine
    {
        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     购买合同飞机ID
        /// </summary>
        public int ContractAircraftId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     购买合同飞机
        /// </summary>
        public virtual PurchaseContractAircraft PurchaseContractAircraft { get; set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置购买合同飞机
        /// </summary>
        /// <param name="purchaseContractAircraft">购买合同飞机</param>
        public void SetContractAircraft(PurchaseContractAircraft purchaseContractAircraft)
        {
            if (purchaseContractAircraft == null || purchaseContractAircraft.IsTransient())
            {
                throw new ArgumentException("购买合同飞机参数为空！");
            }

            PurchaseContractAircraft = purchaseContractAircraft;
            ContractAircraftId = purchaseContractAircraft.Id;
        }

        #endregion
    }
}