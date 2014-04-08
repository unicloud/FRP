#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，11:34
// 方案：FRP
// 项目：Infrastructure.Data.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using System.Data.Entity;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.Repositories
{
    /// <summary>
    ///     订单仓储实现
    /// </summary>
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        public override Order Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as PaymentBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Order>();

            return set.Include(p => p.OrderLines).SingleOrDefault(l => l.Id == (int)id);
        }
        #endregion
    }
}