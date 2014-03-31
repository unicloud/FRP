#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 9:49:18
// 文件名：EnginePlanRepository
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
using UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.Repositories
{
    /// <summary>
    ///     备发计划仓储实现
    /// </summary>
    public class EnginePlanRepository : Repository<EnginePlan>, IEnginePlanRepository
    {
        public EnginePlanRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        public override EnginePlan Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<EnginePlan>();

            return set.Include(p => p.EnginePlanHistories).SingleOrDefault(l => l.Id == (Guid)id);
        }

        /// <summary>
        /// 删除备发计划
        /// </summary>
        /// <param name="enginePlan"></param>
        public void DeleteEnginePlan(EnginePlan enginePlan)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbEnginePlanHistores = currentUnitOfWork.CreateSet<EnginePlanHistory>();
            var dbEnginePlans = currentUnitOfWork.CreateSet<EnginePlan>();
            dbEnginePlanHistores.RemoveRange(enginePlan.EnginePlanHistories);
            dbEnginePlans.Remove(enginePlan);
        }

        /// <summary>
        /// 移除备发计划明细    
        /// </summary>
        /// <param name="eph"></param>
        public void RemoveEnginePlanHistory(EnginePlanHistory eph)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<EnginePlanHistory>().Remove(eph);
        }
        #endregion
    }
}
