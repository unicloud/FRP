﻿#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/13 17:33:38
// 文件名：IFunctionItemQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/13 17:33:38
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;

#endregion

namespace UniCloud.Application.BaseManagementBC.Query.FunctionItemQueries
{
    /// <summary>
    ///     FunctionItem查询接口
    /// </summary>
    public interface IFunctionItemQuery
    {
        /// <summary>
        ///     FunctionItem查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>FunctionItemDTO集合</returns>
        IQueryable<FunctionItemDTO> FunctionItemsQuery(QueryBuilder<FunctionItem> query);

        /// <summary>
        ///     获取FunctionItemWithHierarchy集合。
        ///     如果userName为空，则返回所有。
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>FunctionItemWithHierarchy集合。</returns>
        IEnumerable<FunctionItemDTO> GetFunctionItemsWithHierarchy(string userName = null);

        /// <summary>
        ///     根据用户名称获取FunctionItemWithHierarchy集合。
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>FunctionItemWithHierarchy集合。</returns>
        IEnumerable<FunctionItemDTO> GetFunctionItemsByUser(string userName);
    }
}