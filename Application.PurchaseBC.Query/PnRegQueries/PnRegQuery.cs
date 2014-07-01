﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/18，10:11
// 文件名：ForwarderQuery.cs
// 程序集：UniCloud.Application.PurchaseBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.PnRegAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.PnRegQueries
{
    /// <summary>
    ///     实现部件查询接口。
    /// </summary>
    public class PnRegQuery : IPnRegQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public PnRegQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     部件查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>部件DTO集合</returns>
        public IQueryable<PnRegDTO> PnRegsQuery(QueryBuilder<PnReg> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<PnReg>()).Select(p => new PnRegDTO
            {
                Id = p.Id,
                Description = p.Description,
                Pn = p.Pn
            });
        }
    }
}