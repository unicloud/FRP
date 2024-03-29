﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，11:11
// 文件名：IOrderRepository.cs
// 程序集：UniCloud.Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg
{
    /// <summary>
    ///     订单仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{Order}" />
    /// </summary>
    public interface IOrderRepository : IRepository<Order>
    {
        /// <summary>
        ///     移除订单行
        /// </summary>
        /// <param name="line">订单行</param>
        void RemoveOrderLine(OrderLine line);

        /// <summary>
        ///     移除合同分解内容
        /// </summary>
        /// <param name="content">合同分解内容</param>
        void RemoveContractContent(ContractContent content);
    }
}