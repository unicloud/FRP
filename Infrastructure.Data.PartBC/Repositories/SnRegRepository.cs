#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SnRegRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Data.Entity;
using System.Linq;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    ///     SnReg仓储实现
    /// </summary>
    public class SnRegRepository : Repository<SnReg>, ISnRegRepository
    {
        public SnRegRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载
        public override SnReg Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            return currentUnitOfWork.SnRegs.Include(p => p.SnHistories).Include(p=>p.LifeMonitors).FirstOrDefault(p => p.Id == (int)id);
        }
        #endregion

        /// <summary>
        ///     删除序号件
        /// </summary>
        /// <param name="snReg"></param>
        public void DeleteSnReg(SnReg snReg)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbSnHistories = currentUnitOfWork.CreateSet<SnHistory>();
            var dbLifeMonitors = currentUnitOfWork.CreateSet<LifeMonitor>();
            var dbSnRegs = currentUnitOfWork.CreateSet<SnReg>();
            dbSnHistories.RemoveRange(snReg.SnHistories);
            dbLifeMonitors.RemoveRange(snReg.LifeMonitors);
            dbSnRegs.Remove(snReg);
        }
        
        /// <summary>
        ///     移除装机历史
        /// </summary>
        /// <param name="snHistory">装机历史</param>
        public void RemoveSnHistory(SnHistory snHistory)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<SnHistory>().Remove(snHistory);
        }

        /// <summary>
        ///     移除到寿监控
        /// </summary>
        /// <param name="lifeMonitor">到寿监控</param>
        public void RemoveLifeMonitor(LifeMonitor lifeMonitor)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<LifeMonitor>().Remove(lifeMonitor);
        }
    }
}