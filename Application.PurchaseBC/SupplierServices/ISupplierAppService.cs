#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/18，10:11
// 文件名：ISupplierAppService.cs
// 程序集：UniCloud.Application.PurchaseBC
// 版本：VVersionNumber
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;

#endregion

namespace UniCloud.Application.PurchaseBC.SupplierServices
{
    /// <summary>
    ///     表示用于处理供应商相关信息服务接口。
    /// </summary>
    public interface ISupplierAppService
    {
        /// <summary>
        ///     获取所有供应商公司。
        /// </summary>
        /// <returns>所有供应商公司。</returns>
        IQueryable<SupplierCompanyDTO> GetSupplierCompanys();

        /// <summary>
        ///     获取所有供应商信息，包括银行账户，联系人。
        /// </summary>
        /// <returns>所有供应商。</returns>
        IQueryable<SupplierDTO> GetSuppliers();

        /// <summary>
        ///     更新合作公司。
        /// </summary>
        /// <param name="supplierCompany">合作公司DTO。</param>
        void ModifySupplierCompany(SupplierCompanyDTO supplierCompany);

        #region 联系人相关操作

        /// <summary>
        ///     获取联系人。
        /// </summary>
        /// <returns>所有联系人。</returns>
        IQueryable<LinkmanDTO> GetLinkmans();

        /// <summary>
        ///     新增联系人。
        /// </summary>
        /// <param name="linkman">联系人DTO。</param>
        void InsertLinkman(LinkmanDTO linkman);

        /// <summary>
        ///     更新联系人。
        /// </summary>
        /// <param name="linkman">联系人DTO。</param>
        void ModifyLinkman(LinkmanDTO linkman);


        /// <summary>
        ///     删除联系人。
        /// </summary>
        /// <param name="linkman">联系人DTO。</param>
        void DeleteLinkman(LinkmanDTO linkman);

        #endregion

        #region 供应商物料相关操作

        /// <summary>
        ///     获取供应商物料。
        /// </summary>
        /// <returns>供应商物料。</returns>
        IQueryable<SupplierCompanyMaterialDTO> GetSupplierCompanyMaterials();


        /// <summary>
        ///     更新合作公司物料。
        /// </summary>
        /// <param name="supplierCompanyMaterial">合作公司物料DTO。</param>
        void ModifySupplierCompanyMaterial(SupplierCompanyMaterialDTO supplierCompanyMaterial);
        #endregion
    }
}