#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：ApprovalDocAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.ApprovalDocQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.ApprovalDocServices
{
    /// <summary>
    ///     实现批文服务接口。
    ///     用于处理批文相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class ApprovalDocAppService : IApprovalDocAppService
    {
        private readonly IApprovalDocQuery _approvalDocQuery;

        public ApprovalDocAppService(IApprovalDocQuery approvalDocQuery)
        {
            _approvalDocQuery = approvalDocQuery;
        }

        #region ApprovalDocDTO

        /// <summary>
        ///     获取所有批文
        /// </summary>
        /// <returns></returns>
        public IQueryable<ApprovalDocDTO> GetApprovalDocs()
        {
            var queryBuilder =
                new QueryBuilder<ApprovalDoc>();
            return _approvalDocQuery.ApprovalDocDTOQuery(queryBuilder);
        }

        #endregion
    }
}
