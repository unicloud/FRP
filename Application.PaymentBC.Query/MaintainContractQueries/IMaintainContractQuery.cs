﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/27，13:12
// 文件名：IMaintainContractQuery.cs
// 程序集：UniCloud.Application.PaymentBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainContractAgg;

namespace UniCloud.Application.PaymentBC.Query.MaintainContractQueries
{
    public interface IMaintainContractQuery
    {
        /// <summary>
        ///     维修合同查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>维修合同DTO集合</returns>
        IQueryable<MaintainContractDTO> MaintainContractsQuery(
            QueryBuilder<MaintainContract> query);

    
    }
}
