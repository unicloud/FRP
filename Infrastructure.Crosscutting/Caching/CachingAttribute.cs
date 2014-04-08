#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/19，09:12
// 文件名：CachingAttribute.cs
// 程序集：UniCloud.Infrastructure.Utilities
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Infrastructure.Crosscutting.Caching
{
    /// <summary>
    ///     表示由此特性所描述的方法，能够获得来自基础结构层所提供的缓存功能。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class CachingAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个新的<c>CachingAttribute</c>类型。
        /// </summary>
        /// <param name="method">缓存方式。</param>
        /// <param name="cacheType">缓存类型</param>
        public CachingAttribute(CachingMethod method,Type cacheType)
        {
            Method = method;
            CacheType = cacheType;
        }
     

        #region Public Properties

        /// <summary>
        /// 获取或设置缓存方式。
        /// </summary>
        public CachingMethod Method { get; set; }
        /// <summary>
        /// 获取或设置一个<see cref="Boolean"/>值，该值表示当缓存方式为Put时，是否强制将值写入缓存中。
        /// </summary>
        public bool Force { get; set; }
        /// <summary>
        /// 获取或设置与当前缓存方式相关的方法名称。注：此参数仅在缓存方式为Remove时起作用。
        /// </summary>
        public string[] CorrespondingMethodNames { get; set; }

        /// <summary>
        /// 缓存类型
        /// </summary>
        public Type CacheType { get; set; }

        #endregion
    }
}