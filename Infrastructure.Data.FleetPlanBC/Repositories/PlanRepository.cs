#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 9:47:40
// 文件名：PlanRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using System.Data.Entity;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.Repositories
{
    /// <summary>
    ///     运力增减计划仓储实现
    /// </summary>
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        public PlanRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        public override Plan Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Plan>();

            return set.Include(p => p.PlanHistories).SingleOrDefault(l => l.Id == (Guid)id);
        }

        /// <summary>
        ///     移除计划
        /// </summary>
        /// <param name="plan">计划</param>
        public void DeletePlan(Plan plan)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbPlanHistories = currentUnitOfWork.CreateSet<PlanHistory>();
            var dbPlans = currentUnitOfWork.CreateSet<Plan>();
            dbPlanHistories.RemoveRange(plan.PlanHistories);
            dbPlans.Remove(plan);
        }

        /// <summary>
        /// 移除计划明细
        /// </summary>
        /// <param name="planHistory"></param>
        public void RemovePlanHistory(PlanHistory planHistory)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<PlanHistory>().Remove(planHistory);
        }
        #endregion
    }
}
