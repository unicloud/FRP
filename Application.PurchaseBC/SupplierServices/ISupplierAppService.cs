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

using System.Collections.Generic;
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

        /// <summary>
        ///     同步联系人信息。
        /// </summary>
        /// <param name="linkmen">联系人集合</param>
        void SyncLinkmanInfo(List<LinkmanDTO> linkmen);

        /// <summary>
        ///     同步供应商信息。
        /// </summary>
        /// <param name="suppliers">供应商集合</param>
        void SyncSupplierInfo(List<SupplierDTO> suppliers);

        void SyncBankAccountInfo(List<BankAccountDTO> bankAccounts);

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
        ///     获取合作公司的飞机物料。
        /// </summary>
        /// <returns>合作公司的飞机物料。</returns>
        IQueryable<SupplierCompanyAcMaterialDTO> GetSupplierCompanyAcMaterials();

        /// <summary>
        ///     获取合作公司的发动机物料。
        /// </summary>
        /// <returns>合作公司的发动机物料。</returns>
        IQueryable<SupplierCompanyEngineMaterialDTO> GetSupplierCompanyEngineMaterials();

        /// <summary>
        ///     获取合作公司的BFE物料。
        /// </summary>
        /// <returns>合作公司的BFE物料。</returns>
        IQueryable<SupplierCompanyBFEMaterialDTO> GetSupplierCompanyBFEMaterials();

        /// <summary>
        ///     新增合作公司飞机物料。
        /// </summary>
        /// <param name="supplierCompanyAcMaterial">合作公司飞机物料DTO。</param>
        void InsertSupplierCompanyAcMaterial(SupplierCompanyAcMaterialDTO supplierCompanyAcMaterial);

        /// <summary>
        ///     删除合作公司飞机物料。
        /// </summary>
        /// <param name="supplierCompanyAcMaterial">合作公司飞机物料DTO。</param>
        void DeleteSupplierCompanyAcMaterial(SupplierCompanyAcMaterialDTO supplierCompanyAcMaterial);

        /// <summary>
        ///     新增合作公司发动机物料。
        /// </summary>
        /// <param name="supplierCompanyEngineMaterial">合作公司发动机物料DTO。</param>
        void InsertSupplierCompanyEngineMaterial(SupplierCompanyEngineMaterialDTO supplierCompanyEngineMaterial);

        /// <summary>
        ///     删除合作公司发动机物料。
        /// </summary>
        /// <param name="supplierCompanyEngineMaterial">合作公司发动机物料DTO。</param>
        void DeleteSupplierCompanyEngineMaterial(SupplierCompanyEngineMaterialDTO supplierCompanyEngineMaterial);

        /// <summary>
        ///     新增合作公司BFE物料。
        /// </summary>
        /// <param name="supplierCompanyBfeMaterial">合作公司BFE物料DTO。</param>
        void InsertSupplierCompanyBFEMaterial(SupplierCompanyBFEMaterialDTO supplierCompanyBfeMaterial);

        /// <summary>
        ///     删除合作公司BFE物料。
        /// </summary>
        /// <param name="supplierCompanyBfeMaterial">合作公司BFE物料DTO。</param>
        void DeleteSupplierCompanyBFEMaterial(SupplierCompanyBFEMaterialDTO supplierCompanyBfeMaterial);

        #endregion

        #region 获取供应商信息

        /// <summary>
        /// 获取所有的飞机供应商（飞机采购和租赁供应商）
        /// </summary>
        /// <returns></returns>
        List<SupplierDTO> GetAircraftSuppliers();

        /// <summary>
        /// 获取所有的发动机供应商（发动机采购和租赁供应商）
        /// </summary>
        /// <returns></returns>
        List<SupplierDTO> GetEngineSuppliers();

        /// <summary>
        /// 获取所有的飞机采购供应商
        /// </summary>
        /// <returns></returns>
        List<SupplierDTO> GetAircraftPurchaseSuppliers();

        /// <summary>
        /// 获取所有的飞机租赁供应商
        /// </summary>
        /// <returns></returns>
        List<SupplierDTO> GetAircraftLeaseSuppliers();

        /// <summary>
        /// 获取所有的发动机采购供应商
        /// </summary>
        /// <returns></returns>
        List<SupplierDTO> GetEnginePurchaseSuppliers();

        /// <summary>
        /// 获取所有的发动机租赁供应商
        /// </summary>
        /// <returns></returns>
        List<SupplierDTO> GetEngineLeaseSuppliers();

        /// <summary>
        /// 获取所有的BFE供应商
        /// </summary>
        /// <returns></returns>
        List<SupplierDTO> GetBfeSuppliers();

        /// <summary>
        /// 获取所有的维修供应商
        /// </summary>
        /// <returns></returns>
        List<SupplierDTO> GetMaintainSuppliers();

        /// <summary>
        /// 获取所有的"其他"供应商
        /// </summary>
        /// <returns></returns>
        List<SupplierDTO> GetOtherSuppliers();

        #endregion

    }
}