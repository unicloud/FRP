#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：Service.Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using Telerik.Windows.Data;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Service.Purchase
{
    public interface IPurchaseService : IService
    {
        /// <summary>
        ///     数据服务上下文
        /// </summary>
        PurchaseData Context { get; }

        #region 获取静态数据

        /// <summary>
        ///     获取供应商
        /// </summary>
        /// <param name="loaded">回调</param>
        /// <returns>供应商集合</returns>
        QueryableDataServiceCollectionView<SupplierDTO> GetSupplier(Action loaded);

        /// <summary>
        ///     获取机型
        /// </summary>
        /// <param name="loaded">回调</param>
        /// <returns>机型集合</returns>
        QueryableDataServiceCollectionView<AircraftTypeDTO> GetAircraftType(Action loaded);

        /// <summary>
        ///     获取币种
        /// </summary>
        /// <param name="loaded">回调</param>
        /// <returns>币种集合</returns>
        QueryableDataServiceCollectionView<CurrencyDTO> GetCurrency(Action loaded);

        /// <summary>
        ///     获取联系人
        /// </summary>
        /// <param name="loaded">回调</param>
        /// <returns>联系人集合</returns>
        QueryableDataServiceCollectionView<LinkmanDTO> GetLinkman(Action loaded);

        /// <summary>
        ///     获取飞机物料
        /// </summary>
        /// <param name="loaded">回调</param>
        /// <returns>飞机物料集合</returns>
        QueryableDataServiceCollectionView<SupplierCompanyAcMaterialDTO> GetAircraftMaterial(Action loaded);

        /// <summary>
        ///     获取发动机物料
        /// </summary>
        /// <param name="loaded">回调</param>
        /// <returns>发动机物料集合</returns>
        QueryableDataServiceCollectionView<SupplierCompanyEngineMaterialDTO> GetEngineMaterial(Action loaded);

        /// <summary>
        ///     获取BFE物料
        /// </summary>
        /// <param name="loaded">回调</param>
        /// <returns>BFE物料集合</returns>
        QueryableDataServiceCollectionView<SupplierCompanyBFEMaterialDTO> GetBfeMaterial(Action loaded);

        #endregion
    }
}