﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：AircraftTypeAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.AircraftTypeQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AircraftTypeServices
{
    /// <summary>
    ///     实现机型服务接口。
    ///     用于处理机型相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class AircraftTypeAppService : ContextBoundObject, IAircraftTypeAppService
    {
        private readonly IAircraftTypeQuery _aircraftTypeQuery;

        public AircraftTypeAppService(IAircraftTypeQuery aircraftTypeQuery)
        {
            _aircraftTypeQuery = aircraftTypeQuery;
        }

        #region AircraftTypeDTO

        /// <summary>
        ///     获取所有机型
        /// </summary>
        /// <returns></returns>
        public IQueryable<AircraftTypeDTO> GetAircraftTypes()
        {
            var queryBuilder =
                new QueryBuilder<AircraftType>();
            return _aircraftTypeQuery.AircraftTypeDTOQuery(queryBuilder);
        }

        #endregion
    }
}