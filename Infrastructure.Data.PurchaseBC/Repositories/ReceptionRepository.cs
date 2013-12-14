#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，16:11
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using System.Data.Entity;
using UniCloud.Domain;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Repositories
{
    /// <summary>
    ///     接收仓储实现
    /// </summary>
    public class ReceptionRepository : Repository<Reception>, IReceptionRepository
    {
        public ReceptionRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载

        public override Reception Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as PurchaseBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Reception>();

            return set.Include(p => p.ReceptionLines).Include(q => q.ReceptionSchedules).SingleOrDefault(l => l.Id == (int)id);
        }

        public override IQueryable<Reception> GetAll()
        {
            var currentUnitOfWork = UnitOfWork as PurchaseBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Reception>();

            return set.Include(p => p.ReceptionLines);
        }

        public void DeleteReception(Reception reception)
        {
            var currentUnitOfWork = UnitOfWork as PurchaseBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbReceptionLines = currentUnitOfWork.CreateSet<ReceptionLine>();
            var dbReceptionSchedules = currentUnitOfWork.CreateSet<ReceptionSchedule>();
            var dbReceptions = currentUnitOfWork.CreateSet<Reception>();
            dbReceptionLines.RemoveRange(reception.ReceptionLines);
            dbReceptionSchedules.RemoveRange(reception.ReceptionSchedules);
            dbReceptions.Remove(reception);
        }
        #endregion

    }
}