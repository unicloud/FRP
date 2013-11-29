#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 文件名：ValueObject.cs
// 程序集：UniCloud.Domain
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;

#endregion

namespace UniCloud.Domain
{
    /// <summary>
    ///     领域值对象的基类
    ///     Value
    /// </summary>
    /// <typeparam name="TValueObject">值对象类型</typeparam>
    public class ValueObject<TValueObject> : IEquatable<TValueObject>
        where TValueObject : ValueObject<TValueObject>
    {
        #region IEquatable and Override Equals operators

        /// <summary>
        ///     <see cref="System.IEquatable{TValueObject}" />
        /// </summary>
        /// <param name="other">
        ///     <see cref="System.IEquatable{TValueObject}" />
        /// </param>
        /// <returns>
        ///     <see cref="System.IEquatable{TValueObject}" />
        /// </returns>
        public bool Equals(TValueObject other)
        {
            if ((object) other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            // 对比所有公共属性
            var publicProperties = GetType().GetProperties();

            if (publicProperties.Any())
            {
                return publicProperties.All(p =>
                {
                    var left = p.GetValue(this, null);
                    var right = p.GetValue(other, null);

                    return left is TValueObject ? ReferenceEquals(left, right) : left.Equals(right);
                });
            }
            return true;
        }

        /// <summary>
        ///     一致性检验
        /// </summary>
        /// <param name="obj">检验对象</param>
        /// <returns>一致返回True，否则返回False</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var item = obj as ValueObject<TValueObject>;

            return (object) item != null && Equals((TValueObject) item);
        }

        /// <summary>
        ///     <see cref="M:System.Object.GetHashCode" />
        /// </summary>
        /// <returns>
        ///     <see cref="M:System.Object.GetHashCode" />
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 31;
            var changeMultiplier = false;
            const int index = 1;

            // 对比所有公共属性
            var publicProperties = GetType().GetProperties();

            if (publicProperties.Any())
            {
                foreach (var value in publicProperties.Select(item => item.GetValue(this, null)))
                {
                    if (value != null)
                    {
                        hashCode = hashCode*((changeMultiplier) ? 59 : 114) + value.GetHashCode();
                        changeMultiplier = !changeMultiplier;
                    }
                    else
                        hashCode = hashCode ^ (index*13); //only for support {"a",null,null,"a"} <> {null,"a","a",null}
                }
            }

            return hashCode;
        }

        public static bool operator ==(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            return Equals(left, null) ? (Equals(right, null)) : left.Equals(right);
        }

        public static bool operator !=(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            return !(left == right);
        }

        #endregion
    }
}