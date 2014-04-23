#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/22，17:04
// 文件名：IssuedUnitAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.IssuedUnitQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.IssuedUnitAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.IssuedUnitServices
{
    /// <summary>
    ///     实现发文单位服务接口。
    ///     用于处理发文单位相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class IssuedUnitAppService : IIssuedUnitAppService
    {
        private readonly IIssuedUnitQuery _issuedUnitQuery;

        public IssuedUnitAppService(IIssuedUnitQuery issuedUnitQuery)
        {
            _issuedUnitQuery = issuedUnitQuery;
        }

        #region IssuedUnitDTO

        /// <summary>
        ///     获取所有发文单位
        /// </summary>
        /// <returns></returns>
        public IQueryable<IssuedUnitDTO> GetIssuedUnits()
        {
            var queryBuilder =
                new QueryBuilder<IssuedUnit>();
            return _issuedUnitQuery.IssuedUnitDTOQuery(queryBuilder);
        }

        #endregion
    }
}
