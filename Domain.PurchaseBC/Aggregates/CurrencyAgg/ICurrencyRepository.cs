﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/05，16:11
// 文件名：ICurrencyRepository.cs
// 程序集：UniCloud.Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.CurrencyAgg
{
    /// <summary>
    ///     币种仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{Currency}" />
    /// </summary>
    public interface ICurrencyRepository : IRepository<Currency>
    {
    }
}