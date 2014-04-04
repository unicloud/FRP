#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：PnRegQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.PnRegQueries
{
    /// <summary>
    /// PnReg查询
    /// </summary>
    public class PnRegQuery : IPnRegQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public PnRegQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// PnReg查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>PnRegDTO集合</returns>
        public IQueryable<PnRegDTO> PnRegDTOQuery(QueryBuilder<PnReg> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<PnReg>()).Select(p => new PnRegDTO
            {
                Id = p.Id,
                Pn = p.Pn,
                IsLife = p.IsLife,
                ItemId = p.ItemId,
                Description = p.Description,
                Dependencies = p.Dependencies.Select(q => new DependencyDTO
                {
                    Id = q.Id,
                    Pn = q.Pn,
                    PnRegId = q.PnRegId,
                    DependencyPnId = q.DependencyPnId,
                }).ToList(),
            });
        }
    }
}
