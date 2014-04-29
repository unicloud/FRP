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
using UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg;
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
            var set = currentUnitOfWork.CreateSet<Invoice>().OfType<BasePurchaseInvoice>();
            var result = set.Include(p => p.InvoiceLines).FirstOrDefault(l => l.Id == (int)id);

            if (result == null)
            {
                var maintainInvoiceSet = currentUnitOfWork.CreateSet<Invoice>().OfType<MaintainInvoice>();
                return maintainInvoiceSet.Include(p => p.InvoiceLines).FirstOrDefault(l => l.Id == (int)id);
            }
            return result;
        }

        public BasePurchaseInvoice GetBasePurchaseInvoice(int id)
        {
            var currentUnitOfWork = UnitOfWork as PaymentBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<BasePurchaseInvoice>();
            return set.Include(p => p.InvoiceLines).FirstOrDefault(l => l.Id == id);
        }

        public MaintainInvoice GetMaintainInvoice(int id)
        {
            var currentUnitOfWork = UnitOfWork as PaymentBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<MaintainInvoice>();
            return set.Include(p => p.InvoiceLines).FirstOrDefault(l => l.Id == id);
        }

        public override IQueryable<Invoice> GetAll()
        {
            var currentUnitOfWork = UnitOfWork as PaymentBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Invoice>();

            return set;
        }

        public void DeleteInvoice(Invoice invoice)
        {
            var currentUnitOfWork = UnitOfWork as PaymentBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            if (invoice is BasePurchaseInvoice)
            {
                var dbInvoiceLines = currentUnitOfWork.CreateSet<InvoiceLine>();
                var dbInvoices = currentUnitOfWork.CreateSet<BasePurchaseInvoice>();
                var basePurchaseInvoice = invoice as BasePurchaseInvoice;
                dbInvoiceLines.RemoveRange(basePurchaseInvoice.InvoiceLines);
                dbInvoices.Remove(basePurchaseInvoice);
            }
            else
            {
                var dbInvoiceLines = currentUnitOfWork.CreateSet<InvoiceLine>();
                var dbInvoices = currentUnitOfWork.CreateSet<MaintainInvoice>();
                var maintainInvoice = invoice as MaintainInvoice;
                dbInvoiceLines.RemoveRange(maintainInvoice.InvoiceLines);
                dbInvoices.Remove(maintainInvoice);
            }
        }

        public void RemoveInvoiceLine(InvoiceLine invoiceLine)
        {
            var currentUnitOfWork = UnitOfWork as PaymentBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var set = currentUnitOfWork.CreateSet<InvoiceLine>();
            set.Remove(invoiceLine);
        }
        #endregion
    }
}