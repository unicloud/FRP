#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/15，10:11
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
using System.ComponentModel.Composition;
using Telerik.Windows.Data;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Service.Purchase
{
    [Export(typeof (IPurchaseService))]
    public class PurchaseService : ServiceBase, IPurchaseService
    {
        public PurchaseService()
        {
            context = new PurchaseData(AgentHelper.PurchaseUri);
        }

        #region IPurchaseService 成员

        /// <summary>
        ///     <see cref="IPurchaseService" />
        /// </summary>
        public PurchaseData Context
        {
            get { return context as PurchaseData; }
        }

        #region 获取静态数据

        public QueryableDataServiceCollectionView<SupplierDTO> GetSupplier(Action loaded, bool forceLoad = false)
        {
            return GetStaticData(Context.Suppliers, loaded, forceLoad);
        }

        public QueryableDataServiceCollectionView<AircraftTypeDTO> GetAircraftType(Action loaded, bool forceLoad = false)
        {
            return GetStaticData(Context.AircraftTypes, loaded, forceLoad);
        }

        public QueryableDataServiceCollectionView<CurrencyDTO> GetCurrency(Action loaded, bool forceLoad = false)
        {
            return GetStaticData(Context.Currencies, loaded, forceLoad);
        }

        public QueryableDataServiceCollectionView<LinkmanDTO> GetLinkman(Action loaded, bool forceLoad = false)
        {
            return GetStaticData(Context.Linkmans, loaded, forceLoad);
        }

        public QueryableDataServiceCollectionView<SupplierCompanyAcMaterialDTO> GetAircraftMaterial(Action loaded,
            bool forceLoad = false)
        {
            return GetStaticData(Context.SupplierCompanyAcMaterials, loaded, forceLoad);
        }

        public QueryableDataServiceCollectionView<SupplierCompanyEngineMaterialDTO> GetEngineMaterial(Action loaded,
            bool forceLoad = false)
        {
            return GetStaticData(Context.SupplierCompanyEngineMaterials, loaded, forceLoad);
        }

        public QueryableDataServiceCollectionView<SupplierCompanyBFEMaterialDTO> GetBfeMaterial(Action loaded,
            bool forceLoad = false)
        {
            return GetStaticData(Context.SupplierCompanyBFEMaterials, loaded, forceLoad);
        }

        #endregion

        #endregion
    }
}