#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
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
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.PlanAircraftAgg
{
    /// <summary>
    ///     计划飞机聚合根
    /// </summary>
    public class PlanAircraft : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal PlanAircraft()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     是否锁定，确定计划时锁定相关飞机。一旦锁定，对应的计划明细不能修改机型。
        /// </summary>
        public bool IsLock { get; protected set; }

        /// <summary>
        ///     是否自有，用以区分PlanAircraft，民航局均为False。
        /// </summary>
        public bool IsOwn { get; protected set; }

        /// <summary>
        ///     管理状态
        /// </summary>
        public ManageStatus Status { get; protected set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     合同飞机外键
        /// </summary>
        public int? ContractAircraftId { get; private set; }

        /// <summary>
        ///     实际飞机外键
        /// </summary>
        public Guid? AircraftId { get; protected set; }

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeId { get; protected set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; protected set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     实际飞机
        /// </summary>
        public virtual Aircraft Aircraft { get; protected set; }

        /// <summary>
        ///     机型
        /// </summary>
        public virtual AircraftType AircraftType { get; protected set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置合同飞机ID
        /// </summary>
        /// <param name="contractAircraftId">合同飞机ID</param>
        public void SetContractAircraftId(int? contractAircraftId)
        {
            ContractAircraftId = contractAircraftId;
        }

        #endregion
    }
}