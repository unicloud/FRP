#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     发动机付款计划
    /// </summary>
    [DataServiceKey("EnginePaymentScheduleId")]
    public class EnginePaymentScheduleDTO
    {
        public EnginePaymentScheduleDTO()
        {
            PaymentScheduleLines = new List<PaymentScheduleLineDTO>();
        }

        /// <summary>
        ///     主键
        /// </summary>
        public int EnginePaymentScheduleId { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        ///     币种名称
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///     合同发动机主键
        /// </summary>
        public int ContractEngineId { get; set; }

        /// <summary>
        ///     付款计划行
        /// </summary>
        public List<PaymentScheduleLineDTO> PaymentScheduleLines { get; set; }
    }
}