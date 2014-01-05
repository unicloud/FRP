#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/05，14:19
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Domain.Events;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Events
{
    /// <summary>
    ///     领域事件
    /// </summary>
    public class PurchaseEvent : DomainEvent, IPurchaseEvent
    {
        private SubscriptionToken _fulfilOrder;

        public PurchaseEvent(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            Subscribe();
        }

        private void Subscribe()
        {
            _fulfilOrder = Subscribe<FulfilOrderEvent, Order>(_fulfilOrder, FulfilOrderHandler);
        }

        #region 事件处理程序

        private void FulfilOrderHandler(Order order)
        {
            var q = 1;
        }

        #endregion
    }
}