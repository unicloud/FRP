#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，16:11
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq.Expressions;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using System.Linq;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Repositories
{
    /// <summary>
    ///     供应商仓储实现
    /// </summary>
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion

        public Supplier GetSupplier(Expression<Func<Supplier, bool>> condition)
        {
            var currentUnitOfWork = UnitOfWork as PurchaseBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Supplier>();
            return set.FirstOrDefault(condition);
        }
    }
}