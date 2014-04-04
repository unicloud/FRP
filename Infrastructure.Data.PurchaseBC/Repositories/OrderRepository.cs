#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，11:11
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Data.Entity;
using System.Linq;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Repositories
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

        public override IQueryable<Order> GetAll()
        {
            var currentUnitOfWork = UnitOfWork as PurchaseBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Order>();

            return set.Include(o => o.OrderLines).Include(o => o.ContractContents);
        }

        public override Order Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as PurchaseBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Order>();

            return set.Include(o => o.OrderLines)
                .Include(o => o.ContractContents)
                .SingleOrDefault(o => o.Id == (int) id);
        }

        #endregion

        #region IOrderRepository 成员

        public void RemoveOrderLine(OrderLine line)
        {
            var currentUnitOfWork = UnitOfWork as PurchaseBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<OrderLine>().Remove(line);
        }

        public void RemoveContractContent(ContractContent content)
        {
            var currentUnitOfWork = UnitOfWork as PurchaseBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<ContractContent>().Remove(content);
        }

        #endregion
    }
}