#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，11:12
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
using UniCloud.Domain.UberModel.Aggregates.CurrencyAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierAgg;
using UniCloud.Domain.UberModel.Enums;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.GuaranteeAgg
{
    /// <summary>
    ///     保函聚合根
    /// </summary>
    public abstract class Guarantee : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Guarantee()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        ///     支付金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; private set; }

        /// <summary>
        ///     经办人
        /// </summary>
        public string OperatorName { get; private set; }

        /// <summary>
        ///     审核人
        /// </summary>
        public string Reviewer { get; private set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     审核日期
        /// </summary>
        public DateTime ReviewDate { get; private set; }

        /// <summary>
        ///     保函状态
        /// </summary>
        public GuaranteeStatus Status { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; private set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     供应商
        /// </summary>
        public virtual Supplier Supplier { get; private set; }

        /// <summary>
        ///     币种
        /// </summary>
        public virtual Currency Currency { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置币种
        /// </summary>
        /// <param name="currency">币种</param>
        public void SetCurrency(Currency currency)
        {
            if (currency == null || currency.IsTransient())
            {
                throw new ArgumentException("币种参数为空！");
            }

            Currency = currency;
            CurrencyId = currency.Id;
        }

        /// <summary>
        ///     设置供应商
        /// </summary>
        /// <param name="supplier">供应商</param>
        public void SetSupplier(Supplier supplier)
        {
            if (supplier == null || supplier.IsTransient())
            {
                throw new ArgumentException("供应商参数为空！");
            }

            Supplier = supplier;
            SupplierId = supplier.Id;
            SupplierName = supplier.Name;
        }

        /// <summary>
        ///     设置经办人
        /// </summary>
        /// <param name="operatorName">经办人</param>
        public void SetOperator(string operatorName)
        {
            if (string.IsNullOrWhiteSpace(operatorName))
            {
                throw new ArgumentException("经办人为空！");
            }

            OperatorName = operatorName;
        }

        /// <summary>
        ///     审核发票
        /// </summary>
        /// <param name="reviewer">审核人</param>
        public void Review(string reviewer)
        {
            if (string.IsNullOrWhiteSpace(reviewer))
            {
                throw new ArgumentException("审核人为空！");
            }

            Reviewer = reviewer;
            ReviewDate = DateTime.Now;
            SetGuaranteeStatus(GuaranteeStatus.已审核);
        }

        /// <summary>
        /// </summary>
        /// <param name="status"></param>
        public void SetGuaranteeStatus(GuaranteeStatus status)
        {
            switch (status)
            {
                case GuaranteeStatus.草稿:
                    Status = GuaranteeStatus.草稿;
                    break;
                case GuaranteeStatus.待审核:
                    Status = GuaranteeStatus.待审核;
                    break;
                case GuaranteeStatus.已审核:
                    Status = GuaranteeStatus.已审核;
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