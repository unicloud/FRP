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
using UniCloud.Domain.UberModel.Enums;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.PaymentScheduleAgg
{
    /// <summary>
    ///     付款计划聚合根
    ///     付款计划行
    /// </summary>
    public class PaymentScheduleLine : EntityInt, IValidatableObject
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
        public DateTime ScheduleDate { get; set; }

        /// <summary>
        ///     付款金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     付款控制状态
        /// </summary>
        public ControlStatus Status { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

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
                case ControlStatus.已完成:
                    Status = ControlStatus.已完成;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
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