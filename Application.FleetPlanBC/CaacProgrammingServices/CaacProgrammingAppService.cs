#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：CaacProgrammingAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.CaacProgrammingQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.CaacProgrammingAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.CaacProgrammingServices
{
    /// <summary>
    ///     实现民航局五年规划服务接口。
    ///     用于处理民航局五年规划相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class CaacProgrammingAppService : ICaacProgrammingAppService
    {
        private readonly ICaacProgrammingQuery _caacProgrammingQuery;

        public CaacProgrammingAppService(ICaacProgrammingQuery caacProgrammingQuery)
        {
            _caacProgrammingQuery = caacProgrammingQuery;
        }

        #region CaacProgrammingDTO

        /// <summary>
        ///     获取所有民航局五年规划
        /// </summary>
        /// <returns></returns>
        public IQueryable<CaacProgrammingDTO> GetCaacProgrammings()
        {
            var queryBuilder =
                new QueryBuilder<CaacProgramming>();
            return _caacProgrammingQuery.CaacProgrammingDTOQuery(queryBuilder);
        }

        #endregion
    }
}
