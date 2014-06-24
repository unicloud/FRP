#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:38
// 方案：FRP
// 项目：Infrastructure
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Reflection;

#endregion

namespace UniCloud.Infrastructure.FastReflection
{
    /// <summary>
    ///     构造函数访问器缓存
    /// </summary>
    public class ConstructorInvokerCache : FastReflectionCache<ConstructorInfo, IConstructorInvoker>
    {
        /// <summary>
        ///     创建构造函数访问器的工作对象。
        /// </summary>
        /// <param name="key">缓存构造函数访问器的Key。</param>
        /// <returns>构造函数访问器</returns>
        protected override IConstructorInvoker Create(ConstructorInfo key)
        {
            return FastReflectionFactories.ConstructorInvokerFactory.Create(key);
        }
    }
}