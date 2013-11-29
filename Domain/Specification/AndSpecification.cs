#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 文件名：AndSpecification.cs
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
    ///     A logic AND Specification
    /// </summary>
    /// <typeparam name="T">Type of entity that check this specification</typeparam>
    public sealed class AndSpecification<T>
        : CompositeSpecification<T>
        where T : class
    {
        #region Members

        private readonly ISpecification<T> _leftSideSpecification;
        private readonly ISpecification<T> _rightSideSpecification;

        #endregion

        #region Public Constructor

        /// <summary>
        ///     Default constructor for AndSpecification
        /// </summary>
        /// <param name="leftSide">Left side specification</param>
        /// <param name="rightSide">Right side specification</param>
        public AndSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
        {
            if (leftSide == null)
                throw new ArgumentNullException("leftSide");

            if (rightSide == null)
                throw new ArgumentNullException("rightSide");

            _leftSideSpecification = leftSide;
            _rightSideSpecification = rightSide;
        }

        #endregion

        #region Composite Specification overrides

        /// <summary>
        ///     Left side specification
        /// </summary>
        public override ISpecification<T> LeftSideSpecification
        {
            get { return _leftSideSpecification; }
        }

        /// <summary>
        ///     Right side specification
        /// </summary>
        public override ISpecification<T> RightSideSpecification
        {
            get { return _rightSideSpecification; }
        }

        /// <summary>
        ///     <see cref="UniCloud.Domain.Specification.ISpecification{T}" />
        /// </summary>
        /// <returns>
        ///     <see cref="UniCloud.Domain.Specification.ISpecification{T}" />
        /// </returns>
        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            var left = _leftSideSpecification.SatisfiedBy();
            var right = _rightSideSpecification.SatisfiedBy();

            return (left.And(right));
        }

        #endregion
    }
}