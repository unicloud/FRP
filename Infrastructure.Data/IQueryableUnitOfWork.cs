#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 文件名：IQueryableUnitOfWork.cs
// 程序集：UniCloud.Infrastructure.Data
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Data.Entity;
using UniCloud.Domain;

#endregion

namespace UniCloud.Infrastructure.Data
{
    /// <summary>
    ///     The UnitOfWork contract for EF implementation
    ///     <remarks>
    ///         This contract extend IUnitOfWork for use with EF code
    ///     </remarks>
    /// </summary>
    public interface IQueryableUnitOfWork
        : IUnitOfWork, ISql
    {
        /// <summary>
        ///     Returns a IDbSet instance for access to entities of the given type in the context,
        ///     the ObjectStateManager, and the underlying store.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        /// <summary>
        ///     Attach this item into "ObjectStateManager"
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="item">The item </param>
        void Attach<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        ///     Set object as modified
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="item">The entity item to set as modifed</param>
        void SetModified<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        ///     Apply current values in <paramref name="original" />
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="original">The original entity</param>
        /// <param name="current">The current entity</param>
        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;
    }
}