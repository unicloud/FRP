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

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.InstallControllerAgg;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
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
            var items = _unitOfWork.CreateSet<Item>();
            return query.ApplyTo(_unitOfWork.CreateSet<PnReg>()).Select(p => new PnRegDTO
            {
                Id = p.Id,
                Pn = p.Pn,
                IsLife = p.IsLife,
                ItemId = p.ItemId,
                ItemNo = items.FirstOrDefault(l => l.Id == p.ItemId).ItemNo,
                Description = p.Description,
            });
        }

        /// <summary>
        /// 获取某个项下所带的附件集合（去重）
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public List<PnRegDTO> GetPnRegsByItem(int itemId)
        {
            var result = new List<PnRegDTO>();
            var installControllers = _unitOfWork.CreateSet<InstallController>().Where(p => p.ItemId == itemId).ToList();
            var pnRegs = _unitOfWork.CreateSet<PnReg>().ToList();
            installControllers.ForEach(p =>
            {
                if (result.All(l => l.Id != p.PnRegId))
                {
                    var pnReg = pnRegs.FirstOrDefault(l => l.Id == p.PnRegId);
                    if (pnReg != null)
                    {
                        var dbItem = _unitOfWork.CreateSet<Item>().ToList().FirstOrDefault(l => l.Id == pnReg.ItemId);

                        var pn = new PnRegDTO
                        {
                            Id = pnReg.Id,
                            Description = pnReg.Description,
                            IsLife = pnReg.IsLife,
                            Pn = pnReg.Pn,
                            ItemId = pnReg.ItemId,
                        };
                        if (dbItem != null) pn.ItemNo = dbItem.ItemNo;
                        result.Add(pn);
                    }
                }
            });
            return result;
        }
    }
}
