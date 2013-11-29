#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 文件名：CompositeSpecification.cs
// 程序集：UniCloud.Domain
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain.Specification
{
    /// <summary>
    ///     Base class for composite specifications
    /// </summary>
    /// <typeparam name="TEntity">Type of entity that check this specification</typeparam>
    public abstract class CompositeSpecification<TEntity>
        : Specification<TEntity>
        where TEntity : class
    {
        #region Properties

        /// <summary>
        ///     Left side specification for this composite element
        /// </summary>
        public abstract ISpecification<TEntity> LeftSideSpecification { get; }

        /// <summary>
        ///     Right side specification for this composite element
        /// </summary>
        public abstract ISpecification<TEntity> RightSideSpecification { get; }

        #endregion
    }
}