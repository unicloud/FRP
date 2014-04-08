#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/19，09:12
// 文件名：CachingMethod.cs
// 程序集：UniCloud.Infrastructure.Utilities
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Infrastructure.Crosscutting.Caching
{
    /// <summary>
    ///     表示用于Caching特性的缓存方式。
    /// </summary>
    public enum CachingMethod
    {
        /// <summary>
        ///     表示需要从缓存中获取对象。如果缓存中不存在所需的对象，系统则会调用实际的方法获取对象，
        ///     然后将获得的结果添加到缓存中。
        /// </summary>
        Get,

        /// <summary>
        ///     表示需要将对象存入缓存。此方式会调用实际方法以获取对象，然后将获得的结果添加到缓存中，
        ///     并直接返回方法的调用结果。
        ///</summary>
        Put,

        /// <summary>
        ///     表示需要将对象从缓存中移除。
        /// </summary>
        Remove
    }
}