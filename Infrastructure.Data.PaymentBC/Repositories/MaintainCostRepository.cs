#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 11:38:52
// 文件名：MaintainCostRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 11:38:52
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.PaymentBC.Aggregates.MaintainCostAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.Repositories
{
    public class MaintainCostRepository : Repository<MaintainCost>, IMaintainCostRepository
    {
        public MaintainCostRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
