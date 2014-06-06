#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：AirlinesAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.AirlinesQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AirlinesServices
{
    /// <summary>
    ///     实现航空公司服务接口。
    ///     用于处理航空公司相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class AirlinesAppService : ContextBoundObject, IAirlinesAppService
    {
        private readonly IAirlinesQuery _airlinesQuery;

        public AirlinesAppService(IAirlinesQuery airlinesQuery)
        {
            _airlinesQuery = airlinesQuery;
        }

        #region AirlinesDTO

        /// <summary>
        ///     获取所有航空公司
        /// </summary>
        /// <returns></returns>
        public IQueryable<AirlinesDTO> GetAirlineses()
        {
            var queryBuilder =
                new QueryBuilder<Airlines>();
            return _airlinesQuery.AirlinesDTOQuery(queryBuilder);
        }

        #endregion
    }
}