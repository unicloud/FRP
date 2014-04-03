#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：EngineTypeQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.EngineTypeQueries
{
    public class EngineTypeQuery : IEngineTypeQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public EngineTypeQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     发动机类型查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>发动机类型DTO集合。</returns>
        public IQueryable<EngineTypeDTO> EngineTypeDTOQuery(
            QueryBuilder<EngineType> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<EngineType>()).Select(p => new EngineTypeDTO
            {
                Id = p.Id,
                ManufacturerId = p.ManufacturerId,
                Name = p.Name,
            });
        }
    }
}