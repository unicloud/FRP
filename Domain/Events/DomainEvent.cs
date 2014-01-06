#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/05，13:36
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

#endregion

namespace UniCloud.Domain.Events
{
    public abstract class DomainEvent
    {
        private static IEventAggregator _eventAggregator;

        protected DomainEvent(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        /// <summary>
        ///     发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <typeparam name="TPayload">事件传输消息的类型</typeparam>
        /// <param name="payload">传输的消息</param>
        public static void Publish<TEvent, TPayload>(TPayload payload) where TEvent : CompositeEvent<TPayload>, new()
        {
            _eventAggregator.GetEvent<TEvent>().Publish(payload);
        }

        /// <summary>
        ///     订阅事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <typeparam name="TPayload">事件传输消息的类型</typeparam>
        /// <param name="token">事件订阅的凭据</param>
        /// <param name="action">事件执行的委托</param>
        /// <returns>标识事件的凭据</returns>
        public static SubscriptionToken Subscribe<TEvent, TPayload>(SubscriptionToken token, Action<TPayload> action)
            where TEvent : CompositeEvent<TPayload>, new()
        {
            var evn = _eventAggregator.GetEvent<TEvent>();
            if (token != null) evn.Unsubscribe(token);
            return evn.Subscribe(action);
        }
    }
}