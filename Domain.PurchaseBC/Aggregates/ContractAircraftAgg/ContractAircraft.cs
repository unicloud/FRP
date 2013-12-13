#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/04，14:11
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.PurchaseBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftBFEAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.PlanAircraftAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg
{
    /// <summary>
    ///     合同飞机聚合根
    /// </summary>
    public abstract class ContractAircraft : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<ContractAircraftBFE> _contractAircraftBfes;

        #endregion

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
        public string ContractName { get; set; }

        /// <summary>
        ///     合同编号
        /// </summary>
        public string ContractNumber { get; private set; }

        /// <summary>
        ///     合同Rank号
        /// </summary>
        public string RankNumber { get; set; }

        /// <summary>
        ///     飞机批次号
        /// </summary>
        public string CSCNumber { get; set; }

        /// <summary>
        ///     飞机序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     接收数量
        /// </summary>
        public int ReceivedAmount { get; set; }

        /// <summary>
        ///     接受数量
        /// </summary>
        public int AcceptedAmount { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     机型ID
        /// </summary>
        public Guid AircraftTypeId { get; private set; }

        /// <summary>
        ///     计划飞机
        /// </summary>
        public Guid? PlanAircraftID { get; private set; }

        /// <summary>
        ///     引进方式ID
        /// </summary>
        public Guid ImportCategoryId { get; private set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int? SupplierId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     机型
        /// </summary>
        public virtual AircraftType AircraftType { get; private set; }

        /// <summary>
        ///     计划飞机
        /// </summary>
        public virtual PlanAircraft PlanAircraft { get; private set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public virtual ActionCategory ImportCategory { get; private set; }

        /// <summary>
        ///     合同飞机BFE
        /// </summary>
        public virtual ICollection<ContractAircraftBFE> ContractAircraftBfes
        {
            get { return _contractAircraftBfes ?? (_contractAircraftBfes = new HashSet<ContractAircraftBFE>()); }
            set { _contractAircraftBfes = new HashSet<ContractAircraftBFE>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置机型
        /// </summary>
        /// <param name="aircraftType">机型</param>
        public void SetAircraftType(AircraftType aircraftType)
        {
            if (aircraftType == null || aircraftType.IsTransient())
            {
                throw new ArgumentException("机型参数为空！");
            }

            AircraftType = aircraftType;
            AircraftTypeId = aircraftType.Id;
        }

        /// <summary>
        ///     设置计划飞机
        /// </summary>
        /// <param name="planAircraft">计划飞机</param>
        public void SetPlanAircraft(PlanAircraft planAircraft)
        {
            if (planAircraft == null || planAircraft.IsTransient())
            {
                throw new ArgumentException("计划飞机参数为空！");
            }

            PlanAircraft = planAircraft;
            PlanAircraftID = planAircraft.Id;
        }

        /// <summary>
        ///     移除计划飞机
        /// </summary>
        public void RemovePlanAircraft()
        {
            PlanAircraft = null;
            PlanAircraftID = null;
        }

        /// <summary>
        ///     设置引进方式
        /// </summary>
        /// <param name="importCategory">引进方式</param>
        public void SetImportCategory(ActionCategory importCategory)
        {
            if (importCategory == null || importCategory.IsTransient())
            {
                throw new ArgumentException("引进方式参数为空！");
            }

            ImportCategory = importCategory;
            ImportCategoryId = importCategory.Id;
        }

        /// <summary>
        ///     设置合同编号
        /// </summary>
        /// <param name="contractNumber">合同编号</param>
        public void SetContractNumber(string contractNumber)
        {
            if (string.IsNullOrWhiteSpace(contractNumber))
            {
                throw new ArgumentException("合同编号参数为空！");
            }

            ContractNumber = contractNumber;
        }

        /// <summary>
        ///     设置供应商ID
        /// </summary>
        /// <param name="id">供应商ID</param>
        public void SetSupplier(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("供应商ID参数为空！");
            }

            SupplierId = id;
        }

        #endregion

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}