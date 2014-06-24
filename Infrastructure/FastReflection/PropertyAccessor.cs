#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:47
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
    ///     属性访问器的接口。
    /// </summary>
    public interface IPropertyAccessor
    {
        /// <summary>
        ///     获取属性值。
        /// </summary>
        /// <param name="instance">包含属性的对象实例。</param>
        /// <returns>属性的值。</returns>
        object GetValue(object instance);

        /// <summary>
        ///     设置属性值。
        /// </summary>
        /// <param name="instance">包含属性的对象实例。</param>
        /// <param name="value">属性要设置的值。</param>
        void SetValue(object instance, object value);
    }

    /// <summary>
    ///     属性访问器。
    /// </summary>
    public class PropertyAccessor : IPropertyAccessor
    {
        private Func<object, object> _getter;
        private MethodInvoker _setMethodInvoker;

        /// <summary>
        ///     属性访问器的构造函数。
        /// </summary>
        /// <param name="propertyInfo">属性信息。</param>
        public PropertyAccessor(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
            InitializeGet(propertyInfo);
            InitializeSet(propertyInfo);
        }

        /// <summary>
        ///     属性信息。
        /// </summary>
        public PropertyInfo PropertyInfo { get; private set; }

        /// <summary>
        ///     <see cref="IPropertyAccessor" />
        /// </summary>
        /// <param name="o">
        ///     <see cref="IPropertyAccessor" />
        /// </param>
        /// <returns>
        ///     <see cref="IPropertyAccessor" />
        /// </returns>
        /// <exception cref="NotSupportedException">未定义Get方法异常。</exception>
        public object GetValue(object o)
        {
            if (_getter == null)
            {
                throw new NotSupportedException("未定义Get方法！");
            }

            return _getter(o);
        }

        /// <summary>
        ///     <see cref="IPropertyAccessor" />
        /// </summary>
        /// <param name="o">
        ///     <see cref="IPropertyAccessor" />
        /// </param>
        /// <param name="value">
        ///     <see cref="IPropertyAccessor" />
        /// </param>
        /// <exception cref="NotSupportedException">未定义Set方法异常。</exception>
        public void SetValue(object o, object value)
        {
            if (_setMethodInvoker == null)
            {
                throw new NotSupportedException("未定义Set方法！");
            }

            _setMethodInvoker.Invoke(o, new[] {value});
        }

        #region IPropertyAccessor 成员

        object IPropertyAccessor.GetValue(object instance)
        {
            return GetValue(instance);
        }

        void IPropertyAccessor.SetValue(object instance, object value)
        {
            SetValue(instance, value);
        }

        #endregion

        private void InitializeGet(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanRead) return;

            // 准备参数、对象类型
            var instance = Expression.Parameter(typeof (object), "instance");

            var instanceCast = propertyInfo.GetGetMethod(true).IsStatic
                ? null
                : Expression.Convert(instance, propertyInfo.ReflectedType);

            var propertyAccess = Expression.Property(instanceCast, propertyInfo);

            var castPropertyValue = Expression.Convert(propertyAccess, typeof (object));

            var lambda = Expression.Lambda<Func<object, object>>(castPropertyValue, instance);

            _getter = lambda.Compile();
        }

        private void InitializeSet(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanWrite) return;
            _setMethodInvoker = new MethodInvoker(propertyInfo.GetSetMethod(true));
        }
    }
}