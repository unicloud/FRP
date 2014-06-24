#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:46
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
    ///     方法调用器工厂。
    /// </summary>
    public class MethodInvokerFactory : IFastReflectionFactory<MethodInfo, IMethodInvoker>
    {
        /// <summary>
        ///     创建方法调用器。
        /// </summary>
        /// <param name="key">用于缓存方法调用器的Key。</param>
        /// <returns>方法调用器。</returns>
        public IMethodInvoker Create(MethodInfo key)
        {
            return new MethodInvoker(key);
        }

        #region IFastReflectionFactory<MethodInfo,IMethodInvoker> Members

        IMethodInvoker IFastReflectionFactory<MethodInfo, IMethodInvoker>.Create(MethodInfo key)
        {
            return Create(key);
        }

        #endregion
    }
}