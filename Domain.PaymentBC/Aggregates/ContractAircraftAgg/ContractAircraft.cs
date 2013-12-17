#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/07，15:12
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.PaymentBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.PaymentBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PaymentBC.Enums;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.ContractAircraftAgg
{
    /// <summary>
    ///     合同飞机聚合根
    /// </summary>
    public class ContractAircraft : EntityInt
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ContractAircraft()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     合同名称
        /// </summary>
        public string ContractName { get; protected set; }

        /// <summary>
        ///     合同编号
        /// </summary>
        public string ContractNumber { get; protected set; }

        /// <summary>
        ///     合同Rank号
        /// </summary>
        public string RankNumber { get; protected set; }

        /// <summary>
        ///     飞机批次号
        /// </summary>
        public string CSCNumber { get; protected set; }

        /// <summary>
        ///     飞机序列号
        /// </summary>
        public string SerialNumber { get; protected set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; protected set; }

        /// <summary>
        ///     接收数量
        /// </summary>
        public int ReceivedAmount { get; protected set; }

        /// <summary>
        ///     接受数量
        /// </summary>
        public int AcceptedAmount { get; protected set; }

        /// <summary>
        ///     管理状态
        /// </summary>
        public ContractAircraftStatus Status { get; protected set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     机型ID
        /// </summary>
        public Guid AircraftTypeId { get; protected set; }

        /// <summary>
        ///     引进方式ID
        /// </summary>
        public Guid ImportCategoryId { get; protected set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int? SupplierId { get; protected set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     机型
        /// </summary>
        public virtual AircraftType AircraftType { get; protected set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public virtual ActionCategory ImportCategory { get; protected set; }

        #endregion

        #region 操作

        #endregion
    }
}