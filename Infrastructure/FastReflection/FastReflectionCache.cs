#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:39
// 方案：FRP
// 项目：Infrastructure
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Threading;

#endregion

namespace UniCloud.Infrastructure.FastReflection
{
    /// <summary>
    ///     快速反射缓存。
    /// </summary>
    /// <typeparam name="TKey">缓存的Key。</typeparam>
    /// <typeparam name="TValue">缓存的快速反射工作对象。</typeparam>
    public abstract class FastReflectionCache<TKey, TValue> : IFastReflectionCache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _cache = new Dictionary<TKey, TValue>();
        private readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

        /// <summary>
        ///     获取缓存的工作对象。
        /// </summary>
        /// <param name="key">缓存的Key。</param>
        /// <returns>缓存的快速反射工作对象。</returns>
        public TValue Get(TKey key)
        {
            TValue value;

            _rwLock.EnterReadLock();
            var cacheHit = _cache.TryGetValue(key, out value);
            _rwLock.ExitReadLock();

            if (cacheHit) return value;

            _rwLock.EnterWriteLock();
            if (_cache.TryGetValue(key, out value)) return value;
            try
            {
                value = Create(key);
                _cache[key] = value;
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }

            return value;
        }

        /// <summary>
        ///     创建快速反射缓存工作对象。
        /// </summary>
        /// <param name="key">缓存的Key。</param>
        /// <returns>快速反射工作对象。</returns>
        protected abstract TValue Create(TKey key);
    }
}