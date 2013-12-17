#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/15，14:41
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
using UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;
using System.Data.Entity;
#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.Repositories
{
    /// <summary>
    ///     维修发票仓储实现
    /// </summary>
    public class MaintainInvoiceRepository : Repository<MaintainInvoice>, IMaintainInvoiceRepository
    {
        public MaintainInvoiceRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载
        public override MaintainInvoice Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as PaymentBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<MaintainInvoice>();
            return set.Include(t => t.MaintainInvoiceLines).FirstOrDefault(p => p.Id == (int)id);
        }

        public void RemoveMaintainInvoiceLine(MaintainInvoiceLine maintainInvoiceLine)
        {
            var currentUnitOfWork = UnitOfWork as PaymentBCUnitOfWork;
            if (currentUnitOfWork == null) return ;
            var set = currentUnitOfWork.CreateSet<MaintainInvoiceLine>();
            set.Remove(maintainInvoiceLine);
        }
        #endregion
    }
}