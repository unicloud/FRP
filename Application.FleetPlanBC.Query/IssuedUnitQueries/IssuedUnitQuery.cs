#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/22，17:04
// 文件名：IssuedUnitQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.IssuedUnitAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.IssuedUnitQueries
{
    public class IssuedUnitQuery : IIssuedUnitQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public IssuedUnitQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     发文单位查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>发文单位DTO集合。</returns>
        public IQueryable<IssuedUnitDTO> IssuedUnitDTOQuery(
            QueryBuilder<IssuedUnit> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<IssuedUnit>()).Select(p => new IssuedUnitDTO
            {
                Id = p.Id,
                CnName = p.CnName,
                CnShortName = p.CnShortName,
                IsInner = p.IsInner,
            });
        }
    }
}