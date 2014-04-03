#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：PnRegRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Entity;
using System.Linq;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    /// PnReg仓储实现
    /// </summary>
    public class PnRegRepository : Repository<PnReg>, IPnRegRepository
    {
        public PnRegRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载
        public override PnReg Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            return currentUnitOfWork.PnRegs.Include(p => p.Dependencies).FirstOrDefault(p => p.Id == (int)id);
        }
        #endregion

        /// <summary>
        ///     删除附件
        /// </summary>
        /// <param name="pnReg"></param>
        public void DeletePnReg(PnReg pnReg)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbDependencies = currentUnitOfWork.CreateSet<Dependency>();
            var dbPnRegs = currentUnitOfWork.CreateSet<PnReg>();
            dbDependencies.RemoveRange(pnReg.Dependencies);
            dbPnRegs.Remove(pnReg);
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
