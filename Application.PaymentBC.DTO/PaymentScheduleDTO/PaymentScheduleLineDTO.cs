﻿#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    [DataServiceKey("PaymentScheduleLineId")]
    public class PaymentScheduleLineDTO
    {
        /// <summary>
        ///     主键
        /// </summary>
        public int PaymentScheduleLineId { get; set; }

        /// <summary>
        ///     计划付款日期
        /// </summary>
        public DateTime ScheduleDate { get;  set; }

        /// <summary>
        ///     付款金额
        /// </summary>
        public decimal Amount { get;  set; }

        /// <summary>
        ///     付款控制状态
        /// </summary>
        public int Status { get;  set; }

        /// <summary>
        ///     付款计划ID
        /// </summary>
        public int PaymentScheduleId { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }
    }
}