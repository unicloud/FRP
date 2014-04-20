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

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg
{
    /// <summary>
    ///     供应商公司工厂
    /// </summary>
    public static class SupplierCompanyFactory
    {
        /// <summary>
        ///     创建供应商公司
        /// </summary>
        /// <param name="code">供应商公司编码</param>
        /// <returns></returns>
        public static SupplierCompany CreateSupplieCompany(string code)
        {
            var supplierCompany = new SupplierCompany
            {
                Code = code,
                CreateDate = DateTime.Now
            };
            supplierCompany.GenerateNewIdentity();
            return supplierCompany;
        }
    }
}