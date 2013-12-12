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

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;

#endregion

namespace UniCloud.Application.PurchaseBC.CurrencyServices
{
    public interface ICurrencyAppService
    {
        /// <summary>
        ///     获取币种集合
        /// </summary>
        /// <returns>币种集合</returns>
        IQueryable<CurrencyDTO> GetCurrencies();
    }
}