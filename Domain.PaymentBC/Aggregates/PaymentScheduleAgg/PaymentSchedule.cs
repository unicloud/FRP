#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/07，11:03
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
using System.Linq;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.PaymentScheduleAgg
{
    /// <summary>
    ///     付款计划聚合根
    /// </summary>
    public abstract class PaymentSchedule : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<PaymentScheduleLine> _lines;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal PaymentSchedule()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; private set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsCompleted { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; private set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     币种
        /// </summary>
        public virtual Currency Currency { get; private set; }

        /// <summary>
        ///     供应商
        /// </summary>
        public virtual Supplier Supplier { get; private set; }

        /// <summary>
        ///     付款计划行
        /// </summary>
        public virtual ICollection<PaymentScheduleLine> PaymentScheduleLines
        {
            get { return _lines ?? (_lines = new HashSet<PaymentScheduleLine>()); }
            set { _lines = new HashSet<PaymentScheduleLine>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     添加付款计划行
        /// </summary>
        /// <param name="scheduleDate">付款日期</param>
        /// <param name="amount">付款金额</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="importance">重要性级别</param>
        /// <param name="tempo">进度</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="isAllDay">是否全天事件</param>
        public PaymentScheduleLine AddPaymentScheduleLine(DateTime scheduleDate, decimal amount, string subject,
            string body, string importance, string tempo, DateTime start,
            DateTime end, bool isAllDay)
        {
            var paymentScheduleLine = new PaymentScheduleLine
            {
                ScheduleDate = scheduleDate,
                Amount = amount,
            };

            paymentScheduleLine.GenerateNewIdentity();
            paymentScheduleLine.SetControlStatus(ControlStatus.正常支付);
            paymentScheduleLine.SetSchedule(subject, body, importance, tempo, start, end, isAllDay);
            PaymentScheduleLines.Add(paymentScheduleLine);
            return paymentScheduleLine;
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
        /// <param name="currencyId"></param>
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
        /// <param name="supplierId"></param>
        /// <param name="supplierName"></param>
        public void SetSupplier(int supplierId, string supplierName)
        {
            SupplierId = supplierId;
            SupplierName = supplierName;
        }

        /// <summary>
        ///     设置完成
        /// </summary>
        public void SetCompleted()
        {
            if (PaymentScheduleLines.All(p => p.Status == ControlStatus.已完成))
            {
                IsCompleted = true;
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