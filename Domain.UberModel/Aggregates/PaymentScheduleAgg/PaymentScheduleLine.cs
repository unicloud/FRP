#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/09，23:12
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
using UniCloud.Domain.UberModel.Aggregates.InvoiceAgg;
using UniCloud.Domain.UberModel.Enums;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.PaymentScheduleAgg
{
    /// <summary>
    ///     付款计划聚合根
    ///     付款计划行
    /// </summary>
    public class PaymentScheduleLine : ScheduleBase, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal PaymentScheduleLine()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     计划付款日期
        /// </summary>
        public DateTime ScheduleDate { get; internal set; }

        /// <summary>
        ///     付款金额
        /// </summary>
        public decimal Amount { get; internal set; }

        /// <summary>
        ///     付款控制状态
        /// </summary>
        public ControlStatus Status { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     付款计划ID
        /// </summary>
        public int PaymentScheduleId { get; internal set; }

        /// <summary>
        ///     发票ID
        /// </summary>
        public int? InvoiceId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     发票
        /// </summary>
        public virtual Invoice Invoice { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置付款控制状态
        /// </summary>
        /// <param name="status">付款控制状态</param>
        public void SetControlStatus(ControlStatus status)
        {
            switch (status)
            {
                case ControlStatus.正常支付:
                    Status = ControlStatus.正常支付;
                    break;
                case ControlStatus.暂缓支付:
                    Status = ControlStatus.暂缓支付;
                    break;
                case ControlStatus.已匹配发票:
                    Status = ControlStatus.已匹配发票;
                    break;
                case ControlStatus.已完成:
                    Status = ControlStatus.已完成;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

        /// <summary>
        ///     设置发票
        /// </summary>
        /// <param name="invoice">发票</param>
        public void SetInvoice(Invoice invoice)
        {
            if (invoice == null || invoice.IsTransient())
            {
                throw new ArgumentException("发票参数为空！");
            }

            Invoice = invoice;
            InvoiceId = invoice.Id;
            Status = ControlStatus.已匹配发票;
        }

        /// <summary>
        ///     设置发票ID
        /// </summary>
        /// <param name="id">发票ID</param>
        public void SetInvoice(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("发票ID参数为空！");
            }

            InvoiceId = id;
            Status = ControlStatus.已匹配发票;
        }

        /// <summary>
        ///     设置付款时间
        /// </summary>
        /// <param name="scheduleDate"></param>
        public void SetScheduleDate(DateTime scheduleDate)
        {
            ScheduleDate = scheduleDate;
        }

        /// <summary>
        ///     设置价格
        /// </summary>
        /// <param name="amount"></param>
        public void SetAmount(decimal amount)
        {
            Amount = amount;
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