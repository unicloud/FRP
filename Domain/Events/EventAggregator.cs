#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/03，21:12
// 方案：FRP
// 项目：Domain
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;

#endregion

namespace UniCloud.Domain.Events
{
    /// <summary>
    ///     接口<see cref="T:UniCloud.Domain.Events.IEventAggregator" />的实现。
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, EventBase> _events = new Dictionary<Type, EventBase>();

        /// <summary>
        ///     获取由事件聚合器管理的单例事件，多次访问<typeparamref name="TEventType" />类型的方法返回同一事件实例。
        /// </summary>
        /// <typeparam name="TEventType">
        ///     事件的类型，必须从<see cref="T:UniCloud.Domain.Events.EventBase" />继承。
        /// </typeparam>
        /// <returns>
        ///     事件对象<typeparamref name="TEventType" />的单例。
        /// </returns>
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            EventBase eventBase;
            if (_events.TryGetValue(typeof (TEventType), out eventBase))
                return (TEventType) eventBase;
            var instance = Activator.CreateInstance<TEventType>();
            _events[typeof (TEventType)] = instance;
            return instance;
        }
    }
}