#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 文件名：EntityGuid.cs
// 程序集：UniCloud.Domain
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain
{
    public abstract class EntityGuid
        : Entity
    {
        #region 字段

        private int? _requestedHashCode;

        #endregion

        #region 属性

        /// <summary>
        ///     获取持久化对象的ID
        /// </summary>
        public Guid Id { get; set; }

        #endregion

        #region 公共方法

        /// <summary>
        ///     改变当前实体对象的ID
        /// </summary>
        /// <param name="identity">新的ID</param>
        public void ChangeCurrentIdentity(Guid identity)
        {
            if (identity != Guid.Empty)
                Id = identity;
        }

        #endregion

        #region 重载方法

        /// <summary>
        ///     <see cref="M:UniCloud.Domain.Entity.IsTransient" />
        /// </summary>
        /// <returns>
        ///     <see cref="M:UniCloud.Domain.Entity.IsTransient" />
        /// </returns>
        public override bool IsTransient()
        {
            return Id == Guid.Empty;
        }

        /// <summary>
        ///     <see cref="M:UniCloud.Domain.Entity.GenerateNewIdentity" />
        /// </summary>
        public override void GenerateNewIdentity()
        {
            if (IsTransient())
                Id = IdentityGenerator.NewSequentialGuid();
        }

        /// <summary>
        ///     实体对象一致性检验
        /// </summary>
        /// <param name="obj">检验的实体对象</param>
        /// <returns>一致返回True，否则返回False</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EntityGuid))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var item = (EntityGuid) obj;

            if (item.IsTransient() || IsTransient())
                return false;
            return item.Id == Id;
        }

        /// <summary>
        ///     <see cref="M:UniCloud.Domain.Entity.GetHashCode" />
        /// </summary>
        /// <returns>
        ///     <see cref="M:UniCloud.Domain.Entity.GetHashCode" />
        /// </returns>
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            }
            return GetHash();
        }

        #endregion
    }
}