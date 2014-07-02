#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/17，12:11
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
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.Repositories
{
    /// <summary>
    ///     供应商公司仓储实现
    /// </summary>
    public class SupplierCompanyRepository : Repository<SupplierCompany>, ISupplierCompanyRepository
    {
        public SupplierCompanyRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载
        public override SupplierCompany Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<SupplierCompany>();

            return set.Include(p => p.Suppliers).SingleOrDefault(l => l.Id == (int)id);
        }
        #endregion

        public SupplierCompany GetSupplierCompany(Expression<Func<SupplierCompany, bool>> condition)
        {
            var currentUnitOfWork = UnitOfWork as FleetPlanBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<SupplierCompany>();
            return set.FirstOrDefault(condition);
        }
    }
}