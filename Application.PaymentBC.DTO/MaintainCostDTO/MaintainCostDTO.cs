#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 13:36:40
// 文件名：MaintainCostDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 13:36:40
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    /// 维修成本
    /// </summary>
    public class MaintainCostDTO
    {
        #region 属性
        public int Year { get;  set; }
        /// <summary>
        /// 进厂时间
        /// </summary>
        public DateTime InMaintainTime { get; set; }
        /// <summary>
        /// 出厂时间
        /// </summary>
        public DateTime OutMaintainTime { get; set; }
        /// <summary>
        /// 总周期
        /// </summary>
        public int TotalDays { get; set; }
        /// <summary>
        /// 业务部门申报金额
        /// </summary>
        public decimal DepartmentDeclareAmount { get; set; }
        /// <summary>
        /// 财务批复预算金额
        /// </summary>
        public decimal FinancialApprovalAmount { get; set; }
        /// <summary>
        /// 财务批复预算金额（不含增值税）
        /// </summary>
        public decimal FinancialApprovalAmountNonTax { get; set; }
        /// <summary>
        /// 进厂时间
        /// </summary>
        public DateTime AcutalInMaintainTime { get; set; }
        /// <summary>
        /// 出厂时间
        /// </summary>
        public DateTime AcutalOutMaintainTime { get; set; }
        /// <summary>
        /// 总周期
        /// </summary>
        public int AcutalTotalDays { get; set; }
        /// <summary>
        /// 预估金额
        /// </summary>
        public decimal AcutalBudgetAmount { get; set; }
        /// <summary>
        /// 实际结算金额
        /// </summary>
        public decimal AcutalAmount { get; set; }
        #endregion

        #region 外键属性
        public int? MaintainInvoiceId { get; set; }
        #endregion
    }
}
