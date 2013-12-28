#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
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
using UniCloud.Domain.UberModel.Aggregates.BankAccountAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.UberModel.Enums;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.SupplierAgg
{
    /// <summary>
    ///     供应商聚合根
    /// </summary>
    public class Supplier : EntityInt, IValidatableObject
    {
        #region 属性

        /// <summary>
        ///     供应商类型
        ///     <remarks>
        ///         国外、国内
        ///     </remarks>
        /// </summary>
        public SupplierType SupplierType { get; set; }

        /// <summary>
        ///     组织机构代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     更改日期
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 航空公司Guid
        /// </summary>
        public Guid? AirlineGuid { get; set ; }


        #endregion

        #region 外键属性

        /// <summary>
        ///     供应商公司ID
        /// </summary>
        public int SupplierCompanyId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     供应商公司
        /// </summary>
        public virtual SupplierCompany SupplierCompany { get; private set; }

        /// <summary>
        ///     银行账户
        /// </summary>
        public ICollection<BankAccount> BankAccounts { get; set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置供应商公司
        /// </summary>
        /// <param name="supplierCompany">供应商公司</param>
        public void SetSupplierCompany(SupplierCompany supplierCompany)
        {
            if (supplierCompany == null || supplierCompany.IsTransient())
            {
                throw new ArgumentException("供应商公司参数为空！");
            }

            SupplierCompany = supplierCompany;
            SupplierCompanyId = supplierCompany.Id;
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
