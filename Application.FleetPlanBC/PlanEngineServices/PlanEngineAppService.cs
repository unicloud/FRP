#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：PlanEngineAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.PlanEngineQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanEngineAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.PlanEngineServices
{
    /// <summary>
    ///     实现计划发动机服务接口。
    ///     用于处理计划发动机相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class PlanEngineAppService : IPlanEngineAppService
    {
        private readonly IPlanEngineQuery _planEngineQuery;

        public PlanEngineAppService(IPlanEngineQuery planEngineQuery)
        {
            _planEngineQuery = planEngineQuery;
        }

        #region PlanEngineDTO

        /// <summary>
        ///     获取所有计划发动机
        /// </summary>
        /// <returns></returns>
        public IQueryable<PlanEngineDTO> GetPlanEngines()
        {
            var queryBuilder =
                new QueryBuilder<PlanEngine>();
            return _planEngineQuery.PlanEngineDTOQuery(queryBuilder);
        }

        #endregion
    }
}
