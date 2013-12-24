#region 命名空间

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using UniCloud.Infrastructure.Crosscutting.Caching;

#endregion

namespace UniCloud.Infrastructure.Crosscutting.InterceptionBehaviors
{
    public class CachingBehavior : IInterceptionBehavior
    {
        #region Private Methods

        /// <summary>
        ///     根据指定的<see cref="CachingAttribute" />以及<see cref="IMethodInvocation" />实例，
        ///     获取与某一特定参数值相关的键名。
        /// </summary>
        /// <param name="cachingAttribute"><see cref="CachingAttribute" />实例。</param>
        /// <param name="input"><see cref="IMethodInvocation" />实例。</param>
        /// <returns>与某一特定参数值相关的键名。</returns>
        private string GetValueKey(CachingAttribute cachingAttribute, IMethodInvocation input)
        {
            switch (cachingAttribute.Method)
            {
                    // 如果是Remove，则不存在特定值键名，所有的以该方法名称相关的缓存都需要清除
                case CachingMethod.Remove:
                    return null;
                    // 如果是Get或者Put，则需要产生一个针对特定参数值的键名
                case CachingMethod.Get:
                case CachingMethod.Put:
                    if (input.Arguments != null &&
                        input.Arguments.Count > 0)
                    {
                        var sb = new StringBuilder();
                        for (var i = 0; i < input.Arguments.Count; i++)
                        {
                            sb.Append(input.Arguments[i]);
                            if (i != input.Arguments.Count - 1)
                                sb.Append("_");
                        }
                        return sb.ToString();
                    }
                    return "NULL";
                default:
                    throw new InvalidOperationException("无效的缓存方式。");
            }
        }

        /// <summary>
        ///     根据指定的缓存Obj
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private object SetCachingObj(object obj)
        {
            if (obj.GetType().GetInterface("IEnumerable", true) != null
                && obj.GetType().IsGenericType)
            {
                var listType = typeof (List<>);
                Type targetType = null; //目标类型
                var listObject = new List<object>(); //存放List对象。
                foreach (var value in (IEnumerable) obj)
                {
                    if (targetType == null)
                    {
                        targetType = listType.MakeGenericType(value.GetType());
                    }
                    listObject.Add(value);
                }
                if (targetType != null)
                {
                    var addMethod = targetType.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public);
                    var constructor = targetType.GetConstructor(new Type[] {});
                    if (constructor != null)
                    {
                        var list = constructor.Invoke(new object[] {});
                        foreach (var value in listObject)
                        {
                            addMethod.Invoke(list, new[] {value});
                        }
                        return ((IList) list).AsQueryable();
                    }
                }
            }
            return null;
        }

        #endregion

        #region IInterceptionBehavior Members

        /// <summary>
        ///     通过实现此方法来拦截调用并执行所需的拦截行为。
        /// </summary>
        /// <param name="input">调用拦截目标时的输入信息。</param>
        /// <param name="getNext">通过行为链来获取下一个拦截行为的委托。</param>
        /// <returns>从拦截目标获得的返回信息。</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var method = input.MethodBase;
            if (method.IsDefined(typeof (CachingAttribute), false))
            {
                var cachingAttribute =
                    (CachingAttribute) method.GetCustomAttributes(typeof (CachingAttribute), false)[0];
                var valueKey = GetValueKey(cachingAttribute, input);

                switch (cachingAttribute.Method)
                {
                    case CachingMethod.Get:
                        try
                        {
                            var key = cachingAttribute.CacheType.ToString();
                            if (CacheManager.Instance.Exists(key, valueKey))
                            {
                                var obj = CacheManager.Instance.Get(key, valueKey);
                                var arguments = new object[input.Arguments.Count];
                                input.Arguments.CopyTo(arguments, 0);
                                return new VirtualMethodReturn(input, obj, arguments);
                            }
                            var methodReturn = getNext().Invoke(input, getNext);
                            var list = SetCachingObj(methodReturn.ReturnValue);
                            CacheManager.Instance.Add(key, valueKey, list);
                            return methodReturn;
                        }
                        catch (Exception ex)
                        {
                            return new VirtualMethodReturn(input, ex);
                        }
                    case CachingMethod.Put:
                    case CachingMethod.Remove:
                        try
                        {
                            var removeKey = cachingAttribute.CacheType.ToString();
                            if (CacheManager.Instance.Exists(removeKey))
                                CacheManager.Instance.Remove(removeKey);

                            var methodReturn = getNext().Invoke(input, getNext);
                            return methodReturn;
                        }
                        catch (Exception ex)
                        {
                            return new VirtualMethodReturn(input, ex);
                        }
                }
            }
            return getNext().Invoke(input, getNext);
        }

        /// <summary>
        ///     获取当前行为需要拦截的对象类型接口。
        /// </summary>
        /// <returns>所有需要拦截的对象类型接口。</returns>
        public IEnumerable<Type> GetRequiredInterfaces
            ()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        #endregion
    }
}