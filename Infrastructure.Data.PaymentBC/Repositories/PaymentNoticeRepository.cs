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
using UniCloud.Domain.PaymentBC.Aggregates.PaymentNoticeAgg;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;
using System.Data.Entity;
#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.Repositories
{
    /// <summary>
    ///     付款通知仓储实现
    /// </summary>
    public class PaymentNoticeRepository : Repository<PaymentNotice>, IPaymentNoticeRepository
    {
        public PaymentNoticeRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载
        public override PaymentNotice Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as PaymentBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<PaymentNotice>();
            return set.Include(t => t.PaymentNoticeLines).FirstOrDefault(p => p.Id == (int)id);
        }
        #endregion

        public void RemovePaymentNoticeLine(PaymentNoticeLine paymentNoticeLine)
        {
            var currentUnitOfWork = UnitOfWork as PaymentBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var set = currentUnitOfWork.CreateSet<PaymentNoticeLine>();
            set.Remove(paymentNoticeLine);
        }
    }
}