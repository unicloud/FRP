#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 9:57:44
// 文件名：RequestRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using System.Data.Entity;
#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.Repositories
{
    /// <summary>
    ///     申请仓储实现
    /// </summary>
    public class RequestRepository : Repository<Request>, IRequestRepository
    {
        public RequestRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        public override Request Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Request>();
            return set.Include(t => t.ApprovalHistories).FirstOrDefault(p => p.Id == (Guid)id);
        }

        /// <summary>
        /// 删除批文历史
        /// </summary>
        /// <param name="approvalHistory">批文历史</param>
        public void DelApprovalHistory(ApprovalHistory approvalHistory)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork != null)
            {
                var set = currentUnitOfWork.CreateSet<ApprovalHistory>();
                set.Remove(approvalHistory);
            }
        }

        #endregion
    }
}
