#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/10 22:07:04
// 文件名：RelatedDocQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.RelatedDocAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.RelatedDocQueries
{
    /// <summary>
    ///     实现关联文档查询接口
    /// </summary>
    public class RelatedDocQuery : IRelatedDocQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public RelatedDocQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     关联文档查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>关联文档DTO集合。</returns>
        public IQueryable<RelatedDocDTO> RelatedDocDTOQuery(
            QueryBuilder<RelatedDoc> query)
        {
            return
                query.ApplyTo(_unitOfWork.CreateSet<RelatedDoc>())
                    .Select(p => new RelatedDocDTO
                    {
                        Id = p.Id,
                        SourceId = p.SourceId,
                        DocumentId = p.DocumentId,
                        DocumentName = p.DocumentName,
                    });
        }
    }
}