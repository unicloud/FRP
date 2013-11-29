#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 文件名：NotSpecification.cs
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

#endregion

namespace UniCloud.Domain.Specification
{
    /// <summary>
    ///     NotEspecification convert a original
    ///     specification with NOT logic operator
    /// </summary>
    /// <typeparam name="TEntity">Type of element for this specificaiton</typeparam>
    public sealed class NotSpecification<TEntity>
        : Specification<TEntity>
        where TEntity : class
    {
        #region Members

        private readonly Expression<Func<TEntity, bool>> _originalCriteria;

        #endregion

        #region Constructor

        /// <summary>
        ///     Constructor for NotSpecificaiton
        /// </summary>
        /// <param name="originalSpecification">Original specification</param>
        public NotSpecification(ISpecification<TEntity> originalSpecification)
        {
            if (originalSpecification == null)
                throw new ArgumentNullException("originalSpecification");

            _originalCriteria = originalSpecification.SatisfiedBy();
        }

        /// <summary>
        ///     Constructor for NotSpecification
        /// </summary>
        /// <param name="originalSpecification">Original specificaiton</param>
        public NotSpecification(Expression<Func<TEntity, bool>> originalSpecification)
        {
            if (originalSpecification == null)
                throw new ArgumentNullException("originalSpecification");

            _originalCriteria = originalSpecification;
        }

        #endregion

        #region Override Specification methods

        /// <summary>
        ///     <see cref="UniCloud.Domain.Specification.ISpecification{TEntity}" />
        /// </summary>
        /// <returns>
        ///     <see cref="UniCloud.Domain.Specification.ISpecification{TEntity}" />
        /// </returns>
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return Expression.Lambda<Func<TEntity, bool>>(Expression.Not(_originalCriteria.Body),
                _originalCriteria.Parameters.Single());
        }

        #endregion
    }
}