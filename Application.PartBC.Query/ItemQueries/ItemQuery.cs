#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/03，09:04
// 文件名：ItemQuery.cs
// 程序集：UniCloud.Application.PartBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PartBC.Query.ItemQueries
{
    public class ItemQuery : IItemQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public ItemQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     附件项查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>附件项DTO集合。</returns>
        public IQueryable<ItemDTO> ItemDTOQuery(
            QueryBuilder<Item> query)
        {
            var itemMaintainCtrls = _unitOfWork.CreateSet<ItemMaintainCtrl>();
            var pnRegs = _unitOfWork.CreateSet<PnReg>();
            var pnMaintainCtrls = _unitOfWork.CreateSet<PnMaintainCtrl>();
            return query.ApplyTo(_unitOfWork.CreateSet<Item>()).Select(p => new ItemDTO
            {
                Id=p.Id,
                Name = p.Name,
                FiNumber = p.FiNumber,
                ItemNo = p.ItemNo,
                Description = p.Description,
                ItemMaintainCtrl = itemMaintainCtrls.Where(q => q.ItemId == p.Id).Select(r => new 
                ItemMaintainCtrlDTO
                {
                    Id = r.Id,
                    CtrlStrategy = (int)r.CtrlStrategy,
                    ItemId = r.ItemId,
                    ItemNo = r.ItemNo,
                    MaintainCtrlLines = r.MaintainCtrlLines.Select(l=>new MaintainCtrlLineDTO
                    {
                        Id = l.Id,
                        CtrlUnitId = l.CtrlUnitId,
                        CtrlUnitName = l.CtrlUnit.Name,
                        MaintainCtrlId = l.MaintainCtrlId,
                        MaintainWorkId = l.MaintainWorkId,
                        MaxInterval = l.MaxInterval,
                        MinInterval = l.MinInterval,
                        StandardInterval = l.StandardInterval,
                        WorkCode = l.MaintainWork.WorkCode,
                    }).ToList(),
                }).FirstOrDefault(),
                PnRegs = pnRegs.Where(q=>q.ItemId==p.Id).Select(r=>new PnRegDTO
                {
                    Id = r.Id,
                    Pn = r.Pn,
                    IsLife = r.IsLife,
                    ItemId = r.ItemId,
                    PnMaintainCtrl = pnMaintainCtrls.Where(q => q.PnRegId == r.Id).Select(t => new PnMaintainCtrlDTO
                    {
                        Id=t.Id,
                        CtrlStrategy = (int)t.CtrlStrategy,
                        Pn = t.Pn,
                        PnRegId = t.PnRegId,
                        MaintainCtrlLines = t.MaintainCtrlLines.Select(l => new MaintainCtrlLineDTO
                        {
                            Id = l.Id,
                            CtrlUnitId = l.CtrlUnitId,
                            CtrlUnitName = l.CtrlUnit.Name,
                            MaintainCtrlId = l.MaintainCtrlId,
                            MaintainWorkId = l.MaintainWorkId,
                            MaxInterval = l.MaxInterval,
                            MinInterval = l.MinInterval,
                            StandardInterval = l.StandardInterval,
                            WorkCode = l.MaintainWork.WorkCode,
                        }).ToList(),
                    }).FirstOrDefault(),
                    Dependencies = r.Dependencies.Select(q=>new DependencyDTO
                    {
                        Id = q.Id,
                        Pn = q.Pn,
                        PnRegId = q.PnRegId,
                        DependencyPnId = q.DependencyPnId,
                    }).ToList(),
                }).ToList(),
            });
        }
    }
}