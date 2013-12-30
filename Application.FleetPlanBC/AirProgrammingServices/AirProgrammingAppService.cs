#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：AirProgrammingAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.AirProgrammingQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AirProgrammingServices
{
    /// <summary>
    ///     实现航空公司五年规划服务接口。
    ///     用于处理航空公司五年规划相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AirProgrammingAppService : IAirProgrammingAppService
    {
        private readonly IAirProgrammingQuery _airProgrammingQuery;

        public AirProgrammingAppService(IAirProgrammingQuery airProgrammingQuery)
        {
            _airProgrammingQuery = airProgrammingQuery;
        }

        #region AirProgrammingDTO

        /// <summary>
        ///     获取所有航空公司五年规划
        /// </summary>
        /// <returns></returns>
        public IQueryable<AirProgrammingDTO> GetAirProgrammings()
        {
            var queryBuilder =
                new QueryBuilder<AirProgramming>();
            return _airProgrammingQuery.AirProgrammingDTOQuery(queryBuilder);
        }

        #endregion
    }
}
