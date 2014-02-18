#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：TechnicalSolutionQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.TechnicalSolutionQueries
{
    /// <summary>
    /// TechnicalSolution查询
    /// </summary>
    public class TechnicalSolutionQuery : ITechnicalSolutionQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public TechnicalSolutionQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// TechnicalSolution查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>TechnicalSolutionDTO集合</returns>
        public IQueryable<TechnicalSolutionDTO> TechnicalSolutionDTOQuery(QueryBuilder<TechnicalSolution> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<TechnicalSolution>()).Select(p => new TechnicalSolutionDTO
            {
                Id = p.Id,
            });
        }
    }
}
