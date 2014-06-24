#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:41
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
using System.Linq.Expressions;
using System.Reflection;

#endregion

namespace UniCloud.Infrastructure.FastReflection
{
    /// <summary>
    ///     字段服务器的接口。
    /// </summary>
    public interface IFieldAccessor
    {
        /// <summary>
        ///     获取字段对象。
        /// </summary>
        /// <param name="instance">字段所属的实例对象。</param>
        /// <returns>字段对象。</returns>
        object GetValue(object instance);
    }

    /// <summary>
    ///     字段服务器。
    /// </summary>
    public class FieldAccessor : IFieldAccessor
    {
        private readonly Func<object, object> _getter;

        /// <summary>
        ///     字段访问器的构造函数。
        /// </summary>
        /// <param name="fieldInfo">字段信息</param>
        public FieldAccessor(FieldInfo fieldInfo)
        {
            FieldInfo = fieldInfo;
            _getter = GetDelegate(fieldInfo);
        }

        /// <summary>
        ///     字段信息。
        /// </summary>
        public FieldInfo FieldInfo { get; private set; }

        /// <summary>
        ///     <see cref="IFieldAccessor" />
        /// </summary>
        /// <param name="instance">
        ///     <see cref="IFieldAccessor" />
        /// </param>
        /// <returns>
        ///     <see cref="IFieldAccessor" />
        /// </returns>
        public object GetValue(object instance)
        {
            return _getter(instance);
        }

        #region IFieldAccessor 成员

        object IFieldAccessor.GetValue(object instance)
        {
            return GetValue(instance);
        }

        #endregion

        private static Func<object, object> GetDelegate(FieldInfo fieldInfo)
        {
            if (fieldInfo.ReflectedType == null) throw new Exception("类型不能为空。");
            // 准备参数、对象类型
            var instance = Expression.Parameter(typeof (object), "instance");

            var instanceCast = fieldInfo.IsStatic
                ? null
                : Expression.Convert(instance, fieldInfo.ReflectedType);

            var fieldAccess = Expression.Field(instanceCast, fieldInfo);

            var castFieldValue = Expression.Convert(fieldAccess, typeof (object));

            var lambda = Expression.Lambda<Func<object, object>>(castFieldValue, instance);

            return lambda.Compile();
        }
    }
}