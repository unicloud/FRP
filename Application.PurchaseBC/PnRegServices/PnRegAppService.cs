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
using UniCloud.Application.PurchaseBC.Query.PnRegQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.PnRegAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.PnRegServices
{
    /// <summary>
    ///     实现部件服务接口。
    ///     用于处理部件相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class PnRegAppService : ContextBoundObject, IPnRegAppService
    {
        private readonly IPnRegQuery _pnRegQuery;

        public PnRegAppService(IPnRegQuery pnRegQuery)
        {
            _pnRegQuery = pnRegQuery;
        }

        /// <summary>
        ///     部件查询。
        /// </summary>
        /// <returns>部件DTO集合</returns>
        public IQueryable<PnRegDTO> GetPnRegs()
        {
            var queryBuilder = new QueryBuilder<PnReg>();
            return _pnRegQuery.PnRegsQuery(queryBuilder);
        }
    }
}