#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 文件名：ISql.cs
// 程序集：UniCloud.Infrastructure.Data
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;

#endregion

namespace UniCloud.Infrastructure.Data
{
    /// <summary>
    ///     Base contract for support 'dialect specific queries'.
    /// </summary>
    public interface ISql
    {
        /// <summary>
        ///     Execute specific query with underliying persistence store
        /// </summary>
        /// <typeparam name="TEntity">Entity type to map query results</typeparam>
        /// <param name="sqlQuery">
        ///     Dialect Query
        ///     <example>
        ///         SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        ///     </example>
        /// </param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>
        ///     Enumerable results
        /// </returns>
        IQueryable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters);

        /// <summary>
        ///     Execute arbitrary command into underliying persistence store
        /// </summary>
        /// <param name="sqlCommand">
        ///     Command to execute
        ///     <example>
        ///         SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        ///     </example>
        /// </param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>The number of affected records</returns>
        int ExecuteCommand(string sqlCommand, params object[] parameters);
    }
}