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
            return query.ApplyTo(_unitOfWork.CreateSet<ItemMaintainCtrl>()).Select(p => new ItemMaintainCtrlDTO
            {
                Id = p.Id,
                CtrlStrategy = (int) p.CtrlStrategy,
                ItemNo = p.ItemNo,
                ItemId = p.ItemId,
                MaintainCtrlLines = p.MaintainCtrlLines.Select(q => new MaintainCtrlLineDTO
                {
                    Id = q.Id,
                    MaintainCtrlId = q.MaintainCtrlId,
                    CtrlUnitId = q.CtrlUnitId,
                    CtrlUnitName = q.CtrlUnit.Name,
                    MaintainWorkId = q.MaintainWorkId,
                    MaxInterval = q.MaxInterval,
                    MinInterval = q.MinInterval,
                    StandardInterval = q.StandardInterval,
                    WorkCode = q.MaintainWork.WorkCode,
                }).ToList(),
            });
        }

        /// <summary>
        ///     PnMaintainCtrl查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>PnMaintainCtrlDTO集合</returns>
        public IQueryable<PnMaintainCtrlDTO> PnMaintainCtrlDTOQuery(QueryBuilder<PnMaintainCtrl> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<PnMaintainCtrl>()).Select(p => new PnMaintainCtrlDTO
            {
                Id = p.Id,
                CtrlStrategy = (int) p.CtrlStrategy,
                Pn = p.Pn,
                PnRegId = p.PnRegId,
                MaintainCtrlLines = p.MaintainCtrlLines.Select(q => new MaintainCtrlLineDTO
                {
                    Id = q.Id,
                    MaintainCtrlId = q.MaintainCtrlId,
                    CtrlUnitId = q.CtrlUnitId,
                    CtrlUnitName = q.CtrlUnit.Name,
                    MaintainWorkId = q.MaintainWorkId,
                    MaxInterval = q.MaxInterval,
                    MinInterval = q.MinInterval,
                    StandardInterval = q.StandardInterval,
                    WorkCode = q.MaintainWork.WorkCode,
                }).ToList(),
            });
        }

        /// <summary>
        ///     SnMaintainCtrl查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>SnMaintainCtrlDTO集合</returns>
        public IQueryable<SnMaintainCtrlDTO> SnMaintainCtrlDTOQuery(QueryBuilder<SnMaintainCtrl> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<SnMaintainCtrl>()).Select(p => new SnMaintainCtrlDTO
            {
                Id = p.Id,
                CtrlStrategy = (int) p.CtrlStrategy,
                SnScope = p.SnScope,
                MaintainCtrlLines = p.MaintainCtrlLines.Select(q => new MaintainCtrlLineDTO
                {
                    Id = q.Id,
                    MaintainCtrlId = q.MaintainCtrlId,
                    CtrlUnitId = q.CtrlUnitId,
                    CtrlUnitName = q.CtrlUnit.Name,
                    MaintainWorkId = q.MaintainWorkId,
                    MaxInterval = q.MaxInterval,
                    MinInterval = q.MinInterval,
                    StandardInterval = q.StandardInterval,
                    WorkCode = q.MaintainWork.WorkCode,
                }).ToList(),
            });
        }
    }
}