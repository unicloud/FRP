#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：TechnicalSolutionRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Entity;
using System.Linq;
using UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    /// TechnicalSolution仓储实现
    /// </summary>
    public class TechnicalSolutionRepository : Repository<TechnicalSolution>, ITechnicalSolutionRepository
    {
        public TechnicalSolutionRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载
        public override TechnicalSolution Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            return currentUnitOfWork.TechnicalSolutions.Include(p => p.TsLines).FirstOrDefault(p => p.Id == (int)id);
        }

        #endregion


        /// <summary>
        /// 删除解决方案
        /// </summary>
        /// <param name="ts"></param>
        public void DeleteTechnicalSolution(TechnicalSolution ts)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbTechnicalSolutions = currentUnitOfWork.CreateSet<TechnicalSolution>();
            ts.TsLines.ToList().ForEach(DeleteTsLine);
            dbTechnicalSolutions.Remove(ts);
        }

        /// <summary>
        /// 删除解决方案明细
        /// </summary>
        /// <param name="tsLine"></param>
        public void DeleteTsLine(TsLine tsLine)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbDependencies = currentUnitOfWork.CreateSet<Dependency>();
            var dbTsLines = currentUnitOfWork.CreateSet<TsLine>();
            dbDependencies.RemoveRange(tsLine.Dependencies);
            dbTsLines.Remove(tsLine);
        }

        /// <summary>
        ///     移除依赖项
        /// </summary>
        /// <param name="dependency">依赖项</param>
        public void RemoveDependency(Dependency dependency)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<Dependency>().Remove(dependency);
        }
    }
}
