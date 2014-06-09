#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/6/9 9:17:55
// 文件名：CurrencyFactory
// 版本：V1.0.0
//
// 修改者：  时间：2014/6/9 9:17:55
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.CurrencyAgg
{
    public static class CurrencyFactory
    {
        /// <summary>
        ///   创建币种
        /// </summary>
        /// <param name="cnName">币种中文名称</param>
        /// <param name="enName">币种英文名称</param>
        /// <param name="symbol">货币符号</param>
        /// <returns></returns>
        public static Currency CreateCurrency(string cnName, string enName,string symbol)
        {
            var currency = new Currency
            {
                CnName = cnName,
                EnName = enName,
            };
            currency.GenerateNewIdentity();

            return currency;
        }
    }
}
