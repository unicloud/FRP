#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/03，21:11
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
using System.Reflection;

#endregion

namespace UniCloud.Domain.Events
{
    /// <summary>
    ///     表示对包含<see cref="T:System.WeakReference" /> 目标的<see cref="T:System.Delegate" />委托引用。
    ///     用于组合应用内部。
    /// </summary>
    public class DelegateReference : IDelegateReference
    {
        private readonly Delegate _delegate;
        private readonly Type _delegateType;
        private readonly MethodInfo _method;
        private readonly WeakReference _weakReference;

        /// <summary>
        ///     初始化<see cref="T:UniCloud.Domain.Events.DelegateReference" />的新实例。
        /// </summary>
        /// <param name="delegate">创建引用的原始<see cref="T:System.Delegate" />委托。</param>
        /// <param name="keepReferenceAlive">
        ///     如果<see langword="false" />，创建一个委托的弱引用，允许被回收。否则，保持对目标的强引用。
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     如果<paramref name="delegate" />无法赋值为<see cref="T:System.Delegate" />。
        /// </exception>
        public DelegateReference(Delegate @delegate, bool keepReferenceAlive)
        {
            if (@delegate == null)
                throw new ArgumentNullException("delegate");
            if (keepReferenceAlive)
            {
                _delegate = @delegate;
            }
            else
            {
                _weakReference = new WeakReference(@delegate.Target);
                _method = @delegate.Method;
                _delegateType = @delegate.GetType();
            }
        }

        /// <summary>
        ///     获取<see cref="T:System.Delegate" /> (目标) 当前<see cref="T:UniCloud.Domain.Events.DelegateReference" />对象的引用。
        /// </summary>
        /// <value>
        ///     如果当前<see cref="T:UniCloud.Domain.Events.DelegateReference" />对象被GC回收，值为<see langword="null" />;
        ///     否则, 为当前<see cref="T:UniCloud.Domain.Events.DelegateReference" />对象引用的<see cref="T:System.Delegate" />。
        /// </value>
        public Delegate Target
        {
            get { return _delegate ?? TryGetDelegate(); }
        }

        private Delegate TryGetDelegate()
        {
            if (_method.IsStatic)
                return Delegate.CreateDelegate(_delegateType, null, _method);
            var target = _weakReference.Target;
            return target != null ? Delegate.CreateDelegate(_delegateType, target, _method) : null;
        }
    }
}