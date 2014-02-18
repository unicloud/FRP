#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SnRegQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.SnRegQueries
{
    /// <summary>
    /// SnReg查询
    /// </summary>
    public class SnRegQuery : ISnRegQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public SnRegQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// SnReg查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>SnRegDTO集合</returns>
        public IQueryable<SnRegDTO> SnRegDTOQuery(QueryBuilder<SnReg> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<SnReg>()).Select(p => new SnRegDTO
            {
                Id = p.Id,
            });
        }
    }
}
