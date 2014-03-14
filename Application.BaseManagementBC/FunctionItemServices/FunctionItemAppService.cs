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

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.Query.FunctionItemQueries;
using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;

namespace UniCloud.Application.BaseManagementBC.FunctionItemServices
{
    /// <summary>
    /// 实现FunctionItem的服务接口。
    ///  用于处理FunctionItem相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class FunctionItemAppService : IFunctionItemAppService
    {
        private readonly IFunctionItemQuery _functionItemQuery;
        private readonly IFunctionItemRepository _functionItemRepository;

        public FunctionItemAppService(IFunctionItemQuery functionItemQuery, IFunctionItemRepository functionItemRepository)
        {
            _functionItemQuery = functionItemQuery;
            _functionItemRepository = functionItemRepository;
        }


        /// <summary>
        /// 获取所有FunctionItem。
        /// </summary>
        public IQueryable<FunctionItemDTO> GetFunctionItems()
        {
            var queryBuilder =
               new QueryBuilder<FunctionItem>();
            return _functionItemQuery.FunctionItemsQuery(queryBuilder);
        }

        /// <summary>
        /// 获取所有FunctionItemWithHierarchy
        /// </summary>
        /// <returns>所有的FunctionItemWithHierarchy。</returns>
        public IEnumerable<FunctionItemDTO> GetFunctionItemsWithHierarchy()
        {
            return _functionItemQuery.GetFunctionItemsWithHierarchy();
        }
    }
}
