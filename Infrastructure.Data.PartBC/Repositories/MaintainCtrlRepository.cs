#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:16:58

// 文件名：MaintainCtrlRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Entity;
using System.Linq;
using UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    /// MaintainCtrl仓储实现
    /// </summary>
    public class MaintainCtrlRepository : Repository<MaintainCtrl>, IMaintainCtrlRepository
    {
        public MaintainCtrlRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载

        public override MaintainCtrl Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<ItemMaintainCtrl>();

            return set.Include(p => p.MaintainCtrlLines).SingleOrDefault(l => l.Id == (int)id);
        }
        #endregion

        /// <summary>
        /// 删除项控制组
        /// </summary>
        /// <param name="itemMaintainCtrl">项控制组</param>
        public void DeleteItemMaintainCtrl(ItemMaintainCtrl itemMaintainCtrl)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbMaintainCtrlLines = currentUnitOfWork.CreateSet<MaintainCtrlLine>();
            var dbItemMaintainCtrl = currentUnitOfWork.CreateSet<ItemMaintainCtrl>();
            dbMaintainCtrlLines.RemoveRange(itemMaintainCtrl.MaintainCtrlLines);
            dbItemMaintainCtrl.Remove(itemMaintainCtrl);
        }

        /// <summary>
        /// 删除附件控制组
        /// </summary>
        /// <param name="pnMaintainCtrl">附件控制组</param>
        public void DeletePnMaintainCtrl(PnMaintainCtrl pnMaintainCtrl)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbMaintainCtrlLines = currentUnitOfWork.CreateSet<MaintainCtrlLine>();
            var dbPnMaintainCtrl = currentUnitOfWork.CreateSet<PnMaintainCtrl>();
            dbMaintainCtrlLines.RemoveRange(pnMaintainCtrl.MaintainCtrlLines);
            dbPnMaintainCtrl.Remove(pnMaintainCtrl);
        }

        /// <summary>
        /// 删除序号控制组
        /// </summary>
        /// <param name="snMaintainCtrl">序号控制组</param>
        public void DeleteSnMaintainCtrl(SnMaintainCtrl snMaintainCtrl)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbMaintainCtrlLines = currentUnitOfWork.CreateSet<MaintainCtrlLine>();
            var dbSnMaintainCtrl = currentUnitOfWork.CreateSet<SnMaintainCtrl>();
            dbMaintainCtrlLines.RemoveRange(snMaintainCtrl.MaintainCtrlLines);
            dbSnMaintainCtrl.Remove(snMaintainCtrl);
        }

        /// <summary>
        ///     移除维修控制明细
        /// </summary>
        /// <param name="maintainCtrlLine">维修控制明细</param>
        public void RemoveMaintainCtrlLine(MaintainCtrlLine maintainCtrlLine)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<MaintainCtrlLine>().Remove(maintainCtrlLine);
        }
    }
}
