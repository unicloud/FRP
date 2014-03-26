#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，10:11
// 文件名：MaterialAppService.cs
// 程序集：UniCloud.Application.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.PartQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.PartAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.PartServices
{
    /// <summary>
    ///     实现部件服务接口。
    ///     用于处理部件相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class PartAppService : ContextBoundObject, IPartAppService
    {
        private readonly IPartQuery _partQuery;

        public PartAppService(IPartQuery partQuery)
        {
            _partQuery = partQuery;
        }

        /// <summary>
        ///     部件查询。
        /// </summary>
        /// <returns>部件DTO集合</returns>
        public IQueryable<PartDTO> GetParts()
        {
            var queryBuilder =
                new QueryBuilder<Part>();
            return _partQuery.PartsQuery(queryBuilder);
        }
    }
}