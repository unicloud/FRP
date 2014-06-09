#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/18，17:12
// 方案：FRP
// 项目：Infrastructure.Data.UberModel
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using UniCloud.Domain.UberModel.Aggregates.CurrencyAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class TradeData : InitialDataBase
    {
        public TradeData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        public override void InitialData()
        {
            var currencies = new List<Currency>
            {
                CurrencyFactory.CreateCurrency("美元", "USD", "US$"),
                CurrencyFactory.CreateCurrency("人民币", "CNY", "¥"),
                CurrencyFactory.CreateCurrency("欧元", "EUR", "€"),
                CurrencyFactory.CreateCurrency("英镑", "GBP", "£"),
                CurrencyFactory.CreateCurrency("加元", "CAD", "Can$"),
                CurrencyFactory.CreateCurrency("澳元", "AUD", "$A"),
                CurrencyFactory.CreateCurrency("港元", "HKD", "HK$"),
            };
            currencies.ForEach(a => Context.Currencies.Add(a));
        }
    }
}