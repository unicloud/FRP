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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.UberModel.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.UberModel.Aggregates.PartAgg;
using UniCloud.Domain.UberModel.Enums;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.ContractEngineAgg
{
    /// <summary>
    ///     合同发动机聚合根
    /// </summary>
    public abstract class ContractEngine : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ContractEngine()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     合同名称
        /// </summary>
        public string ContractName { get; internal set; }

        /// <summary>
        ///     合同编号
        /// </summary>
        public string ContractNumber { get; private set; }

        /// <summary>
        ///     合同Rank号
        /// </summary>
        public string RankNumber { get; internal set; }

        /// <summary>
        ///     发动机序列号
        /// </summary>
        public string SerialNumber { get; private set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        ///     接收数量
        /// </summary>
        public int ReceivedAmount { get; private set; }

        /// <summary>
        ///     接受数量
        /// </summary>
        public int AcceptedAmount { get; private set; }

        /// <summary>
        ///     管理状态
        /// </summary>
        public ContractEngineStatus Status { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     附件ID
        /// </summary>
        public int PartId { get; private set; }

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
        ///     附件
        /// </summary>
        public virtual Part Part { get; private set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public virtual ActionCategory ImportCategory { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置附件
        /// </summary>
        /// <param name="part">附件</param>
        public void SetPart(Part part)
        {
            if (part == null || part.IsTransient())
            {
                throw new ArgumentException("附件参数为空！");
            }

            Part = part;
            PartId = part.Id;
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

        /// <summary>
        ///     设置序列号
        /// </summary>
        /// <param name="serialNumber">序列号</param>
        public void SetSerialNumber(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                throw new ArgumentException("序列号参数为空！");
            }

            SerialNumber = serialNumber;
        }

        /// <summary>
        ///     设置接收数量
        /// </summary>
        /// <param name="received">接收数量</param>
        /// <param name="accepted">接受数量</param>
        public void SetReception(int received, int accepted)
        {
            ReceivedAmount = received;
            AcceptedAmount = accepted;
        }

        /// <summary>
        ///     设置管理状态
        /// </summary>
        /// <param name="status">管理状态</param>
        public void SetContractEngineStatus(ContractEngineStatus status)
        {
            switch (status)
            {
                case ContractEngineStatus.预备:
                    Status = ContractEngineStatus.预备;
                    break;
                case ContractEngineStatus.签约:
                    Status = ContractEngineStatus.签约;
                    IsValid = true;
                    break;
                case ContractEngineStatus.接收:
                    Status = ContractEngineStatus.接收;
                    break;
                case ContractEngineStatus.运营:
                    Status = ContractEngineStatus.运营;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
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