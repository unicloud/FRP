#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：AirlinesQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.AirlinesQueries
{
    public class AirlinesQuery : IAirlinesQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public AirlinesQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     航空公司查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>航空公司DTO集合。</returns>
        public IQueryable<AirlinesDTO> AirlinesDTOQuery(
            QueryBuilder<Airlines> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Airlines>()).Select(p => new AirlinesDTO
            {
                Id = p.Id,
                CnName = p.CnName,
                CnShortName = p.CnShortName,
                IsCurrent = p.IsCurrent,
            });
        }
    }
}