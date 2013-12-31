#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：EngineAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.EngineQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.EngineServices
{
    /// <summary>
    ///     实现发动机服务接口。
    ///     用于处理发动机相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class EngineAppService : IEngineAppService
    {
        private readonly IEngineQuery _engineQuery;

        public EngineAppService(IEngineQuery engineQuery)
        {
            _engineQuery = engineQuery;
        }

        #region EngineDTO

        /// <summary>
        ///     获取所有发动机
        /// </summary>
        /// <returns></returns>
        public IQueryable<EngineDTO> GetEngines()
        {
            var queryBuilder =
                new QueryBuilder<Engine>();
            return _engineQuery.EngineDTOQuery(queryBuilder);
        }

        #endregion
    }
}
