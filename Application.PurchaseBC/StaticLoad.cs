#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/17，13:39
// 方案：FRP
// 项目：Application.PurchaseBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.AircraftTypeQueries;
using UniCloud.Application.PurchaseBC.Query.CurrencyQueries;
using UniCloud.Application.PurchaseBC.Query.SupplierQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyMaterialAgg;

#endregion

namespace UniCloud.Application.PurchaseBC
{
    /// <summary>
    /// </summary>
    public class StaticLoad : IStaticLoad
    {
        private static bool _refreshAircraftType;
        private static bool _refreshCurrency;
        private static bool _refreshLinkman;
        private static bool _refreshSupplier;
        private static bool _refreshSupplierMaterial;

        private static IList<SupplierCompanyAcMaterialDTO> _aircraftMaterials;
        private static IList<AircraftTypeDTO> _aircraftTypes;
        private static IList<SupplierCompanyBFEMaterialDTO> _bfeMaterials;
        private static IList<CurrencyDTO> _currencies;
        private static IList<SupplierCompanyEngineMaterialDTO> _engineMaterials;
        private static IList<LinkmanDTO> _linkmen;
        private static IList<SupplierDTO> _suppliers;

        private readonly IAircraftTypeQuery _aircraftTypeQuery;
        private readonly ICurrencyQuery _currencyQuery;
        private readonly ISupplierQuery _supplierQuery;

        public StaticLoad(IAircraftTypeQuery aircraftTypeQuery, ICurrencyQuery currencyQuery,
            ISupplierQuery supplierQuery)
        {
            _aircraftTypeQuery = aircraftTypeQuery;
            _currencyQuery = currencyQuery;
            _supplierQuery = supplierQuery;
        }

        #region IStaticLoad 成员

        public void RefreshAircraftType()
        {
            _refreshAircraftType = true;
        }

        public void RefreshCurrency()
        {
            _refreshCurrency = true;
        }

        public void RefreshLinkman()
        {
            _refreshLinkman = true;
        }

        public void RefreshSupplier()
        {
            _refreshSupplier = true;
        }

        public void RefreshSupplierMaterial()
        {
            _refreshSupplierMaterial = true;
        }

        /// <summary>
        ///     获取机型静态集合
        /// </summary>
        /// <returns>机型静态集合</returns>
        public IQueryable<AircraftTypeDTO> GetAircraftTypes()
        {
            if (_aircraftTypes == null || _refreshAircraftType)
            {
                var query = new QueryBuilder<AircraftType>();
                _aircraftTypes = _aircraftTypeQuery.AircraftTypeDTOQuery(query).ToList();
                _refreshAircraftType = false;
            }
            return _aircraftTypes.AsQueryable<AircraftTypeDTO>();
        }

        /// <summary>
        ///     获取币种静态集合
        /// </summary>
        /// <returns>币种静态集合</returns>
        public IQueryable<CurrencyDTO> GetCurrencies()
        {
            if (_currencies == null || _refreshCurrency)
            {
                var query = new QueryBuilder<Currency>();
                _currencies = _currencyQuery.CurrenciesQuery(query).ToList();
                _refreshCurrency = false;
            }
            return _currencies.AsQueryable<CurrencyDTO>();
        }

        /// <summary>
        ///     获取联系人静态集合
        /// </summary>
        /// <returns>联系人静态集合</returns>
        public IQueryable<LinkmanDTO> GetLinkMen()
        {
            if (_linkmen == null || _refreshLinkman)
            {
                var query = new QueryBuilder<Linkman>();
                _linkmen = _supplierQuery.LinkmansQuery(query).ToList();
                _refreshLinkman = false;
            }
            return _linkmen.AsQueryable<LinkmanDTO>();
        }

        /// <summary>
        ///     获取供应商静态集合
        /// </summary>
        /// <returns>供应商静态集合</returns>
        public IQueryable<SupplierDTO> GetSuppliers()
        {
            if (_suppliers == null || _refreshSupplier)
            {
                var query = new QueryBuilder<Supplier>();
                _suppliers = _supplierQuery.SuppliersQuery(query).ToList();
                _refreshSupplier = false;
            }
            return _suppliers.AsQueryable<SupplierDTO>();
        }

        /// <summary>
        ///     获取供应商飞机物料静态集合
        /// </summary>
        /// <returns>供应商飞机物料静态集合</returns>
        public IQueryable<SupplierCompanyAcMaterialDTO> GetSupplierCompanyAcMaterials()
        {
            if (_aircraftMaterials == null || _refreshSupplierMaterial)
            {
                var query = new QueryBuilder<SupplierCompanyMaterial>();
                _aircraftMaterials = _supplierQuery.SupplierCompanyAcMaterialsQuery(query).ToList();
                _refreshSupplierMaterial = false;
            }
            return _aircraftMaterials.AsQueryable<SupplierCompanyAcMaterialDTO>();
        }

        /// <summary>
        ///     获取供应商发动机物料静态集合
        /// </summary>
        /// <returns>供应商发动机物料静态集合</returns>
        public IQueryable<SupplierCompanyEngineMaterialDTO> GetSupplierCompanyEngineMaterials()
        {
            if (_engineMaterials == null || _refreshSupplierMaterial)
            {
                var query = new QueryBuilder<SupplierCompanyMaterial>();
                _engineMaterials = _supplierQuery.SupplierCompanyEngineMaterialsQuery(query).ToList();
                _refreshSupplierMaterial = false;
            }
            return _engineMaterials.AsQueryable<SupplierCompanyEngineMaterialDTO>();
        }

        /// <summary>
        ///     获取供应商BFE物料静态集合
        /// </summary>
        /// <returns>供应商BFE物料静态集合</returns>
        public IQueryable<SupplierCompanyBFEMaterialDTO> GetSupplierCompanyBFEMaterials()
        {
            if (_bfeMaterials == null || _refreshSupplierMaterial)
            {
                var query = new QueryBuilder<SupplierCompanyMaterial>();
                _bfeMaterials = _supplierQuery.SupplierCompanyBFEMaterialsQuery(query).ToList();
                _refreshSupplierMaterial = false;
            }
            return _bfeMaterials.AsQueryable<SupplierCompanyBFEMaterialDTO>();
        }

        #endregion
    }
}