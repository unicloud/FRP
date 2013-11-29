#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/05，14:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg
{
    /// <summary>
    ///     交易工厂
    /// </summary>
    public static class TradeFactory
    {
        /// <summary>
        ///     创建交易
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="seq">流水号</param>
        /// <returns>创建的交易</returns>
        public static Trade CreateTrade(string name, string description, DateTime startDate, int seq)
        {
            var trade = new Trade();
            trade.GenerateNewIdentity();

            trade.Name = name;
            trade.Description = description;
            trade.CreateDate = DateTime.Now;
            trade.StartDate = startDate;

            trade.SetTradeNumber(seq);

            return trade;
        }
    }
}