#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：linxw 时间：2013/12/17，13:39
// 方案：FRP
// 项目：Application.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.Query.CurrencyQueries;
using UniCloud.Application.PaymentBC.Query.SupplierQueries;
using UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Application.PaymentBC
{
    /// <summary>
    /// </summary>
    public class StaticLoad : IStaticLoad
    {
        private static bool _refreshCurrency;
        private static bool _refreshSupplier;

        private static IList<CurrencyDTO> _currencies;
        private static IList<SupplierDTO> _suppliers;

        private readonly ICurrencyQuery _currencyQuery;
        private readonly ISupplierQuery _supplierQuery;

        public StaticLoad(ICurrencyQuery currencyQuery, ISupplierQuery supplierQuery)
        {
            _currencyQuery = currencyQuery;
            _supplierQuery = supplierQuery;
        }

        #region IStaticLoad 成员

        public void RefreshCurrency()
        {
            _refreshCurrency = true;
        }

        public void RefreshSupplier()
        {
            _refreshSupplier = true;
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
            return _currencies.AsQueryable();
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
            return _suppliers.AsQueryable();
        }

        #endregion
    }
}