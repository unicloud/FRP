#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，16:37
// 方案：FRP
// 项目：Infrastructure.Data.ProjectBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Domain.ProjectBC.Aggregates.WorkGroupAgg;
using UniCloud.Infrastructure.Data.ProjectBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.ProjectBC.Repositories
{
    /// <summary>
    ///     工作组仓储实现
    /// </summary>
    public class WorkGroupRepository : Repository<WorkGroup>, IWorkGroupRepository
    {
        public WorkGroupRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion

        #region IWorkGroupRepository 成员

        public void RemoveMember(Member member)
        {
            var currentUnitOfWork = UnitOfWork as ProjectBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<Member>().Remove(member);
        }

        #endregion
    }
}