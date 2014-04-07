#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/7 20:57:45
// 文件名：InstallControllerRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Data.Entity;
using System.Linq;
using UniCloud.Domain.PartBC.Aggregates.InstallControllerAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    ///     SnReg仓储实现
    /// </summary>
    public class InstallControllerRepository : Repository<InstallController>, IInstallControllerRepository
    {
        public InstallControllerRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        public override InstallController Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            return
                currentUnitOfWork.InstallControllers.Include(p => p.Dependencies).FirstOrDefault(p => p.Id == (int) id);
        }

        #endregion

        public void DeleteInstallController(InstallController installController)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            DbSet<Dependency> dbDependencies = currentUnitOfWork.CreateSet<Dependency>();
            DbSet<InstallController> dbInstallControllers = currentUnitOfWork.CreateSet<InstallController>();
            dbDependencies.RemoveRange(installController.Dependencies);
            dbInstallControllers.Remove(installController);
        }

        public void RemoveDependency(Dependency dependency)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<Dependency>().Remove(dependency);
        }
    }
}