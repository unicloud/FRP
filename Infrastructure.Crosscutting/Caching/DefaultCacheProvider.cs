#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/20，11:12
// 文件名：DefaultCacheProvider.cs
// 程序集：UniCloud.Infrastructure.Crosscutting
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Runtime.Caching;

#endregion

namespace UniCloud.Infrastructure.Crosscutting.Caching
{
    /// <summary>
    /// 系统的缓存处理，采用System.Runtime.Caching缓存
    /// </summary>
    internal class DefaultCacheProvider:ICacheProvider
    {
        #region Private Fields

        private readonly ObjectCache _cacheManager = MemoryCache.Default;

        #endregion

        #region ICacheProvider Members

        /// <summary>
        ///     向缓存中添加一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        public void Add(string key, string valKey, object value)
        {
            Dictionary<string, object> dict;
            if (_cacheManager.Contains(key))
            {
                dict = (Dictionary<string, object>) _cacheManager[key];
                dict[valKey] = value;
            }
            else
            {
                dict = new Dictionary<string, object> {{valKey, value}};
            }
            var policy = new CacheItemPolicy 
            {AbsoluteExpiration = DateTimeOffset.Now.AddMonths(1)};
            _cacheManager.Add(key, dict,policy);
        }

        /// <summary>
        ///     向缓存中更新一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        public void Put(string key, string valKey, object value)
        {
            Add(key, valKey, value);
        }

        /// <summary>
        ///     从缓存中读取对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <returns>被缓存的对象。</returns>
        public object Get(string key, string valKey)
        {
            if (_cacheManager.Contains(key))
            {
                var dict = (Dictionary<string, object>) _cacheManager[key];
                if (dict != null && dict.ContainsKey(valKey))
                    return dict[valKey];
                return null;
            }
            return null;
        }

        /// <summary>
        ///     从缓存中移除对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        public void Remove(string key)
        {
            _cacheManager.Remove(key);
        }

        /// <summary>
        ///     获取一个<see cref="bool" />值，该值表示拥有指定键值的缓存是否存在。
        /// </summary>
        /// <param name="key">指定的键值。</param>
        /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
        public bool Exists(string key)
        {
            return _cacheManager.Contains(key);
        }

        /// <summary>
        ///     获取一个<see cref="bool" />值，该值表示拥有指定键值和缓存值键的缓存是否存在。
        /// </summary>
        /// <param name="key">指定的键值。</param>
        /// <param name="valKey">缓存值键。</param>
        /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
        public bool Exists(string key, string valKey)
        {
            return _cacheManager.Contains(key) &&
                   ((Dictionary<string, object>) _cacheManager[key]).ContainsKey(valKey);
        }

        #endregion
    }
}