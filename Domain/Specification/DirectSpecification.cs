#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 文件名：DirectSpecification.cs
// 程序集：UniCloud.Domain
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq.Expressions;

#endregion

namespace UniCloud.Domain.Specification
{
    /// <summary>
    ///     A Direct Specification is a simple implementation
    ///     of specification that acquire this from a lambda expression
    ///     in  constructor
    /// </summary>
    /// <typeparam name="TEntity">Type of entity that check this specification</typeparam>
    public sealed class DirectSpecification<TEntity>
        : Specification<TEntity>
        where TEntity : class
    {
        #region Members

        private readonly Expression<Func<TEntity, bool>> _matchingCriteria;

        #endregion

        #region Constructor

        /// <summary>
        ///     Default constructor for Direct Specification
        /// </summary>
        /// <param name="matchingCriteria">A Matching Criteria</param>
        public DirectSpecification(Expression<Func<TEntity, bool>> matchingCriteria)
        {
            if (matchingCriteria == null)
                throw new ArgumentNullException("matchingCriteria");

            _matchingCriteria = matchingCriteria;
        }

        #endregion

        #region Override

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return _matchingCriteria;
        }

        #endregion
    }
}