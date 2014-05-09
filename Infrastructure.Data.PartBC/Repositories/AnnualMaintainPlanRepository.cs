#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 10:51:44
// 文件名：AnnualMaintainPlanRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 10:51:44
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.PartBC.Aggregates.AnnualMaintainPlanAgg;
using System.Linq;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using System.Data.Entity;
#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    /// AnnualMaintainPlan仓储实现
    /// </summary>
    public class AnnualMaintainPlanRepository: Repository<AnnualMaintainPlan>, IAnnualMaintainPlanRepository
    {
        public AnnualMaintainPlanRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region EngineMaintainPlan
        public EngineMaintainPlan GetEngineMaintainPlan(int id)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<EngineMaintainPlan>();
            return set.Include(t => t.EngineMaintainPlanDetails).FirstOrDefault(p => p.Id == id);
        }

        public void DeleteEngineMaintainPlan(EngineMaintainPlan engineMaintainPlan)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbEngineMaintainPlanDetails = currentUnitOfWork.CreateSet<EngineMaintainPlanDetail>();
            var dbEngineMaintainPlans = currentUnitOfWork.CreateSet<EngineMaintainPlan>();
            dbEngineMaintainPlanDetails.RemoveRange(engineMaintainPlan.EngineMaintainPlanDetails);
            dbEngineMaintainPlans.Remove(engineMaintainPlan);
        }

        public void RemoveEngineMaintainPlanDetail(EngineMaintainPlanDetail engineMaintainPlanDetail)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<EngineMaintainPlanDetail>().Remove(engineMaintainPlanDetail);
        }
        #endregion

        #region AircraftMaintainPlan
        public AircraftMaintainPlan GetAircraftMaintainPlan(int id)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<AircraftMaintainPlan>();
            return set.Include(t => t.AircraftMaintainPlanDetails).FirstOrDefault(p => p.Id == id);
        }

        public void DeleteAircraftMaintainPlan(AircraftMaintainPlan aircraftMaintainPlan)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbEngineMaintainPlanDetails = currentUnitOfWork.CreateSet<AircraftMaintainPlanDetail>();
            var dbEngineMaintainPlans = currentUnitOfWork.CreateSet<AircraftMaintainPlan>();
            dbEngineMaintainPlanDetails.RemoveRange(aircraftMaintainPlan.AircraftMaintainPlanDetails);
            dbEngineMaintainPlans.Remove(aircraftMaintainPlan);
        }

        public void RemoveAircraftMaintainPlanDetail(AircraftMaintainPlanDetail aircraftMaintainPlanDetail)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<AircraftMaintainPlanDetail>().Remove(aircraftMaintainPlanDetail);
        }
        #endregion
    }
}
