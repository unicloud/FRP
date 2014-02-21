#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：MaintainWorkQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.MaintainWorkQueries
{
    /// <summary>
    /// MaintainWork查询
    /// </summary>
    public class MaintainWorkQuery : IMaintainWorkQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public MaintainWorkQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// MaintainWork查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>MaintainWorkDTO集合</returns>
        public IQueryable<MaintainWorkDTO> MaintainWorkDTOQuery(QueryBuilder<MaintainWork> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<MaintainWork>()).Select(p => new MaintainWorkDTO
            {
                Id = p.Id,
                WorkCode = p.WorkCode,
                Description = p.Description,
            });
        }
    }
}
