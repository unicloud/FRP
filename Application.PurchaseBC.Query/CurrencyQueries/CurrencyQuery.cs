#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/11，17:13
// 方案：FRP
// 项目：Application.PurchaseBC.Query
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.CurrencyAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.CurrencyQueries
{
    public class CurrencyQuery : ICurrencyQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public CurrencyQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ICurrencyQuery 成员

        /// <summary>
        ///     <see cref="ICurrencyQuery" />
        /// </summary>
        /// <param name="query">
        ///     <see cref="ICurrencyQuery" />
        /// </param>
        /// <returns>
        ///     <see cref="ICurrencyQuery" />
        /// </returns>
        public IQueryable<CurrencyDTO> CurrenciesQuery(QueryBuilder<Currency> query)
        {
            var result = query.ApplyTo(_unitOfWork.CreateSet<Currency>()).Select(c => new CurrencyDTO
            {
                Id = c.Id,
                Name = c.CnName
            });

            return result;
        }

        #endregion
    }
}