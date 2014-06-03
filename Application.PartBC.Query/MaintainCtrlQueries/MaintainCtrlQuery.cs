#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：MaintainCtrlQuery
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
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PartBC.Query.MaintainCtrlQueries
{
    /// <summary>
    ///     MaintainCtrl查询
    /// </summary>
    public class MaintainCtrlQuery : IMaintainCtrlQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public MaintainCtrlQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     ItemMaintainCtrl查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>ItemMaintainCtrlDTO集合</returns>
        public IQueryable<ItemMaintainCtrlDTO> ItemMaintainCtrlDTOQuery(QueryBuilder<ItemMaintainCtrl> query)
        {
            var maintainWorks = _unitOfWork.CreateSet<MaintainWork>();
            return query.ApplyTo(_unitOfWork.CreateSet<ItemMaintainCtrl>()).Select(p => new ItemMaintainCtrlDTO
            {
                Id = p.Id,
                CtrlStrategy = (int) p.CtrlStrategy,
                ItemNo = p.ItemNo,
                ItemId = p.ItemId,
                CtrlDetail = p.CtrlDetail,
                Description = p.Description,
                MaintainWorkId = p.MaintainWorkId,
                WorkCode = maintainWorks.FirstOrDefault(l=>l.Id==p.MaintainWorkId).WorkCode,
            });
        }

        /// <summary>
        ///     PnMaintainCtrl查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>PnMaintainCtrlDTO集合</returns>
        public IQueryable<PnMaintainCtrlDTO> PnMaintainCtrlDTOQuery(QueryBuilder<PnMaintainCtrl> query)
        {
            var maintainWorks = _unitOfWork.CreateSet<MaintainWork>();
            return query.ApplyTo(_unitOfWork.CreateSet<PnMaintainCtrl>()).Select(p => new PnMaintainCtrlDTO
            {
                Id = p.Id,
                CtrlStrategy = (int) p.CtrlStrategy,
                Pn = p.Pn,
                PnRegId = p.PnRegId,
                CtrlDetail = p.CtrlDetail,
                Description = p.Description,
                MaintainWorkId = p.MaintainWorkId,
                WorkCode = maintainWorks.FirstOrDefault(l => l.Id == p.MaintainWorkId).WorkCode,
            });
        }

        /// <summary>
        ///     SnMaintainCtrl查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>SnMaintainCtrlDTO集合</returns>
        public IQueryable<SnMaintainCtrlDTO> SnMaintainCtrlDTOQuery(QueryBuilder<SnMaintainCtrl> query)
        {
            var maintainWorks = _unitOfWork.CreateSet<MaintainWork>();
            return query.ApplyTo(_unitOfWork.CreateSet<SnMaintainCtrl>()).Select(p => new SnMaintainCtrlDTO
            {
                Id = p.Id,
                CtrlStrategy = (int) p.CtrlStrategy,
                SnScope = p.SnScope,
                CtrlDetail = p.CtrlDetail,
                Description = p.Description,
                MaintainWorkId = p.MaintainWorkId,
                WorkCode = maintainWorks.FirstOrDefault(l => l.Id == p.MaintainWorkId).WorkCode,
            });
        }
    }
}