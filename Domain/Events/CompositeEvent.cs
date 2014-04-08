#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/03，21:15
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
using System.Linq;

#endregion

namespace UniCloud.Domain.Events
{
    /// <summary>
    ///     管理事件的发布和订阅
    /// </summary>
    /// <typeparam name="TPayload">传输给订阅方的消息类型</typeparam>
    public class CompositeEvent<TPayload> : EventBase
    {
        /// <summary>
        ///     订阅一个将发布在<see cref="F:UniCloud.Domain.Events.ThreadOption.PublisherThread" />的委托。
        ///     <see cref="T:UniCloud.Domain.Events.CompositeEvent`1" /> 包含一个<paramref name="action" />委托的
        ///     <seealso cref="T:System.WeakReference" />引用。
        /// </summary>
        /// <param name="action">事件发布时执行的委托。</param>
        /// <returns>
        ///     唯一标识添加的订阅的<see cref="T:UniCloud.Domain.Events.SubscriptionToken" />凭据。
        /// </returns>
        /// <remarks>
        ///     CompositeEvent集合是线程安全的。
        /// </remarks>
        public SubscriptionToken Subscribe(Action<TPayload> action)
        {
            return Subscribe(action, ThreadOption.PublisherThread);
        }

        /// <summary>
        ///     订阅一个对事件的委托。
        ///     CompositeEvent会包含一个对<paramref name="action" />委托的<seealso cref="T:System.WeakReference" />。
        /// </summary>
        /// <param name="action">事件发生时执行的委托。</param>
        /// <param name="threadOption">接收委托回调的线程。</param>
        /// <returns>
        ///     唯一标识添加的订阅的<see cref="T:UniCloud.Domain.Events.SubscriptionToken" />凭据。
        /// </returns>
        /// <remarks>
        ///     CompositeEvent集合是线程安全的。
        /// </remarks>
        public SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption)
        {
            return Subscribe(action, threadOption, false);
        }

        /// <summary>
        ///     订阅一个将发布在<see cref="F:UniCloud.Domain.Events.ThreadOption.PublisherThread" />的事件的委托。
        /// </summary>
        /// <param name="action">事件发生时执行的委托。</param>
        /// <param name="keepSubscriberReferenceAlive">
        ///     如果<see langword="true" />, <seealso cref="T:UniCloud.Domain.Events.CompositeEvent`1" />会保持对订阅的引用而不会被回收。
        /// </param>
        /// <returns>
        ///     唯一标识添加的订阅的<see cref="T:UniCloud.Domain.Events.SubscriptionToken" />凭据。
        /// </returns>
        /// <remarks>
        ///     如果<paramref name="keepSubscriberReferenceAlive" />设为<see langword="false" />,
        ///     <see cref="T:UniCloud.Domain.Events.CompositeEvent`1" />会包含一个对<paramref name="action" />委托的
        ///     <seealso cref="T:System.WeakReference" />引用。
        ///     如果没使用弱引用(<paramref name="keepSubscriberReferenceAlive" />为<see langword="true" />),
        ///     用户在试图Dispose订阅以避免内存泄露或未预期的行为是必须显式访问事件的Unsubscribe。
        ///     <para />
        ///     CompositeEvent集合是线程安全的。
        /// </remarks>
        public SubscriptionToken Subscribe(Action<TPayload> action, bool keepSubscriberReferenceAlive)
        {
            return Subscribe(action, ThreadOption.PublisherThread, keepSubscriberReferenceAlive);
        }

        /// <summary>
        ///     订阅一个事件的委托
        /// </summary>
        /// <param name="action">事件发生时执行的委托。</param>
        /// <param name="threadOption">接收委托回调的线程。</param>
        /// <param name="keepSubscriberReferenceAlive">
        ///     如果<see langword="true" />, <seealso cref="T:UniCloud.Domain.Events.CompositeEvent`1" />会保持对订阅的引用而不会被回收。
        /// </param>
        /// <returns>
        ///     唯一标识添加的订阅的<see cref="T:UniCloud.Domain.Events.SubscriptionToken" />凭据。
        /// </returns>
        /// <remarks>
        ///     如果<paramref name="keepSubscriberReferenceAlive" />设为<see langword="false" />,
        ///     <see cref="T:UniCloud.Domain.Events.CompositeEvent`1" />会包含一个对<paramref name="action" />委托的
        ///     <seealso cref="T:System.WeakReference" />引用。
        ///     如果没使用弱引用(<paramref name="keepSubscriberReferenceAlive" />为<see langword="true" />),
        ///     用户在试图Dispose订阅以避免内存泄露或未预期的行为是必须显式访问事件的Unsubscribe。
        ///     <para />
        ///     CompositeEvent集合是线程安全的。
        /// </remarks>
        public SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption,
            bool keepSubscriberReferenceAlive)
        {
            return Subscribe(action, threadOption, keepSubscriberReferenceAlive, null);
        }

        /// <summary>
        ///     订阅一个事件的委托
        /// </summary>
        /// <param name="action">事件发生时执行的委托。</param>
        /// <param name="threadOption">接收委托回调的线程。</param>
        /// <param name="keepSubscriberReferenceAlive">
        ///     如果<see langword="true" />, <seealso cref="T:UniCloud.Domain.Events.CompositeEvent`1" />会保持对订阅的引用而不会被回收。
        /// </param>
        /// <param name="filter">接收事件的过滤条件</param>
        /// <returns>
        ///     唯一标识添加的订阅的<see cref="T:UniCloud.Domain.Events.SubscriptionToken" />凭据。
        /// </returns>
        /// <remarks>
        ///     如果<paramref name="keepSubscriberReferenceAlive" />设为<see langword="false" />,
        ///     <see cref="T:UniCloud.Domain.Events.CompositeEvent`1" />会包含一个对<paramref name="action" />委托的
        ///     <seealso cref="T:System.WeakReference" />引用。
        ///     如果没使用弱引用(<paramref name="keepSubscriberReferenceAlive" />为<see langword="true" />),
        ///     用户在试图Dispose订阅以避免内存泄露或未预期的行为是必须显式访问事件的Unsubscribe。
        ///     <para />
        ///     CompositeEvent集合是线程安全的。
        /// </remarks>
        public virtual SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption,
            bool keepSubscriberReferenceAlive, Predicate<TPayload> filter)
        {
            var actionReference = (IDelegateReference) new DelegateReference(action, keepSubscriberReferenceAlive);
            var filterReference = filter == null
                ? new DelegateReference(new Predicate<TPayload>(t=>true), true)
                : (IDelegateReference) new DelegateReference(filter, keepSubscriberReferenceAlive);
            EventSubscription<TPayload> eventSubscription;
            switch (threadOption)
            {
                case ThreadOption.PublisherThread:
                    eventSubscription = new EventSubscription<TPayload>(actionReference, filterReference);
                    break;
                case ThreadOption.BackgroundThread:
                    eventSubscription = new BackgroundEventSubscription<TPayload>(actionReference, filterReference);
                    break;
                default:
                    eventSubscription = new EventSubscription<TPayload>(actionReference, filterReference);
                    break;
            }
            return InternalSubscribe(eventSubscription);
        }

        /// <summary>
        ///     发布<see cref="T:UniCloud.Domain.Events.CompositeEvent`1" />事件。
        /// </summary>
        /// <param name="payload">传输给订阅者的消息</param>
        public virtual void Publish(TPayload payload)
        {
            InternalPublish(new object[]
            {
                payload
            });
        }

        /// <summary>
        ///     从订阅集合中移除匹配<seealso cref="T:System.Action`1" />的第一项订阅。
        /// </summary>
        /// <param name="subscriber">订阅事件时使用的<see cref="T:System.Action`1" />。</param>
        public virtual void Unsubscribe(Action<TPayload> subscriber)
        {
            lock (Subscriptions)
            {
                IEventSubscription local0 =
                    Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == subscriber);
                if (local0 == null)
                    return;
                Subscriptions.Remove(local0);
            }
        }

        /// <summary>
        ///     如果订阅匹配<seealso cref="T:System.Action`1" />则返回<see langword="true" />。
        /// </summary>
        /// <param name="subscriber">订阅事件时使用的<see cref="T:System.Action`1" />。</param>
        /// <returns>
        ///     如果有匹配的<seealso cref="T:System.Action`1" />，返回 <see langword="true" />; 否则返回<see langword="false" />。
        /// </returns>
        public virtual bool Contains(Action<TPayload> subscriber)
        {
            IEventSubscription eventSubscription;
            lock (Subscriptions)
                eventSubscription =
                    Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == subscriber);
            return eventSubscription != null;
        }
    }
}