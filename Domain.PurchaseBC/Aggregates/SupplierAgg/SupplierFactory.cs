#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，14:11
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
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg
{
    /// <summary>
    ///     供应商工厂
    /// </summary>
    public static class SupplierFactory
    {
        /// <summary>
        ///     创建供应商
        ///     供应商公司与供应商具有一致的编码
        /// </summary>
        /// <returns>创建的供应商</returns>
        public static Supplier CreateSupplier()
        {
            var supplier = new Supplier
            {
                CreateDate = DateTime.Now,
            };
            supplier.GenerateNewIdentity();
            return supplier;
        }

        public static void SetSupplier(Supplier supplier, SupplierType supplierType, string code, string name, string shortName, bool isValid, string note, int supplierCompanyId)
        {
            supplier.SupplierType = supplierType;
            supplier.Code = code;
            supplier.CnName = name;
            supplier.CnShortName = shortName;
            supplier.UpdateDate = DateTime.Now;
            supplier.IsValid = isValid;
            supplier.Note = note;
            supplier.SupplierCompanyId = supplierCompanyId;
        }
    }
}