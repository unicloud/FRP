#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 文件名：ISpecification.cs
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
    ///     Base contract for Specification pattern, for more information
    ///     about this pattern see http://martinfowler.com/apsupp/spec.pdf
    ///     or http://en.wikipedia.org/wiki/Specification_pattern.
    ///     This is really a variant implementation where we have added Linq and
    ///     lambda expression into this pattern.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        /// <summary>
        ///     Check if this specification is satisfied by a
        ///     specific expression lambda
        /// </summary>
        /// <returns></returns>
        Expression<Func<TEntity, bool>> SatisfiedBy();
    }
}