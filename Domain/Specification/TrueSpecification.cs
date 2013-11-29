#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 文件名：TrueSpecification.cs
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
    ///     True specification
    /// </summary>
    /// <typeparam name="TEntity">Type of entity in this specification</typeparam>
    public sealed class TrueSpecification<TEntity>
        : Specification<TEntity>
        where TEntity : class
    {
        #region Specification overrides

        /// <summary>
        ///     <see cref=" UniCloud.Domain.Specification.Specification{TEntity}" />
        /// </summary>
        /// <returns>
        ///     <see cref=" UniCloud.Domain.Specification.Specification{TEntity}" />
        /// </returns>
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            //Create "result variable" transform adhoc execution plan in prepared plan
            //for more info: http://geeks.ms/blogs/unai/2010/07/91/ef-4-0-performance-tips-1.aspx
            const bool result = true;

            Expression<Func<TEntity, bool>> trueExpression = t => result;
            return trueExpression;
        }

        #endregion
    }
}