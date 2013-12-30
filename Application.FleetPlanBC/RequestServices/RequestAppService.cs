#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：RequestAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.RequestQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.RequestServices
{
    /// <summary>
    ///     实现申请服务接口。
    ///     用于处理申请相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class RequestAppService : IRequestAppService
    {
        private readonly IRequestQuery _requestQuery;

        public RequestAppService(IRequestQuery requestQuery)
        {
            _requestQuery = requestQuery;
        }

        #region RequestDTO

        /// <summary>
        ///     获取所有申请
        /// </summary>
        /// <returns></returns>
        public IQueryable<RequestDTO> GetRequests()
        {
            var queryBuilder =
                new QueryBuilder<Request>();
            return _requestQuery.RequestDTOQuery(queryBuilder);
        }

        #endregion
    }
}
