#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 9:46:46
// 文件名：AircraftRepository
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
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.Repositories
{
    /// <summary>
    ///     实际飞机仓储实现
    /// </summary>
    public class AircraftRepository : Repository<Aircraft>, IAircraftRepository
    {
        public AircraftRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载
        public override Aircraft Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Aircraft>();

            return set.Include(p => p.AircraftBusinesses).Include(p=>p.OperationHistories).Include(p=>p.OwnershipHistories).SingleOrDefault(l => l.Id == (Guid)id);
        }

        /// <summary>
        /// 删除飞机
        /// </summary>
        /// <param name="aircraft"></param>
        public void DeleteAircraft(Aircraft aircraft)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbAircraftBusinesses = currentUnitOfWork.CreateSet<AircraftBusiness>();
            var dbOperationHistories = currentUnitOfWork.CreateSet<OperationHistory>();
            var dbOwnershipHistories = currentUnitOfWork.CreateSet<OwnershipHistory>();
            var dbAcConfigHistories = currentUnitOfWork.CreateSet<AcConfigHistory>(); 
            var dbAircrafts = currentUnitOfWork.CreateSet<Aircraft>();
            dbAircraftBusinesses.RemoveRange(aircraft.AircraftBusinesses);
            dbOperationHistories.RemoveRange(aircraft.OperationHistories);
            dbOwnershipHistories.RemoveRange(aircraft.OwnershipHistories);
            dbAcConfigHistories.RemoveRange(aircraft.AcConfigHistories);
            dbAircrafts.Remove(aircraft);
        }

        /// <summary>
        /// 移除商业数据历史
        /// </summary>
        /// <param name="ab"></param>
        public void RemoveAircraftBusiness(AircraftBusiness ab)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<AircraftBusiness>().Remove(ab);
        }

        /// <summary>
        /// 移除运营权历史
        /// </summary>
        /// <param name="oh"></param>
        public void RemoveOperationHistory(OperationHistory oh)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<OperationHistory>().Remove(oh);
        }

        /// <summary>
        /// 移除所有权历史
        /// </summary>
        /// <param name="oh"></param>
        public void RemoveOwnershipHistory(OwnershipHistory oh)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<OwnershipHistory>().Remove(oh);
        }

        /// <summary>
        /// 移除飞机配置历史
        /// </summary>
        /// <param name="ah">飞机配置历史</param>
        public void RemoveAcConfigHistory(AcConfigHistory ah)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<AcConfigHistory>().Remove(ah);
        }

        /// <summary>
        /// 获取单个运营权历史
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OperationHistory GetPh(object id)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            if (id == null) return null;
           
            var operationHistory = currentUnitOfWork.CreateSet<OperationHistory>().Find((Guid)id);
            return operationHistory;
        }

        /// <summary>
        /// 获取单个的商业数据历史
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AircraftBusiness GetAb(object id)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            if (id == null) return null;
            var aircraftBusiness = currentUnitOfWork.CreateSet<AircraftBusiness>().Find((Guid)id);
            return aircraftBusiness;
        }
        #endregion
    }
}
