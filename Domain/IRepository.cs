#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 文件名：IRepository.cs
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
using System.Linq.Expressions;
using UniCloud.Domain.Specification;

#endregion

namespace UniCloud.Domain
{
    /// <summary>
    ///     实现“仓储模式”的基础接口，
    ///     更多信息见 http://martinfowler.com/eaaCatalog/repository.html
    ///     或者
    ///     http://blogs.msdn.com/adonet/archive/2009/06/16/using-repository-and-unit-of-work-patterns-with-entity-framework-4-0.aspx
    /// </summary>
    /// <remarks>
    ///     Indeed, one might think that IDbSet already a generic repository and therefore
    ///     would not need this item. Using this interface allows us to ensure PI principle
    ///     within our domain model
    /// </remarks>
    /// <typeparam name="TEntity">仓储相关的实体类型</typeparam>
    public interface IRepository<TEntity> : IDisposable
        where TEntity : Entity
    {
        /// <summary>
        ///     Get the unit of work in this repository
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        ///     Add item into repository
        /// </summary>
        /// <param name="item">Item to add to repository</param>
        void Add(TEntity item);

        /// <summary>
        ///     Delete item
        /// </summary>
        /// <param name="item">Item to delete</param>
        void Remove(TEntity item);

        /// <summary>
        ///     Set item as modified
        /// </summary>
        /// <param name="item">Item to modify</param>
        void Modify(TEntity item);

        /// <summary>
        ///     Track entity into this repository, really in UnitOfWork.
        ///     In EF this can be done with Attach and with Update in NH
        /// </summary>
        /// <param name="item">Item to attach</param>
        void TrackItem(TEntity item);

        /// <summary>
        ///     Sets modified entity into the repository.
        ///     When calling Commit() method in UnitOfWork
        ///     these changes will be saved into the storage
        /// </summary>
        /// <param name="persisted">The persisted item</param>
        /// <param name="current">The current item</param>
        void Merge(TEntity persisted, TEntity current);

        /// <summary>
        ///     Get element by entity key
        /// </summary>
        /// <param name="id">Entity key value</param>
        /// <returns></returns>
        TEntity Get(object id);

        /// <summary>
        ///     Get all elements of type TEntity in repository
        /// </summary>
        /// <returns>List of selected elements</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        ///     Get all elements of type TEntity that matching a
        ///     Specification <paramref name="specification" />
        /// </summary>
        /// <param name="specification">Specification that result meet</param>
        /// <returns></returns>
        IQueryable<TEntity> AllMatching(ISpecification<TEntity> specification);

        /// <summary>
        ///     Get all elements of type TEntity in repository
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="orderByExpression">Order by expression for this query</param>
        /// <param name="ascending">Specify if order is ascending</param>
        /// <returns>List of selected elements</returns>
        IQueryable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount,
            Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending);

        /// <summary>
        ///     Get  elements of type TEntity in repository
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <returns>List of selected elements</returns>
        IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter);
    }
}