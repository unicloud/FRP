#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：AnnualAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.AnnualQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AnnualServices
{
    /// <summary>
    ///     实现计划年度服务接口。
    ///     用于处理计划年度相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AnnualAppService : IAnnualAppService
    {
        private readonly IAnnualQuery _annualQuery;

        public AnnualAppService(IAnnualQuery annualQuery)
        {
            _annualQuery = annualQuery;
        }

        #region AnnualDTO

        /// <summary>
        ///     获取所有计划年度
        /// </summary>
        /// <returns></returns>
        public IQueryable<AnnualDTO> GetAnnuals()
        {
            var queryBuilder =
                new QueryBuilder<Annual>();
            return _annualQuery.AnnualDTOQuery(queryBuilder);
        }

        #endregion
    }
}
