#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/05，16:11
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
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg
{
    /// <summary>
    ///     维修合同聚合根
    /// </summary>
    public class MaintainContract : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能通过工厂方法去创建新实例
        /// </summary>
        internal MaintainContract()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     合同号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        ///     合同名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     签约对象
        /// </summary>
        public string Signatory { get; set; }

        /// <summary>
        ///     签约日期
        /// </summary>
        public DateTime SignDate { get; set; }

        /// <summary>
        ///     摘要
        /// </summary>
        public string Abstract { get; set; }

        /// <summary>
        ///     文档名称
        /// </summary>
        public string DocumentName { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     签约对象ID
        /// </summary>
        public int SignatoryId { get; set; }

        /// <summary>
        ///     文档ID
        /// </summary>
        public Guid DocumentId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     签约对象
        /// </summary>
        public virtual Supplier Supplier { get; set; }

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