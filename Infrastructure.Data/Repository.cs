#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 文件名：Repository.cs
// 程序集：UniCloud.Infrastructure.Data
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using UniCloud.Domain;
using UniCloud.Domain.Specification;
using UniCloud.Infrastructure.Crosscutting.Logging;
using UniCloud.Infrastructure.Data.Resources;

#endregion

namespace UniCloud.Infrastructure.Data
{
    /// <summary>
    ///     仓储基类
    /// </summary>
    /// <typeparam name="TEntity">仓储实体类</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        #region Members

        private readonly IQueryableUnitOfWork _unitOfWork;
        #endregion

        #region Constructor

        /// <summary>
        ///     创建仓储的新实例
        /// </summary>
        /// <param name="unitOfWork">关联的Unit Of Work</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        /// <summary>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        /// <summary>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </summary>
        /// <param name="item">
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </param>
        public virtual void Add(TEntity item)
        {
            if (item != null)
                GetSet().Add(item);
            else
            {
                LoggerFactory.Log.Info(Messages.info_CannotAddNullEntity, typeof (TEntity).ToString());
            }
        }

        /// <summary>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </summary>
        /// <param name="item">
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </param>
        public virtual void Remove(TEntity item)
        {
            if (item != null)
            {
                // 如果项不存在，则先附加
                _unitOfWork.Attach(item);
               
                // 把项设置为Removed状态
                GetSet().Remove(item);
              
            }
            else
            {
                LoggerFactory.Log.Info(Messages.info_CannotRemoveNullEntity, typeof (TEntity).ToString());
            }
        }

        /// <summary>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </summary>
        /// <param name="item">
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </param>
        public virtual void TrackItem(TEntity item)
        {
            if (item != null)
                _unitOfWork.Attach<TEntity>(item);
            else
            {
                LoggerFactory.Log.Info(Messages.info_CannotRemoveNullEntity, typeof (TEntity).ToString());
            }
        }

        /// <summary>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </summary>
        /// <param name="item">
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </param>
        public virtual void Modify(TEntity item)
        {
            if (item != null)
                _unitOfWork.SetModified(item);
            else
            {
                LoggerFactory.Log.Info(Messages.info_CannotRemoveNullEntity, typeof (TEntity).ToString());
            }
        }

        /// <summary>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </summary>
        /// <param name="id">
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </param>
        /// <returns>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </returns>
        public virtual TEntity Get(object id)
        {
            return id != null ? GetSet().Find(id) : null;
        }

        /// <summary>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </summary>
        /// <returns>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            return GetSet();
        }

        /// <summary>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </summary>
        /// <param name="specification">
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </param>
        /// <returns>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </returns>
        public virtual IQueryable<TEntity> AllMatching(ISpecification<TEntity> specification)
        {
            return GetSet().Where(specification.SatisfiedBy());
        }

        /// <summary>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </summary>
        /// <typeparam name="KProperty">
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </typeparam>
        /// <param name="pageIndex">
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </param>
        /// <param name="pageCount">
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </param>
        /// <param name="orderByExpression">
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </param>
        /// <param name="ascending">
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </param>
        /// <returns>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </returns>
        public virtual IQueryable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount,
            Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                    .Skip(pageCount*pageIndex)
                    .Take(pageCount);
            }
            return set.OrderByDescending(orderByExpression)
                .Skip(pageCount*pageIndex)
                .Take(pageCount);
        }

        /// <summary>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </summary>
        /// <param name="filter">
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </param>
        /// <returns>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </returns>
        public virtual IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter)
        {
            return GetSet().Where(filter);
        }

        /// <summary>
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </summary>
        /// <param name="persisted">
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </param>
        /// <param name="current">
        ///     <see cref="UniCloud.Domain.IRepository{TValueObject}" />
        /// </param>
        public virtual void Merge(TEntity persisted, TEntity current)
        {
            _unitOfWork.ApplyCurrentValues(persisted, current);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        ///     <see cref="M:System.IDisposable.Dispose" />
        /// </summary>
        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }

        #endregion

        #region Private Methods

        private IDbSet<TEntity> GetSet()
        {
            return _unitOfWork.CreateSet<TEntity>();
        }

        #endregion
    }
}