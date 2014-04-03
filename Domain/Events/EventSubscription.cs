#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/03，21:13
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
using System.Globalization;
using UniCloud.Domain.Property;

#endregion

namespace UniCloud.Domain.Events
{
    /// <summary>
    ///     提供获取<see cref="T:System.Delegate" />委托的途径，执行依赖于第二个过滤器值的调用。
    /// </summary>
    /// <typeparam name="TPayload">
    ///     用以生成<see cref="T:System.Action`1" />和<see cref="T:System.Predicate`1" />的类型。
    /// </typeparam>
    internal class EventSubscription<TPayload> : IEventSubscription
    {
        private readonly IDelegateReference _actionReference;
        private readonly IDelegateReference _filterReference;

        /// <summary>
        ///     创建<see cref="T:UniCloud.Domain.Events.EventSubscription`1" />的新实例
        /// </summary>
        /// <param name="actionReference"><see cref="T:System.Action`1" />类型委托的引用。</param>
        /// <param name="filterReference"><see cref="T:System.Predicate`1" />类型委托的引用。</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     当<paramref name="actionReference" />或<see paramref="filterReference" />为<see langword="null" />。
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     当<paramref name="actionReference" />引用的目标不是<see cref="T:System.Action`1" />类型,
        ///     或者<paramref name="filterReference" />引用的目标不是<see cref="T:System.Predicate`1" />类型。
        /// </exception>
        public EventSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
        {
            if (actionReference == null)
                throw new ArgumentNullException("actionReference");
            if (!(actionReference.Target is Action<TPayload>))
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, Resources.InvalidDelegateRerefenceTypeException,
                        new object[]
                        {
                            typeof (Action<TPayload>).FullName
                        }), "actionReference");
            }
            if (filterReference == null)
                throw new ArgumentNullException("filterReference");
            if (!(filterReference.Target is Predicate<TPayload>))
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, Resources.InvalidDelegateRerefenceTypeException,
                        new object[]
                        {
                            typeof (Predicate<TPayload>).FullName
                        }), "filterReference");
            }
            _actionReference = actionReference;
            _filterReference = filterReference;
        }

        /// <summary>
        ///     获取由<see cref="T:UniCloud.Domain.Events.IDelegateReference" />引用的目标<see cref="T:System.Action`1" />。
        /// </summary>
        /// <value>
        ///     值为<see cref="T:System.Action`1" />；如果引用目标未生存，为 <see langword="null" />。
        /// </value>
        public Action<TPayload> Action
        {
            get { return (Action<TPayload>) _actionReference.Target; }
        }

        /// <summary>
        ///     获取由<see cref="T:UniCloud.Domain.Events.IDelegateReference" />引用的目标<see cref="T:System.Predicate`1" />。
        /// </summary>
        /// <value>
        ///     值为<see cref="T:System.Predicate`1" />；如果引用目标未生存，为<see langword="null" />。
        /// </value>
        public Predicate<TPayload> Filter
        {
            get { return (Predicate<TPayload>) _filterReference.Target; }
        }

        /// <summary>
        ///     获取或设置指示<see cref="T:UniCloud.Domain.Events.IEventSubscription" />的
        ///     <see cref="T:UniCloud.Domain.Events.SubscriptionToken" />凭据。
        /// </summary>
        /// <value>
        ///     指示<see cref="T:UniCloud.Domain.Events.IEventSubscription" />的凭据。
        /// </value>
        public SubscriptionToken SubscriptionToken { get; set; }

        /// <summary>
        ///     获取发布事件的执行策略。
        /// </summary>
        /// <returns>
        ///     包含执行策略的<see cref="T:System.Action`1" />委托； 如果<see cref="T:UniCloud.Domain.Events.IEventSubscription" />不再有效，则返回
        ///     <see langword="null" />
        /// </returns>
        /// <remarks>
        ///     如果<see cref="P:UniCloud.Domain.Events.EventSubscription`1.Action" />或者
        ///     <see cref="P:UniCloud.Domain.Events.EventSubscription`1.Filter" />因为被GC回收而不再有效, 方法返回<see langword="null" />。
        ///     否则，如果依据<see cref="P:UniCloud.Domain.Events.EventSubscription`1.Filter" />过滤返回<see langword="true" />，则访问
        ///     <see cref="M:UniCloud.Domain.Events.EventSubscription`1.InvokeAction(System.Action{`0},`0)" />。返回的委托保持对
        ///     <see cref="P:UniCloud.Domain.Events.EventSubscription`1.Action" />与
        ///     <see cref="P:UniCloud.Domain.Events.EventSubscription`1.Filter" />及目标<see cref="T:System.Delegate">委托</see>的强引用。
        ///     返回的委托没有被GC回收，则<see cref="P:UniCloud.Domain.Events.EventSubscription`1.Action" />和
        ///     <see cref="P:UniCloud.Domain.Events.EventSubscription`1.Filter" />引用也不会被回收。
        /// </remarks>
        public virtual Action<object[]> GetExecutionStrategy()
        {
            var action = Action;
            var filter = Filter;
            if (action != null && filter != null)
                return arguments =>
                {
                    var local0 = default(TPayload);
                    if (arguments != null && arguments.Length > 0 && arguments[0] != null)
                        local0 = (TPayload) arguments[0];
                    if (!filter(local0))
                        return;
                    InvokeAction(action, local0);
                };
            return null;
        }

        /// <summary>
        ///     没重写是以同步方式调用<see cref="T:System.Action`1" />。
        /// </summary>
        /// <param name="action">被执行的调用</param>
        /// <param name="argument">调用时传给<paramref name="action" />的参数。</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     如果<paramref name="action" />为空，返回<see cref="T:System.ArgumentNullException" />异常。
        /// </exception>
        public virtual void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            action(argument);
        }
    }
}