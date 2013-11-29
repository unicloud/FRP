#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/18，10:11
// 文件名：ISupplierQuery.cs
// 程序集：UniCloud.Application.PurchaseBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.BankAccountAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierRoleAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.SupplierQueries
{
    /// <summary>
    ///     供应商查询接口。
    /// </summary>
    public interface ISupplierQuery
    {
        /// <summary>
        ///     查询供应商公司。
        /// </summary>
        /// <param name="query">查询条件。</param>
        /// <returns>供应商公司。</returns>
        IQueryable<SupplierCompanyDTO> SupplierCompanysQuery(
            QueryBuilder<SupplierCompany> query);

        /// <summary>
        ///     查询供应商。
        /// </summary>
        /// <param name="query">查询条件。</param>
        /// <returns>供应商。</returns>
        IQueryable<SupplierDTO> SuppliersQuery(
            QueryBuilder<Supplier> query);

        /// <summary>
        ///     查询联系人。
        /// </summary>
        /// <param name="query">查询条件。</param>
        /// <returns>联系人。</returns>
        IQueryable<LinkmanDTO> LinkmansQuery(
            QueryBuilder<Linkman> query);

        /// <summary>
        ///     合作公司相关物料。
        /// </summary>
        /// <param name="query">查询条件。</param>
        /// <returns>合作公司相关物料。</returns>
        IQueryable<SupplierCompanyMaterialDTO> SupplierCompanyMaterialsQuery(
            QueryBuilder<SupplierCompany> query);
    }
}