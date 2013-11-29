//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System;

namespace UniCloud.Application.ApplicationExtension
{
    /// <summary>
    ///     自定义DeleteAttribute属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DeleteAttribute : Attribute
    {
        private readonly Type _type;

        /// <summary>
        ///     删除的对象类型。
        /// </summary>
        /// <param name="type">对象类型。</param>
        public DeleteAttribute(Type type)
        {
            _type = type;
        }

        /// <summary>
        ///     对象类型。
        /// </summary>
        public Type Type
        {
            get { return _type; }
        }
    }
}