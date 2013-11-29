#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
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
using UniCloud.Domain.UberModel.Aggregates.ContractAircraftAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.ReceptionAgg
{
    /// <summary>
    ///     接收聚合根
    ///     租赁飞机接收行
    /// </summary>
    public class AircraftLeaseReceptionLine : ReceptionLine
    {
        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     租赁合同飞机ID
        /// </summary>
        public int ContractAircraftId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     租赁合同飞机
        /// </summary>
        public virtual LeaseContractAircraft LeaseContractAircraft { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置租赁合同飞机
        /// </summary>
        /// <param name="leaseContractAircraft">租赁合同飞机</param>
        public void SetContractAircraft(LeaseContractAircraft leaseContractAircraft)
        {
            if (leaseContractAircraft == null || leaseContractAircraft.IsTransient())
            {
                throw new ArgumentException("租赁合同飞机参数为空！");
            }

            LeaseContractAircraft = leaseContractAircraft;
            ContractAircraftId = leaseContractAircraft.Id;
        }

        #endregion
    }
}