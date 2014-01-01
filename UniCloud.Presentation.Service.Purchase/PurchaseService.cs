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
using Telerik.Windows.Data;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Service.Purchase
{
    public class PurchaseService : ServiceBase, IPurchaseService
    {
        private static QueryableDataServiceCollectionView<SupplierDTO> _supplier;
        private static QueryableDataServiceCollectionView<AircraftTypeDTO> _aircraftType;
        private static QueryableDataServiceCollectionView<CurrencyDTO> _currency;
        private static QueryableDataServiceCollectionView<LinkmanDTO> _linkman;
        private static QueryableDataServiceCollectionView<SupplierCompanyAcMaterialDTO> _aircraftMaterials;
        private static QueryableDataServiceCollectionView<SupplierCompanyEngineMaterialDTO> _engineMaterials;
        private static QueryableDataServiceCollectionView<SupplierCompanyBFEMaterialDTO> _bfeMaterials;
        private readonly PurchaseData _context;

        public PurchaseService(PurchaseData context)
            : base(context)
        {
            _context = context;
        }

        #region IPurchaseService 成员

        /// <summary>
        ///     <see cref="IPurchaseService" />
        /// </summary>
        public PurchaseData Context
        {
            get { return _context; }
        }

        #region 获取静态数据

        public QueryableDataServiceCollectionView<SupplierDTO> GetSupplier(Action loaded)
        {
            return GetStaticData(_supplier, loaded, _context.Suppliers);
        }

        public QueryableDataServiceCollectionView<AircraftTypeDTO> GetAircraftType(Action loaded)
        {
            return GetStaticData(_aircraftType, loaded, _context.AircraftTypes);
        }

        public QueryableDataServiceCollectionView<CurrencyDTO> GetCurrency(Action loaded)
        {
            return GetStaticData(_currency, loaded, _context.Currencies);
        }

        public QueryableDataServiceCollectionView<LinkmanDTO> GetLinkman(Action loaded)
        {
            return GetStaticData(_linkman, loaded, _context.Linkmans);
        }

        public QueryableDataServiceCollectionView<SupplierCompanyAcMaterialDTO> GetAircraftMaterial(Action loaded)
        {
            return GetStaticData(_aircraftMaterials, loaded, _context.SupplierCompanyAcMaterials);
        }

        public QueryableDataServiceCollectionView<SupplierCompanyEngineMaterialDTO> GetEngineMaterial(Action loaded)
        {
            return GetStaticData(_engineMaterials, loaded, _context.SupplierCompanyEngineMaterials);
        }

        public QueryableDataServiceCollectionView<SupplierCompanyBFEMaterialDTO> GetBfeMaterial(Action loaded)
        {
            return GetStaticData(_bfeMaterials, loaded, _context.SupplierCompanyBFEMaterials);
        }

        #endregion

        #endregion
    }
}