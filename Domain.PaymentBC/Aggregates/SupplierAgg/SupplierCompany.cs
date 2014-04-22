#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/17，11:11
// 方案：FRP
// 项目：Domain.PurchaseBC
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

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg
{
    /// <summary>
    ///     供应商聚合根
    ///     供应商公司
    /// </summary>
    public class SupplierCompany : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<Supplier> _suppliers;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal SupplierCompany()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     组织机构代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     联系人GUID
        /// </summary>
        public Guid LinkmanId { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     更改日期
        /// </summary>
        public DateTime UpdateDate { get; set; }
        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        /// <summary>
        ///     供应商集合
        /// </summary>
        public virtual ICollection<Supplier> Suppliers
        {
            get { return _suppliers ?? (_suppliers = new HashSet<Supplier>()); }
            set { _suppliers = new HashSet<Supplier>(value); }
        }

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