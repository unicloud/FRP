#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，11:33
// 方案：FRP
// 项目：Infrastructure.Data.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Data.Entity;
using System.Linq;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.Repositories
{
    /// <summary>
    ///     发票仓储实现
    /// </summary>
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        public override Invoice Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as PaymentBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Invoice>();

            return set.Include(p => p.InvoiceLines).SingleOrDefault(l => l.Id == (int)id);
        }

        public override IQueryable<Invoice> GetAll()
        {
            var currentUnitOfWork = UnitOfWork as PaymentBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Invoice>();

            return set.Include(p => p.InvoiceLines);
        }

        public void DeleteInvoice(Invoice invoice)
        {
            var currentUnitOfWork = UnitOfWork as PaymentBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbInvoiceLines = currentUnitOfWork.CreateSet<InvoiceLine>();
            var dbInvoices = currentUnitOfWork.CreateSet<Invoice>();
            dbInvoiceLines.RemoveRange(invoice.InvoiceLines);
            dbInvoices.Remove(invoice);
        }
        #endregion
    }
}