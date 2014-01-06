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

#endregion

namespace UniCloud.Domain.Events
{
    /// <summary>
    ///     扩展<see cref="T:UniCloud.Domain.Events.EventSubscription`1" />调用特定
    ///     <see cref="T:System.Windows.Threading.Dispatcher" />的
    ///     <see cref="P:UniCloud.Domain.Events.EventSubscription`1.Action" /> 委托。
    /// </summary>
    /// <typeparam name="TPayload">
    ///     用以生成<see cref="T:System.Action`1" />和<see cref="T:System.Predicate`1" />的类型。
    /// </typeparam>
    internal class DispatcherEventSubscription<TPayload> : EventSubscription<TPayload>
    {
        private readonly IDispatcherFacade _dispatcher;

        /// <summary>
        ///     创建<see cref="T:UniCloud.Domain.Events.BackgroundEventSubscription`1" />的新实例。
        /// </summary>
        /// <param name="actionReference"><see cref="T:System.Action`1" />类型委托的引用。</param>
        /// <param name="filterReference"><see cref="T:System.Predicate`1" />类型委托的引用。</param>
        /// <param name="dispatcher">执行<paramref name="actionReference" />委托时的分配器。</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     当 <paramref name="actionReference" /> 或者<see paramref="filterReference" />为<see langword="null" />时。
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     如果<paramref name="actionReference" />的目标不是<see cref="T:System.Action`1" />类型,
        ///     或者<paramref name="filterReference" />的目标不是<see cref="T:System.Predicate`1" />类型
        /// </exception>
        public DispatcherEventSubscription(IDelegateReference actionReference, IDelegateReference filterReference,
            IDispatcherFacade dispatcher)
            : base(actionReference, filterReference)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        ///     在特定的<see cref="T:System.Windows.Threading.Dispatcher" />线程同步调用<see cref="T:System.Action`1" />。
        /// </summary>
        /// <param name="action">执行的Action</param>
        /// <param name="argument">调用<paramref name="action" />时传输的参数</param>
        public override void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            _dispatcher.BeginInvoke(action, argument);
        }
    }
}