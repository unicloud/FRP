#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 文件名：QueryBuilder.cs
// 程序集：UniCloud.Application
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace UniCloud.Application
{
    public class QueryBuilder<T>
    {
        private Func<IQueryable<T>, IQueryable<T>> _queryableReplay;

        public QueryBuilder()
            : this(false)
        {
        }

        public QueryBuilder(bool requestTotalItemCount)
        {
            RequestTotalItemCount = requestTotalItemCount;
        }

        public bool RequestTotalItemCount { get; set; }

        public QueryBuilder<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            if (keySelector == null)
                throw new ArgumentNullException("keySelector");
            var queryableReplay = _queryableReplay;
            _queryableReplay = queryable =>
            {
                if (queryableReplay != null)
                    return (IQueryable<T>) Queryable.OrderBy<T, TKey>(queryableReplay(queryable), keySelector);
                return (IQueryable<T>) queryable.OrderBy(keySelector);
            };
            return this;
        }

        public QueryBuilder<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            if (keySelector == null)
                throw new ArgumentNullException("keySelector");
            var queryableReplay = _queryableReplay;
            _queryableReplay = queryable =>
            {
                if (queryableReplay != null)
                    return (IQueryable<T>) Queryable.OrderByDescending<T, TKey>(queryableReplay(queryable), keySelector);
                return (IQueryable<T>) queryable.OrderByDescending(keySelector);
            };
            return this;
        }

        public QueryBuilder<T> Select(Expression<Func<T, T>> selector)
        {
            if (selector == null)
                throw new ArgumentNullException("selector");
            var queryableReplay = _queryableReplay;
            _queryableReplay =
                queryable =>
                    Queryable.Select<T, T>(queryableReplay != null ? queryableReplay(queryable) : queryable, selector);
            return this;
        }

        public QueryBuilder<T> Skip(int count)
        {
            var queryableReplay = _queryableReplay;
            _queryableReplay =
                queryable => Queryable.Skip<T>(queryableReplay != null ? queryableReplay(queryable) : queryable, count);
            return this;
        }

        public QueryBuilder<T> Take(int count)
        {
            var queryableReplay = _queryableReplay;
            _queryableReplay =
                queryable => Queryable.Take<T>(queryableReplay != null ? queryableReplay(queryable) : queryable, count);
            return this;
        }

        public QueryBuilder<T> ThenBy<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            if (keySelector == null)
                throw new ArgumentNullException("keySelector");
            var queryableReplay = _queryableReplay;
            _queryableReplay = queryable =>
            {
                if (queryableReplay != null)
                    return (IQueryable<T>) ((IOrderedQueryable<T>) queryableReplay(queryable)).ThenBy(keySelector);
                return (IQueryable<T>) ((IOrderedQueryable<T>) queryable).ThenBy(keySelector);
            };
            return this;
        }

        public QueryBuilder<T> ThenByDescending<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            if (keySelector == null)
                throw new ArgumentNullException("keySelector");
            var queryableReplay = _queryableReplay;
            _queryableReplay = queryable =>
            {
                if (queryableReplay != null)
                    return
                        (IQueryable<T>)
                            ((IOrderedQueryable<T>) queryableReplay(queryable)).ThenByDescending(keySelector);
                return (IQueryable<T>) ((IOrderedQueryable<T>) queryable).ThenByDescending(keySelector);
            };
            return this;
        }

        public QueryBuilder<T> Where(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            var queryableReplay = _queryableReplay;
            _queryableReplay =
                queryable =>
                    Queryable.Where<T>(queryableReplay != null ? queryableReplay(queryable) : queryable, predicate);
            return this;
        }

        public IEnumerable<T> ApplyTo(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            return ApplyTo(enumerable.AsQueryable());
        }

        public IQueryable<T> ApplyTo(IQueryable<T> queryable)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");
            if (_queryableReplay != null)
                queryable = _queryableReplay(queryable);
            return queryable;
        }

        public ObservableCollection<T> ApplyTo(ObservableCollection<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            return new ObservableCollection<T>(ApplyTo((IEnumerable<T>) collection));
        }
    }
}