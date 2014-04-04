#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/05，14:21
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
    public class FulfilOrderEvent : CompositeEvent<Order>
    {
    }
}