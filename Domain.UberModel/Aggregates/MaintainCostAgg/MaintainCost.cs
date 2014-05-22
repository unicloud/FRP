#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 10:40:01
// 文件名：MaintainCost
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 10:40:01
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.UberModel.Aggregates.MaintainInvoiceAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.MaintainCostAgg
{
    /// <summary>
    ///   维修成本聚合根
    /// </summary>
    public abstract class MaintainCost : EntityInt, IValidatableObject
    {
        #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal MaintainCost()
        {
        }

        #endregion

        #region 属性
        public int Year { get; internal set; }
        /// <summary>
        /// 进厂时间
        /// </summary>
        public DateTime InMaintainTime { get; internal set; }
        /// <summary>
        /// 出厂时间
        /// </summary>
        public DateTime OutMaintainTime { get; internal set; }
        /// <summary>
        /// 总周期
        /// </summary>
        public int TotalDays { get; internal set; }
        /// <summary>
        /// 业务部门申报金额
        /// </summary>
        public decimal DepartmentDeclareAmount { get; internal set; }
        /// <summary>
        /// 财务批复预算金额
        /// </summary>
        public decimal FinancialApprovalAmount { get; internal set; }
        /// <summary>
        /// 财务批复预算金额（不含增值税）
        /// </summary>
        public decimal FinancialApprovalAmountNonTax { get; internal set; }
        /// <summary>
        /// 进厂时间
        /// </summary>
        #endregion

        #region 外键属性
        public int? MaintainInvoiceId { get; internal set; }
        #endregion

        #region 导航属性
        public MaintainInvoice MaintainInvoice { get; set; }
        #endregion

        #region 操作
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
