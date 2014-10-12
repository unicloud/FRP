#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/01/04，11:01
// 文件名：AircraftSeriesAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.AircraftSeriesQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftSeriesAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AircraftSeriesServices
{
    /// <summary>
    ///     实现飞机系列服务接口。
    ///     用于处理飞机系列相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AircraftSeriesAppService : IAircraftSeriesAppService
    {
        private readonly IAircraftSeriesQuery _actionCategoryQuery;

        public AircraftSeriesAppService(IAircraftSeriesQuery actionCategoryQuery)
        {
            _actionCategoryQuery = actionCategoryQuery;
        }

        #region AircraftSeriesDTO

        /// <summary>
        ///     获取所有飞机系列
        /// </summary>
        /// <returns></returns>
        public IQueryable<AircraftSeriesDTO> GetAircraftSeries()
        {
            var queryBuilder =
                new QueryBuilder<AircraftSeries>();
            return _actionCategoryQuery.AircraftSeriesDTOQuery(queryBuilder);
        }

        #endregion
    }
}
