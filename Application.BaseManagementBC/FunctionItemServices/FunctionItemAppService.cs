#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/13 17:38:40
// 文件名：FunctionItemAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/13 17:38:40
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.Query.FunctionItemQueries;
using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;

#endregion

namespace UniCloud.Application.BaseManagementBC.FunctionItemServices
{
    /// <summary>
    ///     实现FunctionItem的服务接口。
    ///     用于处理FunctionItem相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class FunctionItemAppService : ContextBoundObject, IFunctionItemAppService
    {
        private readonly IFunctionItemQuery _functionItemQuery;

        public FunctionItemAppService(IFunctionItemQuery functionItemQuery)
        {
            _functionItemQuery = functionItemQuery;
        }


        /// <summary>
        ///     获取所有FunctionItem。
        /// </summary>
        public IQueryable<FunctionItemDTO> GetFunctionItems()
        {
            var queryBuilder = new QueryBuilder<FunctionItem>();
            return _functionItemQuery.FunctionItemsQuery(queryBuilder);
        }

        /// <summary>
        ///     获取FunctionItemWithHierarchy集合。
        ///     如果userName为空，则返回所有。
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>FunctionItemWithHierarchy集合。</returns>
        public IEnumerable<FunctionItemDTO> GetFunctionItemsWithHierarchy(string userName = null)
        {
            return _functionItemQuery.GetFunctionItemsWithHierarchy(userName);
        }

        /// <summary>
        ///     <see cref="UniCloud.Application.BaseManagementBC.FunctionItemServices.IFunctionItemAppService" />
        /// </summary>
        /// <param name="userName">
        ///     <see cref="UniCloud.Application.BaseManagementBC.FunctionItemServices.IFunctionItemAppService" />
        /// </param>
        /// <returns>
        ///     <see cref="UniCloud.Application.BaseManagementBC.FunctionItemServices.IFunctionItemAppService" />
        /// </returns>
        public IEnumerable<FunctionItemDTO> GetFunctionItemsByUser(string userName)
        {
            return _functionItemQuery.GetFunctionItemsByUser(userName);
        }
    }
}