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

using System;
using System.Linq.Expressions;

namespace UniCloud.Domain.FleetPlanBC.Aggregates.SupplierCompanyAgg
{
    /// <summary>
    ///     供应商仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{SupplierCompany}" />
    /// </summary>
    public interface ISupplierCompanyRepository : IRepository<SupplierCompany>
    {
        SupplierCompany GetSupplierCompany(Expression<Func<SupplierCompany, bool>> condition);
    }
}