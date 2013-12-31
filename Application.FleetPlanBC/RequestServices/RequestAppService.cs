#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/12，16:12
// 文件名：ContractAircraftAppService.cs
// 程序集：UniCloud.Application.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO.RequestDTO;
using UniCloud.Application.FleetPlanBC.Query.RequestQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.RequestServices
{
    public class RequestAppService : IRequestAppService
    {
        private readonly IRequestQuery _requestQuery;

        public RequestAppService(IRequestQuery requestQuery)
        {
            _requestQuery = requestQuery;
        }

        public IQueryable<RequestDTO> GetRequests()
        {
            return _requestQuery.RequestsQuery(new QueryBuilder<Request>());
        }
    }
}