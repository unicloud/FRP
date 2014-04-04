#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/03，21:05
// 方案：FRP
// 项目：Domain
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

namespace UniCloud.Domain.Events
{
    /// <summary>
    ///     聚合事件接口
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        ///     获取某个类型的事件
        /// </summary>
        /// <typeparam name="TEventType">事件类型</typeparam>
        /// <returns>
        ///     <typeparamref name="TEventType" />的实例。
        /// </returns>
        TEventType GetEvent<TEventType>() where TEventType : EventBase, new();
    }
}