#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/03，21:06
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
using System.Linq;

#endregion

namespace UniCloud.Domain.Events
{
    /// <summary>
    ///     发布、订阅事件的基类
    /// </summary>
    public abstract class EventBase
    {
        private readonly List<IEventSubscription> _subscriptions = new List<IEventSubscription>();

        /// <summary>
        ///     当前订阅的集合
        /// </summary>
        /// <value>
        ///     当前的订阅集合
        /// </value>
        protected ICollection<IEventSubscription> Subscriptions
        {
            get { return _subscriptions; }
        }

        /// <summary>
        ///     往订阅集合添加<see cref="T:UniCloud.Domain.Events.IEventSubscription" />事件。
        /// </summary>
        /// <param name="eventSubscription">订阅的事件</param>
        /// <returns>
        ///     <see cref="T:UniCloud.Domain.Events.SubscriptionToken" />每个事件的唯一标识。
        /// </returns>
        /// <remarks>
        ///     往内部集合添加<see cref="T:UniCloud.Domain.Events.SubscriptionToken" />。
        /// </remarks>
        protected virtual SubscriptionToken InternalSubscribe(IEventSubscription eventSubscription)
        {
            if (eventSubscription == null)
                throw new ArgumentNullException("eventSubscription");
            eventSubscription.SubscriptionToken = new SubscriptionToken(Unsubscribe);
            lock (Subscriptions)
                Subscriptions.Add(eventSubscription);
            return eventSubscription.SubscriptionToken;
        }

        /// <summary>
        ///     访问集合暴露的<see cref="T:UniCloud.Domain.Events.IEventSubscription" />订阅。
        /// </summary>
        /// <param name="arguments">传输给侦听器的参数</param>
        /// <remarks>
        ///     执行策略前, 调用<see cref="M:UniCloud.Domain.Events.IEventSubscription.GetExecutionStrategy" />方法删除集合中的订阅并返回
        ///     <see langword="null" /><see cref="T:System.Action`1" />。
        /// </remarks>
        protected virtual void InternalPublish(params object[] arguments)
        {
            foreach (var action in PruneAndReturnStrategies())
                action(arguments);
        }

        /// <summary>
        ///     移除符合<seealso cref="T:UniCloud.Domain.Events.SubscriptionToken" />的订阅。
        /// </summary>
        /// <param name="token">
        ///     订阅事件时，由<see cref="T:UniCloud.Domain.Events.EventBase" />返回的
        ///     <see cref="T:UniCloud.Domain.Events.SubscriptionToken" />。
        /// </param>
        public virtual void Unsubscribe(SubscriptionToken token)
        {
            lock (Subscriptions)
            {
                var local0 = Subscriptions.FirstOrDefault(evt => evt.SubscriptionToken == token);
                if (local0 == null)
                    return;
                Subscriptions.Remove(local0);
            }
        }

        /// <summary>
        ///     如果有订阅符合<see cref="T:UniCloud.Domain.Events.SubscriptionToken" />，返回<see langword="true" />。
        /// </summary>
        /// <param name="token">
        ///     订阅事件时，<see cref="T:UniCloud.Domain.Events.EventBase" />返回的<see cref="T:UniCloud.Domain.Events.SubscriptionToken" />。
        /// </param>
        /// <returns>
        ///      如果有匹配的<see cref="T:UniCloud.Domain.Events.SubscriptionToken" />，返回<see langword="true" />; 否则 <see langword="false" />。
        /// </returns>
        public virtual bool Contains(SubscriptionToken token)
        {
            lock (Subscriptions)
                return Subscriptions.FirstOrDefault(evt => evt.SubscriptionToken == token) != null;
        }

        private IEnumerable<Action<object[]>> PruneAndReturnStrategies()
        {
            var list = new List<Action<object[]>>();
            lock (Subscriptions)
            {
                for (var local1 = Subscriptions.Count - 1; local1 >= 0; --local1)
                {
                    var local2 = _subscriptions[local1].GetExecutionStrategy();
                    if (local2 == null)
                        _subscriptions.RemoveAt(local1);
                    else
                        list.Add(local2);
                }
            }
            return list;
        }
    }
}