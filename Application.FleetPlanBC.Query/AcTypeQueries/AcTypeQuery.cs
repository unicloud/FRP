#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/01/04，11:01
// 文件名：AcTypeQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.AcTypeAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.AcTypeQueries
{
    public class AcTypeQuery : IAcTypeQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public AcTypeQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     飞机系列查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>飞机系列DTO集合。</returns>
        public IQueryable<AcTypeDTO> AcTypeDTOQuery(
            QueryBuilder<AcType> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AcType>()).Select(p => new AcTypeDTO
            {
                Id = p.Id,
                Name = p.Name,
            });

        }
    }
}