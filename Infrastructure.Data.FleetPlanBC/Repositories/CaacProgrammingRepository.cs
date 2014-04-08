#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 9:48:30
// 文件名：CaacProgrammingRepository
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
using UniCloud.Domain.FleetPlanBC.Aggregates.CaacProgrammingAgg;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.Repositories
{
    /// <summary>
    ///     民航局五年规划仓储实现
    /// </summary>
    public class CaacProgrammingRepository : Repository<CaacProgramming>, ICaacProgrammingRepository
    {
        public CaacProgrammingRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        public override CaacProgramming Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<CaacProgramming>();

            return set.Include(p => p.CaacProgrammingLines).SingleOrDefault(l => l.Id == (Guid)id);
        }

        /// <summary>
        /// 删除民航局五年规划
        /// </summary>
        /// <param name="caacProgramming"></param>
        public void DeleteCaacProgramming(CaacProgramming caacProgramming)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbCaacProgrammingLines = currentUnitOfWork.CreateSet<CaacProgrammingLine>();
            var dbCaacProgrammings = currentUnitOfWork.CreateSet<CaacProgramming>();
            dbCaacProgrammingLines.RemoveRange(caacProgramming.CaacProgrammingLines);
            dbCaacProgrammings.Remove(caacProgramming);
        }

        /// <summary>
        /// 移除规划行
        /// </summary>
        /// <param name="line"></param>
        public void RemoveCaacProgrammingLine(CaacProgrammingLine line)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<CaacProgrammingLine>().Remove(line);
        }

        #endregion
    }
}
