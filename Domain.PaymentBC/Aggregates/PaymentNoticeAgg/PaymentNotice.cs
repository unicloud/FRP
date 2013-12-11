#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/07，11:00
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
using UniCloud.Domain.PaymentBC.Aggregates.BankAccountAgg;
using UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PaymentBC.Enums;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.PaymentNoticeAgg
{
    /// <summary>
    ///     付款通知聚合根
    /// </summary>
    public class PaymentNotice : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<PaymentNoticeLine> _lines;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal PaymentNotice()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     通知编号
        /// </summary>
        public string NoticeNumber { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     付款期限
        /// </summary>
        public DateTime DeadLine { get; internal set; }

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
        ///     审核日期
        /// </summary>
        public DateTime ReviewDate { get; private set; }

        /// <summary>
        ///     付款通知状态
        /// </summary>
        public PaymentNoticeStatus Status { get; private set; }

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

        /// <summary>
        ///     银行账户ID
        /// </summary>
        public int BankAccountId { get; private set; }

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
        ///     银行账户
        /// </summary>
        public virtual BankAccount BankAccount { get; private set; }

        /// <summary>
        ///     付款通知行
        /// </summary>
        public virtual ICollection<PaymentNoticeLine> PaymentNoticeLines
        {
            get { return _lines ?? (_lines = new HashSet<PaymentNoticeLine>()); }
            set { _lines = new HashSet<PaymentNoticeLine>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置通知编号
        /// </summary>
        /// <param name="seq">流水号</param>
        public void SetNoticeNumber(int seq)
        {
            if (seq < 1)
            {
                throw new ArgumentException("流水号参数为空！");
            }

            var date = DateTime.Now;
            NoticeNumber = string.Format("{0:yyyyMMdd}{1}", date, seq.ToString("D2"));
        }

        /// <summary>
        ///     设置通知编号
        /// </summary>
        /// <param name="noticeNumber">通知编号</param>
        public void SetInvoiceNumber(string noticeNumber)
        {
            if (string.IsNullOrWhiteSpace(noticeNumber))
            {
                throw new ArgumentException("通知编号参数为空！");
            }

            NoticeNumber = noticeNumber;
        }

        /// <summary>
        ///     添加付款通知行
        /// </summary>
        /// <param name="amount">金额</param>
        /// <param name="invoice">发票</param>
        /// <param name="note">备注</param>
        /// <returns></returns>
        public PaymentNoticeLine AddPaymentNoticeLine(decimal amount, Invoice invoice, string note)
        {
            var paymentNoticeLine = new PaymentNoticeLine
            {
                Amount = amount,
                Note = note
            };

            paymentNoticeLine.GenerateNewIdentity();
            paymentNoticeLine.SetInvoice(invoice);

            return paymentNoticeLine;
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
        ///     设置银行账户
        /// </summary>
        /// <param name="account">银行账户</param>
        public void SetBankAccount(BankAccount account)
        {
            if (account == null || account.IsTransient())
            {
                throw new ArgumentException("银行账户参数为空！");
            }

            BankAccount = account;
            BankAccountId = account.Id;
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
        ///     审核付款通知
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
        }

        /// <summary>
        ///     设置付款通知状态
        /// </summary>
        /// <param name="status">付款通知状态</param>
        public void SetPaymentNoticeStatus(PaymentNoticeStatus status)
        {
            switch (status)
            {
                case PaymentNoticeStatus.草稿:
                    Status = PaymentNoticeStatus.草稿;
                    break;
                case PaymentNoticeStatus.待审核:
                    Status = PaymentNoticeStatus.待审核;
                    break;
                case PaymentNoticeStatus.已审核:
                    Status = PaymentNoticeStatus.已审核;
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