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

using System;

namespace UniCloud.Domain.FleetPlanBC.Aggregates.SupplierRoleAgg
{
    /// <summary>
    ///     供应商角色仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{SupplierRole}" />
    /// </summary>
    public interface ISupplierRoleRepository : IRepository<SupplierRole>
    {
        ///  <summary>
        /// 根据合作公司Id，获取角色。
        ///  </summary>
        /// <param name="type">类型</param>
        /// <param name="supplierCmpyId">主键</param>
        ///  <returns></returns>
        SupplierRole GetSupplierRoleBySupplierCmpyId(Type type, int supplierCmpyId);
    }
}