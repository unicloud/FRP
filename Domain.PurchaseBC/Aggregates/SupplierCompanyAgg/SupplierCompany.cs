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
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyMaterialAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg
{
    /// <summary>
    ///     供应商聚合根
    ///     供应商公司
    /// </summary>
    public class SupplierCompany : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<SupplierCompanyMaterial> _supplierCompanyMaterials;
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

        /// <summary>
        ///     添加供应商物料
        /// </summary>
        /// <param name="material">物料</param>
        public SupplierCompanyMaterial AddMaterial(Material material)
        {
            if (material == null || material.IsTransient())
            {
                throw new ArgumentException("物料参数为空！");
            }

            var supplierMaterial = new SupplierCompanyMaterial
            {
                SupplierCompanyId = Id,
                SupplierCompany = this,
                MaterialId = material.Id,
                Material = material
            };

            SupplierCompanyMaterials.Add(supplierMaterial);

            return supplierMaterial;
        }

        /// <summary>
        /// 添加物料
        /// </summary>
        /// <param name="mterialId">物料主键</param>
        /// <returns></returns>
        public SupplierCompanyMaterial AddMaterial(int mterialId)
        {
            if (mterialId==0)
            {
                throw new ArgumentException("物料参数为空！");
            }

            var supplierMaterial = new SupplierCompanyMaterial
            {
                SupplierCompanyId = Id,
                MaterialId = mterialId,
            };

            SupplierCompanyMaterials.Add(supplierMaterial);
            return supplierMaterial;
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