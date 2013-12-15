#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，11:11
// 文件名：AircraftLeaseOrderLine.cs
// 程序集：UniCloud.Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.UberModel.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.UberModel.Aggregates.MaterialAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.OrderAgg
{
    /// <summary>
    ///     订单聚合根
    ///     飞机租赁订单行
    /// </summary>
    public class AircraftLeaseOrderLine : OrderLine
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftLeaseOrderLine()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机物料ID
        /// </summary>
        public int AircraftMaterialId { get; private set; }

        /// <summary>
        ///     租赁合同飞机ID
        /// </summary>
        public int ContractAircraftId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     飞机物料
        /// </summary>
        public virtual AircraftMaterial AircraftMaterial { get; private set; }

        /// <summary>
        ///     租赁合同飞机
        /// </summary>
        public virtual LeaseContractAircraft LeaseContractAircraft { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置飞机物料
        /// </summary>
        /// <param name="aircraftMaterial">飞机物料</param>
        public void SetAircraftMaterial(AircraftMaterial aircraftMaterial)
        {
            if (aircraftMaterial == null || aircraftMaterial.IsTransient())
            {
                throw new ArgumentException("飞机物料参数为空！");
            }

            AircraftMaterial = aircraftMaterial;
            AircraftMaterialId = aircraftMaterial.Id;
        }

        /// <summary>
        ///     设置飞机物料
        /// </summary>
        /// <param name="aircraftMaterialId">飞机物料ID</param>
        public void SetAircraftMaterial(int aircraftMaterialId)
        {
            if (aircraftMaterialId == 0)
            {
                throw new ArgumentException("飞机物料ID参数为空！");
            }

            AircraftMaterialId = aircraftMaterialId;
        }

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