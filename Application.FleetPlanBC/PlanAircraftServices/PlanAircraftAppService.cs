#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：PlanAircraftAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.PlanAircraftQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.PlanAircraftServices
{
    /// <summary>
    ///     实现计划飞机服务接口。
    ///     用于处理计划飞机相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class PlanAircraftAppService : IPlanAircraftAppService
    {
        private readonly IPlanAircraftQuery _planAircraftQuery;

        public PlanAircraftAppService(IPlanAircraftQuery planAircraftQuery)
        {
            _planAircraftQuery = planAircraftQuery;
        }

        #region PlanAircraftDTO

        /// <summary>
        ///     获取所有计划飞机
        /// </summary>
        /// <returns></returns>
        public IQueryable<PlanAircraftDTO> GetPlanAircrafts()
        {
            var queryBuilder =
                new QueryBuilder<PlanAircraft>();
            return _planAircraftQuery.PlanAircraftDTOQuery(queryBuilder);
        }

        #endregion
    }
}
