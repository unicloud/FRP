#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/11，17:38
// 方案：FRP
// 项目：Application.PurchaseBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.Query.CurrencyQueries;
using UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg;

#endregion

namespace UniCloud.Application.PaymentBC.CurrencyServices
{
    [LogAOP]
    public class CurrencyAppService : ContextBoundObject, ICurrencyAppService
    {
        private readonly ICurrencyQuery _currencyQuery;

        public CurrencyAppService(ICurrencyQuery currencyQuery)
        {
            _currencyQuery = currencyQuery;
        }

        #region ICurrencyAppService 成员

        public IQueryable<CurrencyDTO> GetCurrencies()
        {
            var query = new QueryBuilder<Currency>();
            return _currencyQuery.CurrenciesQuery(query);
        }

        #endregion
    }
}