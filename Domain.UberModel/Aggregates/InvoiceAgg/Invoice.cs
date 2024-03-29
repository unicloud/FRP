﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/07，10:57
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.UberModel.Aggregates.CurrencyAgg;
using UniCloud.Domain.UberModel.Aggregates.OrderAgg;
using UniCloud.Domain.UberModel.Aggregates.PaymentScheduleAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.InvoiceAgg
{
    /// <summary>
    ///     发票聚合根
    /// </summary>
    public abstract class Invoice : EntityInt, IValidatableObject
    {
        #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Invoice()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        ///     发票类型
        /// </summary>
        public InvoiceType InvoiceType { get; private set; }

        /// <summary>
        ///     发票编号
        /// </summary>
        public string InvoiceNumber { get; private set; }

        /// <summary>
        ///     发票号码
        /// </summary>
        public string InvoideCode { get; internal set; }

        /// <summary>
        ///     发票日期
        /// </summary>
        public DateTime InvoiceDate { get; internal set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; private set; }

        /// <summary>
        ///     发票金额
        /// </summary>
        public decimal InvoiceValue { get; private set; }

        /// <summary>
        ///     已付金额
        /// </summary>
        public decimal PaidAmount { get; private set; }

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
        public DateTime? ReviewDate { get; private set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        ///     发票状态
        /// </summary>
        public InvoiceStatus Status { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     订单ID
        /// </summary>
        public int? OrderId { get; private set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; private set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; private set; }

        /// <summary>
        ///     付款计划行ID
        /// </summary>
        public int? PaymentScheduleLineId { get; private set; }

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
        public virtual PaymentScheduleLine PaymentScheduleLine { get; set; }
        #endregion

        #region 操作
        /// <summary>
        /// 设置发票类型
        /// </summary>
        /// <param name="invoiceType">发票类型</param>
        public void SetInvoiceType(InvoiceType invoiceType)
        {
            InvoiceType = invoiceType;
        }

        /// <summary>
        ///     设置发票编号
        /// </summary>
        /// <param name="seq">流水号</param>
        public void SetInvoiceNumber(int seq)
        {
            if (seq < 1)
            {
                throw new ArgumentException("流水号参数为空！");
            }

            var date = DateTime.Now;
            InvoiceNumber = string.Format("{0:yyyyMMdd}{1}", date, seq.ToString("D2"));
        }

        /// <summary>
        ///     设置发票编号
        /// </summary>
        /// <param name="invoiceNumber">发票编号</param>
        public void SetInvoiceNumber(string invoiceNumber)
        {
            if (string.IsNullOrWhiteSpace(invoiceNumber))
            {
                throw new ArgumentException("发票编号参数为空！");
            }

            InvoiceNumber = invoiceNumber;
        }

        /// <summary>
        ///     设置发票金额
        /// </summary>
        public void SetInvoiceValue(decimal invoiceValue)
        {
            InvoiceValue = invoiceValue;
        }

        /// <summary>
        ///     设置订单
        /// </summary>
        /// <param name="order">订单</param>
        public void SetOrder(Order order)
        {
            if (order == null || order.IsTransient())
            {
                throw new ArgumentException("订单参数为空！");
            }

            OrderId = order.Id;
        }

        /// <summary>
        ///     设置已付金额
        /// </summary>
        /// <param name="amount">付款金额</param>
        public void SetPaidAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("付款金额非正数！");
            }

            PaidAmount = PaidAmount + amount;
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
            SetInvoiceStatus(InvoiceStatus.已审核);
        }

        /// <summary>
        ///     设置完成
        /// </summary>
        public void SetCompleted()
        {
            // TODO：待完善
            IsCompleted = true;
        }

        /// <summary>
        ///     设置发票状态
        /// </summary>
        /// <param name="status">发票状态</param>
        public void SetInvoiceStatus(InvoiceStatus status)
        {
            switch (status)
            {
                case InvoiceStatus.草稿:
                    Status = InvoiceStatus.草稿;
                    break;
                case InvoiceStatus.待审核:
                    Status = InvoiceStatus.待审核;
                    break;
                case InvoiceStatus.已审核:
                    Status = InvoiceStatus.已审核;
                    IsValid = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

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
        ///     设置币种
        /// </summary>
        /// <param name="currencyId">币种Id</param>
        public void SetCurrency(int currencyId)
        {
            CurrencyId = currencyId;
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
            SupplierName = supplier.CnName;
        }

        /// <summary>
        ///     设置供应商
        /// </summary>
        /// <param name="supplierId">供应商Id</param>
        /// <param name="supplierName">供应商名字</param>
        public void SetSupplier(int supplierId, string supplierName)
        {
            SupplierId = supplierId;
            SupplierName = supplierName;
        }

        /// <summary>
        ///     设置付款计划行ID
        /// </summary>
        /// <param name="id">付款计划行ID</param>
        public void SetPaymentScheduleLine(int? id)
        {
            if (id == 0)
            {
                throw new ArgumentException("付款计划行ID参数为空！");
            }

            PaymentScheduleLineId = id;
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