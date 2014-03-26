#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：EngineTypeAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.EngineTypeQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.EngineTypeServices
{
    /// <summary>
    ///     实现发动机型号服务接口。
    ///     用于处理发动机型号相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class EngineTypeAppService : ContextBoundObject, IEngineTypeAppService
    {
        private readonly IEngineTypeQuery _engineTypeQuery;

        public EngineTypeAppService(IEngineTypeQuery engineTypeQuery)
        {
            _engineTypeQuery = engineTypeQuery;
        }

        #region EngineTypeDTO

        /// <summary>
        ///     获取所有发动机型号
        /// </summary>
        /// <returns></returns>
        public IQueryable<EngineTypeDTO> GetEngineTypes()
        {
            var queryBuilder =
                new QueryBuilder<EngineType>();
            return _engineTypeQuery.EngineTypeDTOQuery(queryBuilder);
        }

        #endregion
    }
}
