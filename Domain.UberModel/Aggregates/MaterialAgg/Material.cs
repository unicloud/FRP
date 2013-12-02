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
using UniCloud.Domain.UberModel.Aggregates.ManufacturerAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierCompanyMaterialAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.MaterialAgg
{
    /// <summary>
    ///     采购物料聚合根
    /// </summary>
    public abstract class Material : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<SupplierCompanyMaterial> _supplierCompanyMaterials;

        #endregion

        #region 属性

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     制造商
        /// </summary>
        public Guid? ManufacturerID { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     制造商
        /// </summary>
        public virtual Manufacturer Manufacturer { get; set; }

        /// <summary>
        ///     供应商物料集合
        /// </summary>
        public virtual ICollection<SupplierCompanyMaterial> SupplierCompanyMaterials
        {
            get
            {
                return _supplierCompanyMaterials ?? (_supplierCompanyMaterials = new HashSet<SupplierCompanyMaterial>());
            }
            set { _supplierCompanyMaterials = new HashSet<SupplierCompanyMaterial>(value); }
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