#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ScnRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Domain.PartBC.Aggregates.ScnAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using System.Data.Entity;
#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    /// Scn仓储实现
    /// </summary>
    public class ScnRepository : Repository<Scn>, IScnRepository
    {
        public ScnRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载
        public override Scn Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Scn>();
            return set.Include(t => t.ApplicableAircrafts).FirstOrDefault(p => p.Id == (int)id);
        }
        #endregion


        /// <summary>
        /// 删除SCN
        /// </summary>
        /// <param name="scn"></param>
        public void DeleteScn(Scn scn)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbApplicableAircrafts = currentUnitOfWork.CreateSet<ApplicableAircraft>();
            var dbScns = currentUnitOfWork.CreateSet<Scn>();
            dbApplicableAircrafts.RemoveRange(scn.ApplicableAircrafts);
            dbScns.Remove(scn);
        }


        /// <summary>
        ///     移除适用飞机
        /// </summary>
        /// <param name="applicableAircraft">适用飞机</param>
        public void RemoveApplicableAircraft(ApplicableAircraft applicableAircraft)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<ApplicableAircraft>().Remove(applicableAircraft);
        }
    }
}
