﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/19，10:12
// 文件名：CacheManager.cs
// 程序集：UniCloud.Infrastructure.Utilities
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.Infrastructure.Crosscutting.Caching
{
    /// <summary>
    /// 表示向Byteart Retail系统提供缓存功能的缓存管理器。
    /// </summary>
    public sealed class CacheManager : ICacheProvider
    {
        #region Private Fields
        private readonly ICacheProvider _cacheProvider;
        private static readonly CacheManager _instance = new CacheManager();
        #endregion

        #region Ctor
        static CacheManager() { }

        private CacheManager()
        {
            _cacheProvider = DefaultContainer.Resolve<ICacheProvider>();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// 获取<c>CacheManager</c>类型的单件（Singleton）实例。
        /// </summary>
        public static CacheManager Instance
        {
            get { return _instance; }
        }
        #endregion

        #region ICacheProvider Members
        /// <summary>
        /// 向缓存中添加一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        public void Add(string key, string valKey, object value)
        {
            _cacheProvider.Add(key, valKey, value);
        }
        /// <summary>
        /// 向缓存中更新一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        public void Put(string key, string valKey, object value)
        {
            _cacheProvider.Put(key, valKey, value);
        }
        /// <summary>
        /// 从缓存中读取对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <returns>被缓存的对象。</returns>
        public object Get(string key, string valKey)
        {
            return _cacheProvider.Get(key, valKey);
        }
        /// <summary>
        /// 从缓存中移除对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        public void Remove(string key)
        {
            _cacheProvider.Remove(key);
        }
        /// <summary>
        /// 获取一个<see cref="Boolean"/>值，该值表示拥有指定键值的缓存是否存在。
        /// </summary>
        /// <param name="key">指定的键值。</param>
        /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
        public bool Exists(string key)
        {
            return _cacheProvider.Exists(key);
        }
        /// <summary>
        /// 获取一个<see cref="Boolean"/>值，该值表示拥有指定键值和缓存值键的缓存是否存在。
        /// </summary>
        /// <param name="key">指定的键值。</param>
        /// <param name="valKey">缓存值键。</param>
        /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
        public bool Exists(string key, string valKey)
        {
            return _cacheProvider.Exists(key, valKey);
        }
        #endregion
    }
}
