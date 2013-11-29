//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------
namespace UniCloud.Application.ApplicationExtension
{
    using System;

    /// <summary>
    ///     自定义Insert属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false,
        Inherited = false)]
    public class InsertAttribute : Attribute
    {
        private readonly Type _type;

        /// <summary>
        ///     新增的对象类型。
        /// </summary>
        /// <param name="type">对象类型。</param>
        public InsertAttribute(Type type)
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