#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：ProgrammingAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
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
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.ProgrammingQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.ProgrammingServices
{
    /// <summary>
    ///     实现规划期间服务接口。
    ///     用于处理规划期间相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class ProgrammingAppService : ContextBoundObject, IProgrammingAppService
    {
        private readonly IProgrammingQuery _programmingQuery;

        public ProgrammingAppService(IProgrammingQuery programmingQuery)
        {
            _programmingQuery = programmingQuery;
        }

        #region ProgrammingDTO

        /// <summary>
        ///     获取所有规划期间
        /// </summary>
        /// <returns></returns>
        public IQueryable<ProgrammingDTO> GetProgrammings()
        {
            var queryBuilder =
                new QueryBuilder<Programming>();
            return _programmingQuery.ProgrammingDTOQuery(queryBuilder);
        }

        #endregion
    }
}