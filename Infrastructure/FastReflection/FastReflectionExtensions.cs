#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:40
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
    ///     快速反射扩展方法。
    /// </summary>
    public static class FastReflectionExtensions
    {
        /// <summary>
        ///     快速反射方法调用的扩展方法。
        /// </summary>
        /// <param name="methodInfo">方法信息。</param>
        /// <param name="instance">调用方法的实例。</param>
        /// <param name="parameters">方法参数。</param>
        /// <returns>方法调用返回的对象。</returns>
        public static object FastInvoke(this MethodInfo methodInfo, object instance, params object[] parameters)
        {
            return FastReflectionCaches.MethodInvokerCache.Get(methodInfo).Invoke(instance, parameters);
        }

        /// <summary>
        ///     快速反射属性设置的扩展方法。
        /// </summary>
        /// <param name="propertyInfo">属性信息。</param>
        /// <param name="instance">访问属性的实例。</param>
        /// <param name="value">要对属性进行设置的值。</param>
        public static void FastSetValue(this PropertyInfo propertyInfo, object instance, object value)
        {
            FastReflectionCaches.PropertyAccessorCache.Get(propertyInfo).SetValue(instance, value);
        }

        /// <summary>
        ///     快速反射属性获取的扩展方法。
        /// </summary>
        /// <param name="propertyInfo">属性信息。</param>
        /// <param name="instance">访问属性的实例。</param>
        /// <returns>属性的值。</returns>
        public static object FastGetValue(this PropertyInfo propertyInfo, object instance)
        {
            return FastReflectionCaches.PropertyAccessorCache.Get(propertyInfo).GetValue(instance);
        }

        /// <summary>
        ///     快速反射字段获取的扩展方法。
        /// </summary>
        /// <param name="fieldInfo">字段信息。</param>
        /// <param name="instance">访问字段的实例。</param>
        /// <returns>字段的对象。</returns>
        public static object FastGetValue(this FieldInfo fieldInfo, object instance)
        {
            return FastReflectionCaches.FieldAccessorCache.Get(fieldInfo).GetValue(instance);
        }

        /// <summary>
        ///     快速反射构造函数调用的扩展方法。
        /// </summary>
        /// <param name="constructorInfo">构造函数信息。</param>
        /// <param name="parameters">构造函数参数。</param>
        /// <returns>调用构造函数返回的对象。</returns>
        public static object FastInvoke(this ConstructorInfo constructorInfo, params object[] parameters)
        {
            return FastReflectionCaches.ConstructorInvokerCache.Get(constructorInfo).Invoke(parameters);
        }
    }
}