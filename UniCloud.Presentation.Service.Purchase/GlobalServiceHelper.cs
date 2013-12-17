#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/21 11:35:52
// 文件名：PurchaseService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using Telerik.Windows.Data;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Service.Purchase
{
    public static class GlobalServiceHelper
    {
        private static readonly PurchaseData Context;

        static GlobalServiceHelper()
        {
            Context = new PurchaseData(AgentHelper.PurchaseUri);
        }

        #region Supplier

        private static QueryableDataServiceCollectionView<SupplierDTO> _supplier;

        /// <summary>
        ///     加载数据
        /// </summary>
        public static QueryableDataServiceCollectionView<SupplierDTO> GetSupplier(Action loaded)
        {
            if (_supplier == null)
            {
                _supplier = new QueryableDataServiceCollectionView<SupplierDTO>(Context, Context.Suppliers)
                {
                    AutoLoad = true
                };
                _supplier.LoadedData += (o, e) => loaded();
            }
            return _supplier;
        }

        #endregion

        #region AircraftType

        private static QueryableDataServiceCollectionView<AircraftTypeDTO> _aircraftType;

        /// <summary>
        ///     加载数据
        /// </summary>
        public static QueryableDataServiceCollectionView<AircraftTypeDTO> GetAircraftType(Action loaded)
        {
            if (_aircraftType == null)
            {
                _aircraftType = new QueryableDataServiceCollectionView<AircraftTypeDTO>(Context, Context.AircraftTypes)
                {
                    AutoLoad = true
                };
                _aircraftType.LoadedData += (o, e) => loaded();
            }
            return _aircraftType;
        }

        #endregion

        #region Currency

        private static QueryableDataServiceCollectionView<CurrencyDTO> _currency;

        /// <summary>
        ///     加载数据
        /// </summary>
        public static QueryableDataServiceCollectionView<CurrencyDTO> GetCurrency(Action loaded)
        {
            if (_currency == null)
            {
                _currency = new QueryableDataServiceCollectionView<CurrencyDTO>(Context, Context.Currencies)
                {
                    AutoLoad = true
                };
                _currency.LoadedData += (o, e) => loaded();
            }
            return _currency;
        }

        #endregion

        #region Linkman

        private static QueryableDataServiceCollectionView<LinkmanDTO> _linkman;

        /// <summary>
        ///     加载数据
        /// </summary>
        public static QueryableDataServiceCollectionView<LinkmanDTO> GetLinkman(Action loaded)
        {
            if (_linkman == null)
            {
                _linkman = new QueryableDataServiceCollectionView<LinkmanDTO>(Context, Context.Linkmans)
                {
                    AutoLoad = true
                };
                _linkman.LoadedData += (o, e) => loaded();
            }
            return _linkman;
        }

        #endregion

        #region SupplierCompanyAcMaterial

        private static QueryableDataServiceCollectionView<SupplierCompanyAcMaterialDTO> _aircraftMaterials;

        /// <summary>
        ///     加载数据
        /// </summary>
        public static QueryableDataServiceCollectionView<SupplierCompanyAcMaterialDTO> GetAircraftMaterial(Action loaded)
        {
            if (_aircraftMaterials == null)
            {
                _aircraftMaterials = new QueryableDataServiceCollectionView<SupplierCompanyAcMaterialDTO>(Context,
                    Context.SupplierCompanyAcMaterials)
                {
                    AutoLoad = true
                };
                _aircraftMaterials.LoadedData += (o, e) => loaded();
            }
            return _aircraftMaterials;
        }

        #endregion

        #region SupplierCompanyEngineMaterial

        private static QueryableDataServiceCollectionView<SupplierCompanyEngineMaterialDTO> _engineMaterials;

        /// <summary>
        ///     加载数据
        /// </summary>
        public static QueryableDataServiceCollectionView<SupplierCompanyEngineMaterialDTO> GetEngineMaterial(
            Action loaded)
        {
            if (_engineMaterials == null)
            {
                _engineMaterials = new QueryableDataServiceCollectionView<SupplierCompanyEngineMaterialDTO>(Context,
                    Context.SupplierCompanyEngineMaterials)
                {
                    AutoLoad = true
                };
                _engineMaterials.LoadedData += (o, e) => loaded();
            }
            return _engineMaterials;
        }

        #endregion

        #region SupplierCompanyEngineMaterial

        private static QueryableDataServiceCollectionView<SupplierCompanyBFEMaterialDTO> _bfeMaterials;

        /// <summary>
        ///     加载数据
        /// </summary>
        public static QueryableDataServiceCollectionView<SupplierCompanyBFEMaterialDTO> GetBfeMaterial(Action loaded)
        {
            if (_bfeMaterials == null)
            {
                _bfeMaterials = new QueryableDataServiceCollectionView<SupplierCompanyBFEMaterialDTO>(Context,
                    Context.SupplierCompanyBFEMaterials)
                {
                    AutoLoad = true
                };
                _bfeMaterials.LoadedData += (o, e) => loaded();
            }
            return _bfeMaterials;
        }

        #endregion
    }
}