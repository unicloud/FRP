//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------
namespace UniCloud.Application.ApplicationExtension
{
    using System;

    /// <summary>
    /// 自定义UpdateAttribute属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class UpdateAttribute : Attribute
    {
        private readonly Type _type;

        /// <summary>
        /// 对象类型。
        /// </summary>
        public Type Type
        {
            get { return _type; }
        }
        /// <summary>
        /// 更新的对象类型。
        /// </summary>
        /// <param name="type">对象类型。</param>
        public UpdateAttribute(Type type)
        {
            _type = type;
        }
    }
}