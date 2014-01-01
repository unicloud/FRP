﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：PlanAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.AircraftPlanQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AircraftPlanServices
{
    /// <summary>
    ///     实现运力增减计划服务接口。
    ///     用于处理运力增减计划相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class PlanAppService : IPlanAppService
    {
        private readonly IPlanQuery _planQuery;

        public PlanAppService(IPlanQuery planQuery)
        {
            _planQuery = planQuery;
        }

        #region PlanDTO

        /// <summary>
        ///     获取所有运力增减计划
        /// </summary>
        /// <returns></returns>
        public IQueryable<PlanDTO> GetPlans()
        {
            var queryBuilder =
                new QueryBuilder<Plan>();
            return _planQuery.PlanDTOQuery(queryBuilder);
        }

        #endregion
    }
}