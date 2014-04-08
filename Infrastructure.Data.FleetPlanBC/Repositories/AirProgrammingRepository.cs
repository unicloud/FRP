#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 9:48:18
// 文件名：AirProgrammingRepository
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
using UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.Repositories
{
    /// <summary>
    ///     航空公司五年规划仓储实现
    /// </summary>
    public class AirProgrammingRepository : Repository<AirProgramming>, IAirProgrammingRepository
    {
        public AirProgrammingRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载
        public override AirProgramming Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<AirProgramming>();

            return set.Include(p => p.AirProgrammingLines).SingleOrDefault(l => l.Id == (Guid)id);
        }

        /// <summary>
        /// 删除航空公司五年规划
        /// </summary>
        /// <param name="airProgramming"></param>
        public void DeleteAirProgramming(AirProgramming airProgramming)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbAirProgrammingLines = currentUnitOfWork.CreateSet<AirProgrammingLine>();
            var dbAirProgrammings = currentUnitOfWork.CreateSet<AirProgramming>();
            dbAirProgrammingLines.RemoveRange(airProgramming.AirProgrammingLines);
            dbAirProgrammings.Remove(airProgramming);
        }

        /// <summary>
        /// 移除规划行
        /// </summary>
        /// <param name="line"></param>
        public void RemoveAirProgrammingLine(AirProgrammingLine line)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<AirProgrammingLine>().Remove(line);
        }

        #endregion
    }
}
