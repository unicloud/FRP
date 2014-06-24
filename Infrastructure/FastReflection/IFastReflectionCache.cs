#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:43
// 方案：FRP
// 项目：Infrastructure
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

namespace UniCloud.Infrastructure.FastReflection
{
    /// <summary>
    ///     快速反射缓存接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IFastReflectionCache<in TKey, out TValue>
    {
        /// <summary>
        ///     获取缓存中的快速反射表达式。
        /// </summary>
        /// <param name="key">缓存工作对象的Key。</param>
        /// <returns>工作对象。</returns>
        TValue Get(TKey key);
    }
}