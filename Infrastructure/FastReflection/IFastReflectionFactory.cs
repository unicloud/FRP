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
    ///     快速反射工厂的接口。
    /// </summary>
    /// <typeparam name="TKey">工作对象的Key。</typeparam>
    /// <typeparam name="TValue">快速反射工作对象。</typeparam>
    public interface IFastReflectionFactory<in TKey, out TValue>
    {
        /// <summary>
        ///     创建快速反射工作对象。
        /// </summary>
        /// <param name="key">创建快速反射工作对象的Key。</param>
        /// <returns>创建的快速反射工作对象。</returns>
        TValue Create(TKey key);
    }
}