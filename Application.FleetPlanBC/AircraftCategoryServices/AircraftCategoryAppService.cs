#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：AircraftCategoryAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.AircraftCategoryQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AircraftCategoryServices
{
    /// <summary>
    ///     实现座级服务接口。
    ///     用于处理座级相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class AircraftCategoryAppService : ContextBoundObject, IAircraftCategoryAppService
    {
        private readonly IAircraftCategoryQuery _aircraftCategoryQuery;

        public AircraftCategoryAppService(IAircraftCategoryQuery aircraftCategoryQuery)
        {
            _aircraftCategoryQuery = aircraftCategoryQuery;
        }

        #region AircraftCategoryDTO

        /// <summary>
        ///     获取所有座级
        /// </summary>
        /// <returns></returns>
        public IQueryable<AircraftCategoryDTO> GetAircraftCategories()
        {
            var queryBuilder =
                new QueryBuilder<AircraftCategory>();
            return _aircraftCategoryQuery.AircraftCategoryDTOQuery(queryBuilder);
        }

        #endregion
    }
}