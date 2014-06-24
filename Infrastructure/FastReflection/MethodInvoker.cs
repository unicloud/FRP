#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:44
// 方案：FRP
// 项目：Infrastructure
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

#endregion

namespace UniCloud.Infrastructure.FastReflection
{
    /// <summary>
    ///     方法调用器的接口。
    /// </summary>
    public interface IMethodInvoker
    {
        /// <summary>
        ///     调用方法
        /// </summary>
        /// <param name="instance">方法对应的对象实例。</param>
        /// <param name="parameters">方法参数。</param>
        /// <returns>调用方法返回的对象。</returns>
        object Invoke(object instance, params object[] parameters);
    }

    /// <summary>
    ///     方法调用器
    /// </summary>
    public class MethodInvoker : IMethodInvoker
    {
        private readonly Func<object, object[], object> _invoker;

        /// <summary>
        ///     方法调用器的构造函数。
        /// </summary>
        /// <param name="methodInfo">方法信息。</param>
        public MethodInvoker(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
            _invoker = CreateInvokeDelegate(methodInfo);
        }

        /// <summary>
        ///     方法信息。
        /// </summary>
        public MethodInfo MethodInfo { get; private set; }

        /// <summary>
        ///     <see cref="IMethodInvoker" />
        /// </summary>
        /// <param name="instance">
        ///     <see cref="IMethodInvoker" />
        /// </param>
        /// <param name="parameters">
        ///     <see cref="IMethodInvoker" />
        /// </param>
        /// <returns>
        ///     <see cref="IMethodInvoker" />
        /// </returns>
        public object Invoke(object instance, params object[] parameters)
        {
            return _invoker(instance, parameters);
        }

        private static Func<object, object[], object> CreateInvokeDelegate(MethodInfo methodInfo)
        {
            // 委托的参数
            var instanceParameter = Expression.Parameter(typeof (object), "instance");
            var parametersParameter = Expression.Parameter(typeof (object[]), "parameters");

            // 构建方法参数
            var parameterExpressions = new List<Expression>();
            var paramInfos = methodInfo.GetParameters();
            for (var i = 0; i < paramInfos.Length; i++)
            {
                var valueObj = Expression.ArrayIndex(parametersParameter, Expression.Constant(i));
                var valueCast = Expression.Convert(valueObj, paramInfos[i].ParameterType);

                parameterExpressions.Add(valueCast);
            }

            var instanceCast = methodInfo.IsStatic
                ? null
                : Expression.Convert(instanceParameter, methodInfo.ReflectedType);

            var methodCall = Expression.Call(instanceCast, methodInfo, parameterExpressions);

            if (methodCall.Type == typeof (void))
            {
                var lambda = Expression.Lambda<Action<object, object[]>>(methodCall, instanceParameter,
                    parametersParameter);

                var execute = lambda.Compile();
                return (instance, parameters) =>
                {
                    execute(instance, parameters);
                    return null;
                };
            }
            else
            {
                var castMethodCall = Expression.Convert(methodCall, typeof (object));
                var lambda = Expression.Lambda<Func<object, object[], object>>(
                    castMethodCall, instanceParameter, parametersParameter);

                return lambda.Compile();
            }
        }

        #region IMethodInvoker 成员

        object IMethodInvoker.Invoke(object instance, params object[] parameters)
        {
            return Invoke(instance, parameters);
        }

        #endregion
    }
}