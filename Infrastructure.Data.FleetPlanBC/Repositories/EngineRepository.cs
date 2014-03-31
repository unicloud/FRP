#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 9:49:05
// 文件名：EngineRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Entity;
using System.Linq;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.Repositories
{
    /// <summary>
    ///     发动机仓储实现
    /// </summary>
    public class EngineRepository : Repository<Engine>, IEngineRepository
    {
        public EngineRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载
        public override Engine Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Engine>();

            return set.Include(p => p.EngineBusinessHistories).Include(p => p.EngineOwnerShipHistories).SingleOrDefault(l => l.Id == (Guid)id);
        }

        /// <summary>
        /// 删除发动机
        /// </summary>
        /// <param name="engine"></param>
        public void DeleteEngine(Engine engine)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbEngineBusinessesHistories = currentUnitOfWork.CreateSet<EngineBusinessHistory>();
            var dbEngineOwnershipHistories = currentUnitOfWork.CreateSet<EngineOwnershipHistory>();
            var dbEngines = currentUnitOfWork.CreateSet<Engine>();
            dbEngineBusinessesHistories.RemoveRange(engine.EngineBusinessHistories);
            dbEngineOwnershipHistories.RemoveRange(engine.EngineOwnerShipHistories);
            dbEngines.Remove(engine);
        }

        /// <summary>
        /// 移除商业数据历史
        /// </summary>
        /// <param name="ebh"></param>
        public void RemoveEngineBusinessHistory(EngineBusinessHistory ebh)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<EngineBusinessHistory>().Remove(ebh);
        }

        /// <summary>
        /// 移除所有权历史
        /// </summary>
        /// <param name="eoh"></param>
        public void RemoveEngineOwnershipHistory(EngineOwnershipHistory eoh)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<EngineOwnershipHistory>().Remove(eoh);
        }
        #endregion
    }
}
