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
    ///     租赁飞机接收行
    /// </summary>
    public class AircraftLeaseReceptionLine : ReceptionLine
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftLeaseReceptionLine()
        {
        }

        #endregion

        #region 属性
        //选呼号
        public string DailNumber { get; set; }
        //调机航班号
        public string FlightNumber { get; set; }

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