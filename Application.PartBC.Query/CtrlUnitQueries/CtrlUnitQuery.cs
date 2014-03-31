#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：CtrlUnitQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.CtrlUnitAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.CtrlUnitQueries
{
    /// <summary>
    /// CtrlUnit查询
    /// </summary>
    public class CtrlUnitQuery : ICtrlUnitQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public CtrlUnitQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// CtrlUnit查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>CtrlUnitDTO集合</returns>
        public IQueryable<CtrlUnitDTO> CtrlUnitDTOQuery(QueryBuilder<CtrlUnit> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<CtrlUnit>()).Select(p => new CtrlUnitDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
            });
        }
    }
}
