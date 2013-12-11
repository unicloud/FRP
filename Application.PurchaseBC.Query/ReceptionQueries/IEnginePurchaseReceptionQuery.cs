﻿#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/16 14:45:39
// 文件名：IEnginePurchaseReceptionQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;

namespace UniCloud.Application.PurchaseBC.Query.ReceptionQueries
{
    /// <summary>
    /// 采购发动机接收项目查询接口
    /// </summary>
    public interface IEnginePurchaseReceptionQuery
    {
        /// <summary>
        ///    采购发动机接收项目查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>采购发动机接收项目DTO集合。</returns>
        IQueryable<EnginePurchaseReceptionDTO> EnginePurchaseReceptionDTOQuery(
            QueryBuilder<EnginePurchaseReception> query);
    }
}
