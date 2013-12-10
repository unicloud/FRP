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
            var currency = new Currency
            {
                CnName = "美元",
                EnName = "Dollers"
            };
            currency.GenerateNewIdentity();
            Context.Currencies.Add(currency);
        }
    }
}