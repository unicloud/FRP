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
        /// <param name="supplierType">供应商类型</param>
        /// <param name="code">供应商编码</param>
        /// <param name="name">名称</param>
        /// <param name="note">备注</param>
        /// <returns>创建的供应商</returns>
        public static Supplier CreateSupplier(SupplierType supplierType, string code, string name, string note)
        {
            var supplier = new Supplier
            {
                SupplierType = supplierType,
                Code = code,
                CnName = name,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsValid = true,
                Note = note
            };

            return supplier;
        }
    }
}