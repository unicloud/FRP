﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/17，20:11
// 方案：FRP
// 项目：Application.PurchaseBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.TradeQueries
{
    public class TradeQuery : ITradeQuery
    {
        private readonly ITradeRepository _tradeRepository;

        public TradeQuery(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        #region ITradeQuery 成员

        /// <summary>
        ///     <see cref="ITradeQuery" />
        /// </summary>
        /// <param name="query">
        ///     <see cref="ITradeQuery" />
        /// </param>
        /// <returns>
        ///     <see cref="ITradeQuery" />
        /// </returns>
        public IQueryable<TradeDTO> TradesQuery(QueryBuilder<Trade> query)
        {
            var result = query.ApplyTo(_tradeRepository.GetAll()).Select(t => new TradeDTO
            {
                Id = t.Id,
                TradeNumber = t.TradeNumber,
                Name = t.Name,
                Description = t.Description,
                SupplierId = t.SupplierId,
                StartDate = t.StartDate,
                IsClosed = t.IsClosed,
                Status = (int) t.Status
            });
            return result;
        }

        #endregion
    }
}