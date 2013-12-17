#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/17，15:42
// 方案：FRP
// 项目：Application.PurchaseBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;

#endregion

namespace UniCloud.Application.PurchaseBC
{
    /// <summary>
    ///     静态数据加载接口
    /// </summary>
    public interface IStaticLoad
    {
        /// <summary>
        ///     设置刷新机型
        /// </summary>
        void RefreshAircraftType();

        /// <summary>
        ///     设置刷新货币
        /// </summary>
        void RefreshCurrency();

        /// <summary>
        ///     设置刷新联系人
        /// </summary>
        void RefreshLinkman();

        /// <summary>
        ///     设置刷新供应商
        /// </summary>
        void RefreshSupplier();

        /// <summary>
        ///     设置刷新供应商物料
        /// </summary>
        void RefreshSupplierMaterial();

        /// <summary>
        ///     获取机型静态集合
        /// </summary>
        /// <returns>机型静态集合</returns>
        IQueryable<AircraftTypeDTO> GetAircraftTypes();

        /// <summary>
        ///     获取币种静态集合
        /// </summary>
        /// <returns>币种静态集合</returns>
        IQueryable<CurrencyDTO> GetCurrencies();

        /// <summary>
        ///     获取联系人静态集合
        /// </summary>
        /// <returns>联系人静态集合</returns>
        IQueryable<LinkmanDTO> GetLinkMen();

        /// <summary>
        ///     获取供应商静态集合
        /// </summary>
        /// <returns>供应商静态集合</returns>
        IQueryable<SupplierDTO> GetSuppliers();

        /// <summary>
        ///     获取供应商飞机物料静态集合
        /// </summary>
        /// <returns>供应商飞机物料静态集合</returns>
        IQueryable<SupplierCompanyAcMaterialDTO> GetSupplierCompanyAcMaterials();

        /// <summary>
        ///     获取供应商发动机物料静态集合
        /// </summary>
        /// <returns>供应商发动机物料静态集合</returns>
        IQueryable<SupplierCompanyEngineMaterialDTO> GetSupplierCompanyEngineMaterials();

        /// <summary>
        ///     获取供应商BFE物料静态集合
        /// </summary>
        /// <returns>供应商BFE物料静态集合</returns>
        IQueryable<SupplierCompanyBFEMaterialDTO> GetSupplierCompanyBFEMaterials();
    }
}