#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 文件名：Entity.cs
// 程序集：UniCloud.Domain
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain
{
    /// <summary>
    ///     实体类型的基类
    /// </summary>
    public abstract class Entity
    {
        #region 公共方法

        /// <summary>
        ///     检查实体对象是否临时性，例如没有ID
        /// </summary>
        /// <returns>临时返回True，否则返回False</returns>
        public abstract bool IsTransient();

        /// <summary>
        ///     生成实体对象的ID
        /// </summary>
        public abstract void GenerateNewIdentity();

        #endregion

        #region 重载方法

        /// <summary>
        ///     实体对象一致性检验
        /// </summary>
        /// <param name="obj">检验的实体对象</param>
        /// <returns>一致返回True，否则返回False</returns>
        public abstract override bool Equals(object obj);

        /// <summary>
        ///     <see cref="M:System.Object.GetHashCode" />
        /// </summary>
        /// <returns>
        ///     <see cref="M:System.Object.GetHashCode" />
        /// </returns>
        public abstract override int GetHashCode();

        protected int GetHash()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return Equals(left, null) ? (Equals(right, null)) : left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        #endregion
    }
}