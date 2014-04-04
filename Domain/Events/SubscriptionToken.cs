#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/03，21:09
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
    ///     <see cref="T:UniCloud.Domain.Events.EventBase" />订阅凭据
    /// </summary>
    public class SubscriptionToken : IEquatable<SubscriptionToken>, IDisposable
    {
        private readonly Guid _token;
        private Action<SubscriptionToken> _unsubscribeAction;

        /// <summary>
        ///     初始化<see cref="T:UniCloud.Domain.Events.SubscriptionToken" />的实例。
        /// </summary>
        public SubscriptionToken(Action<SubscriptionToken> unsubscribeAction)
        {
            _unsubscribeAction = unsubscribeAction;
            _token = Guid.NewGuid();
        }

        /// <summary>
        ///     释放订阅凭据, 移除与<see cref="T:UniCloud.Domain.Events.EventBase" />相关的事件。
        /// </summary>
        public virtual void Dispose()
        {
            if (_unsubscribeAction != null)
            {
                _unsubscribeAction(this);
                _unsubscribeAction = null;
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     判断当前对象是否等于另一个对象
        /// </summary>
        /// <returns>
        ///     如果当前对象等于<paramref name="other" />参数，为<see langword="true" />; 否则, <see langword="false" />。
        /// </returns>
        /// <param name="other">比较的对象</param>
        public bool Equals(SubscriptionToken other)
        {
            return other != null && Equals(_token, other._token);
        }

        /// <summary>
        ///     判断对象<see cref="T:System.Object" />等于当前对象<see cref="T:System.Object" />。
        /// </summary>
        /// <returns>
        ///     如果<see cref="T:System.Object" />等于当前对象<see cref="T:System.Object" />，返回true; 否则, 返回false。
        /// </returns>
        /// <param name="obj"> <see cref="T:System.Object" />对象，用以 比较当前对象<see cref="T:System.Object" /></param>
        /// <exception cref="T:System.NullReferenceException"><paramref name="obj" />参数为空！</exception>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || Equals(obj as SubscriptionToken);
        }

        /// <summary>
        ///     哈希函数
        /// </summary>
        /// <returns>
        ///     <see cref="T:System.Object" />的哈希码。
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return _token.GetHashCode();
        }
    }
}