#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/03/13，15:03
// 文件名：AircraftConfigurationAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.AircraftConfigurationQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftConfigurationAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AircraftConfigurationServices
{
    /// <summary>
    ///     实现飞机配置服务接口。
    ///     用于处理飞机配置相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class AircraftConfigurationAppService : ContextBoundObject, IAircraftConfigurationAppService
    {
        private readonly IAircraftConfigurationQuery _aircraftConfigurationQuery;

        public AircraftConfigurationAppService(IAircraftConfigurationQuery aircraftConfigurationQuery)
        {
            _aircraftConfigurationQuery = aircraftConfigurationQuery;
        }

        #region AircraftConfigurationDTO

        /// <summary>
        ///     获取所有飞机配置
        /// </summary>
        /// <returns></returns>
        public IQueryable<AircraftConfigurationDTO> GetAircraftConfigurations()
        {
            var queryBuilder =
                new QueryBuilder<AircraftConfiguration>();
            return _aircraftConfigurationQuery.AircraftConfigurationDTOQuery(queryBuilder);
        }

        #endregion
    }
}
