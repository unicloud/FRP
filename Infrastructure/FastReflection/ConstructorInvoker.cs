#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:37
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
    ///     构造函数访问器的接口
    /// </summary>
    public interface IConstructorInvoker
    {
        /// <summary>
        ///     调用命令
        /// </summary>
        /// <param name="parameters">调用命令的参数。</param>
        /// <returns>调用后返回的对象。</returns>
        object Invoke(params object[] parameters);
    }

    /// <summary>
    ///     构造函数访问器
    /// </summary>
    public class ConstructorInvoker : IConstructorInvoker
    {
        private readonly Func<object[], object> _invoker;

        /// <summary>
        ///     构造函数访问器的构造函数。
        /// </summary>
        /// <param name="constructorInfo">构造函数信息</param>
        public ConstructorInvoker(ConstructorInfo constructorInfo)
        {
            ConstructorInfo = constructorInfo;
            _invoker = InitializeInvoker(constructorInfo);
        }

        /// <summary>
        ///     构造函数信息
        /// </summary>
        public ConstructorInfo ConstructorInfo { get; private set; }

        /// <summary>
        ///     <see cref="IConstructorInvoker" />
        /// </summary>
        /// <param name="parameters">
        ///     <see cref="IConstructorInvoker" />
        /// </param>
        /// <returns>
        ///     <see cref="IConstructorInvoker" />
        /// </returns>
        public object Invoke(params object[] parameters)
        {
            return _invoker(parameters);
        }

        #region IConstructorInvoker 成员

        object IConstructorInvoker.Invoke(params object[] parameters)
        {
            return Invoke(parameters);
        }

        #endregion

        private static Func<object[], object> InitializeInvoker(ConstructorInfo constructorInfo)
        {
            // 委托的参数
            var parametersParameter = Expression.Parameter(typeof (object[]), "parameters");

            // 构建构造函数参数
            var parameterExpressions = new List<Expression>();
            var paramInfos = constructorInfo.GetParameters();
            for (var i = 0; i < paramInfos.Length; i++)
            {
                var valueObj = Expression.ArrayIndex(parametersParameter, Expression.Constant(i));
                var valueCast = Expression.Convert(valueObj, paramInfos[i].ParameterType);

                parameterExpressions.Add(valueCast);
            }

            var instanceCreate = Expression.New(constructorInfo, parameterExpressions);

            var instanceCreateCast = Expression.Convert(instanceCreate, typeof (object));

            var lambda = Expression.Lambda<Func<object[], object>>(instanceCreateCast, parametersParameter);

            return lambda.Compile();
        }
    }
}